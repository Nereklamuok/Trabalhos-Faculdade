clear all; close all; clc;

umax = 220;
Ts = 10;
Gz = tf([0 0.5542],[1 -0.7763 -0.05567], Ts, 'variable','z^-1');
 
Kp = 8;
Ti = 29;
Td = 0;
 
z = tf('z',Ts);
s = tf('s');

dC = Kp * (1+ ((1/Ti)*(Ts/(z-1)))+ Td*((z-1)/Ts));
C = Kp * (1 + (1/Ti)*(1/s) + Td*s) 

G = d2c(Gz)

sys_cl1 = feedback(dC*Gz,1);
sys_cl2 = feedback(C*G,1);

opt = stepDataOptions('StepAmplitude', umax);

step(sys_cl1, opt);
figure();
step(sys_cl2, opt);
