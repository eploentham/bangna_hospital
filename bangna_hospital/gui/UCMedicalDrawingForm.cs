using bangna_hospital.control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace bangna_hospital.object1
{
    public class UCMedicalDrawingForm : UserControl
    {
        BangnaControl BC;
        String PRENO = "", VSDATE = "", HN = "", DTRCODE = "";
        private DrawingPictureBox drawingBox;
        private Panel toolPanel, pnView;
        private Button penButton, eraserButton, clearButton, saveButton, loadButton, btnImgFinger, btnImgFootL, btnImgFootR, btnImgHandL,btnImgHandR, btnImgHead;
        private TrackBar penSizeTrackBar;
        private Button colorButton;
        private Label penSizeLabel;
        private ComboBox drawingModeCombo;
        Patient PTT;
        Boolean imageLoaded = false;
        public UCMedicalDrawingForm(BangnaControl bc, String dtrcode, String hn, String vsdate, String preno, Patient ptt)
        {
            this.BC = bc;
            this.DTRCODE = dtrcode;
            this.HN = hn;
            this.VSDATE = vsdate;
            this.PRENO = preno;
            this.PTT = ptt;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            imageLoaded = true;
            this.SuspendLayout();
            this.Name = "MedicalDrawingForm";
            //this.Size = new System.Drawing.Size(1184, 761);
            SetupDrawingInterface();
            
            this.Load += (s, e) => {                if (!imageLoaded)                {                    imageLoaded = true;                }            };
            this.ResumeLayout(false);
            imageLoaded = false;
        }

        private void UCMedicalDrawingForm_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!imageLoaded)
            {
                //DrawingBoxLoad();
                imageLoaded = true;
            }
        }

        private void SetupDrawingInterface()
        {
            toolPanel = new Panel           {   Dock = DockStyle.Top,                Height = 40,                BackColor = Color.LightGray            };
            penButton = new Button          {   Text = "Pen",           Location = new Point(5, 5),        Size = new Size(40, 30),        BackColor = Color.Blue,     ForeColor = Color.White            };
            penButton.Click += (s, e) => drawingBox.SetDrawingMode(DrawingMode.Pen);

            eraserButton = new Button       {   Text = "Del",        Location = new Point(penButton.Width+ penButton.Left+3, 5),        Size = new Size(40, 30)            };
            eraserButton.Click += (s, e) => drawingBox.SetDrawingMode(DrawingMode.Eraser);

            penSizeLabel = new Label        {   Text = "Size:",         Location = new Point(eraserButton.Width + eraserButton.Left + 3, 15),   Size = new Size(35, 20)            };

            penSizeTrackBar = new TrackBar  {   Location = new Point(140, 5),       Size = new Size(100, 40),       Minimum = 1,    Maximum = 20,       Value = 3            };
            penSizeTrackBar.ValueChanged += (s, e) => drawingBox.SetPenSize(penSizeTrackBar.Value);

            colorButton = new Button        {   Text = "Color",         Location = new Point(penSizeTrackBar.Width + penSizeTrackBar.Left + 5, 5),  Size = new Size(50, 30),                BackColor = Color.Black            };
            colorButton.Click += ColorButton_Click;

            drawingModeCombo = new ComboBox {   Location = new Point(colorButton.Width + colorButton.Left + 5, 5),     Size = new Size(100, 30),      DropDownStyle = ComboBoxStyle.DropDownList            };
            drawingModeCombo.Items.AddRange(new string[] { "Free Draw", "Line", "Rectangle", "Circle", "Arrow" });
            drawingModeCombo.SelectedIndex = 0;
            drawingModeCombo.SelectedIndexChanged += DrawingModeCombo_SelectedIndexChanged;

            clearButton = new Button        {   Text = "Clear",     Location = new Point(drawingModeCombo.Width + drawingModeCombo.Left + 5, 5),    Size = new Size(50, 30)            };
            clearButton.Click += (s, e) => drawingBox.ClearDrawing();

            saveButton = new Button         {   Text = "save",     Location = new Point(clearButton.Width + clearButton.Left + 5, 5),       Size = new Size(40, 30)         };
            saveButton.Click += SaveButton_Click;

            loadButton = new Button         {   Text = "load",      Location = new Point(saveButton.Width + saveButton.Left +5 , 5),        Size = new Size(40, 30)         };
            loadButton.Click += LoadButton_Click;
            btnImgFinger = new Button { Text = "finger", Location = new Point(loadButton.Width + loadButton.Left + 5, 5),   Size = new Size(45, 30) };
            btnImgFinger.Click += BtnImgFinger_Click;
            btnImgFootL = new Button { Text = "foot l", Location = new Point(btnImgFinger.Width + btnImgFinger.Left + 5, 5),Size = new Size(45, 30) };
            btnImgFootL.Click += BtnImgFootL_Click;
            btnImgFootR = new Button { Text = "foot r", Location = new Point(btnImgFootL.Width + btnImgFootL.Left + 5, 5), Size = new Size(45, 30) };
            btnImgFootR.Click += BtnImgFootR_Click;
            btnImgHandL = new Button { Text = "hand l", Location = new Point(btnImgFootR.Width + btnImgFootR.Left + 5, 5), Size = new Size(45, 30) };
            btnImgHandL.Click += BtnImgHandL_Click;
            btnImgHandR = new Button { Text = "hand r", Location = new Point(btnImgHandL.Width + btnImgHandL.Left + 5, 5), Size = new Size(45, 30) };
            btnImgHandR.Click += BtnImgHandR_Click;
            btnImgHead = new Button { Text = "head", Location = new Point(btnImgHandR.Width + btnImgHandR.Left + 5, 5), Size = new Size(45, 30) };
            btnImgHead.Click += BtnImgHead_Click;

            toolPanel.Controls.AddRange(new Control[]
            {
                penButton, eraserButton, penSizeLabel, penSizeTrackBar,
                colorButton, drawingModeCombo, clearButton, saveButton, loadButton,btnImgFinger,btnImgFootL,btnImgFootR,btnImgHandL,btnImgHandR,btnImgHead
            });

            drawingBox = new DrawingPictureBox            {     Dock = DockStyle.Fill,                BackColor = Color.White            };

            // Fix: Add toolPanel AFTER drawingBox so it appears at the top
            
            this.Controls.Add(drawingBox);
            this.Controls.Add(toolPanel);
        }

        private void BtnImgFinger_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("finger.jpg");
        }
        private void BtnImgFootL_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("foot_left.jpg");
        }
        private void BtnImgFootR_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("foot_right.jpg");
        }
        private void BtnImgHandR_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("hand_right.jpg");
        }
        private void BtnImgHandL_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("hand_left.jpg");
        }
        private void BtnImgHead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            drawingBox.LoadImage("head.jpg");
        }
        private void ColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    colorButton.BackColor = colorDialog.Color;
                    drawingBox.SetPenColor(colorDialog.Color);
                }
            }
        }
        private void DrawingModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (drawingModeCombo.SelectedIndex)
            {
                case 0: drawingBox.SetShapeMode(ShapeMode.FreeDraw); break;
                case 1: drawingBox.SetShapeMode(ShapeMode.Line); break;
                case 2: drawingBox.SetShapeMode(ShapeMode.Rectangle); break;
                case 3: drawingBox.SetShapeMode(ShapeMode.Circle); break;
                case 4: drawingBox.SetShapeMode(ShapeMode.Arrow); break;
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            FtpClient ftp = new FtpClient(BC.iniC.hostFTP, BC.iniC.userFTP, BC.iniC.passFTP, BC.ftpUsePassive);
            DocScan dsc = new DocScan();
            dsc.active = "1";
            dsc.doc_scan_id = "";
            dsc.doc_group_id = "1100000001";
            dsc.hn = HN;
            dsc.an = "";
            dsc.visit_date = VSDATE;
            dsc.host_ftp = BC.iniC.hostFTP;
            dsc.image_path = "";
            dsc.doc_group_sub_id = "1200000003";
            dsc.pre_no = PRENO;
            dsc.folder_ftp = BC.iniC.folderFTP;
            dsc.row_no = "1";
            dsc.row_cnt = "1";
            dsc.status_version = "2";
            dsc.req_id = "";
            dsc.date_req = "";
            dsc.status_ipd = "O";
            dsc.ml_fm = "FM-MED-902";
            dsc.remark = "JPG";
            dsc.sort1 = "1";

            BC.bcDB.dscDB.voidDocScanByStatusDoctorOrder(HN, VSDATE, PRENO, DTRCODE);
            dsc.patient_fullname = PTT.Name;
            dsc.status_record = "5";
            dsc.comp_labout_id = "";
            String re = BC.bcDB.dscDB.insertDoctorOrder(dsc, BC.userId);
            dsc.image_path = BC.hn + "//1200000003_" + BC.hn + "_" + BC.vsdate + "_" + BC.preno + "_" + re + ".JPG";
            String re1 = BC.bcDB.dscDB.updateImagepath(dsc.image_path, re);
            ftp.createDirectory(BC.iniC.folderFTP + "//" + BC.hn.Replace("/", "-"));
            ftp.delete(BC.iniC.folderFTP + "//" + dsc.image_path);

            // Convert drawingSurface to MemoryStream
            using (var ms = new System.IO.MemoryStream())
            {
                drawingBox.drawingSurface.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Position = 0;
                if (ftp.upload(BC.iniC.folderFTP + "//" + dsc.image_path, ms))
                {
                    // Optionally handle success
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog openDialog = new OpenFileDialog())
            //{
            //    openDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files|*.*";
            //    if (openDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        drawingBox.LoadImage(openDialog.FileName);
            //    }
            //}
            DrawingBoxLoad();
        }
        public void DrawingBoxLoad()
        {
            // Custom painting logic if needed
            DataTable dt = BC.bcDB.dscDB.selectByDoctorOrderPhysicalExam(HN, VSDATE, PRENO);
            if (dt.Rows.Count <= 0) return;
            FtpClient ftp = new FtpClient(BC.iniC.hostFTP, BC.iniC.userFTP, BC.iniC.passFTP, BC.ftpUsePassive);
            MemoryStream stream = ftp.download(dt.Rows[0][BC.bcDB.dscDB.dsc.folder_ftp] + "/" + dt.Rows[0][BC.bcDB.dscDB.dsc.image_path].ToString());
            stream.Position = 0;
            drawingBox.LoadImageFTP(stream);
        }
    }
}
// Custom PictureBox สำหรับการวาดรูป
public class DrawingPictureBox : PictureBox
{
    public Bitmap drawingSurface;
    private Graphics drawingGraphics;
    private bool isDrawing = false;
    private Point lastPoint;
    private List<Point> currentStroke = new List<Point>();

    // การตั้งค่าการวาด
    private DrawingMode currentDrawingMode = DrawingMode.Pen;
    private ShapeMode currentShapeMode = ShapeMode.FreeDraw;
    private Color penColor = Color.Black;
    private int penSize = 3;
    private Point shapeStartPoint;

    // เก็บประวัติการวาดสำหรับ Undo/Redo
    private Stack<Bitmap> undoStack = new Stack<Bitmap>();
    private Stack<Bitmap> redoStack = new Stack<Bitmap>();

    public DrawingPictureBox()
    {
        this.MouseDown += DrawingPictureBox_MouseDown;
        this.MouseMove += DrawingPictureBox_MouseMove;
        this.MouseUp += DrawingPictureBox_MouseUp;
        this.Paint += DrawingPictureBox_Paint;
        this.Resize += DrawingPictureBox_Resize;

        InitializeDrawingSurface();
    }

    private void InitializeDrawingSurface()
    {
        if (drawingSurface != null)
            drawingSurface.Dispose();

        drawingSurface = new Bitmap(this.Width > 0 ? this.Width : 800,
                                  this.Height > 0 ? this.Height : 600);
        drawingGraphics = Graphics.FromImage(drawingSurface);
        drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        drawingGraphics.Clear(Color.White);
    }

    private void DrawingPictureBox_Resize(object sender, EventArgs e)
    {
        if (this.Width > 0 && this.Height > 0)
        {
            InitializeDrawingSurface();
            this.Invalidate();
        }
    }

    private void DrawingPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            lastPoint = e.Location;
            shapeStartPoint = e.Location;
            currentStroke.Clear();
            currentStroke.Add(e.Location);

            // บันทึกสถานะสำหรับ Undo
            SaveStateForUndo();
        }
    }

    private void DrawingPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing && e.Button == MouseButtons.Left)
        {
            currentStroke.Add(e.Location);

            if (currentShapeMode == ShapeMode.FreeDraw)
            {
                DrawLine(lastPoint, e.Location);
                lastPoint = e.Location;
                this.Invalidate();
            }
            else
            {
                // สำหรับรูปทรง ให้แสดง preview
                this.Invalidate();
            }
        }
    }

    private void DrawingPictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            isDrawing = false;

            switch (currentShapeMode)
            {
                case ShapeMode.Line:
                    DrawLine(shapeStartPoint, e.Location);
                    break;
                case ShapeMode.Rectangle:
                    DrawRectangle(shapeStartPoint, e.Location);
                    break;
                case ShapeMode.Circle:
                    DrawCircle(shapeStartPoint, e.Location);
                    break;
                case ShapeMode.Arrow:
                    DrawArrow(shapeStartPoint, e.Location);
                    break;
            }

            this.Invalidate();
        }
    }

    private void DrawingPictureBox_Paint(object sender, PaintEventArgs e)
    {
        if (drawingSurface != null)
        {
            e.Graphics.DrawImage(drawingSurface, 0, 0);

            // แสดง preview สำหรับรูปทรง
            if (isDrawing && currentShapeMode != ShapeMode.FreeDraw)
            {
                using (Pen previewPen = new Pen(penColor, penSize))
                {
                    previewPen.DashStyle = DashStyle.Dash;

                    switch (currentShapeMode)
                    {
                        case ShapeMode.Line:
                            e.Graphics.DrawLine(previewPen, shapeStartPoint, lastPoint);
                            break;
                        case ShapeMode.Rectangle:
                            var rect = GetRectangle(shapeStartPoint, lastPoint);
                            e.Graphics.DrawRectangle(previewPen, rect);
                            break;
                        case ShapeMode.Circle:
                            var circleRect = GetRectangle(shapeStartPoint, lastPoint);
                            e.Graphics.DrawEllipse(previewPen, circleRect);
                            break;
                    }
                }
            }
        }
    }

    private void DrawLine(Point start, Point end)
    {
        using (Pen pen = new Pen(penColor, penSize))
        {
            if (currentDrawingMode == DrawingMode.Eraser)
            {
                pen.Color = Color.White;
                pen.Width = penSize * 2;
            }

            drawingGraphics.DrawLine(pen, start, end);
        }
    }

    private void DrawRectangle(Point start, Point end)
    {
        using (Pen pen = new Pen(penColor, penSize))
        {
            var rect = GetRectangle(start, end);
            drawingGraphics.DrawRectangle(pen, rect);
        }
    }

    private void DrawCircle(Point start, Point end)
    {
        using (Pen pen = new Pen(penColor, penSize))
        {
            var rect = GetRectangle(start, end);
            drawingGraphics.DrawEllipse(pen, rect);
        }
    }

    private void DrawArrow(Point start, Point end)
    {
        using (Pen pen = new Pen(penColor, penSize))
        {
            // วาดเส้น
            drawingGraphics.DrawLine(pen, start, end);

            // วาดหัวลูกศร
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
            int arrowLength = 15;
            double arrowAngle = Math.PI / 6; // 30 degrees

            Point arrow1 = new Point(
                (int)(end.X - arrowLength * Math.Cos(angle - arrowAngle)),
                (int)(end.Y - arrowLength * Math.Sin(angle - arrowAngle))
            );

            Point arrow2 = new Point(
                (int)(end.X - arrowLength * Math.Cos(angle + arrowAngle)),
                (int)(end.Y - arrowLength * Math.Sin(angle + arrowAngle))
            );

            drawingGraphics.DrawLine(pen, end, arrow1);
            drawingGraphics.DrawLine(pen, end, arrow2);
        }
    }

    private Rectangle GetRectangle(Point start, Point end)
    {
        return new Rectangle(
            Math.Min(start.X, end.X),
            Math.Min(start.Y, end.Y),
            Math.Abs(end.X - start.X),
            Math.Abs(end.Y - start.Y)
        );
    }

    private void SaveStateForUndo()
    {
        if (drawingSurface != null)
        {
            undoStack.Push(new Bitmap(drawingSurface));
            redoStack.Clear();

            // จำกัดจำนวน undo states
            if (undoStack.Count > 50)
            {
                var oldStates = undoStack.ToArray();
                undoStack.Clear();
                for (int i = 0; i < 25; i++)
                {
                    undoStack.Push(oldStates[i]);
                }
            }
        }
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(new Bitmap(drawingSurface));
            var previousState = undoStack.Pop();

            drawingGraphics.Clear(Color.White);
            drawingGraphics.DrawImage(previousState, 0, 0);
            this.Invalidate();
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new Bitmap(drawingSurface));
            var nextState = redoStack.Pop();

            drawingGraphics.Clear(Color.White);
            drawingGraphics.DrawImage(nextState, 0, 0);
            this.Invalidate();
        }
    }

    public void SetDrawingMode(DrawingMode mode)
    {
        currentDrawingMode = mode;
    }

    public void SetShapeMode(ShapeMode mode)
    {
        currentShapeMode = mode;
    }

    public void SetPenColor(Color color)
    {
        penColor = color;
    }

    public void SetPenSize(int size)
    {
        penSize = size;
    }

    public void ClearDrawing()
    {
        SaveStateForUndo();
        drawingGraphics.Clear(Color.White);
        this.Invalidate();
    }

    public void SaveDrawing(string filePath)
    {
        if (drawingSurface != null)
        {
            drawingSurface.Save(filePath);
        }
    }

    public void LoadImage(string filePath)
    {
        try
        {
            SaveStateForUndo();
            using (var image = Image.FromFile(filePath))
            {
                drawingGraphics.Clear(Color.White);
                drawingGraphics.DrawImage(image, 0, 0, this.Width, this.Height);
                this.Invalidate();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading image: {ex.Message}");
        }
    }
    public void LoadImageFTP(MemoryStream stream)
    {
        try
        {
            SaveStateForUndo();
            stream.Position = 0;
            using (var image = Image.FromStream(stream))
            {
                drawingGraphics.Clear(Color.White);
                this.Width = image.Width;
                this.Height = image.Height;
                drawingGraphics.DrawImage(image, 0, 0, this.Width, this.Height);
                this.Invalidate();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading image: {ex.Message}");
        }
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            drawingGraphics?.Dispose();
            drawingSurface?.Dispose();
        }
        base.Dispose(disposing);
    }
}

// Enums สำหรับโหมดการวาด
public enum DrawingMode
{
    Pen,
    Eraser
}

public enum ShapeMode
{
    FreeDraw,
    Line,
    Rectangle,
    Circle,
    Arrow
}