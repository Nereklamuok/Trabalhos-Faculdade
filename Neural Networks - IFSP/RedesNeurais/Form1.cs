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
    public partial class Form1 : Form
    {
        private Thread thr;
        private Casos numeros;
        private Casos casos;
        private RN rede;
        private Random rnd = new Random((int)DateTime.Now.Ticks);

       #region inicializacao
        public Form1()
        {
            InitializeComponent();

            caracter.Controls.Clear();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int idx = 10 * i + j; //tabindex
                    int py = 20 * i + 20; //linha
                    int px = 20 * j + 20; //coluna                    
                    caracter.Controls.Add(getLabel(px, py, idx));
                }
            }

            numeros = Casos.Carregar("numeros.xml");
            listBox2.Items.Clear();
            for (int i = 0; i < numeros.Count; i++)
            {
                listBox2.Items.Add("Numero " + numeros.lista_numero[i]);
            }

            thr = new Thread(new ThreadStart(treinar_rede));
            update_buttons(true);

            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;            
            ProgressBar.CheckForIllegalCrossThreadCalls = false;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0) return;
            bool[][] caso = numeros[listBox2.SelectedIndex];
            textBox1.Text = numeros.lista_numero[listBox2.SelectedIndex].ToString();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Control c = caracter.Controls[10 * i + j];
                    if (caso[i][j]) c.BackColor = Color.Blue;
                    else c.BackColor = Color.White;
                }
            }
        }

        private Label getLabel(int px, int py, int idx)
        {
            Label lb = new Label();
            lb.TabIndex = idx;
            lb.BackColor = Color.White;
            lb.AutoSize = false;
            lb.Size = new Size(20, 20);
            lb.Location = new Point(px, py);
            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.Click += new EventHandler(lb_Click);
            return lb;
        }

        private void lb_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            if (lb.BackColor == Color.White) lb.BackColor = Color.Blue;
            else lb.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //limpa tudo
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int idx = 10 * i + j;
                    Control c = caracter.Controls[idx];
                    c.BackColor = Color.White;
                }
            }
        }

        #endregion

        #region gerar casos aleatorios
        private void button2_Click(object sender, EventArgs e)
        {
            //gerar casos randomicos
            casos = new Casos();
            
            double taxa_variacao_do_azul = 0.10;
            double taxa_variacao_do_branco = 0.01;
            int num_variacoes = 20;

            listBox1.Items.Clear();

            for (int k = 0; k < numeros.Count; k++)
            {
                bool[][] caso_base = numeros.lista_caso[k];

                for (int n = 0; n < num_variacoes; n++)
                {
                    listBox1.Items.Add(string.Format("Caso {0} - {1}", k, n + 1));

                    bool[][] caso = new bool[12][];
                    for (int i = 0; i < 12; i++)
                    {
                        caso[i] = new bool[10];
                        for (int j = 0; j < 10; j++)
                        {
                            if (caso_base[i][j])
                            {
                                if (rnd.NextDouble() > taxa_variacao_do_azul)
                                    caso[i][j] = true;
                            }
                            else
                            {
                                if (rnd.NextDouble() < taxa_variacao_do_branco)
                                    caso[i][j] = true;
                            }
                        }
                    }

                    casos.AddCaso(caso, numeros.lista_numero[k]);

                }

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (casos == null) return;

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "casos__.xml";
            save.Title = "Savar Casos Gerados";
            save.Filter = "Formato XML (.xml)|*.xml|All files (*.*)|*.*";

            if (save.ShowDialog() != DialogResult.OK) return;

            casos.Salvar(save.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Title = "Abrir Casos Gerados";
            abrir.Filter = "Formato XML (.xml)|*.xml|All files (*.*)|*.*";

            if (abrir.ShowDialog() != DialogResult.OK) return;

            casos = Casos.Carregar(abrir.FileName);
            listBox1.Items.Clear();
            for (int i = 0; i < casos.Count; i++)
            {
                listBox1.Items.Add(string.Format("Caso {0}", i));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (casos == null) return;
            bool[][] caso = casos[lb.SelectedIndex];
            textBox1.Text = casos.lista_numero[lb.SelectedIndex].ToString();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int idx = 10 * i + j;
                    Control c = caracter.Controls[idx];
                    if (caso[i][j]) c.BackColor = Color.Blue;
                    else c.BackColor = Color.White;
                }
            }
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            if (thr.ThreadState == ThreadState.Stopped ||
                thr.ThreadState == ThreadState.Unstarted)
            {
                thr = new Thread(new ThreadStart(treinar_rede));
                thr.Start();
                update_buttons(false);
            }            
        }

        private void button9_Click(object sender, EventArgs e)
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
 
            int n1 = int.Parse(textBox5.Text);
            int n2 = int.Parse(textBox2.Text);
            int n3 = int.Parse(textBox3.Text);
            int n4 = int.Parse(textBox4.Text);
            N.e = float.Parse(textBox6.Text);

            rede = new RN(120, n1, n2, n3);

            List<float[]> entradas = new List<float[]>();
            List<float[]> saidas = new List<float[]>();

            //for (int c = 0; c < casos.Count; c++)
            for (int c = 0; c < numeros.Count; c++)
            {
                //bool[][] caso = casos.lista_caso[c];
                bool[][] caso = numeros.lista_caso[c];

                float[] ent = new float[12 * 10];
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        int idx = 10 * i + j; //tabindex
                        if (caso[i][j])
                        {
                            ent[idx] = 1.0f;
                        }
                    }
                }

                entradas.Add(ent);

                //saidas.Add(new float[] { (float)casos.lista_numero[c] / 10f });
                
                float[] vetor = new float[n3];
                vetor[numeros.lista_numero[c]] = 1.0f;
                saidas.Add(vetor);

            }

            float step = n4 / 100f;
            float value = step;

            for (int i = 0; i < n4; i++)
            {
                if (i > value)
                {
                    value += step;
                    progressBar1.Value += 1;
                }

                rede.treinar(entradas, saidas);
            }

            progressBar1.Value = 100;
            update_buttons(true);
            thr.Abort();            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (rede == null) return;

            float[] ent = new float[12 * 10];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int idx = 10 * i + j;                    
                    if (caracter.Controls[idx].BackColor == Color.Blue)
                        ent[idx] = 1.0f;
                }
            }

            float[] s = rede.update(ent);

            listBox3.Items.Clear();
            float max = 0f;
            int k = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] > max)
                {
                    max = s[i];
                    k = i;
                }
                this.listBox3.Items.Add(string.Format("{0:0.000}", s[i]));
            }
            
            //label7.Text = string.Format("{0:0.000}", s);
            //float num = 10f * (float)Math.Round(s, 1);            
            //saida.Text = num.ToString();

            saida.Text = k.ToString();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox2_SelectedIndexChanged(null, null);

            double taxa_variacao_do_azul = 0.10;
            double taxa_variacao_do_branco = 0.01;

            for (int i = 0; i < 12; i++)
            {                
                for (int j = 0; j < 10; j++)
                {
                    int idx = 10 * i + j;
                    if (caracter.Controls[idx].BackColor == Color.Blue)
                    {
                        if (rnd.NextDouble() < taxa_variacao_do_azul)
                            caracter.Controls[idx].BackColor = Color.White;
                    }
                    else
                    {
                        if (rnd.NextDouble() < taxa_variacao_do_branco)
                            caracter.Controls[idx].BackColor = Color.Blue;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (rede == null) return;

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "minha rede neural_.rn";
            save.Title = "Savar RedeNeural";
            save.Filter = "Formato RN (.rn)|*.rn|All files (*.*)|*.*";

            if (save.ShowDialog() != DialogResult.OK) return;
            
            rede.Salvar(save.FileName);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Title = "Abrir RedeNeural";
            abrir.Filter = "Formato RN (.rn)|*.rn|All files (*.*)|*.*";

            if (abrir.ShowDialog() != DialogResult.OK) return;

            rede = RN.Carregar(abrir.FileName);
        }
        
        private void update_buttons(bool value)
        {
            button1.Enabled = value;
            button2.Enabled = value;
            button3.Enabled = value;
            button4.Enabled = value;
            button5.Enabled = value;
            button6.Enabled = value;
            button7.Enabled = value;
            button8.Enabled = value;
            button9.Enabled = !value;
            button10.Enabled = value;
        }        
            
    }
}