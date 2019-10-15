namespace RedesNeurais
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.entrada = new System.Windows.Forms.GroupBox();
            this.caracter = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saida = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.entrada.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Limpa Tudo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // entrada
            // 
            this.entrada.Controls.Add(this.caracter);
            this.entrada.Controls.Add(this.button10);
            this.entrada.Controls.Add(this.button5);
            this.entrada.Controls.Add(this.listBox2);
            this.entrada.Controls.Add(this.label2);
            this.entrada.Controls.Add(this.groupBox1);
            this.entrada.Controls.Add(this.button1);
            this.entrada.Controls.Add(this.button9);
            this.entrada.Controls.Add(this.button3);
            this.entrada.Controls.Add(this.button7);
            this.entrada.Controls.Add(this.button8);
            this.entrada.Location = new System.Drawing.Point(12, 12);
            this.entrada.Name = "entrada";
            this.entrada.Size = new System.Drawing.Size(384, 427);
            this.entrada.TabIndex = 1;
            this.entrada.TabStop = false;
            this.entrada.Text = "Entrada";
            // 
            // caracter
            // 
            this.caracter.Location = new System.Drawing.Point(6, 19);
            this.caracter.Name = "caracter";
            this.caracter.Size = new System.Drawing.Size(229, 266);
            this.caracter.TabIndex = 17;
            this.caracter.TabStop = false;
            this.caracter.Text = "número";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(263, 296);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(110, 23);
            this.button10.TabIndex = 1;
            this.button10.Text = "RND";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(263, 273);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Qual valor?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(241, 35);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(132, 186);
            this.listBox2.TabIndex = 2;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lista de números:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saida);
            this.groupBox1.Location = new System.Drawing.Point(263, 325);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saida";
            // 
            // saida
            // 
            this.saida.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saida.Location = new System.Drawing.Point(14, 16);
            this.saida.Name = "saida";
            this.saida.Size = new System.Drawing.Size(90, 77);
            this.saida.TabIndex = 0;
            this.saida.Text = "_";
            this.saida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(6, 343);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(229, 26);
            this.button9.TabIndex = 15;
            this.button9.Text = "Parar treinamento";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 317);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(229, 26);
            this.button3.TabIndex = 0;
            this.button3.Text = "Treinar Rede";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(6, 369);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(229, 26);
            this.button7.TabIndex = 0;
            this.button7.Text = "Salvar Rede";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(6, 395);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(229, 26);
            this.button8.TabIndex = 0;
            this.button8.Text = "Abrir Rede";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 237);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 26);
            this.button2.TabIndex = 0;
            this.button2.Text = "Gerar Casos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(132, 186);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 211);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 289);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 26);
            this.button4.TabIndex = 0;
            this.button4.Text = "Abrir Casos";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 573);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(537, 13);
            this.progressBar1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 521);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(289, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Numero de iteracoes no treinamento:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 496);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Numero de neuronios na camda de saida:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 471);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(289, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Numero de neuronios na camda escondida:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 446);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(289, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Numero de neuronios na camda de entrada:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(307, 521);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(89, 20);
            this.textBox4.TabIndex = 7;
            this.textBox4.Text = "1000";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(307, 495);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(89, 20);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "10";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(307, 469);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(89, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "100";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(307, 443);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(89, 20);
            this.textBox5.TabIndex = 8;
            this.textBox5.Text = "120";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(3, 263);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(135, 26);
            this.button6.TabIndex = 0;
            this.button6.Text = "Salvar Casos";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(307, 547);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(89, 20);
            this.textBox6.TabIndex = 7;
            this.textBox6.Text = "0,2";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 547);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Taxa de aprendizado";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(402, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 319);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gerar casos aleatorios";
            // 
            // listBox3
            // 
            this.listBox3.Location = new System.Drawing.Point(410, 337);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(132, 186);
            this.listBox3.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 591);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.entrada);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Redes neurais para reconhecimento de caracteres";
            this.entrada.ResumeLayout(false);
            this.entrada.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox entrada;
        private System.Windows.Forms.Label saida;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox caracter;
        private System.Windows.Forms.ListBox listBox3;
    }
}

