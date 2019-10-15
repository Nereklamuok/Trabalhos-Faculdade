//Inclusão de biblioteca para utilização de motores de passo
#include <Stepper.h> 

//Define número de passos correspondente a uma revolução do eixo do motor
const int stepsPerRevolution = 200; 
  
// Define os pinos que servirão para o acionamento alternado das bobinas do motor de passo
Stepper myStepper(stepsPerRevolution, 8,10,9,11); 
  
//Declaração de variáveis
//Variáveis do controlador PID:
  float kp = 10; //KP
  float ki = 5; //KI
  float kd = 5; //KD
  float T = 1; //Tempo de amostragem
  float setpoint = 3; //Setpoint, inicializado em 3
  float erro; // Erro = (Setpoint - Nível atual)
  float derivada; //Parcela derivada
  float integral; //Parcela integral
  float erroant; //Erro Anterior
  float integralant; //Integral Anterior
  long int saida; // Saída, resultado do algoritmo do controlador PID

//Variáveis do sensor de Nível:
  int cont = 0;
  int IR_SENSOR = 0; // Sensor is connected to the analog A0
  int intSensorResult = 0; //Sensor result
  float fltSensorCalc = 0; //Calculated value
  int contador  = 0;
  float total = 0;
  float media;
  long int vel;

  bool ligado = false;
  String dado;
  
void setup() {
  myStepper.setSpeed(60); //Determina a velocidade inicial do motor 
  Serial.begin(9600);   
}

void loop() {
  // put your main code here, to run repeatedly:

//-----------------------------------conexão com o supervisório-------------------

  if (Serial.available())  //se byte pronto para leitura
  {                                
    //verifica qual caracter recebido...  
    switch(Serial.read())  
    {
     case 'A': //Liga o controle                 
        ligado = true;
        //digitalWrite(3, HIGH);
                
        break;   
           
      case 'B':  //desliga o controle               
        ligado = false;
        //digitalWrite(3, LOW);
        break;
        
      case 'W':
       if(ligado){
          dado =  String(nivel, DEC) + "#"; //pega valor de A0
          Serial.println(nivel); //Send distance to computer          
        break;

      case 'K':
        if(ligado){
          kp = Serial.parseInt();
          ki = Serial.parseInt();
          kd = Serial.parseInt();
          //Serial.println(kp + ' '  + ki + ' ' + kd );  //envia a string com dados lidos...
        }
        break;     
        
      case 'P':
        setpoint = Serial.parseInt();
        if(setpoint < 0 ){
          setpoint = 0;
        }
         else if(setpoint > 4){
          setpoint=4;
        }
        break;     
    } 
  }        
  }
//--------------------------------------

      
      erro = setpoint - nivel;            
      derivada= (kd* (erro - erroant)/T); 
      integral = (ki*(erro + erroant)*(T/2)); 

      saida = (kp * erro) + integralant + integral + derivada; 
      vel = saida;

    if(vel < 0){

      vel = abs(vel);
      
      }
    else if (vel > 60){
      vel = 60;
      }

   
      erroant = erro;
      integralant = integral;


// read the value from the ir sensor

 intSensorResult = analogRead(IR_SENSOR); //Get sensor value
 fltSensorCalc = (6787.0 / (intSensorResult - 3.0)) - 4.0; //Calculate distance in cm
 total = total + fltSensorCalc;
 


    

    if(contador==1000){
  
      media = total /1000;
      nivel = 13 - media ;
      total = 0;
      contador = 0;
    
      if(nivel >= 4){

         nivel = 4;
  
      }
      if(nivel <=0){

        nivel = 0;
  
      }
       
  
  
} 
  

//motor

   
      myStepper.setSpeed (60); 
    if(nivel>setpoint && cont<300){
       
        myStepper.step(-1);//fecha o registro
        cont++;
        
      } 
      else if(nivel<setpoint && cont>0){
       // myStepper.setSpeed(vel);
        myStepper.step(1);//abre o registro
        cont--;
        
     }
  contador++;

  
}




