using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RedesNeurais
{
    public partial class Form2 : Form
    {
        private Random rnd = new Random((int)DateTime.Now.Ticks);

        private Thread thr;
        private float W, H;
        private List<ponto> pontos = new List<ponto>();
        private Bitmap bmp;
        private Graphics gp;

        public Form2()
        {
            InitializeComponent();
            
            W = pictureBox1.Width;
            H = pictureBox1.Height;

            bmp = new Bitmap((int)W, (int)H);
            gp = Graphics.FromImage(bmp);

            thr = new Thread(new ThreadStart(treinar_rede));
            update_buttons(true);

            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            ProgressBar.CheckForIllegalCrossThreadCalls = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pontos.Add(new ponto(e.X, e.Y));
            gp.FillEllipse(Brushes.Black, e.X - 2, e.Y - 2, 5, 5);
            pictureBox1.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {                       
            if (thr.ThreadState == ThreadState.Stopped  ||thr.ThreadState == ThreadState.Unstarted)
            {
                thr = new Thread(new ThreadStart(treinar_rede));
                thr.Start();
                update_buttons(false);
            }           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (thr.ThreadState == ThreadState.Running)
            {
                thr.Abort();
                progressBar1.Value = 0;
                update_buttons(true);
            }
        }

        private void treinar_rede()
        {
            progressBar1.Value = 0;

            int n1 = int.Parse(textBox1.Text);
            int n2 = int.Parse(textBox2.Text);
            int n3 = int.Parse(textBox3.Text);
            int max = int.Parse(textBox4.Text);
            N.e = float.Parse(textBox5.Text);

            RN rede = new RN(1, n1, n2, n3);

            List<float[]> entradas = new List<float[]>();
            List<float[]> saidas = new List<float[]>();

            gp.Clear(pictureBox1.BackColor);
            foreach (ponto pt in pontos)
            {
                entradas.Add(new float[] { pt.x / W });
                saidas.Add(new float[] { pt.y / H });
                gp.FillEllipse(Brushes.Black, pt.x - 3, pt.y - 3, 6, 6);
            }
           
            float step = max / 100f;
            float value = step;

            //treinamento da rede
            for (int iteracao = 0; iteracao < max; iteracao++)
            {
                if (iteracao > value)
                {
                    value += step;
                    progressBar1.Value += 1;
                }

                rede.treinar(entradas, saidas);

            }

            //desenha curva apartir da rede treinada
            float py = 0f;
            for (int px = 0; px < W; px += 2)
            {
                py = H * rede.update(new float[] { px / W })[0];
                gp.FillEllipse(Brushes.Red, px - 2, py - 2, 4, 4);
            }

            pictureBox1.Image = bmp;
            progressBar1.Value = 100;
            update_buttons(true);
            thr.Abort(); 
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gp.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bmp;
            pontos.Clear();
        }

        private void update_buttons(bool value)
        {
            button1.Enabled = value;
            button2.Enabled = value;
            button3.Enabled = value;
            button4.Enabled = value;
            button5.Enabled = !value;
        }        
        
    }

    public class ponto
    {
        public float x, y;
        public ponto(float px, float py)
        {
            x = px;
            y = py;
        }
    }
}