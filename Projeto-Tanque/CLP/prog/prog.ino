#include <Stepper.h> 
#include <TimerOne.h>

const int stepsPerRevolution = 200; 
  
//Inicializa a biblioteca utilizando as portas de 8 a 11 para 
//ligacao ao motor 
Stepper myStepper(stepsPerRevolution, 8,10,9,11); 
  
  float kp = 10;
  float ki = 5;
  float kd = 5;
  float T = 1;
  float nivel = 0;
  float setpoint = 3;
  float erro;
  float derivada;
  float integral;
  float erroant;
  float integralant;
  float saida;
  int cont = 0;
  int IR_SENSOR = 0; // Sensor is connected to the analog A0
  int intSensorResult = 0; //Sensor result
  float fltSensorCalc = 0; //Calculated value
  int contador  = 0;
  float total = 0;
  float media;

  bool ligado = false;
  int valorA0 = 0, totalA0 = 0;
  int valorA1 = 0, totalA1 = 0;
  byte i = 0;
  int pwm = 0;
  String dado;
  
  float vazao; //Variável para armazenar o valor em L/min
  int contaPulso; //Variável para a quantidade de pulsos
  int Min = 00; //Variável para minutos
  float Litros = 0; //Variável para Quantidade de agua
  float MiliLitros = 0; //Variavel para Conversão  


void setup() {  
  myStepper.setSpeed(60); //Determina a velocidade inicial do motor 
  pinMode(A0, INPUT);     //configura analog0 como o sensor de nível
  pinMode(A1, INPUT);     //configura analog1 como o sensor de vazao
  Serial.begin(9600);
  
  Timer1.initialize(1000000);
  Timer1.attachInterrupt(callback);
  
  pinMode(2, INPUT);
  attachInterrupt(0, incpulso, RISING); //Configura o pino 2(Interrupção 0) interrupção  
}

void callback(){    
 vazao = contaPulso / 5.5;
 i++;
 contaPulso = 0;
}

void loop() {
  // put your main code here, to run repeatedly:

      
  if (Serial.available())  //se byte pronto para leitura
  {                                
    //verifica qual caracter recebido...  
    switch(Serial.read())  
    {
      case 'A': //Liga o controle                 
        ligado = true;
        //digitalWrite(13, HIGH);
                
        break;   
           
      case 'B':  //desliga o controle               
        ligado = false;
        //digitalWrite(13, LOW);
        break;
        
      case 'W':
        if(ligado){
          //cria string de comunicacao com os dados a serem enviados... 
          dado =  String(valorA0, DEC) + "#"; //pega valor de A0
          dado +=  String(valorA1, DEC) + "#"; //pega valor de A1
          Serial.println(dado);  //envia a string com dados lidos...
        }
        break;

      case 'K':
        if(ligado){
          kp = Serial.parseInt();
          ki = Serial.parseInt();
          kd = Serial.parseInt();
          Serial.println(kp + ' '  + ki + ' ' + kd );  //envia a string com dados lidos...
        }
        break;     
        
      case 'P':
        setpoint = Serial.parseInt();
        if(setpoint < 0 ){
          setpoint = 0;
        }
         else if(setpoint > 255){
          setpoint=255;
        }
        break;     
    }        
  }  
      
      erro = setpoint - nivel;            
      derivada= (kd* (erro - erroant)/T); 
      integral = (ki*(erro + erroant)*(T/2)); 

      saida = (kp * erro) + integralant + integral + derivada; 

     if (saida > 60){
         saida = 60;
      }
      else if(saida<0){
         saida = 0;
     }

      erroant = erro;
      integralant = integral;

//--------------------------------------
  //Le valor Analg. 8x e faz a media...
  totalA0 += analogRead(A0);
  totalA1 += analogRead(A1);
   i++;    
  if(i == 8)
  { //divide total por 8...   
    
    valorA0 = totalA0 >> 3; totalA0 = 0;
    valorA1 = totalA1 >> 3; totalA1 = 0;
    valorA0 = map(valorA0,0,1023,0,255);// mudou a escala de 1023 para 0-255
    valorA1 = map(valorA1,0,1023,0,255);// mudou a escala de 1023 para 0-255
      i = 0;        
  }

    intSensorResult = analogRead(IR_SENSOR); //Get sensor value
    fltSensorCalc = (6787.0 / (intSensorResult - 3.0)) - 4.0; //Calculate distance in cm
    total += fltSensorCalc;
    
    contador++;
    
    if(contador == 1000){  
      media = total /contador;
      nivel = 13 - media ;
      
      if(nivel >= 4){
        nivel = 4;
      }
      if(nivel <=0){
        nivel = 0;
      }
      contador = 0;
      Serial.println(nivel);  //envia a string com dados lidos...
    }

 //Serial.print(cmMsec)
  
    if(nivel>setpoint && cont<300){
        myStepper.setSpeed(saida);
        myStepper.step(-1);//fecha o registro
        cont++;  
      } 
    else if(nivel<setpoint && cont>0){
       myStepper.setSpeed(saida);
       myStepper.step(1);//abre o registro
       cont--;
     }
}

void incpulso ()
{
 contaPulso++; //Incrementa a variável de pulsos 
}

