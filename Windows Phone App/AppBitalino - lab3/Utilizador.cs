using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml.Media.Imaging; //bitmaps


namespace AppBitalino___lab3
{
    [DataContract]
    public class Utilizador
    {

        [DataMember]
        public int Id_utilizador { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public int Idade { get; set; }
        [DataMember]
        public DateTime DataNascimento { get; set; }
        [DataMember]
        public string Genero { get; set; }
        [DataMember]
        public int Altura { get; set; }
        [DataMember]
        public float Peso { get; set; }
        [DataMember]
        public string Fotografia { get; set; }
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public int N_registos { get; set; }

        [XmlIgnore]
        public BitmapImage imagem_user{ get; set; }

        public void showUtilizador(int id) { }
        public void atualizaDados(int id) { }
        public void atribuiID() { }

        public void eliminaUtilizador() { }
        public void calculaIdade() { }

        public Utilizador()
        {

        }

        public Utilizador(int id, string _nome, DateTime data, string gen, int alt, float peso, BitmapImage image, int status)
        {
            Id_utilizador = id;
            Nome = _nome;
            DataNascimento = data;
            Genero = gen;
            Altura = alt;
            Peso = peso;
            imagem_user = image;
            Estado = status;
        }

        public Utilizador(int id, string _nome, DateTime data, string gen, int alt, float peso, string path, int status)
        {
            Id_utilizador = id;
            Nome = _nome;
            DataNascimento = data;
            Genero = gen;
            Altura = alt;
            Peso = peso;
            Fotografia = path;
            Estado = status;
        }

        public Utilizador(int id, string _nome, DateTime data, string gen, int alt, float peso, string path)
        {
            Id_utilizador = id;
            Nome = _nome;
            DataNascimento = data;
            Genero = gen;
            Altura = alt;
            Peso = peso;
            Fotografia = path;
        }

        public Utilizador(string _nome, DateTime data, string gen, int alt, float peso, string path)
        {
            Nome = _nome;
            DataNascimento = data;
            Genero = gen;
            Altura = alt;
            Peso = peso;
            Fotografia = path;
        }

        public Utilizador(int id, string _nome, DateTime data, string gen, int alt, float peso)
        {
            Id_utilizador = id;
            Nome = _nome;
            DataNascimento = data;
            Genero = gen;
            Altura = alt;
            Peso = peso;
        }
    }
}
