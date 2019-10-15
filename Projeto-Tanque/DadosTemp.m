clear all; close all; clc;%Limpa a área de trabalho

dados = fopen('dados_Temp.txt','r');%Abre o arquivo com os dados
valores = fscanf(dados,'%f %f',[2 Inf]);%Realiza a leitura dos dados no arquivo
fclose(dados);%Fecha o arquivo

%Manipula os dados e os divide em variáveis diferentes
t = valores(1,:)';
y0 = valores(2,:)';
u0 = 12*ones(size(y0));
%Gera um gráfico com a curva gerada a partir dos dados obtidos
stairs(t,y0); grid;
title('Temperatura x tempo');
xlabel('Tempo (s)');
ylabel('Temperatura (°C)');
 
% Ts = 10;
% % Gz = tf([0 0.01953],[1 -1.751 -0.7576], Ts, 'variable','z^-1');
% %Gz = tf([0 0.5542],[1 -0.7763 -0.05567], Ts, 'variable','z^-1');
% % Gz = tf([0 1.595],[1 +0.04233 -0.5872], Ts, 'variable','z^-1');
% %  Função de transferência da vazão abaixo:
% 
% Gz = tf([0 0.2222],[1 -0.2897 -0.05165], Ts, 'variable','z^-1');
% Kp = 8;
% Ti = 29;
% Td = 0;
%   
% z = tf('z',Ts);
% s = tf('s');
% 
% dC = Kp * (1+ ((1/Ti)*(Ts/(z-1)))+ Td*((z-1)/Ts));
% C = Kp * (1 + (1/Ti)*(1/s) + Td*s) 
% 
% sys_cl1 = feedback(dC*Gz,1);
% opt = stepDataOptions('StepAmplitude', 5);
% step(sys_cl1, opt);
% 
% figure();
% 
% TFG = d2c(Gz)
% sys_cl2 = feedback(C*TFG,1);
% step(sys_cl2, opt)