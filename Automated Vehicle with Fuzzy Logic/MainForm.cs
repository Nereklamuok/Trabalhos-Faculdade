using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Diagnostics;
using AForge.Fuzzy;

namespace FuzzyAGV
{
    public class MainForm : System.Windows.Forms.Form
    {
        #region Private members
        private float distPath = 0;
        private string RunLabel;
        private Point InitialPos;
        private bool FirstInference;
        private int LastX;
        private int LastY;
        private double Angle;
        private Bitmap OriginalMap, InitialMap;
        private InferenceSystem IS;
        private Thread thMovement;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.PictureBox pbTerrain;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.CheckBox cbLasers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label txtRight;
        private System.Windows.Forms.Label txtLeft;
        private System.Windows.Forms.Label txtFront;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtAngle;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbRobot;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox gbComandos;
        private System.ComponentModel.Container components = null;
        private GroupBox groupBox3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label angulo;
        private Label label10;
        private System.Windows.Forms.CheckBox cbTrajeto;
        #endregion

        #region Class constructor, destructor and Main method

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.Run(new MainForm());
        }


        public MainForm()
        {
            InitializeComponent();
            Angle = 0;
            OriginalMap = new Bitmap(pbTerrain.Image);
            InitialMap = new Bitmap(pbTerrain.Image);

            InitFuzzyEngine();
            FirstInference = true;
            pbRobot.Top = pbTerrain.Bottom - 50;
            pbRobot.Left = pbTerrain.Left + 60;
            InitialPos = pbRobot.Location;
            RunLabel = btnRun.Text;
        }

        /// <summary>
        /// Stoping the movement thread
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            StopMovement();
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pbTerrain = new System.Windows.Forms.PictureBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.cbLasers = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRight = new System.Windows.Forms.Label();
            this.txtLeft = new System.Windows.Forms.Label();
            this.txtFront = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAngle = new System.Windows.Forms.Label();
            this.gbComandos = new System.Windows.Forms.GroupBox();
            this.cbTrajeto = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pbRobot = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.angulo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTerrain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbComandos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRobot)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbTerrain
            // 
            this.pbTerrain.BackColor = System.Drawing.SystemColors.ControlText;
            this.pbTerrain.ErrorImage = null;
            this.pbTerrain.Image = ((System.Drawing.Image)(resources.GetObject("pbTerrain.Image")));
            this.pbTerrain.InitialImage = null;
            this.pbTerrain.Location = new System.Drawing.Point(160, 8);
            this.pbTerrain.Name = "pbTerrain";
            this.pbTerrain.Size = new System.Drawing.Size(500, 500);
            this.pbTerrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTerrain.TabIndex = 10;
            this.pbTerrain.TabStop = false;
            this.pbTerrain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbTerrain_MouseDown);
            this.pbTerrain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbTerrain_MouseMove);
            // 
            // btnStep
            // 
            this.btnStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStep.Location = new System.Drawing.Point(6, 109);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 14;
            this.btnStep.Text = "&One Step";
            this.btnStep.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnRun
            // 
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Location = new System.Drawing.Point(6, 138);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 15;
            this.btnRun.Text = "&Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(6, 83);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(72, 20);
            this.txtInterval.TabIndex = 16;
            this.txtInterval.Text = "10";
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbLasers
            // 
            this.cbLasers.Checked = true;
            this.cbLasers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLasers.Location = new System.Drawing.Point(8, 40);
            this.cbLasers.Name = "cbLasers";
            this.cbLasers.Size = new System.Drawing.Size(120, 24);
            this.cbLasers.TabIndex = 17;
            this.cbLasers.Text = "&Show Beams";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRight);
            this.groupBox1.Controls.Add(this.txtLeft);
            this.groupBox1.Controls.Add(this.txtFront);
            this.groupBox1.Controls.Add(this.lbl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 72);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensor readings::";
            // 
            // txtRight
            // 
            this.txtRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRight.Location = new System.Drawing.Point(104, 48);
            this.txtRight.Name = "txtRight";
            this.txtRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRight.Size = new System.Drawing.Size(32, 16);
            this.txtRight.TabIndex = 29;
            this.txtRight.Text = "0";
            this.txtRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLeft
            // 
            this.txtLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLeft.Location = new System.Drawing.Point(104, 32);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLeft.Size = new System.Drawing.Size(32, 16);
            this.txtLeft.TabIndex = 28;
            this.txtLeft.Text = "0";
            this.txtLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFront
            // 
            this.txtFront.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFront.Location = new System.Drawing.Point(104, 16);
            this.txtFront.Name = "txtFront";
            this.txtFront.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFront.Size = new System.Drawing.Size(32, 16);
            this.txtFront.TabIndex = 27;
            this.txtFront.Text = "0";
            this.txtFront.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl
            // 
            this.lbl.Location = new System.Drawing.Point(8, 48);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(100, 16);
            this.lbl.TabIndex = 26;
            this.lbl.Text = "Right (pixels):";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Left (pixels):";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Frontal (pixels):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.angulo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtAngle);
            this.groupBox2.Location = new System.Drawing.Point(8, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 55);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Angle (degrees):";
            // 
            // txtAngle
            // 
            this.txtAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAngle.Location = new System.Drawing.Point(96, 16);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(40, 16);
            this.txtAngle.TabIndex = 29;
            this.txtAngle.Text = "0,00";
            this.txtAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbComandos
            // 
            this.gbComandos.Controls.Add(this.cbTrajeto);
            this.gbComandos.Controls.Add(this.btnReset);
            this.gbComandos.Controls.Add(this.label4);
            this.gbComandos.Controls.Add(this.btnStep);
            this.gbComandos.Controls.Add(this.cbLasers);
            this.gbComandos.Controls.Add(this.btnRun);
            this.gbComandos.Controls.Add(this.txtInterval);
            this.gbComandos.Location = new System.Drawing.Point(8, 136);
            this.gbComandos.Name = "gbComandos";
            this.gbComandos.Size = new System.Drawing.Size(144, 200);
            this.gbComandos.TabIndex = 26;
            this.gbComandos.TabStop = false;
            this.gbComandos.Text = "Tools:";
            // 
            // cbTrajeto
            // 
            this.cbTrajeto.Location = new System.Drawing.Point(8, 16);
            this.cbTrajeto.Name = "cbTrajeto";
            this.cbTrajeto.Size = new System.Drawing.Size(120, 24);
            this.cbTrajeto.TabIndex = 19;
            this.cbTrajeto.Text = "&Track Path";
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(6, 167);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Rest&art";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Move Interval (ms):";
            // 
            // pbRobot
            // 
            this.pbRobot.BackColor = System.Drawing.Color.Transparent;
            this.pbRobot.Image = ((System.Drawing.Image)(resources.GetObject("pbRobot.Image")));
            this.pbRobot.Location = new System.Drawing.Point(216, 472);
            this.pbRobot.Name = "pbRobot";
            this.pbRobot.Size = new System.Drawing.Size(10, 10);
            this.pbRobot.TabIndex = 11;
            this.pbRobot.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(8, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(144, 77);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hints:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 59);
            this.label5.TabIndex = 10;
            this.label5.Text = "Left click the image to draw passages (white), right click the image to draw wall" +
    "s (black).";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 435);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Dist:";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(67, 435);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(66, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = "0";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 451);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Temp(seg):";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(67, 451);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(66, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "0";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // angulo
            // 
            this.angulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.angulo.Location = new System.Drawing.Point(96, 34);
            this.angulo.Name = "angulo";
            this.angulo.Size = new System.Drawing.Size(40, 16);
            this.angulo.TabIndex = 30;
            this.angulo.Text = "0,00";
            this.angulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 31;
            this.label10.Text = "Actual value:";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 513);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbComandos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pbRobot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pbTerrain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fuzzy Auto Guided Vehicle Sample";
            ((System.ComponentModel.ISupportInitialize)(this.pbTerrain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbComandos.ResumeLayout(false);
            this.gbComandos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRobot)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Auxiliary functions

        // Run one epoch of the Fuzzy Inference System 
        private void DoInference()
        {
            // Setting inputs
            IS.SetInput("DistDir", Convert.ToSingle(txtRight.Text));
            IS.SetInput("DistEsq", Convert.ToSingle(txtLeft.Text));
            IS.SetInput("DistFront", Convert.ToSingle(txtFront.Text));

            // Setting outputs
            try
            {
                double NewAngle = IS.Evaluate("Ang");
                txtAngle.Text = NewAngle.ToString("##0.#0");
                Angle += NewAngle;

                if(Angle > 360)
                {
                    double ang = Angle;
                    int x = (int)ang / 360;
                    ang -= (360 * x);

                    angulo.Text = ang.ToString("##0.#0");
                    Angle -= 360;
                }
                else
                {
                    angulo.Text = Angle.ToString("##0.#0");
                }
            }
            catch (Exception)
            {
            }
        }

        // AGV's terrain drawing
        private void pbTerrain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pbTerrain.Image = CopyImage(OriginalMap);
            LastX = e.X;
            LastY = e.Y;
        }

        // AGV's terrain drawing
        private void pbTerrain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pbTerrain.Image);

            Color c = Color.Yellow;

            if (e.Button == MouseButtons.Left)
                c = Color.White;
            else if (e.Button == MouseButtons.Right)
                c = Color.Black;

            if (c != Color.Yellow)
            {
                g.FillRectangle(new SolidBrush(c), e.X - 40, e.Y - 40, 80, 80);

                LastX = e.X;
                LastY = e.Y;

                g.DrawImage(pbTerrain.Image, 0, 0);
                OriginalMap = CopyImage(pbTerrain.Image as Bitmap);
                pbTerrain.Refresh();
                g.Dispose();
            }

        }

        // Getting sensors measures
        private void GetMeasures()
        {
            // Getting AGV's position
            pbTerrain.Image = CopyImage(OriginalMap);
            Bitmap b = pbTerrain.Image as Bitmap;
            Point pPos = new Point(pbRobot.Left - pbTerrain.Left + 5, pbRobot.Top - pbTerrain.Top + 5);

            // AGV on the wall
            if ((b.GetPixel(pPos.X, pPos.Y).R == 0) && (b.GetPixel(pPos.X, pPos.Y).G == 0) && (b.GetPixel(pPos.X, pPos.Y).B == 0))
            {
                if (btnRun.Text != RunLabel)
                {
                    btnRun_Click(btnRun, null);
                }
                string Msg = "The vehicle is on the solid area!";
                MessageBox.Show(Msg, "Error!");
                throw new Exception(Msg);
            }

            // Getting distances
            Point pFrontObstacle = GetObstacle(pPos, b, -1, 0);
            Point pLeftObstacle = GetObstacle(pPos, b, 1, 90);
            Point pRightObstacle = GetObstacle(pPos, b, 1, -90);

            // Showing beams
            Graphics g = Graphics.FromImage(b);
            if (cbLasers.Checked)
            {
                g.DrawLine(new Pen(Color.Green, 1), pFrontObstacle, pPos);
                g.DrawLine(new Pen(Color.Red, 1), pLeftObstacle, pPos);
                g.DrawLine(new Pen(Color.Red, 1), pRightObstacle, pPos);
            }

            // Drawing AGV
            if (btnRun.Text != RunLabel)
            {
                g.FillEllipse(new SolidBrush(Color.Navy), pPos.X - 5, pPos.Y - 5, 10, 10);
            }

            g.DrawImage(b, 0, 0);
            g.Dispose();

            pbTerrain.Refresh();

            // Updating distances texts
            label7.Text = distPath.ToString("#0.00");
            label9.Text = (sw.ElapsedMilliseconds/1000).ToString();

            txtFront.Text = GetDistance(pPos, pFrontObstacle).ToString();
            txtLeft.Text = GetDistance(pPos, pLeftObstacle).ToString();
            txtRight.Text = GetDistance(pPos, pRightObstacle).ToString();

        }

        // Calculating distances
        private int GetDistance(Point p1, Point p2)
        {
            return (Convert.ToInt32(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))));
        }

        // Finding obstacles
        private Point GetObstacle(Point Start, Bitmap Map, int Inc, int AngleOffset)
        {
            Point p = new Point(Start.X, Start.Y);

            double rad = ((Angle + 90 + AngleOffset) * Math.PI) / 180;
            int IncX = 0;
            int IncY = 0;
            int Offset = 0;

            while ((p.X + IncX >= 0) && (p.X + IncX < Map.Width) && (p.Y + IncY >= 0) && (p.Y + IncY < Map.Height))
            {
                if ((Map.GetPixel(p.X + IncX, p.Y + IncY).R == 0) && (Map.GetPixel(p.X + IncX, p.Y + IncY).G == 0) && (Map.GetPixel(p.X + IncX, p.Y + IncY).B == 0))
                    break;
                Offset += Inc;
                IncX = Convert.ToInt32(Offset * Math.Cos(rad));
                IncY = Convert.ToInt32(Offset * Math.Sin(rad));
            }
            p.X += IncX;
            p.Y += IncY;

            return p;
        }

        // Copying bitmaps
        private Bitmap CopyImage(Bitmap Src)
        {
            return new Bitmap(Src);
        }

        // Restarting the AGVs simulation
        private void btnReset_Click(object sender, System.EventArgs e)
        {
            Angle = 0;
            pbTerrain.Image = new Bitmap(InitialMap);
            OriginalMap = new Bitmap(InitialMap);
            FirstInference = true;
            pbRobot.Location = InitialPos;
            txtFront.Text = "0";
            txtLeft.Text = "0";
            txtRight.Text = "0";
            txtAngle.Text = "0,00";
        }

        // Moving the AGV
        private void MoveAGV()
        {
            double rad = ((Angle + 90) * Math.PI) / 180;
            int Offset = 0;
            int Inc = -4;

            Offset += Inc;
            int IncX = Convert.ToInt32(Offset * Math.Cos(rad));
            int IncY = Convert.ToInt32(Offset * Math.Sin(rad));

            //calcula a distancia andada:
            distPath += (float)Math.Sqrt(IncX * IncX + IncY * IncY);

            // Leaving the track 
            if (cbTrajeto.Checked)
            {
                Graphics g = Graphics.FromImage(OriginalMap);
                Point p1 = new Point(pbRobot.Left - pbTerrain.Left + pbRobot.Width / 2, pbRobot.Top - pbTerrain.Top + pbRobot.Height / 2);
                Point p2 = new Point(p1.X + IncX, p1.Y + IncY);
                g.DrawLine(new Pen(new SolidBrush(Color.Blue)), p1, p2);
                g.DrawImage(OriginalMap, 0, 0);
                g.Dispose();
            }

            pbRobot.Top = pbRobot.Top + IncY;
            pbRobot.Left = pbRobot.Left + IncX;
        }

        // Starting and stopping the AGV's moviment a
        private Stopwatch sw;
        private void btnRun_Click(object sender, System.EventArgs e)
        {
            Button b = (sender as Button);

            if (b.Text == RunLabel)
            {
                distPath = 0;
                sw = Stopwatch.StartNew();

                b.Text = "&Stop";
                btnStep.Enabled = false;
                btnReset.Enabled = false;
                txtInterval.Enabled = false;
                cbLasers.Enabled = false;
                cbTrajeto.Enabled = false;
                pbRobot.Hide();
                StartMovement();
            }
            else
            {
                sw.Stop();

                StopMovement();
                b.Text = RunLabel;
                btnReset.Enabled = true;
                btnStep.Enabled = true;
                txtInterval.Enabled = true;
                cbLasers.Enabled = true;
                cbTrajeto.Enabled = true;
                pbRobot.Show();
                pbTerrain.Image = CopyImage(OriginalMap);
                pbTerrain.Refresh();
            }
        }

        // One step of the AGV
        private void button3_Click(object sender, System.EventArgs e)
        {
            pbRobot.Hide();
            AGVStep();
            pbRobot.Show();
        }

        // Thread for the AGVs movement
        private void StartMovement()
        {
            thMovement = new Thread(new ThreadStart(MoveCycle));
            thMovement.IsBackground = true;
            thMovement.Priority = ThreadPriority.AboveNormal;
            thMovement.Start();
        }

        // Thread main cycle
        private void MoveCycle()
        {
            try
            {
                while (Thread.CurrentThread.IsAlive)
                {
                    MethodInvoker mi = new MethodInvoker(AGVStep);
                    this.BeginInvoke(mi);
                    Thread.Sleep(Convert.ToInt32(txtInterval.Text));
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        // One step of the AGV
        private void AGVStep()
        {
            if (FirstInference) GetMeasures();

            try
            {
                DoInference();
                MoveAGV();
                GetMeasures();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // Stop background thread
        private void StopMovement()
        {
            if (thMovement != null)
            {
                thMovement.Interrupt();
                thMovement = null;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
        // => Trabalho sobre Log. Fuzzy: alterar apenas a função InitFuzzyEngine!
        // - Criar um controlador Fuzzy com 3 entradas e 1 saída;
        // - Objetivo: o robô deve explorar o ambiente sem colidir com obstáculos;
        // - Desempenho: a navegação deve ser a mais suave possível

        void InitFuzzyEngine()
        {   
            //Esta função cria o controlador Fuzzy com 3 entradas, 1 saida e regras fuzzy

            //Cria 3 memberships (fuzzy sets) para "fuzzificar" os valores de distância lidos pelos sensores do robô:
            FuzzySet msPequeno = new FuzzySet("Pequeno", new TrapezoidalFunction(10, 30, TrapezoidalFunction.EdgeType.Right));
            FuzzySet msMedio = new FuzzySet("Medio", new TrapezoidalFunction(10, 30, 70, 100));
            FuzzySet msGrande = new FuzzySet("Grande", new TrapezoidalFunction(70, 100, TrapezoidalFunction.EdgeType.Left));

            //Variável fuzzy de entrada 1 --> Sensor de distância na lateral direita do robô
            LinguisticVariable SenDir = new LinguisticVariable("DistDir", 0, 120); //Range de 0 a 120
            SenDir.AddLabel(msPequeno);
            SenDir.AddLabel(msMedio);
            SenDir.AddLabel(msGrande);

            //Variável fuzzy de entrada 2 --> Sensor de distância na lateral esquerda do robô
            LinguisticVariable SenEsq = new LinguisticVariable("DistEsq", 0, 120); //Range de 0 a 120
            SenEsq.AddLabel(msPequeno);
            SenEsq.AddLabel(msMedio);
            SenEsq.AddLabel(msGrande);

            //Variável fuzzy de entrada 3 --> Sensor de distância na frente do robô
            LinguisticVariable SenFront = new LinguisticVariable("DistFront", 0, 120); //Range de 0 a 120
            SenFront.AddLabel(msPequeno);
            SenFront.AddLabel(msMedio);
            SenFront.AddLabel(msGrande);

            //Cria 7 memberships (fuzzy sets) para "fuzzificar" o sinal de controle do robô --> saída do controle (orientacao angular)
            FuzzySet msMNeg = new FuzzySet("MNeg", new TrapezoidalFunction(-45, -35, TrapezoidalFunction.EdgeType.Right));
            FuzzySet msNeg = new FuzzySet("Neg", new TrapezoidalFunction(-45, -40, -30, -25));
            FuzzySet msPNeg = new FuzzySet("PNeg", new TrapezoidalFunction(-30, -25, -15, -10));
            FuzzySet msZero = new FuzzySet("Zero", new TrapezoidalFunction(-15, -5, 5, 15));
            FuzzySet msPPos = new FuzzySet("PPos", new TrapezoidalFunction(10, 15, 25, 30));
            FuzzySet msPos = new FuzzySet("Pos", new TrapezoidalFunction(25, 30, 40, 45));
            FuzzySet msMPos = new FuzzySet("MPos", new TrapezoidalFunction(35, 45, TrapezoidalFunction.EdgeType.Left));

            //Variável fuzzy de saída 1 --> Sinal de controle 1 --> orientação angular do robô
            LinguisticVariable AngRobo = new LinguisticVariable("Ang", -50, 50); //Range de -50 a 50
            AngRobo.AddLabel(msMNeg);
            AngRobo.AddLabel(msNeg);
            AngRobo.AddLabel(msPNeg);
            AngRobo.AddLabel(msZero);
            AngRobo.AddLabel(msPPos);
            AngRobo.AddLabel(msPos);
            AngRobo.AddLabel(msMPos);

            //Cria uma base de dados com as variáveis fuzzy criadas:
            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(SenFront);
            fuzzyDB.AddVariable(SenEsq);
            fuzzyDB.AddVariable(SenDir);
            fuzzyDB.AddVariable(AngRobo);

            //Cria o sistema fuzzy/controlador fuzzy -> Defuzzificação via método do centróide.
            IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Para inferência ao sistema fuzzy criado, deve-se adicionar algumas regras de controle:

            // Regra 1: Se o veículo não estiver muito próximo de nenhuma parede, seguir em frente
            IS.NewRule("Regra1", "IF DistFront IS Not Pequeno AND DistEsq IS Not Pequeno AND DistDir IS Not Pequeno THEN Ang IS Zero");

            // Regra 2: Se distância frontal e esquerda do veículo forem médias enquanto a distância direita é grande, ajustar um pouco à direita
            IS.NewRule("Regra2", "IF DistFront IS Medio AND DistEsq IS Medio AND DistDir IS Grande THEN Ang IS PPos");

            // Regra 3: Se distância frontal e direita do veículo forem médias enquanto a distância esquerda é grande, ajustar um pouco à esquerda
            IS.NewRule("Regra3", "IF DistFront IS Medio AND DistDir IS Medio AND DistEsq IS Grande THEN Ang IS PNeg");

            // Regra 4: Se a distância frontal for pequena e a distância à esquerda não for grande mas à direita for, virar à direita
            IS.NewRule("Regra4", "IF DistFront IS Pequeno AND DistEsq IS Not Grande AND DistDir IS Grande THEN Ang IS Pos");

            // Regra 5: Se a distância frontal for pequena e a distância à direita não for grande mas a esquerda for, virar à esquerda
            IS.NewRule("Regra5", "IF DistFront IS Pequeno AND DistDir IS Not Grande AND DistEsq IS Grande THEN Ang IS Neg");

            // Regra 6: Se a distância frontal e esquerda do veículo forem pequenas e a distância à direita não for grande, virar pouco à direita
            IS.NewRule("Regra6", "IF DistFront IS Pequeno AND DistEsq IS Pequeno AND DistDir IS Not Grande THEN Ang IS PPos");

            // Regra 7: Se a distância frontal e direita do veículo forem pequenas e a distância à esquerda não for grande, virar pouco à esquerda
            IS.NewRule("Regra7", "IF DistFront IS Pequeno AND DistDir IS Pequeno AND DistEsq IS Not Grande THEN Ang IS PNeg");

            // Regra 8: Se o veículo estiver muito próximo das paredes em todas as direções, virar bruscamente em uma direção arbitrária (no caso foi escolhido à esquerda)
            IS.NewRule("Regra8", "IF DistFront IS Pequeno AND DistEsq IS Pequeno AND DistDir IS Pequeno THEN Ang IS MNeg");

            // Regra 9: Se a distância frontal for pequena, mas as demais forem grandes, virar em uma diração arbitrária (no caso foi escolhida à esquerda)
            IS.NewRule("Regra9", "IF DistFront IS Pequeno AND DistDir IS Grande AND DistEsq IS Grande THEN Ang IS Neg");

            // Regra 10: Se a distância frontal for pequenas, mas as demais forem médias, virar pouco em uma diração arbitrária (no caso foi escolhida à esquerda)
            IS.NewRule("Regra10", "IF DistFront IS Pequeno AND DistDir IS Medio AND DistEsq IS Medio THEN Ang IS PNeg");

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }
        //----------------------------------------------------------------------------------------------------------
    }
}
