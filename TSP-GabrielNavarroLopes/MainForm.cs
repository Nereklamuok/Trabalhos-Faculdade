using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using AForge;
using AForge.Controls;
using System.Collections.Generic;
using System.Linq;

namespace TSP
{	
	public class MainForm : System.Windows.Forms.Form
	{				
        #region Windows Form Designer generated code

        private System.Windows.Forms.GroupBox groupBox1;
        private AForge.Controls.Chart mapControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox citiesCountBox;
        private System.Windows.Forms.Button generateMapButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox populationSizeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox iterationsBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox pathLengthBox;
        private TextBox mutationChanceBox;
        private Label label3;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button button2;
        private GroupBox groupBox4;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label8;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private static MainForm form = null;

        // Constructor
        public MainForm( )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            form = this;
			// set up map control
			mapControl.RangeX = new Range( 0, 1000 );
			mapControl.RangeY = new Range( 0, 1000 );
			mapControl.AddDataSeries( "map", Color.Red, Chart.SeriesType.Dots, 5, false );
			mapControl.AddDataSeries( "path", Color.Blue, Chart.SeriesType.Line, 1, false );
            mapControl.AddDataSeries("start_end", Color.Black, Chart.SeriesType.Dots, 5, false);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.generateMapButton = new System.Windows.Forms.Button();
            this.citiesCountBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mapControl = new AForge.Controls.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.mutationChanceBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.iterationsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.populationSizeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pathLengthBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.generateMapButton);
            this.groupBox1.Controls.Add(this.citiesCountBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mapControl);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 354);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mapa";
            // 
            // generateMapButton
            // 
            this.generateMapButton.Location = new System.Drawing.Point(111, 314);
            this.generateMapButton.Name = "generateMapButton";
            this.generateMapButton.Size = new System.Drawing.Size(180, 22);
            this.generateMapButton.TabIndex = 3;
            this.generateMapButton.Text = "Gerar Mapa";
            this.generateMapButton.Click += new System.EventHandler(this.generateMapButton_Click);
            // 
            // citiesCountBox
            // 
            this.citiesCountBox.Location = new System.Drawing.Point(55, 315);
            this.citiesCountBox.Name = "citiesCountBox";
            this.citiesCountBox.Size = new System.Drawing.Size(50, 20);
            this.citiesCountBox.TabIndex = 2;
            this.citiesCountBox.Text = "20";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cidades:";
            // 
            // mapControl
            // 
            this.mapControl.Location = new System.Drawing.Point(10, 20);
            this.mapControl.Name = "mapControl";
            this.mapControl.RangeX = ((AForge.Range)(resources.GetObject("mapControl.RangeX")));
            this.mapControl.RangeY = ((AForge.Range)(resources.GetObject("mapControl.RangeY")));
            this.mapControl.Size = new System.Drawing.Size(280, 280);
            this.mapControl.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.iterationsBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.populationSizeBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(320, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 179);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configura��es";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.mutationChanceBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(13, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 99);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Muta��o";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 26);
            this.label8.TabIndex = 10;
            this.label8.Text = "Tipo de\r\nmuta��o:";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(71, 70);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(90, 17);
            this.radioButton2.TabIndex = 9;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Inverter pares";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(71, 50);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(78, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Embaralhar";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // mutationChanceBox
            // 
            this.mutationChanceBox.Location = new System.Drawing.Point(91, 17);
            this.mutationChanceBox.Name = "mutationChanceBox";
            this.mutationChanceBox.Size = new System.Drawing.Size(65, 20);
            this.mutationChanceBox.TabIndex = 7;
            this.mutationChanceBox.Text = "0,0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Chance (%):";
            // 
            // iterationsBox
            // 
            this.iterationsBox.Location = new System.Drawing.Point(110, 51);
            this.iterationsBox.Name = "iterationsBox";
            this.iterationsBox.Size = new System.Drawing.Size(65, 20);
            this.iterationsBox.TabIndex = 5;
            this.iterationsBox.Text = "10000";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Gera��es:";
            // 
            // populationSizeBox
            // 
            this.populationSizeBox.Location = new System.Drawing.Point(110, 25);
            this.populationSizeBox.Name = "populationSizeBox";
            this.populationSizeBox.Size = new System.Drawing.Size(65, 20);
            this.populationSizeBox.TabIndex = 1;
            this.populationSizeBox.Text = "100";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Popula��o:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.startButton);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.pathLengthBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(320, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 169);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resultados:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 45);
            this.button2.TabIndex = 7;
            this.button2.Text = "Pausar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Interromper \r\nExecu��o";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "0";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(13, 19);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(162, 33);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Executar";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Gera��o Atual:";
            // 
            // pathLengthBox
            // 
            this.pathLengthBox.Location = new System.Drawing.Point(100, 59);
            this.pathLengthBox.Name = "pathLengthBox";
            this.pathLengthBox.ReadOnly = true;
            this.pathLengthBox.Size = new System.Drawing.Size(79, 20);
            this.pathLengthBox.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Percurso:";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(514, 370);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Problema do Caixeiro Viajante utilizando Algoritmo Gen�tico";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }     

        #endregion

        public int NCities = 20;    //N�mero de cidades
        public int NPop = 100;      //N�mero da indiv�duos na popula��o
        public int NGer = 1000;     //N�mero de gera��es (itera��es)
        public double NMut = 0;     //Chance de muta��o
        public List<City> Map = new List<City>();                   //Lista do mapa
        public List<City> Path = new List<City>();                  //Lista do caminho
        public List<Individuo> Population = new List<Individuo>();  //Lista da popula��o
        public Thread GA_Thread;

        // On "Generate Map" button click - generate map
        private void generateMapButton_Click(object sender, System.EventArgs e)
        {
            if (GA_Thread == null)            //Executar apenas se o algoritmo n�o estiver sendo executado
            {
                Map.Clear();            //Limpa a lista mapa
                Path.Clear();           //Limpa a lista de caminho
                Population.Clear();     //Limpa a lista de popula��o
                // get cities count:
                try                     //Tentar Executar o seguinte:
                {
                    //Obt�m o n�mero de cidades da TextBox, limitando entre um m�ximo e m�nimo
                    NCities = Math.Max(5, Math.Min(199, int.Parse(citiesCountBox.Text)));
                }
                catch                   //Em caso de uma exce��o (erro):
                {
                    //Definir o n�mero de cidades como uma constante (20)
                    NCities = 20;       
                }
                //Atualizar o texto da TextBox com o n�mero de cidades
                citiesCountBox.Text = NCities.ToString();

                //-------------------------------------------------------------------------------
                // Generate new map for the Traveling Salesman problem:
                Random rand = new Random((int)DateTime.Now.Ticks);

                //Gera as cidades:
                for (int i = 0; i < NCities; i++)           //Enquanto i estiver entre 0 e N Cidades
                {
                    int x = rand.Next(1001);                //Gera um n�mero aleat�rio para a coordenada X
                    int y = rand.Next(1001);

                    City city = new City(x, y);             //Cria nova cidade nas coordenadas X e Y
                    Map.Add(city);                          //Adiciona a cidade rec�m criada ao mapa
                }

                double[,] map = new double[NCities, 2];     //Gera a matriz do mapa com tamanho igual ao n�mero de cidades
                map = ListToMatrix(Map);                    //Converte a lista Mapa para uma matriz e a armazena

                // set the map
                mapControl.UpdateDataSeries("map", map);    //Utiliza a matriz para preencher o gr�fico com os pontos das cidades

                // erase path if it is "null"
                mapControl.UpdateDataSeries("path", null);  //Se o caminho n�o tiver sido gerado ainda, apag�-lo
                mapControl.UpdateDataSeries("start_end", null); //Se o ponto de in�cio/fim n�o tiver sido gerado ainda, apag�-lo
            }
            else //Se o algoritmo estiver em andamento, impedir gera��o de novo mapa
                MessageBox.Show("O algoritmo est� em execu��o, aguarde o t�rmino.");
        }

        //On "MainForm" form load 
        private void MainForm_Load(object sender, EventArgs e)
        {   //generate new map:
            generateMapButton_Click(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        //Algoritmo gen�tico:
        private void GeneticAlgorithm()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);  // gerador de num. aleat�rio
            int iterationCount = 0;                             //Inicializa vari�vel que armazena o valor da gera��o atual
            int noProgressIterations = 0;
            double bestDist = 0;
            bool allEqual = false;                              //Inicia a vari�vel de converg�ncia em falso

            Path.Clear();                                       //Limpa a lista caminho
            Population.Clear();                                 //Limpa a lista de popula��o
            // get population size
            try
            {
                //Obt�m o n�mero de popula��o
                NPop = Math.Max(10, Math.Min(100000, int.Parse(populationSizeBox.Text)));
            }
            catch
            {
                //Se anteriormente houve uma exce��o, estabelecer popula��o em um valor constante
                NPop = 100;
            }

            //get iterations num.
            try
            {
                //Obt�m o n�mero de gera��es
                NGer = Math.Max(100, Math.Min(1000000000, int.Parse(iterationsBox.Text)));
            }
            catch
            {
                //Se anteriormente houve uma exce��o, estabelecer n�mero de gera��es em um valor constante
                NGer = 100;
            }

            try
            {
                //Obt�m chance de muta��o
                NMut = Math.Max(0, Math.Min(100, double.Parse(mutationChanceBox.Text)));
            }
            catch
            {
                //Se anteriormente houve uma exce��o, estabelecer chance de muta��o em um valor constante
                NMut = 0;
            }

            //Atualiza itens da interface, que est�o em outra Thread
            this.Invoke((MethodInvoker)delegate
            {
                //Atualiza valores das textbox:
                //---------------------------------------------
                populationSizeBox.Text = NPop.ToString();
                iterationsBox.Text = NGer.ToString();
                mutationChanceBox.Text = NMut.ToString();
                //---------------------------------------------

                //Impede a altera��o dos valores das textbox at� o t�rmino da execu��o:
                //---------------------------------------------
                iterationsBox.ReadOnly = true;
                populationSizeBox.ReadOnly = true;
                mutationChanceBox.ReadOnly = true;
                citiesCountBox.ReadOnly = true;
                //---------------------------------------------
            });

            //Enquanto i for menor que o valor populacional...
            for (int i = 0; i < NPop; i++)
            {
                Individuo indv = new Individuo(Map, rand);  //Criar um novo indiv�duo com genes aleat�rios
                Population.Add(indv);                       //Adicionar o indiv�duo � popula��o
            }

            //Enquanto n�o tiver atingido o n�mero m�ximo de gera��es...
            while (iterationCount < NGer)
            {
                //Ordena os indiv�duos da popula��o de acordo com seu fitness (menor dist�ncia do percurso)
                Population.Sort((x, y) => x.distance.CompareTo(y.distance));

                //Cria uma lista para os piores indiv�duos da popula��o (1/3)
                List<Individuo> PopulationWorst = new List<Individuo>();
                //Cria uma lista para os melhores ind�vuos da popula��o (2/3)
                //Esses indiv�duos ser�o os pais da nova gera��o
                List<Individuo> PossibleParents = new List<Individuo>();

                //Calcular o valor aproximado de dois ter�os da popula��o
                int parentFraction = (Population.Count * 2) / 3;

                //Povoar a lista dos piores indiv�duos com os piores indiv�duos (1/3 da popula��o)
                PopulationWorst = Population.GetRange(parentFraction, NPop - parentFraction);
                //Povoar a lista dos poss�veis pais com os melhores indiv�duos (2/3 da popula��o)
                PossibleParents = Population.GetRange(0, parentFraction);
                //Atualizar a popula��o atual para conter apenas os melhores indiv�duos da atualidade
                Population = Population.GetRange(0, parentFraction);

                //Cria uma lista para aqueles que j� foram pais
                List<Individuo> Parents = new List<Individuo>();
                //Cria uma lista para os filhos que ser�o gerados
                List<Individuo> Children = new List<Individuo>();

                //Enquanto o n�mero de filhos for menor do que a quantidade de piores indiv�duos da popula��o...
                while (Children.Count < PopulationWorst.Count)
                {
                    int parentCount = PossibleParents.Count;    //Obter o n�mero de indiv�duos que ainda podem ser pais

                    if (parentCount > 1)                        //Se o n�mero de poss�veis pais for maior que 1
                    {
                        int randFather;                         //Posi��o da popula��o correspondente ao pai
                        int randMother;                         //Posi��o da popula��o correspondente � m�e
                        do
                        {
                            randFather = rand.Next(parentCount);//Gera uma posi��o aleat�ria na popula��o para o pai
                            randMother = rand.Next(parentCount);//Gera uma posi��o aleat�ria na popula��o para a m�e
                        }
                        while (randFather == randMother);       //Se as posi��es de pai e m�e forem as mesmas, repetir o processo

                        //Obt�m o indiv�duo pai correspondente � posi��o da popula��o gerada aleatoriamente
                        Individuo father = PossibleParents[randFather];
                        //Obt�m o indiv�duo m�e correspondente � posi��o da popula��o gerada aleatoriamente
                        Individuo mother = PossibleParents[randMother];

                        Parents.Add(father);                    //Adiciona o pai � lista daqueles que j� tiveram filhos
                        Parents.Add(mother);                    //Adiciona a m�e � lista daqueles que j� tiveram filhos
                        PossibleParents.Remove(father);         //Remove o pai da lista de poss�veis pais
                        PossibleParents.Remove(mother);         //Remove a m�e da lista de poss�veis pais

                        Individuo child;                        //Declara o indiv�duo filho
                        child = Individuo.Offspring(father, mother, rand);    //Gera o filho de acordo com os genes do pai e da m�e
                        Children.Add(child);                    //Adiciona a crian�a � lista de filhos
                    }
                    else
                    {
                        //Se n�o houver mais pais mas ainda faltar filhos
                        Individuo new_child = new Individuo(Map, rand);
                        Children.Add(new_child);            //Adicione um filho que ser� um novo indiv�duo aleat�rio
                    }       
                }

                //Adicione � lista de filhos os piores indiv�duos da popula��o
                //(� poss�vel que existam filhos piores do que os piores indiv�duos da popula��o anterior)
                Children.AddRange(PopulationWorst);
                //Ordenar a lista de filhos de acordo com a fun��o de fitness (dist�ncia do percurso)
                Children.Sort((x, y) => x.distance.CompareTo(y.distance));
                //Manter apenas os melhores indiv�duos entre essa popula��o (1/3 do total)
                //Children = Children.Take<Individuo>(NPop - parentFraction).ToList();
                Children = Children.GetRange(0, NPop - parentFraction);
                //Adiciona esses filhos novamente � popula��o total
                Population.AddRange(Children);

                if (NMut > 0)       //Se a chance de muta��o for maior que 0
                {
                    //Para cada indiv�duo na popula��o...
                    foreach (Individuo indv in Population)
                    {
                        //Se esse indiv�duo for o melhor indiv�duo da popula��o atual...
                        if (indv == Population.First())
                        {
                            //Passe para o pr�ximo indiv�duo, esse aqui � muito bom para sofrer muta��o
                            //Just too good for it
                            continue;    
                        }
                        //Obt�m um n�mero aleat�rio entre 0.0 e 1.0
                        double chance = rand.NextDouble();
                        //Multiplicar esse n�mero por 100, para coloc�-lo na faixa de 0.0 � 100.0
                        chance *= 100;
                        //Se o valor estiver abaixo do valor de muta��o definido pelo usu�rio...
                        if (chance <= NMut)
                        {
                            indv.Mutate(rand);          //Realizar a muta��o no indiv�duo
                            indv.RecalculateDistance(); //Recalcular o fitness desse indiv�duo
                        }
                    }
                }
                
                iterationCount++;                       //Incrementa o valor da gera��o atual

                this.Invoke((MethodInvoker)delegate
                {
                    //Atualiza o valor do textbox que exibe a melhor dist�ncia
                    pathLengthBox.Text = Population[0].distance.ToString("#0.0");
                    //Atualiza o valor da label, que est� em outra Thread
                    label6.Text = iterationCount.ToString();
                });

                if (bestDist <= Population[0].distance)
                {
                    noProgressIterations++;
                }
                else
                {
                    noProgressIterations = 0;
                }

                //Define que a melhor dist�ncia � aquela do indiv�duo na primeira posi��o
                bestDist = Population[0].distance;
                //Se todos os indiv�duos da popula��o tiverem esse mesmo valor, todos s�o iguais
                allEqual = Population.All(o => o.distance == bestDist);

                if (allEqual)
                {
                    ResetarPopulacao(Population, rand);
                }
                else if (noProgressIterations >= Math.Max(NGer * 0.025, 20))
                {
                    if (noProgressIterations > NGer *0.2)
                    {
                        //Early Convergence
                        break;
                    }
                    else
                    {
                        ResetarPopulacao(Population, rand);
                    }
                }      
            }

            //Reordena a popula��o de acordo com a fun��o de fitness (dist�ncia do percurso)
            //Population.Sort((x, y) => x.distance.CompareTo(y.distance));;

            //Declara uma matriz de N�mero de cidades x 2 que armazenar� o melhor caminho
            double[,] path = new double[NCities, 2];

            //Adiciona os genes do melhor indiv�duo da popula��o � lista do melhor caminho
            Path.AddRange(Population[0].genes);
            //Converte essa lista em uma matriz que ser� utilizada para desenhar o caminho
            path = ListToMatrix(Path);
            //Obt�m as coordenadas do ponto de in�cio/fim, que correspondem ao primeiro item do caminho
            double[,] start_end = new double[,] { { Path[0].X, Path[0].Y } };

            //atualiza mapa com o caminho a ser percorrido:
            mapControl.UpdateDataSeries("path", path);
            //Atualiza o mapa com o ponto de in�cio e fim
            mapControl.UpdateDataSeries("start_end", start_end);

            //calcula o caminho total percorrido:
            double caminho = Population[0].distance;

            pathLengthBox.Invoke((MethodInvoker)delegate
            {
                //Atualiza o valor do textbox que exibe a melhor dist�ncia
                pathLengthBox.Text = caminho.ToString("#0.0");
            });

            this.Invoke((MethodInvoker)delegate
            {
                //Permite que as textbox sejam modificadas novamente
                iterationsBox.ReadOnly = false;
                populationSizeBox.ReadOnly = false;
                mutationChanceBox.ReadOnly = false;
                citiesCountBox.ReadOnly = false;
            });
            //Anula a Thread que executa o algoritmo gen�tico
            GA_Thread = null;
        }

        // On "Run GA..." button click Thread GA_Thread 
        private void startButton_Click(object sender, System.EventArgs e)
		{
            if (GA_Thread == null)
            {
                GA_Thread = new Thread(new ThreadStart(GeneticAlgorithm));
                GA_Thread.IsBackground = true;
                GA_Thread.Start();
            }
            else
                MessageBox.Show("O algoritmo est� em execu��o, aguarde o t�rmino.");
        }
        
        public Double[,] ListToMatrix(List<City> list)
        {
            double[,] matrix = new double[list.Count, 2];
            for (int i = 0; i < list.Count; i++)
            {
                matrix[i, 0] = list[i].X;
                matrix[i, 1] = list[i].Y;
            }
            return matrix;
        }

        public static double DistanceBetweenCities(City origin, City ending)  //Calcula dist�ncia entre duas cidades
        {
            double distance;
            //Teorema de Pit�goras para c�lculo da dist�ncia
            distance = (Math.Sqrt(Math.Pow((origin.X - ending.X), 2) + Math.Pow((origin.Y - ending.Y), 2)));
            return distance;
        }

        public static int GetMutationType(MainForm form)
        {
            int mutType = 0;

            if (form.radioButton1.Checked)
                mutType = 0;
            else if (form.radioButton2.Checked)
                mutType = 1;

            return mutType;
        }

        public void ResetarPopulacao(List<Individuo> populacao, Random rand)
        {
            List<Int32> rand_Indexes = new List<Int32>();
            int rand_number;

            while(rand_Indexes.Count < populacao.Count * 0.8)
            {
                do
                {
                    rand_number = rand.Next(1, populacao.Count - 1);
                }
                while (rand_Indexes.Contains(rand_number));
                rand_Indexes.Add(rand_number);
            }

            rand_Indexes.Sort();
            rand_Indexes.Reverse();

            foreach(Int32 index in rand_Indexes)
            {
                populacao.RemoveAt(index);
            }

            while (populacao.Count() < NPop)
            {
                Individuo indv = new Individuo(Map, rand);  //Criar um novo indiv�duo com genes aleat�rios
                populacao.Add(indv);                       //Adicionar o indiv�duo � popula��o
            }
            populacao.Sort((x, y) => x.distance.CompareTo(y.distance));
        }


        public class City
        {
            public int X, Y;

            public City(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class Individuo
        {
            public List<City> genes;
            public double distance;
            public double chanceRoleta;

            public Individuo(List<City> Map = null, Random rand = null)
            {
                genes = new List<City>();
                distance = 0;
                chanceRoleta = 0;

                if (Map != null && rand != null)
                {
                    int numberofCities = Map.Count;
                    int i = 0;

                    while (i < numberofCities)
                    {
                        int randCity = rand.Next(numberofCities);
                        if (!genes.Contains(Map[randCity]))
                        {
                            genes.Add(Map[randCity]);
                            i++;
                        }
                    }

                    genes.Add(genes[0]);

                    for (i = 0; i < numberofCities+1; i++)
                    {
                        if (i + 1 < genes.Count)
                            distance += DistanceBetweenCities(genes[i], genes[i + 1]);
                    }
                }                
            }

            public static Individuo Offspring(Individuo father, Individuo mother, Random rand)
            {
                Individuo child = new Individuo();

                int cutIndex = rand.Next(father.genes.Count);

                List<City> fatherGenes = new List<City>();
                List<City> motherGenes = new List<City>();

                for (int i = 0; i < cutIndex; i++)
                {
                    fatherGenes.Add(father.genes[i]);
                }

                child.genes.AddRange(fatherGenes);

                for(int i = 0; i < mother.genes.Count; i++)
                {
                    if (!child.genes.Contains(mother.genes[i]))
                        motherGenes.Add(mother.genes[i]);
                }

                child.genes.AddRange(motherGenes);

                child.genes = child.genes.Distinct().ToList();

                child.genes.Add(child.genes[0]);

                child.RecalculateDistance();

                return child;
            }

            public void RecalculateDistance()
            {
                distance = 0;
                for (int i = 0; i < genes.Count; i++)
                {
                    if (i + 1 < genes.Count)
                        distance += DistanceBetweenCities(genes[i], genes[i + 1]);
                }
                return;
            }

            public void Mutate(Random rand)
            {
                genes = genes.Distinct().ToList();
                int mutType = GetMutationType(form);
                City temp;
                
                if(mutType == 0)
                {
                    int i = genes.Count;
                    while (i > 1)
                    {
                        int j = rand.Next(i--);
                        temp = genes[i];
                        genes[i] = genes[j];
                        genes[j] = temp;
                    }
                }

                else if(mutType == 1)
                {

                    int rand1 = rand.Next(genes.Count);
                    int rand2 = rand.Next(genes.Count);

                    temp = genes[rand1];

                    genes[rand1] = genes[rand2];
                    genes[rand2] = temp;
                }

                genes.Add(genes[0]);

                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GA_Thread != null && GA_Thread.ThreadState != (ThreadState.Suspended | ThreadState.Background))
            {
                GA_Thread.Abort();
                GA_Thread = null;

                this.Invoke((MethodInvoker)delegate
                {
                    iterationsBox.ReadOnly = false;
                    populationSizeBox.ReadOnly = false;
                    mutationChanceBox.ReadOnly = false;
                    citiesCountBox.ReadOnly = false;
                });

            }
            else
                MessageBox.Show("O algoritmo n�o est� em execu��o!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GA_Thread != null)
            {
                if (GA_Thread.ThreadState != (ThreadState.Suspended | ThreadState.Background))
                {
                    GA_Thread.Suspend();
                    button2.Text = "Resumir";
                }
                else if (GA_Thread.ThreadState == (ThreadState.Suspended | ThreadState.Background))
                {
                    GA_Thread.Resume();
                    button2.Text = "Pausar";
                }
            }
            else
                MessageBox.Show("O algoritmo n�o est� em execu��o!");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                radioButton1.Checked = false;
        }
    }
}
