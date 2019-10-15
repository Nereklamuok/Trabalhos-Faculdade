#include "TimerOne.h"

  float vazao; //Variável para armazenar o valor em L/min
  float media = 0;
  int contaPulso; //Variável para a quantidade de pulsos
  int i = 0; //Variável para segundos
  String dado; 
   
void setup()
{
 Serial.begin(9600);
 Timer1.initialize(1000000);
 Timer1.attachInterrupt(callback);
  
 pinMode(2, INPUT);
 attachInterrupt(0, incpulso, RISING); //Configura o pino 2(Interrupção 0) interrupção
}
  
void loop ()
{
  dado =  String(vazao, DEC) + "#"; //pega valor de vazao
  Serial.println(dado);  //envia a string com dados lidos...
}

void incpulso ()
{
 contaPulso++; //Incrementa a variável de pulsos 
}

void callback(){    
 vazao = contaPulso / 5.5;
 i++;
 contaPulso = 0;
}
