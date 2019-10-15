using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RedesNeurais
{    
    public class Casos
    {
        public List<int> lista_numero;
        public List<bool[][]> lista_caso;

        public Casos()
        {
            lista_numero = new List<int>();
            lista_caso = new List<bool[][]>();
        }

        public bool[][] this[int i]
        {
            get { return lista_caso[i]; }
        }


        public void AddCaso(bool[][] caso, int numero)
        {
            lista_caso.Add(caso);
            lista_numero.Add(numero);
        }

        public int Count
        {
            get { return lista_caso.Count; }
        }

        public static Casos Carregar(string path)
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(Casos));
                StreamReader file = new StreamReader(path);
                Casos nc = (Casos)reader.Deserialize(file);
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
                XmlSerializer ser = new XmlSerializer(typeof(Casos));
                TextWriter writer = new StreamWriter(path);
                ser.Serialize(writer, this);
                writer.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }    
}
