using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Bitalino___Projeto_LAB3
{
    class ModelBit
    {
        public List<Utilizador> listaUtilizadores { get; set; }
        public List<Registo> listaRegistos { get; set; }

        public event MetodosSemParametros resposta_criar_user;
        public event MetodosComInt resposta_calcular_idade; //y
        public event MetodosComInt resposta_gerar_id; //y
        public event MetodosSemParametros AtualizarDataUtilizadores;
        public event MetodosSemParametros resposta_editar_user; //Rui
        public event MetodosCom2Strings respostaProcurarUtilizadorPorTipo;
        public event MetodosCom2Strings respostaProcurarUtilizadorPorTipoParaLista;
        public event MetodosComInt respostaGerarIDRegistos;
        public event MetodosCom2Strings respostaProcurarUserParaRegisto;
        public event MetodosSemParametros respostaCriarRegisto;
        public event MetodosSemParametros AtualizarDataRegistos;

        public ModelBit()//construtor
        {
            listaUtilizadores = new List<Utilizador>();
            listaRegistos = new List<Registo>();
        }

        public void AtualizarDataRegistosMetodo()
        {
            if (AtualizarDataRegistos != null)
                AtualizarDataRegistos();
        }
        public void fechar()
        {
            Application.Exit();
        }
        public void guardarRegisto(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, long d, string fra, bool a, bool b, bool c, bool dd, bool e, bool ff, string vT)
        {
            Registo R = new Registo(idRegisto, idUtilizador, tiposE, inicio, fim, f, d, fra,a, b,  c, dd, e,  ff, vT);
            R.Estado = 0;
            listaRegistos.Add(R);

            atualizaFicheiroRegistos();
            
            if (respostaCriarRegisto != null)
                respostaCriarRegisto();
  
            if (AtualizarDataRegistos != null)
                AtualizarDataRegistos();
        }
        
        public void id_newRegistos()
        {
            Registo a = listaRegistos.FindLast(
                     delegate (Registo R)
                     {
                         int estado = 0;
                         return R.Estado == estado;
                     });
            int id;
            if (listaRegistos.Count > 0)
                id = a.Id_Registo + 1;
            else
                id = 1000;

            if (respostaGerarIDRegistos != null)
                respostaGerarIDRegistos(id);
        }

        public void ProcurarValorPorTipoViewListagem(string tipo, string valor) {
            string cor = "", texto = "";
            int num, index = -1;

            if (valor == "")
            {
                cor = "default";
            }
            else {
                switch (tipo)
                {
                    case "ID":
                        if (int.TryParse(valor, out num))
                        {
                            index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Convert.ToInt32(valor));
                        }
                        break;
                    case "Nome":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Nome == valor);
                        break;
                    case "Género":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Genero == valor);
                        break;
                    case "Idade":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Idade == Convert.ToInt32(valor));
                        break;
                    case "Altura":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Altura == Convert.ToInt32(valor));
                        break;
                    case "Peso":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Peso == Convert.ToInt32(valor));
                        break;
                }

                if (index == -1)
                {
                    texto = "Não encontrado";
                    cor = "red";
                }
                else
                {
                    if (listaUtilizadores[index].Estado == 0)
                    {
                        texto = "Encontrado!";
                        cor = "green";
                    }
                    else
                    {
                        texto = "Utilizador foi removido.";
                        cor = "black";
                    }
                }
            }
            if(Program.V_CriarRegisto.Visible == true)
            {
                if (respostaProcurarUserParaRegisto != null)
                    respostaProcurarUserParaRegisto(cor, texto);
            }
            else {
                if (respostaProcurarUtilizadorPorTipoParaLista != null)
                respostaProcurarUtilizadorPorTipoParaLista(cor, texto);
             }
        }

        public void ProcurarValorPorTipo(string tipo, string valor)
        {
            string cor="", texto="";
            int num, index = -1;

            if (valor == "")
            {
                cor = "default";
            }
            else {
                switch (tipo)
                {
                    case "ID":
                        if (int.TryParse(valor, out num))
                        {
                            index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Convert.ToInt32(valor));
                        }
                        break;
                    case "Nome":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Nome == valor);
                        break;
                    case "Género":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Genero == valor);
                        break;
                    case "Idade":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Idade == Convert.ToInt32(valor));
                        break;
                    case "Altura":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Altura == Convert.ToInt32(valor));
                        break;
                    case "Peso":
                        index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Peso == Convert.ToInt32(valor));
                        break;
                }

                if (index == -1)
                {
                    texto = "Não encontrado";
                    cor = "red";
                }
                else
                {
                    if (listaUtilizadores[index].Estado == 0)
                    {
                        texto = "Encontrado!";
                        cor = "green";
                    }
                    else
                    {
                        texto = "Utilizador foi removido.";
                        cor = "black";
                    }
                }
            }
            if (respostaProcurarUtilizadorPorTipo != null)
                respostaProcurarUtilizadorPorTipo(cor, texto);
        }

        //0 - Carregar a pagina para editar com dados.

        //apagar user
        public void Delete_user(int id)
        {
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Utilizador a = Program.Model.listaUtilizadores[index];
            listaUtilizadores[index].Estado = 1;
            atualizaFicheiroUtilizadores();
            for (int i = 0; i< listaRegistos.Count;i++ )
            {
                if (listaRegistos[i].Id_utilizador == id)
                    listaRegistos[i].Estado = 1;
            }
            if (AtualizarDataUtilizadores != null)
                AtualizarDataUtilizadores();
        }

        public void Delete_Registo(int id)
        {
            int index = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == id);
            listaRegistos.RemoveAt(index);
            //listaRegistos[index].Estado = 1;
            atualizaFicheiroRegistos();
            if (AtualizarDataRegistos != null)
                AtualizarDataRegistos();
        }

        private void atualizaFicheiroUtilizadores()
        {
            if (System.IO.File.Exists(Environment.CurrentDirectory + ("\\listaUtilizadores.xml"))) { File.WriteAllText(Environment.CurrentDirectory + ("\\listaUtilizadores.xml"), string.Empty); }
            Stream stream = File.OpenWrite(Environment.CurrentDirectory + ("\\listaUtilizadores.xml"));
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Utilizador>));
            xmlSer.Serialize(stream, Program.Model.listaUtilizadores);
            stream.Close();
        }

        private void atualizaFicheiroRegistos()
        {
            if (System.IO.File.Exists(Environment.CurrentDirectory + ("\\listaRegistos.xml"))) { File.WriteAllText(Environment.CurrentDirectory + ("\\listaRegistos.xml"), string.Empty); }
            Stream stream = File.OpenWrite(Environment.CurrentDirectory + ("\\listaRegistos.xml"));
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Registo>));
            xmlSer.Serialize(stream, Program.Model.listaRegistos);
            stream.Close();
        }

        //1 - id
        public void id_new()
        {
            Utilizador a = listaUtilizadores.FindLast(
                    delegate (Utilizador U)
                    {
                        int estado = 0;
                        return U.Estado == estado;
                    });
            int id;
            if (listaUtilizadores.Count > 0)
                id = a.Id_utilizador + 1;
            else
                id = 1000;

            if (resposta_gerar_id != null)
                resposta_gerar_id(id);
        }

        //2 - criar
        public void GuardarUtilizador(string id, string nome, DateTime DataNascimento, string genero, string altura, string peso, string caminhofoto)
        {
            //copiar a imagem para a pasta predefinida
            string[] path = caminhofoto.Split('\\');
            path = path.Last().Split('.');

            string caminho = Application.StartupPath + @"\imagemUtilizadores\" + id + '.' + path.Last();
            System.IO.File.Copy(caminhofoto, caminho);
            
            Utilizador NewUser = new Utilizador(Convert.ToInt32(id), nome, DataNascimento, genero, Convert.ToInt32(altura), Convert.ToInt32(peso), id + '.' + path.Last(), 0);
            NewUser.Idade = calcular_idade_Retornar(NewUser.DataNascimento);
            listaUtilizadores.Add(NewUser);

            StreamWriter dlg = new StreamWriter("DadosUtilizadores.txt", true);
            dlg.WriteLine(id + ";" + nome + ";" + DataNascimento.Day + "/" + DataNascimento.Month + "/" + DataNascimento.Year + ";" + genero + ";" + altura + ";" + peso + ";" + (id + '.' + path.Last()) + ";" + NewUser.Estado);
            dlg.Close();

            if (resposta_criar_user != null) //tem o metodo de receção e o metodo de atualizar lista inscritos
                resposta_criar_user();
        }
        
        // 3- idade
        public void calcula_idade(DateTime date)
        {
            int anos = DateTime.Today.Year - date.Year; //assumindo que já fez anos este ano

            if (date.Month > DateTime.Today.Month) //se ainda nao fez retira um
                anos = anos - 1;
            if (date.Month == DateTime.Today.Month && date.Day > DateTime.Today.Day)  //se os meses forem iguais mas ainda nao fez anos retira um
                anos = anos - 1;

            if (resposta_calcular_idade != null)
                resposta_calcular_idade(anos);
        }

        //este metodo serve só para calculos intermedios e não para eventos
        public int calcular_idade_Retornar(DateTime date)
        {
            int anos = DateTime.Today.Year - date.Year; //assumindo que já fez anos este ano

            if (date.Month > DateTime.Today.Month) //se ainda nao fez retira um
                anos = anos - 1;
            if (date.Month == DateTime.Today.Month && date.Day > DateTime.Today.Day)  //se os meses forem iguais mas ainda nao fez anos retira um
                anos = anos - 1;

            return anos;
        }
        

        public void AtualizarDataUtilizadoresMetodo() //chamado no fim de ler ficheiro
        {
            if (AtualizarDataUtilizadores != null)
                AtualizarDataUtilizadores();
        }
        
        //5 - Metodo que é chamado que é necessario editar o user, depois de ter todos os dados, vamos substituir na lista e 
        //de seguida substituir no ficheiro, depois de TUDO, atualiza a GridView.

        public void EditarUtilizador(string id, string nome, DateTime DataNascimento, string genero, string altura, string peso, string caminhofoto)
        {
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Convert.ToInt32(id));
            
            //copiar a imagem para a pasta predefinida
            string[] path = caminhofoto.Split('\\');
            path = path.Last().Split('.');

            string []partes = listaUtilizadores[index].Fotografia.Split('_');

            string filename;
            if (partes[0] != listaUtilizadores[index].Fotografia)
            {
                string []temp = partes[1].Split('.');
                int v = Convert.ToInt32(temp[0]);
                v++;
                filename = id + "_" + v + '.' + path.Last();
            }
            else filename = id + "_1." + path.Last();

            string caminho = Application.StartupPath + @"\imagemUtilizadores\" + filename;
            System.IO.File.Copy(caminhofoto, caminho);

            Utilizador aux = new Utilizador(Convert.ToInt32(id), nome, DataNascimento, genero, Convert.ToInt32(altura), Convert.ToInt32(peso), filename); //Utilizador Auxiliar.
            aux.Idade = calcular_idade_Retornar(aux.DataNascimento);

            listaUtilizadores[index] = aux;

            atualizaFicheiroUtilizadores();
            if (resposta_editar_user != null) 
                resposta_editar_user();
            if (AtualizarDataUtilizadores != null)
                AtualizarDataUtilizadores();

        }
    }
}