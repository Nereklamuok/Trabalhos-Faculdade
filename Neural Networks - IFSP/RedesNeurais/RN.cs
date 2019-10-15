using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RedesNeurais
{
    public enum TIPO { camada_de_entrada, camada_escondida, camada_de_saida };

    //classe que modela uma rede neural artificial
    public class RN
    {
        //matriz com os neuronios da rede
        public N[][] net;

        //valores de saidas das camadas
        public float[] s0, s1, s2;  

        public RN() 
        {

        }

        public RN(int num_entradas, int camada_entrada, int camada_escondida, int camada_saida)
        {
            //cria uma matriz de neuronios onde as colunas sao as camadas da rede
            //a rede tera sempre 3 camadas: entrada, escondida e saida

            net = new N[3][];
            net[0] = new N[camada_entrada];
            net[1] = new N[camada_escondida];
            net[2] = new N[camada_saida];

            s0 = new float[net[0].Length];
            s1 = new float[net[1].Length];
            s2 = new float[net[2].Length];

            //cria os neuronios da camada de entrada
            for (int i = 0; i < camada_entrada; i++) 
                net[0][i] = new N(num_entradas);            

            //cria os neuronios da camada escondida
            for (int i = 0; i < camada_escondida; i++) 
                net[1][i] = new N(camada_entrada);           

            //cria os neuronios da camada de saida
            for (int i = 0; i < camada_saida; i++) 
                net[2][i] = new N(camada_escondida);            

        }

        public void treinar(List<float[]> entradas, List<float[]> saidas)
        {
            //back propagation algorithm: regra delta generalizada             
            for (int k = 0; k < entradas.Count; k++)
            {                
                //steps 1, 2 e 3
                update(entradas[k]);

                //step 4
                //calcula os erros da camada de saida e ajusta pesos           
                for (int i = 0; i < net[2].Length; i++)
                {                    
                    net[2][i].erro = (saidas[k][i] - s2[i]) * s2[i] * (1.0f - s2[i]);
                    net[2][i].ajusta_peso(s1);
                }

                //step 5
                //calcula os erros da camada escondida e ajusta pesos           
                for (int i = 0; i < net[1].Length; i++)
                {
                    net[1][i].erro = 0f;
                    for (int j = 0; j < net[2].Length; j++)
                    {
                        net[1][i].erro += net[2][j].erro * net[2][j].W[i];
                    }
                    net[1][i].erro *= s1[i] * (1.0f - s1[i]);
                    net[1][i].ajusta_peso(s0);
                }

                //step 6
                //calcula os erros da camada de entrada e ajusta pesos           
                for (int i = 0; i < net[0].Length; i++)
                {
                    net[0][i].erro = 0f;
                    for (int j = 0; j < net[1].Length; j++)
                    {
                        net[0][i].erro += net[1][j].erro * net[1][j].W[i];
                    }
                    net[0][i].erro *= s0[i] * (1.0f - s0[i]);
                    net[0][i].ajusta_peso(entradas[k]);
                }
            }

            //alica os deltas de aprendizado
            for (int i = 0; i < net[0].Length; i++) net[0][i].aplica_delta();            
            for (int i = 0; i < net[1].Length; i++) net[1][i].aplica_delta();            
            for (int i = 0; i < net[2].Length; i++) net[2][i].aplica_delta();            

        }

        public float[] update(float[] ent)
        {
            //calcula a saida da rede a partir dos valores de entrada            
            
            //step 1
            //calcula as saidas da camada de entrada           
            for (int i = 0; i < net[0].Length; i++) s0[i] = net[0][i].update(ent);            

            //step 2
            //calcula as saidas da camada escondida
            for (int i = 0; i < net[1].Length; i++) s1[i] = net[1][i].update(s0);           

            //step 3
            //calcula as saidas da camada de saida
            for (int i = 0; i < net[2].Length; i++) s2[i] = net[2][i].update(s1);           

            return s2;

        }

        #region LOAD/SAVE XML
        public static RN Carregar(string path)
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(RN));
                StreamReader file = new StreamReader(path);
                RN nc = (RN)reader.Deserialize(file);
                file.Close();
                return nc;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Salvar(string path)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(RN));
                TextWriter writer = new StreamWriter(path);
                ser.Serialize(writer, this);
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

    }

    #region Neuronio Artificial
    public class N
    {
        #region RND: numeros randomicos (-0.5f ~ 0.5)
        
        private static Random rand = new Random((int)System.DateTime.Now.Ticks);
        
        private static float RND //retorna um numero entre -0.5f e 0.5f
        {
            get { return (float)rand.NextDouble() - 0.5f; } 
        }

        #endregion

        public static float e = 0.2f;   //taxa de aprendizado

        public float net, saida;
        public float erro, count;
        public float E0, W0, D0;        //bias
        public float[] W;               //pesos das conexoes
        public float[] D;               //deltas de treinamento

        public N() 
        {

        }

        public N(int num_entradas)
        {
            E0 = 1.0f;  //sempre 1 => bias
            W0 = RND;   //peso do bias
            D0 = 0.0f;  //delta do bias

            W = new float[num_entradas];
            D = new float[num_entradas];
            for (int i = 0; i < num_entradas; i++)
            {
                W[i] = RND;
                D[i] = 0f;
            }
        }

        public float update(float[] entrada)
        {
            net = 0f;
            net = W0 * E0;
            for (int i = 0; i < W.Length; i++)
            {
                net += W[i] * entrada[i];
            }            
            
            //atualiza a saida apartir de uma funcao de ativacao sigmoidal
            saida = 1.0f / (1.0f + (float)Math.Exp(-net));            
            
            return saida;
        }

        public void ajusta_peso(float[] entrada)
        {            
            D0 += e * erro * E0;        //versao batch
            //W0 += e * erro * E0;      //versao online

            for (int i = 0; i < W.Length; i++)
            {
                D[i] += e * erro * entrada[i];      //versao batch
                //W[i] += e * erro * entrada[i];    //versao online
            }
        }
    
        public void aplica_delta()
        {
            W0 += D0;
            D0 = 0f;
            for (int i = 0; i < W.Length; i++)
            {
                W[i] += D[i];
                D[i] = 0f;
            }            

        }
    }
    #endregion

}
