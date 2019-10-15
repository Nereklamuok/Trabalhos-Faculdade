using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PathPlanningIAA
{
    public partial class Form1 : Form
    {

        public List<Node> map;          //Mapa que inclui todos os nós
        public List<Node> open;         //Lista aberta de nós
        public List<Node> closed;       //Lista fechada de nós
        public List<Node> path;         //Lista de nós que compõem o caminho
        public Node start, end;         //Nós início e fim

        public Form1()
        {
            InitializeComponent();
        }        

        //Preenche a lista de nós do mapa assim como estabelece os vizinhos de cada nó
        private void button1_Click(object sender, EventArgs e)
        {
            map = new List<Node>();                 //Inicializa o mapa
            if(open != null)                        //Limpa a lista aberta caso exista algo nela
                open.Clear();
            if(closed != null)                      //Limpa a lista fechada caso exista algo nela
                closed.Clear();
            if (path != null)                       //Limpa a lista caminho caso exista algo nela
                path.Clear();
            start = null;                           //Torna o nó início nulo
            end = null;                             //Torna o nó fim nulo

            int maxX = (int)numericUpDown1.Value;   //Estabelece máxima coordenada em X
            int maxY = (int)numericUpDown2.Value;   //Estabelece máxima coordenada em Y

            for (int x = 0; x < maxX; x++)          //Circula pelos valores de coordenada X...
            {
                for (int y = 0; y < maxY; y++)      //Circula pelos valores de coordenada Y...
                {
                    Node no = new Node(x, y);       //Gera um nó em determinada coordenada X,Y
                    map.Add(no);                    //Adiciona esse nó ao mapa
                }
            }

            foreach(Node no in map)                 //Para cada nó na lista mapa...
            {
                //A lista de vizinhos desse nó é retornado pela função "getNeighbors"
                no.neighbors = getNeighbors(no);    
            }

            updatePictureBox();                     //Chama a função de atualização da PictureBox
        }

        /*Função de atualização da PictureBox. Constrói o grid do mapa e pinta os nós de acordo
          com seu estado (start, end, walkable, etc) */
        private void updatePictureBox()            
        {
            float maxX = (float)numericUpDown1.Value;   //Estabelece máxima coordenada em X
            float maxY = (float)numericUpDown2.Value;   //Estabelece máxima coordenada em Y

            float wx = (float)pictureBox1.Width;        //Armazena a largura da PictureBox
            float hy = (float)pictureBox1.Height;       //Armazena a altura da PictureBox

            float deltaX = wx / maxX;                   //Estabelece as subdivisões em X
            float deltaY = hy / maxY;                   //Estabelece as subdivisões em Y

            //Declara e inicializa uma imagem com o mesmo tamanho da PictureBox
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); 
            //Declara e inicializa um objeto Gráfico a partir da imagem bmp
            Graphics gp = Graphics.FromImage(bmp);
            //Declara e inicializa um objeto Caneta de cor preta e espessura 1.5
            Pen pen = new Pen(Color.Black, 1.5f);
            //Declara e inicializa uma fonte genérica sem serifa e de tamanho dependente de deltaX
            Font font = new Font(FontFamily.GenericSansSerif, deltaX / 2, FontStyle.Regular);
           
            if(open != null)                            //Se a lista aberta não estiver vazia
            {
                foreach (Node no in open)               //Para cada nó na lista aberta...
                {
                    //Preencher nas coordenadas do nó com a cor Roxa
                    gp.FillRectangle(Brushes.Purple, no.X * deltaX, no.Y * deltaY, deltaX, deltaY);
                }
            }

            if (closed != null)                         //Se a lista aberta não estiver vazia
            {
                foreach (Node no in closed)             //Para cada nó na lista aberta...
                {
                    //Preencher nas coordenadas do nó com a cor Roxo médio
                    gp.FillRectangle(Brushes.MediumPurple, no.X * deltaX, no.Y * deltaY, deltaX, deltaY);
                }
            }

            if (path != null)                           //Se a lista caminho não estiver vazia
            {
                int i = 0;                              //Declara e inicializa i (contagem) em 0
                foreach (Node no in path)               //Para cada nó na lista caminho...
                {
                    //Preencher nas coordenadas do nó com a cor Aqua
                    gp.FillRectangle(Brushes.Aqua, no.X * deltaX, no.Y * deltaY, deltaX, deltaY);
                    //Escrever nas coordenadas do nó uma string correspondendo à ordem do nó no caminho
                    gp.DrawString("" + i, font, new SolidBrush(Color.Black), no.X * deltaX, no.Y * deltaY);
                    //Incrementar a contagem
                    i++;
                }
            }

            if (end != null)                //Se o nó final não for nulo     
            {
                //Preencher nas coordenadas do nó final com a cor Vermelha
                gp.FillRectangle(Brushes.Red, end.X * deltaX, end.Y * deltaY, deltaX, deltaY);
            }       

            if (start != null)                //Se o nó inicial não for nulo   
            {
                //Preencher nas coordenadas do nó inicial com a cor Azul
                gp.FillRectangle(Brushes.Blue, start.X * deltaX, start.Y * deltaY, deltaX, deltaY);
            }

            if (map != null)                //Se a lista de nós mapa não estiver vazia
            {
                foreach (Node no in map)    //Para cada nó no mapa...
                {
                    if (no.walkable == false)   //Se o nó não for andável
                    {
                        //Preencher nas coordenadas do nó com a cor Preta
                        gp.FillRectangle(Brushes.Black, no.X * deltaX, no.Y * deltaY, deltaX, deltaY);
                    }
                }
            }

            for (int x = 0; x < maxX; x++)                          //Varre as coordenadas X
            {
                gp.DrawLine(pen, x * deltaX, 0, x * deltaX, hy);    //Desenha as linhas horizontais do grid
            }
            for (int y = 0; y < maxY; y++)                          //Varre as coordenadas Y
            {
                gp.DrawLine(pen, 0, y * deltaY, wx, y * deltaY);    //Desenha as linhas verticais do grid
            }

            pictureBox1.Image = bmp;            //Atualiza a imagem do PictureBox
            gp.Dispose();                       //Libera os recursos usados pelo objeto Gráfico gp
        }

        //Ao clicar na PictureBox:
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Se não existir ainda uma lista mapa, quer dizer que o mapa não foi gerado
            if(map == null)
            {
                MessageBox.Show("É necessário gerar o mapa primeiramente!");
                return;
            }
            //Se não houver uma opção de preenchimento selecionada na ComboBox, deve-se selecionar
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione uma opção!!!");
                return;
            }

            float maxX = (float)numericUpDown1.Value;   //Estabelece máxima coordenada em X
            float maxY = (float)numericUpDown2.Value;   //Estabelece máxima coordenada em Y

            float deltaX = (float)pictureBox1.Width / maxX;  //Estabelece as subdivisões em X
            float deltaY = (float)pictureBox1.Height / maxY; //Estabelece as subdivisões em Y

            float px = 0;                               //Inicializa px em 0
            for (int x = 0; x < maxX; x++)              //Circula pelos valores de coordenada em X
            {
                float py = 0;                           //Inicializa py em 0
                for (int y = 0; y < maxY; y++)          //Circula pelos valores de coordenada em Y
                {
                    //Verifique se o ponto de clique do mouse está dentro de um limiar
                    if (e.X >= px && e.X < (px + deltaX) &&
                        e.Y >= py && e.Y < (py + deltaY))
                    {
                        Node no = getNode(x, y);        //Obtém o nó na posição do clique

                        //Se a opção do comboBox for 0 (blocked)
                        if (comboBox1.SelectedIndex == 0)
                        {
                            no.walkable = !no.walkable; //Inverta a "andabilidade" do nó (é andável?)
                        }
                        //Se a opção do comboBox for 1 (start)
                        else if (comboBox1.SelectedIndex == 1)
                        {
                            start = no;                 //Defina o nó selecionado como início
                        }
                        //Se a opção do comboBo for 2 (end)
                        else if (comboBox1.SelectedIndex == 2)
                        {
                            end = no;                   //Defina o nó selecionado como final
                        }

                        updatePictureBox();             //Atualiza a pictureBox
                        return;

                    }
                    py += deltaY;                       //Incrementa py
                }
                px += deltaX;                           //Incrementa px
            }
        }

        //Se a pictureBox mudar de tamanho
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            updatePictureBox();             //Atualizar a PictureBox
        }               

        //Função de obtenção de um nó através de suas coordenadas x, y
        private Node getNode(int x, int y)
        {
            foreach (Node no in map)        //Para cada nó no mapa
            {
                if (no.X == x && no.Y == y) //Se as coordenadas x e y do nós corresponderem com as de entrada
                    return no;              //Retornar esse nó
            }
            //Caso nenhum nó no mapa corresponda às coordenadas, retornar nulo
            return null;                    
        }       

        //Função de obtenção da lista de vizinhos de um dado nó
        private List<Node> getNeighbors(Node node)
        {
            List<Node> nodes;                   //Declara a lista de nós
            nodes = new List<Node>();           //Inicializa a lista como uma nova lista (vazia)

            for(int x = -1; x <= 1; x++)        //Variar as coordenadas em X entre -1 e 1 (inclusive)
            {
                for(int y = -1; y <= 1; y++)    //Varias as coordenadas em Y entre -1 e 1 (inclusive)
                {
                    /*O nó vizinho é igual ao valor de retorno da função getNode 
                    em determinada posição ao redor do nó original*/
                    Node neighbor = getNode(node.X + x, node.Y + y);
                    /*Se o nó vizinho não for igual ao nó original e
                      não for um nó de valor nulo: */
                    if(neighbor != node && neighbor != null)
                        nodes.Add(neighbor);    //Adicionar esse nó à lista
                }              
            }
            return nodes;                       //Retornar a lista de nós vizinhos ao nó original
        }

        //Obtém o melhor caminho entre o nó inicial e o final através do algoritmo A*
        private void button2_Click(object sender, EventArgs e)
        {
            open = new List<Node>();                //Inicializa a lista aberta
            closed = new List<Node>();              //Inicializa a lista fechada
            path = new List<Node>();                //Inicializa a lista caminho

            foreach (Node no in map)                //Para cada nó no mapa  
                no.Parent = null;                   //Anular os pais vestigiais de execuções anteriores

            if (end == null)                        //Se não existir um nó final (deve existir)
            {
                MessageBox.Show("Nó de final inexistente ou inválido!");
                return;
            }

            else if (start != null)                 //Se existir um nó inicial
                open.Add(start);                    //Adicionar o nó inicial à lista aberta

            else                                    //Caso não exista o nó inicial (deve existir)
            {
                MessageBox.Show("Nó de início inexistente ou inválido!");
                return;
            }

            Node actual = start;                    //Declara o nó atual e o inicializa com o nó inicial

            while (open.Any())                      //Enquanto houver algo na lista aberta:
            {
                float maxF = 10000;                 //Variável de valor inicial absurdamente grande     
                
                //A estrutura de repetição a seguir tem o objetivo de obter o nó de menor valor F
                foreach (Node node in open)         //Para cada nó na lista aberta
                {
                    if (node.F < maxF)              //Se o valor F do nó for menor que o de maxF
                    {
                        actual = node;              //Atualizar o nó atual com o novo nó de menor valor F
                        maxF = node.F;              //O novo valor de maxF é igual ao F do novo nó
                    }
                }
                //Ao final do foreach acima, nosso nó atual será aquele de menor F na lista aberta

                open.Remove(actual);                //Remover o nó atual da lista aberta
                closed.Add(actual);                 //Adicionar o nó atual na lista fechada

                if (actual == end)                  //Se o nó atual for igual ao nó final
                    break;                          //Sair do loop (encontramos o caminho)
               
                foreach(Node neighbor in actual.neighbors)      //Para cada nó na lista de vizinhos do nó atual
                {
                    //Se o nó vizinho não é andável ou está na lista fechada
                    if (!neighbor.walkable || closed.Contains(neighbor))    
                        continue;                   //Seguimos para o próximo nó   
                    
                    //Se o nó vizinho não está na lista aberta
                    if (!open.Contains(neighbor))               
                    {
                        neighbor.Parent = actual;   //O pai do nó vizinho agora é o nó atual

                        //------------------------------------------------------------------------------------
                        //Atualiza os valores de G, H e F do nó vizinho

                        /*Pseudo-código:
                         G = (G do pai) + (distância entre o nó e seu pai)
                         H = distância entre o nó e o final
                         F = soma dos valores de G e H (G + H)
                         */

                        neighbor.G = neighbor.Parent.G + Node.DistanceBetweenNodes(neighbor, neighbor.Parent);
                        neighbor.H = Node.DistanceBetweenNodes(neighbor, end);
                        neighbor.F = neighbor.G + neighbor.H;
                        //------------------------------------------------------------------------------------
                        open.Add(neighbor);         //Adiciona esse vizinho à lista aberta
                    }
                    //Se o nó vizinho já está na lista aberta
                    else if (open.Contains(neighbor))
                    {
                        //Calcula qual seria o novo G desse vizinho caso o caminho passasse por aqui
                        float newG = actual.G + Node.DistanceBetweenNodes(neighbor, actual);

                        //Se o novo G for menor que o valor de G atual
                        if (newG < neighbor.G)
                        {
                            //------------------------------------------------------------------------------------
                            //Atualiza os valores de G, H e F do vizinho, de acordo com o novo G:
                            neighbor.G = newG;
                            neighbor.F = neighbor.G + neighbor.H;
                            neighbor.Parent = actual;
                            //------------------------------------------------------------------------------------
                        }
                    }
                }
            }
            if (actual == end)  //Se o nó atual for o nó final (encontramos o caminho)
            {
                Node pathNode = end;                        //Inicializar o nó path (caminho) de acordo com o nó final
                while (pathNode != null)                    //Enquanto existir um nó de caminho
                {
                    path.Add(pathNode);                     //Adicionar esse nó à lista caminho                   
                    pathNode = pathNode.Parent;             //Tornar o pai do nó atual o novo nó de caminho 
                }                  
                path.Reverse();                             //Inverter a ordem dos nós na lista caminho
                /*OBS: a linha acima é incluída pois na ordem original, o caminho inicia-se no nó final
                  já que ele é o "pai de todos os pais" e a partir dele regredimos até o início.
                  Invertendo a lista, iniciamos com o nó inicial e vamos até o final. Isso será
                  útil para o preenchimento da PictureBox a seguir. */

                updatePictureBox();                         //Atualiza a PictureBox
            }
            else //Se o nó atual não for o nó final (não achamos o caminho, provavelmente ele não existe)
                MessageBox.Show("Caminho não encontrado! Certifique-se que existe um caminho possível.");
        }
    }

    public class Node
    {
        public int X, Y;                    //Localização no grid
        public float G, H, F;               //distancias e custo
        public bool walkable;               //indica se é obstaculo ou não
        public Node Parent;                 //pai do nó ( null -> não tem pai )

        public List<Node> neighbors;        //Lista de vizinhos do nó
        public Node(int x, int y)           //Função de inicialização do nó
        {
            X = x;                          //Inicializa coordenada em X
            Y = y;                          //Inicializa coordenada em Y
            Parent = null;                  //Inicializa nó pai como nulo
            walkable = true;                //Inicializa o nó como andável
        }

        public override string ToString()   //Retorna uma string com as coordenadas do nó
        {
            return "px = " + X + " - py = " + Y;
        }


        public static float DistanceBetweenNodes(Node origin, Node ending)  //Calcula distância entre dois nós
        {
            float distance;                 
            //Teorema de Pitágoras para cálculo da distância
            distance = (float)(Math.Sqrt(Math.Pow((origin.X - ending.X), 2) + Math.Pow((origin.Y - ending.Y), 2)));
            return distance;
        }
    }
}