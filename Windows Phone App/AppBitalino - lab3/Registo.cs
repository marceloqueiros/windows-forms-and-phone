using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml.Media.Imaging;

namespace AppBitalino___lab3
{
    public delegate void MetodoCriarRegisto(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, long d, string frames);

    [DataContract]
    public class Registo
    {
        public Registo()
        {

        }

        public Registo(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, string fra, int estado)
        {
            Id_Registo = idRegisto;
            Id_utilizador = idUtilizador;
            tiposExame = tiposE;
            dataInicio = inicio;
            dataFim = fim;
            Resultados = f;
            frames = fra;
            Estado = estado;
        }

        public Registo(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, string fra, int estado)
        {
            Id_Registo = idRegisto;
            Id_utilizador = idUtilizador;
            tiposExame = tiposE;
            dataInicio = inicio;
            dataFim = fim;
            frames = fra;
            Estado = estado;
        }

        [DataMember]
        public string tiposExame { get; set; } //estão no formato ECG|...|...|
        [DataMember]
        public int Id_Registo { get; set; }
        [DataMember]
        public long duração { set; get; }
        [DataMember]
        public int Id_tipoExame { get; set; }
        [DataMember]
        public int Id_utilizador { get; set; }
        [DataMember]
        public string frames { get; set; }
        [DataMember]
        public DateTime dataInicio { get; set; }
        [DataMember]
        public DateTime dataFim { get; set; }
        [XmlIgnore]
        public List<FrameRegisto> Resultados { get; set; }    //lista de frames
        [DataMember]
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

        public BitmapImage imagem_registo { get; set; }

    }
}
