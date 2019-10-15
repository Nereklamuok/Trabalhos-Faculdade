using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;  // necessário para ter acesso as portas

using EasyModbus;



namespace ExNivel
{
    public partial class Form1 : Form
    {   
        //delcara variável MODBUS
        private ModbusClient modbusClient;

        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Rotina para atualizar comboBox com as portas COMs:                                    
            foreach (string comStr in SerialPort.GetPortNames())
            {   //Adiciona todas as COM diponíveis na lista
                comboBox1.Items.Add(comStr);
                comboBox2.Items.Add(comStr);
            }
            //Seleciona a primeira posição da lista
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }


            //Inicia comunicação MODBUS
            try 
            {
                modbusClient = new ModbusClient("COM5");
                modbusClient.UnitIdentifier = 1; //slaveID = 1 (CLP ID)
                modbusClient.Baudrate = 57600;
                modbusClient.Parity = System.IO.Ports.Parity.None;
                modbusClient.StopBits = System.IO.Ports.StopBits.One;
                modbusClient.ConnectionTimeout = 1000;
                modbusClient.Connect();
            }
            catch (Exception ex)
            {
                modbusClient = null;
                MessageBox.Show(ex.Message, "Falha na conexão Modbus com o CLP!");
            }
       }        

        //Botao conectar para o Arduino
        private void button1_Click(object sender, EventArgs e)
        {
            //Verifica se a porta COM do Arduino foi selecionada:
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Adicionar/Selecionar uma COM!");
                return;
            }

            if (serialPort1.IsOpen == false)
            {   //Se a conexao serial estiver fechada... abre uma conexao com o Arduino:
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();
                    timer1.Enabled = true;
                    comboBox1.Enabled = false;
                    button1.Text = "Desconectar";
                }
                catch (Exception ex)
                {   //Erro ao conectar com o Arduino:
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {   //Se a conexao serial estiver aberta... fecha a conexao com o Arduino:
                try
                {
                    serialPort1.Close();
                    timer1.Enabled = false;
                    comboBox1.Enabled = true;
                    button1.Text = "Conectar";
                }
                catch (Exception ex)
                {   //Erro ao fechar a conexao com Arduino:
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //botao conectar arduino 2
        private void button6_Click(object sender, EventArgs e)
        {
            //Verifica se a porta COM do Arduino foi selecionada:
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Adicionar/Selecionar uma COM!");
                return;
            }

            if (serialPort2.IsOpen == false)
            {   //Se a conexao serial estiver fechada... abre uma conexao com o Arduino:
                try
                {
                    serialPort2.PortName = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    serialPort2.Open();
                    timer3.Enabled = true;
                    comboBox2.Enabled = false;
                    button6.Text = "Desconectar";
                }
                catch (Exception ex)
                {   //Erro ao conectar com o Arduino:
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {   //Se a conexao serial estiver aberta... fecha a conexao com o Arduino:
                try
                {
                    serialPort2.Close();
                    timer3.Enabled = false;
                    comboBox2.Enabled = true;
                    button6.Text = "Conectar";
                }
                catch (Exception ex)
                {   //Erro ao fechar a conexao com Arduino:
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {   
            //Se a conexao serial estiver aberta... fecha a conexao com o Arduino:
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write("B"); // desliga o arduino 1
                serialPort1.Close();
            }

            //Se a conexao serial estiver aberta... fecha a conexao com o Arduino 2:
            if (serialPort2.IsOpen == true)
            {
                serialPort2.Close();
            }

            //Se a conexão modbus estiver aberta, fecha conexão com CLP
            if (modbusClient != null)
            {
                modbusClient.Disconnect();
                modbusClient = null;
            }
        }
        

        //-------------------------------------------PARTE ARDUINO---------------------------------------------

        bool ligado = false;

        private void button3_Click(object sender, EventArgs e)
        {
            

                if (serialPort1.IsOpen == true)
                {
                    ligado = true;
                    serialPort1.Write("A");
                }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

                if (serialPort1.IsOpen == true)
                {
                    ligado = false;
                    serialPort1.Write("B");
                }
            
        }

        private float nivel = 0;
        private float vazao = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                if (ligado)
                {
                    serialPort1.Write("W");
                    label4.Text = nivel.ToString("#0.00"); //escreve o nivel
                    desenha(nivel);

                }
            }
        }
       
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {   //receber valor pela serial...
            string RxStr = serialPort1.ReadLine();  //le o dado disponível na serial                                                           
            RxStr = RxStr.Replace("\r", "").Replace(".", ",");        //retirar codigo de nova linha...          

            string[] valores = RxStr.Split('#');    //divide a string pelo #

            ////converte o valor recebido do Arduino... 
            nivel = float.Parse (valores[0]);
            label15.Text = valores[0];
        }

        private void desenha(float nivel)
        {
            int tx = pictureBox1.Width;
            int ty = pictureBox1.Height;

            Bitmap bmp = new Bitmap(tx, ty);
            Graphics gp = Graphics.FromImage(bmp);

            float val = 1.0f - (float)nivel/4;
            float alt = (float)ty * val;

            gp.FillRectangle(Brushes.Blue, 0, alt, tx, ty - alt);
            pictureBox1.Image = bmp;

        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {   
               

                    serialPort1.Write("K");
                    serialPort1.WriteLine(textBox5.Text.ToString());
                    serialPort1.WriteLine(textBox3.Text.ToString());
                    serialPort1.WriteLine(textBox4.Text.ToString()); 
               
            }
        }    


        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            //Pega valor do trackbar para enviar na saida analógica do arduino...
            if (ligado)
            {
                if (serialPort1.IsOpen == true)
                {   //Envia valor para a saida pwm do Arduino...
                    float valor = (float)trackBar1.Value/100;
                    string val = valor.ToString();
                    label9.Text = val;
                    serialPort1.Write("P");
                    serialPort1.Write(val);
                }
            }

        }

        //-------------------------------------------------PARTE ARDUINO 2---------------------------------

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {   //receber valor pela serial...
            string RxStr = serialPort2.ReadLine();  //le o dado disponível na serial                                                           
            RxStr = RxStr.Replace("\r", "").Replace(".", ",");        //retirar codigo de nova linha...            

            string[] valores = RxStr.Split('#');    //divide a string pelo #
            ////converte o valor recebido do Arduino... 
            vazao = float.Parse(valores[0]);

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen == true)
            {
                //string val = String.Format("{0:0.00}",valo);
                string val = vazao.ToString("#0.00");
                label21.Text = val;
            }

        }

        //-------------------------------------------------PARTE CLP----------------------------------------

        bool ligadot = false; 

        private void button8_Click(object sender, EventArgs e) //botao liga controle de temp
        {

            if (modbusClient == null) return;

            try
            {
                //liga o clp e o supervisorio do clp (coloca o bool em uma memória)
                ligadot = true;
                modbusClient.WriteSingleCoil( 7005, true);
                modbusClient.WriteSingleCoil(7006, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
            }
        }

        private void button9_Click(object sender, EventArgs e) //botao desliga controle de temp
        {
            if (modbusClient == null) return;

            try
            {
                //desliga o clp e o supervisorio do clp
                ligadot = false;
                modbusClient.WriteSingleCoil(7006, true);
                modbusClient.WriteSingleCoil(7005, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (ligadot)
            {
                if (modbusClient == null) return;

                try
                {
                    //le a temperatura e mostra no supervisório  tratar tensão de o a 10
                    int temperatura = modbusClient.ReadInputRegisters(128, 1)[0];
                    temperatura = (temperatura / 1000) + 20;
                    trackBar4.Value = temperatura;
                    string temp = temperatura.ToString();
                    label19.Text = temp;

                    //le a tensão e mostra no supervisório tratar de 1 a 5
                    int tensao = modbusClient.ReadHoldingRegisters(3128, 1)[0]; // recebe valor de 400 a 2000 (1 a 5)
                    double tensa = (tensao - 400) * 0.137; 
                    string tens = tensa.ToString();
                    label31.Text = tens;

                    // pega a vazão do arduino e envia para o CLP
                    //transforma o valor de vazao para o CLP
                    float vaz = vazao * 1000;
                    int val = (int)vaz;
                    modbusClient.WriteSingleRegister(10020, val); 

                        
                       
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
                }
            }
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (ligadot)
            {
                if (modbusClient == null) return;

                try
                {
                    //pega o valor para o setpoint da temperatura e escreve na variavel do CLP
                    string val = trackBar2.Value.ToString();
                    label23.Text = val;
                    int valor = (trackBar2.Value-20)*100;
                    modbusClient.WriteSingleRegister(10000, valor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e) // uptade constante mestre
        {
            if (ligadot)
            {
                if (modbusClient == null) return;

                try
                {
                    //pega o valor para o setpoint da temperatura e escreve na variavel do CLP
                    
                    int kp = int.Parse(textBox1.Text);
                    modbusClient.WriteSingleRegister(10001, kp);
                    int ki = int.Parse(textBox6.Text);
                    modbusClient.WriteSingleRegister(10002, ki);
                    int kd = int.Parse(textBox2.Text);
                    modbusClient.WriteSingleRegister(10003, kd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (ligadot)
            {
                if (modbusClient == null) return;

                try
                {
                    //pega o valor para o setpoint da temperatura e escreve na variavel do CLP
                    
                    int kp = int.Parse(textBox7.Text);
                    modbusClient.WriteSingleRegister(10009, kp);
                    int ki = int.Parse(textBox9.Text);
                    modbusClient.WriteSingleRegister(10010, ki);
                    int kd = int.Parse(textBox8.Text);
                    modbusClient.WriteSingleRegister(10011, kd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro na escrita de dados no CLP!");
                }
            }
        }


    }
}
