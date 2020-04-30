import pydicom
from pydicom.data import get_testdata_files

filename = get_testdata_files("D://dicom//202003//05//5007025//CR//1.2.840.114315.1.2.630.20200305062807.9392.1.1.dcm")[0]
ds = pydicom.dcmread(filename)
