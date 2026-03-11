import ftplib
import hashlib
import fnmatch
from io import BytesIO
from typing import Optional, List
class FTPMixin:
    def connect_ftp(self) -> ftplib.FTP:
        """เชื่อมต่อ FTP server"""
        try:
            ftp = ftplib.FTP(self.ftp_host)
            ftp.login(self.ftp_user, self.ftp_pass)
            print(f"✓ เชื่อมต่อ FTP server: {self.ftp_host}")
            return ftp
        except Exception as e:
            print(f"✗ ไม่สามารถเชื่อมต่อ FTP: {e}")
            raise
    
    def list_files(self, ftp: ftplib.FTP, directory: str = '.', 
                   pattern: str = None) -> List[str]:
        """
        แสดงรายการไฟล์บน FTP server
        
        Args:
            ftp: FTP connection object
            directory: ไดเรกทอรีที่ต้องการดู (default: '.' = current)
            pattern: filter ชื่อไฟล์ (เช่น '*.pdf')
        
        Returns:
            List ของชื่อไฟล์
        """
        try:
            print(f"\nรายการไฟล์ใน: {directory}")
            print("="*70)
            
            # เปลี่ยนไปยัง directory
            if directory != '.':
                ftp.cwd(directory)
            
            # ดึงรายการไฟล์
            files = ftp.nlst()
            
            # Filter ตาม pattern (ถ้ามี)
            if pattern:
                import fnmatch
                files = [f for f in files if fnmatch.fnmatch(f, pattern)]
            
            if files:
                print(f"พบ {len(files)} ไฟล์:")
                for i, filename in enumerate(files, 1):
                    # ลองดึงขนาดไฟล์
                    try:
                        size = ftp.size(filename)
                        size_str = f"{size:,} bytes"
                        if size > 1024*1024:
                            size_str += f" ({size/1024/1024:.2f} MB)"
                        elif size > 1024:
                            size_str += f" ({size/1024:.2f} KB)"
                    except:
                        size_str = "N/A"
                    
                    print(f"  {i:3d}. {filename:<50} {size_str}")
            else:
                print("ไม่พบไฟล์")
            
            print("="*70)
            
            return files
            
        except Exception as e:
            print(f"✗ ไม่สามารถดูรายการไฟล์: {e}")
            return []
    def calculate_md5_from_stream(self, stream: BytesIO) -> str:
        """คำนวณ MD5 hash จาก BytesIO"""
        current_pos = stream.tell()
        stream.seek(0)
    
        md5_hash = hashlib.md5()
    
        while True:
            chunk = stream.read(8192)
            if not chunk:
                break
            md5_hash.update(chunk)
    
        stream.seek(current_pos)
        return md5_hash.hexdigest()
    def download_to_memory(self, ftp: ftplib.FTP, remote_path: str, 
                          chunk_size: int = 8192) -> Optional[BytesIO]:
        """
        ดาวน์โหลดไฟล์จาก FTP ลงใน memory stream (BytesIO)
        
        Args:
            ftp: FTP connection object
            remote_path: path ของไฟล์บน FTP server
            chunk_size: ขนาด buffer สำหรับ streaming (bytes)
        
        Returns:
            BytesIO object หรือ None ถ้าล้มเหลว
        """
        try:
            # สร้าง memory stream
            memory_stream = BytesIO()
            
            # ดึงขนาดไฟล์
            file_size = 0
            downloaded = 0
            
            try:
                file_size = ftp.size(remote_path)
            except:
                file_size = 0
            
            # Callback สำหรับ streaming
            def write_callback(data):
                nonlocal downloaded
                memory_stream.write(data)  # เขียนลง memory
                downloaded += len(data)
                
                # แสดง progress
                if file_size > 0:
                    percent = (downloaded / file_size) * 100
                    print(f"\rดาวน์โหลด: {downloaded:,} / {file_size:,} bytes ({percent:.1f}%)", 
                          end='', flush=True)
                else:
                    print(f"\rดาวน์โหลด: {downloaded:,} bytes", end='', flush=True)
            
            # Download แบบ streaming ลง memory
            print(f"\nกำลังดาวน์โหลด: {remote_path}")
            ftp.retrbinary(f'RETR {remote_path}', write_callback, blocksize=chunk_size)
            
            print()
            print(f"✓ ดาวน์โหลดเสร็จสิ้น: {downloaded:,} bytes (memory stream)")
            
            # Reset pointer ไปที่ต้น stream
            memory_stream.seek(0)
            
            return memory_stream
            
        except ftplib.error_perm as e:
            print(f"\n✗ ไม่มีสิทธิ์เข้าถึงไฟล์: {e}")
            return None
        except Exception as e:
            print(f"\n✗ ไม่สามารถดาวน์โหลดไฟล์: {e}")
            return None
    
    def detect_file_type_from_stream(self, stream: BytesIO) -> Optional[str]:
        """
        ตรวจสอบประเภทไฟล์จาก memory stream
        
        Args:
            stream: BytesIO object
            
        Returns:
            'pdf' หรือ 'image' หรือ None
        """
        try:
            # อ่าน header
            current_pos = stream.tell()
            stream.seek(0)
            header = stream.read(8)
            stream.seek(current_pos)  # คืน position
            
            if header.startswith(b'%PDF'):
                return 'pdf'
            elif header.startswith(b'\xff\xd8\xff'):  # JPEG
                return 'image'
            elif header.startswith(b'\x89PNG'):  # PNG
                return 'image'
            return None
        except Exception as e:
            print(f"✗ ไม่สามารถตรวจสอบประเภทไฟล์: {e}")
            return None