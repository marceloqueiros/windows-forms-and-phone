using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitalino___Projeto_LAB3
{
    [Serializable()]
    public class FrameRegisto
    {//cada uma das posições do frame correspondem a um resultado de um exame
        public int resultado0 { set; get; }  //corresponde a ECG por exemplo
        public int resultado1 { set; get; }  //corresponde a Lux por exemplo 
        public int resultado2 { set; get; }  //etc
        public int resultado3 { set; get; }
        public int resultado4 { set; get; }
        public int resultado5 { set; get; }
    }
}
