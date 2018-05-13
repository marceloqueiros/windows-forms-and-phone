using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitalino___Projeto_LAB3
{
    [Serializable()]
    public class Utilizador
    {
        public int Id_utilizador { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public int Altura { get; set; }
        public float Peso { get; set; }
        public string Fotografia { get; set; }
        public int Estado { get; set; }
        public int N_registos { get; set; }

        public void showUtilizador(int id) { }
        public void atualizaDados(int id) { }
        public void atribuiID() { }

        public void eliminaUtilizador() { }
        public void calculaIdade() { }

        public Utilizador()
        {

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
