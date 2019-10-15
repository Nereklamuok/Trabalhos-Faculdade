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
  float nivel = 0; //Armazena o valor de nível medido
  int intSensorResult = 0; //Valor fornecido pelo sensor, do tipo inteiro
  float fltSensorCalc = 0; //Valor calculado de nível, do tipo ponto flutuante
  int contador  = 0; //Número de medições realizadas
  float total = 0; //Valor total, soma das medições realizadas até então
  float media; //Valor médio das medições

//Variáveis de controle do motor de passo
  long int vel; //Armazena o valor de velocidade do motor de passo
  int cont = 0; //Armazena o valor de posição do eixo do motor de passo
  
//Outras variáveis
  bool ligado = true; //Controla o estado do controle, false = desligado e true = ligado
  String dado; //Armazena as variáveis a serem enviadas via serial para o sistema supervisório
  
  void setup() {
      myStepper.setSpeed(60); //Determina a velocidade inicial do motor em 60 
      Serial.begin(9600); //Inicia a comunicação serial com baud rate = 9600   
  }

  void loop() {
     //Obtém o valor do sensor de nível através da leitura analógica da entrada A1
     intSensorResult = analogRead(A1); 
     //Manipula o valor do sensor de nível de forma a obter o equivalente em centímetros
     fltSensorCalc = (6787.0 / (intSensorResult - 3.0)) - 4.0; 
     total += fltSensorCalc; //Acumula o valor da medição no total medido
     contador++; //Incrementa o número de medições realizadas
     
     if(contador == 1000){  //Caso já tenham sido realizadas 1000 medições, prosseguir com o seguinte:
        media = total /1000; //Calcula a média das 1000 últimas medições
        nivel = 13 - media ; //Ajusta o valor de nível de acordo com a média das medições
        total = 0; //Zera o valor de total das medições
        contador = 0; //Zera o valor do número de medições

        //Caso o valor de nível exceda 4, limite-o em 4
        if(nivel > 4)
           nivel = 4;
        //Caso o valor de nível seja menor que 0, limite-o em 0
        if(nivel < 0)
          nivel = 0;
     }
     
//-----------------------------------Início da conexão com supervisório-------------------

  if (Serial.available())  //Se a serial estiver disponível
  {                                
    switch(Serial.read())  //Lê o caráter recebido e toma uma ação correspondente:
    {
     case 'A': //Caso receba "A" do sistema supervisório...                
        ligado = true; //... ativa o controle
        break;   
           
      case 'B':  //Caso receba "B" do sistema supervisório...               
        ligado = false; //... desativa o controle
        break;
        
      case 'W': //Caso receba "W" do sistema supervisório...
       if(ligado){ //Se o sistema de controle estiver ativo:
          dado =  String(nivel, DEC) + "#"; //Formata uma String com o valor de nível medido
          Serial.println(dado);  //Envia a string formatada para o sistema supervisório
       }
        break;

      case 'K': //Caso receba "K" do sistema supervisório...
        if(ligado){ //Se o sistema de controle estiver ativo:
          kp = Serial.parseInt(); //Atualiza o valor de "kp" com o enviado pelo sistema supervisório
          ki = Serial.parseInt(); //Atualiza o valor de "ki" com o enviado pelo sistema supervisório
          kd = Serial.parseInt(); //Atualiza o valor de "kd" com o enviado pelo sistema supervisório
        }
        break;     
        
      case 'P': //Caso receba "P" do sistema supervisório...
        setpoint = Serial.parseInt();//Atualiza o valor de setpoint com o enviado pelo sistema supervisório

        //Se o valor de setpoint enviado for menor que 0, limita-o em 0
        if(setpoint < 0 )
          setpoint = 0;
        //Se o valor de setpoint enviado exceder 4, limita-o em 4
        else if(setpoint > 4)
          setpoint=4;
        break;     
    } 
  }        

//-----------------------------------Fim da conexão com supervisório-------------------

//Algoritmo do controlador PID
  if(ligado){
        erro = setpoint - nivel; //Calcula o erro atual entre setpoint e variável de processo         
        derivada = (kd* (erro - erroant)/T); //Calcula a parcela derivada
        integral = (ki*(erro + erroant)*(T/2)); //Calcula a parcela integral
  
        saida = (kp * erro) + integralant + integral + derivada; //Realiza o cálculo do sinal de controle
        vel = saida; //Iguala o valor de velocidade ao da saída do controlador
  
        //Caso o valor de velocidade seja menor que zero, obtenha o módulo
        if(vel < 0)
          vel = abs(vel);
        //Caso o valor de velocidade seja maior que 60, limita-o em 60
        else if (vel > 60)
          vel = 60;
     
        erroant = erro; //Atualizar o valor de erro anterior para o próximo cálculo
        integralant = integral; //Atualizar o vlaor de integral anterior para o próximo cálculo
  }
  //Acionamento do motor
  if(ligado){ //Se o sistema de controle estiver ativo:    
      myStepper.setSpeed (60); //Define o valor de velocidade do motor de passo 
      
      if(nivel > setpoint && cont<300){ //Se o nível ultrapassar o setpoint e o registro não estiver em seu limite...
        myStepper.step(-1);//fecha o registro
        cont++; //Contabiliza um passo
      } 
      else if(nivel<setpoint && cont>0){ //Se o nível baixar além do setpoint e o registro não estiver em seu limite...
        myStepper.step(1);//abre o registro
        cont--; //Contabiliza um passo
      }
   }
}


