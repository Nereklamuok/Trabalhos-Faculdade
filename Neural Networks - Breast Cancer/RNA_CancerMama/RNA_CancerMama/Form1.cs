using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Windows.Forms;
using ExcelDataReader;
using Accord;
using Accord.IO;
using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.MachineLearning;

namespace RNA_CancerMama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread workerThread = null;

        List<Amostra_Paciente> amostras_treinamento;
        List<Amostra_Paciente> amostras_teste;
        ActivationNetwork RNA;
        Random rnd = new Random();

        public List<double> erros = new List<double>();

        public static double[] idade_minmax = new double[2];
        public static double[] glicose_minmax = new double[2];
        public static double[] IMC_minmax = new double[2];
        public static double[] HOMA_minmax = new double[2];
        public static double[] MCP_minmax = new double[2];
        public static double[] insulina_minmax = new double[2];
        public static double[] leptina_minmax = new double[2];
        public static double[] adiponectina_minmax = new double[2];
        public static double[] resistina_minmax = new double[2];

        private void button1_Click(object sender, EventArgs e)
        {
            if (workerThread != null && workerThread.IsAlive)
                workerThread.Abort();

            workerThread = new Thread(new ThreadStart(SearchSolution));
            workerThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            amostras_treinamento = CarregaDados();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RNA == null)
            {
                MessageBox.Show("É necessário treinar a rede neural antes de utilizá-la!");
                return;
            }
            else
            {
                double[] input = new double[9];

                input[0] = Convert.ToDouble(textBox1.Text);
                input[0] = ((input[0] - idade_minmax[0]) / (idade_minmax[1] - idade_minmax[0]));

                input[1] = Convert.ToDouble(textBox2.Text);
                input[1] = ((input[1] - IMC_minmax[0]) / (IMC_minmax[1] - IMC_minmax[0]));

                input[2] = Convert.ToDouble(textBox3.Text);
                input[2] = ((input[2] - glicose_minmax[0]) / (glicose_minmax[1] - glicose_minmax[0]));

                input[3] = Convert.ToDouble(textBox4.Text);
                input[3] = ((input[3] - insulina_minmax[0]) / (insulina_minmax[1] - insulina_minmax[0]));

                input[4] = Convert.ToDouble(textBox5.Text);
                input[4] = ((input[4] - HOMA_minmax[0]) / (HOMA_minmax[1] - HOMA_minmax[0]));

                input[5] = Convert.ToDouble(textBox6.Text);
                input[5] = ((input[5] - leptina_minmax[0]) / (leptina_minmax[1] - leptina_minmax[0]));

                input[6] = Convert.ToDouble(textBox7.Text);
                input[6] = ((input[6] - adiponectina_minmax[0]) / (adiponectina_minmax[1] - adiponectina_minmax[0]));

                input[7] = Convert.ToDouble(textBox8.Text);
                input[7] = ((input[7] - resistina_minmax[0]) / (resistina_minmax[1] - resistina_minmax[0]));

                input[8] = Convert.ToDouble(textBox9.Text);
                input[8] = ((input[8] - MCP_minmax[0]) / (MCP_minmax[1] - MCP_minmax[0]));

                double[] resultado = RNA.Compute(input);

                if (resultado[0] < 0)
                    resultado[0] = 0;
                else if (resultado[0] > 1)
                    resultado[0] = 1;

                resultado[0] *= 100;
                resultado[0] = Math.Round(resultado[0], 2);
                SetText(textBox16, resultado[0].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet result;
            List<Amostra_Paciente> amostras = new List<Amostra_Paciente>();

            using (OpenFileDialog ofd = new OpenFileDialog()
            {

                Title = "Selecionar planilha de dados de teste",
                Filter = "Planilha Excel (*.XLSX)|*.XLSX|" + "All files (*.*)|*.*",
                InitialDirectory = @"C:\",
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true,
                ShowReadOnly = true
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox11.Text = ofd.FileName;
                    FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);

                    IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable dt = result.Tables[0];

                    List<DataRow> colunas = new List<DataRow>(dt.Select());

                    foreach (DataRow coluna in colunas)
                    {
                        Amostra_Paciente amostra = new Amostra_Paciente(coluna.ItemArray);
                        amostras.Add(amostra);
                    }
                    reader.Close();

                    foreach (Amostra_Paciente amostra in amostras)
                    {
                        amostra.NormalizarDados();
                        amostra.AtualizarArray();
                    }
                }
                amostras_teste = amostras;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(amostras_teste== null || !amostras_teste.Any())
            {
                MessageBox.Show("É necessário carregar os dados de teste!");
                return;
            }
            else
            {
                erros.Clear();
                string erro_string = "Resultados Teste: (real/estimado)\r\n";
                int i = 0;

                foreach (Amostra_Paciente amostra in amostras_teste)
                {
                    double[] resultado = RNA.Compute(amostra.arrayDados);
                    if (resultado[0] > 1)
                        resultado[0] = 1;
                    else if (resultado[0] < 0)
                        resultado[0] = 0;

                    double erro = Math.Abs(amostra.label - resultado[0]);
                    erros.Add(erro);
                    erro_string += "Amostra " + i + ": " + amostra.label + "/" + resultado[0] + "\r\n";
                    i++;
                }
                erro_string += "Taxa de acerto: " + ((1 - erros.Average()) * 100);
                MessageBox.Show(erro_string);
            }
        }

        public void SearchSolution()
        {
            if (amostras_treinamento == null || !amostras_treinamento.Any())
            {
                MessageBox.Show("É necessário carregar os dados de treinamento!");
                return;
            }
            else
            {
                erros.Clear();
                double alfaSigmoide;
                double erro_desejado;
                int numNeuronios;

                try
                {
                    alfaSigmoide = Math.Max(0.001, Math.Min(50, double.Parse(textBox12.Text)));
                }
                catch
                {
                    alfaSigmoide = 2;
                }

                try
                {
                    numNeuronios = Math.Max(1, Math.Min(1000, int.Parse(textBox13.Text)));
                }
                catch
                {
                    numNeuronios = 5;
                }

                try
                {
                    erro_desejado = Math.Max(0, Math.Min(100, double.Parse(textBox18.Text)));
                }
                catch
                {
                    erro_desejado = 1;
                }

                SetText(textBox12, alfaSigmoide.ToString());
                SetText(textBox13, numNeuronios.ToString());
                SetText(textBox18, erro_desejado.ToString());

                RNA = new ActivationNetwork(new BipolarSigmoidFunction(alfaSigmoide), 9, numNeuronios, 1);

                new NguyenWidrow(RNA).Randomize();

                var teacher = new ParallelResilientBackpropagationLearning(RNA);


                double error = double.PositiveInfinity;
                double previous;

                double[][] input = new double[amostras_treinamento.Count][];
                double[][] output = new double[amostras_treinamento.Count][];

                int i = 0;

                foreach (Amostra_Paciente amostra in amostras_treinamento)
                {
                    double[] temp = new double[] { amostra.label };

                    input[i] = amostra.arrayDados;
                    output[i] = temp;
                    i++;
                }
                int iter = 0;
                do
                {
                    previous = error;
                    error = teacher.RunEpoch(input, output);
                    SetText(textBox15, error.ToString());
                    SetText(textBox14, iter.ToString());
                    iter++;
                }
                while (error > erro_desejado);

                erros.Clear();
                string erro_string = "Resultados Treinamento\r\n";
                i = 0;

                foreach (Amostra_Paciente amostra in amostras_treinamento)
                {
                    double[] resultado = RNA.Compute(amostra.arrayDados);
                    if (resultado[0] > 1)
                        resultado[0] = 1;
                    else if (resultado[0] < 0)
                        resultado[0] = 0;

                    double erro = Math.Abs(amostra.label - resultado[0]);
                    erros.Add(erro);
                    i++;
                }
                erro_string += "Taxa de acerto: " + ((1 - erros.Average()) * 100);
                MessageBox.Show(erro_string);

            }            
        }

        public List<Amostra_Paciente> CarregaDados()
        {
            DataSet result;
            List<Amostra_Paciente> amostras = new List<Amostra_Paciente>();

            using (OpenFileDialog ofd = new OpenFileDialog()
            {

                Title = "Selecionar planilha de dados das amostras",
                Filter = "Planilha Excel (*.XLSX)|*.XLSX|" + "All files (*.*)|*.*",
                InitialDirectory = @"C:\",
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true,
                ShowReadOnly = true
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    textBox10.Text = ofd.FileName;
                    FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);

                    IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable dt = result.Tables[0];

                    List<DataRow> colunas = new List<DataRow>(dt.Select());

                    foreach (DataRow coluna in colunas)
                    {
                        Amostra_Paciente amostra = new Amostra_Paciente(coluna.ItemArray);
                        amostras.Add(amostra);
                    }
                    reader.Close();

                    amostras.Sort((x, y) => x.idade.CompareTo(y.idade));
                    idade_minmax[0] = amostras.First().idade;
                    idade_minmax[1] = amostras.Last().idade;

                    amostras.Sort((x, y) => x.IMC.CompareTo(y.IMC));
                    IMC_minmax[0] = amostras.First().IMC;
                    IMC_minmax[1] = amostras.Last().IMC;

                    amostras.Sort((x, y) => x.glicose.CompareTo(y.glicose));
                    glicose_minmax[0] = amostras.First().glicose;
                    glicose_minmax[1] = amostras.Last().glicose;

                    amostras.Sort((x, y) => x.insulina.CompareTo(y.insulina));
                    insulina_minmax[0] = amostras.First().insulina;
                    insulina_minmax[1] = amostras.Last().insulina;
                    
                    amostras.Sort((x, y) => x.HOMA.CompareTo(y.HOMA));
                    HOMA_minmax[0] = amostras.First().HOMA;
                    HOMA_minmax[1] = amostras.Last().HOMA;
                    
                    amostras.Sort((x, y) => x.leptina.CompareTo(y.leptina));
                    leptina_minmax[0] = amostras.First().leptina;
                    leptina_minmax[1] = amostras.Last().leptina;
                    
                    amostras.Sort((x, y) => x.adiponectina.CompareTo(y.adiponectina));
                    adiponectina_minmax[0] = amostras.First().adiponectina;
                    adiponectina_minmax[1] = amostras.Last().adiponectina;
                    
                    amostras.Sort((x, y) => x.resistina.CompareTo(y.resistina));
                    resistina_minmax[0] = amostras.First().resistina;
                    resistina_minmax[1] = amostras.Last().resistina;
                    
                    amostras.Sort((x, y) => x.MCP.CompareTo(y.MCP));
                    MCP_minmax[0] = amostras.First().MCP;
                    MCP_minmax[1] = amostras.Last().MCP;

                    foreach(Amostra_Paciente amostra in amostras)
                    {
                        amostra.NormalizarDados();
                        amostra.AtualizarArray();
                    }
                }
            }
            return amostras;
        }

        private void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                Invoke((Action)(() => SetText(control, text)));
            }
            else
            {
                control.Text = text;
            }
        }

        public class Amostra_Paciente
        {
            public double[] arrayDados;

            public double label;

            public double idade;
            public double IMC;
            public double glicose;
            public double HOMA;
            public double MCP;
            public double insulina;
            public double leptina;
            public double adiponectina;
            public double resistina;


            public Amostra_Paciente(object[] _arrayDados)
            {
                idade = Convert.ToDouble(_arrayDados[0]);
                IMC = Convert.ToDouble(_arrayDados[1]);
                glicose = Convert.ToDouble(_arrayDados[2]);
                insulina = Convert.ToDouble(_arrayDados[3]);
                HOMA = Convert.ToDouble(_arrayDados[4]);
                leptina = Convert.ToDouble(_arrayDados[5]);
                adiponectina = Convert.ToDouble(_arrayDados[6]);
                resistina = Convert.ToDouble(_arrayDados[7]);
                MCP = Convert.ToDouble(_arrayDados[8]);
                label = Convert.ToDouble(_arrayDados[9]);

                arrayDados = new double[] {idade, IMC, glicose, insulina, HOMA, leptina, adiponectina, resistina, MCP};
            }

            public void NormalizarDados()
            {
                idade = ((idade - idade_minmax[0]) / (idade_minmax[1] - idade_minmax[0]));
                IMC = ((IMC - IMC_minmax[0]) / (IMC_minmax[1] - IMC_minmax[0]));
                glicose = ((glicose - glicose_minmax[0]) / (glicose_minmax[1] - glicose_minmax[0]));
                insulina = ((insulina - insulina_minmax[0]) / (insulina_minmax[1] - insulina_minmax[0]));
                HOMA = ((HOMA - HOMA_minmax[0]) / (HOMA_minmax[1] - HOMA_minmax[0]));
                leptina = ((leptina - leptina_minmax[0]) / (leptina_minmax[1] - leptina_minmax[0]));
                adiponectina = ((adiponectina - adiponectina_minmax[0]) / (adiponectina_minmax[1] - adiponectina_minmax[0]));
                resistina = ((resistina - resistina_minmax[0]) / (resistina_minmax[1] - resistina_minmax[0]));
                MCP = ((MCP - MCP_minmax[0]) / (MCP_minmax[1] - MCP_minmax[0]));

                return;
            }

            public void AtualizarArray()
            {
                arrayDados = new double[] {idade, IMC, glicose, insulina, HOMA, leptina, adiponectina, resistina, MCP};

                return;
            }          
        }
    }
}
