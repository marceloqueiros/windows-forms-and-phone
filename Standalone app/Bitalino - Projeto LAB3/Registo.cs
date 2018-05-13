using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitalino___Projeto_LAB3
{
    public delegate void MetodoCriarRegisto(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, long d, string frames, bool a, bool b, bool c, bool dd, bool e, bool ff, string vT);
    [Serializable()]
    public class Registo
    {
        public Registo()
        {

        }
        public Registo(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, long d, string fra, bool a, bool b, bool c, bool dd, bool e, bool ff, string vT)
        {
            Id_Registo = idRegisto;
            Id_utilizador = idUtilizador;
            tiposExame = tiposE;
            dataInicio = inicio;
            dataFim = fim;
            Resultados = f;
            duração = d;
            frames = fra;
            ECG = a;
            AAC = b;
            EDA = c;
            LUX = dd;
            BATT = e;
            EMG = ff;
            valoresTexto= vT;
    }
        public string tiposExame { get; set; } //estão no formato ECG|...|...|
        public int Id_Registo { get; set; }
        public long duração { set; get; }
        public int Id_tipoExame { get; set; }
        public int Id_utilizador { get; set; }
        public bool ECG { get; set; }
        public bool AAC { get; set; }
        public bool EDA { get; set; }
        public bool LUX { get; set; }
        public bool BATT { get; set; }
        public bool EMG { get; set; }
        public string frames { get; set; }
        public string valoresTexto { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }

        public List<FrameRegisto> Resultados { get; set; }    //lista de frames

        public int Estado { get; set; }

        public void obterTipo(int idTipo) { }

        public void obterUtilizador(int idUtilizador) { }

        public void showExame(int idRegisto, int idTipo) { }

        public void criaRegisto(int idUtilizador) { }

        public void comparaID_U(int id) { }

        public void comparaData(DateTime data) { }
        public void comparaData(DateTime dataini, DateTime datafim) { }

        public void atribuiID() { }

        public void eliminaRegisto(int idUtilizador) { }

    }
}
