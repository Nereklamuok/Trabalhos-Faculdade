float vazao;    //Variável para armazenar o valor de vazão, em L/min
int contaPulso; //Variável para realizar a contagem de pulsos do sensor de vazão
int i=0;        //Variável para contagem
String dado;    //Variável para armazenar a String com os dados a serem enviados para o sistema supervisório

void incpulso (){ //Função que incrementa o número de pulsos gerados pelo sensor de fluxo
  contaPulso++;  //Incrementa a variável de contagem dos pulsos
}

void setup() //Inicialização
{ 
  Serial.begin(9600); //Inicia a serial com um baud rate de 9600
  
  pinMode(3, INPUT); //Define o pino 3 como entrada
  attachInterrupt(1, incpulso, RISING); //Configura o pino 3 para trabalhar como interrupção, acionada na borda de subida
} 


void loop ()
{  
  contaPulso = 0;   //Zera a variável para contar os giros por segundos
  sei();            //Habilita interrupção
  delay (1000);     //Aguarda 1 segundo, para realizar contagem de pulsos
  cli();            //Desabilita interrupção
  
  vazao = contaPulso / 5.5; //Determina o valor de vazão com base na contagem de pulsos, em L/min
  
  dado =  String(vazao, DEC) + "#"; //Formata a string dado com o valor de vazão
  Serial.println(dado);             //Envia a string com os dados para o sistema supervisório
}  
  
  
 
 

