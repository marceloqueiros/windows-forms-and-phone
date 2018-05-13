using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;//escrever em ficheiros
using System.IO;

using System.Runtime.Serialization; //Serializacao
using Windows.UI.Xaml.Media.Imaging;
using System.Globalization;

namespace AppBitalino___lab3
{
    public class ModelBit
    {
        public bool flagID = false;
        public bool flagTipo = false;
        public bool flagDI = false;
        public bool flagDF = false;
        int lastID;

        App Program = App.Current as App;

        public List<Utilizador> listaUtilizadores { get; set; }
        public List<Registo> listaRegistos { get; set; }
        public List<Registo> aux { get; set; }

        public event MetodosSemParametros resposta_criar_user;
        public event MetodosComInt resposta_calcular_idade; //y
        public event MetodosComInt resposta_calcular_idade_editar; //y
        public event MetodosComInt resposta_calcular_idade_visualizar;
        public event MetodosComInt resposta_gerar_id; //y
        public event MetodosSemParametros AtualizarDataUtilizadores;
        public event MetodosSemParametros AtualizarDataRegistos;
        public event MetodosComUser resposta_editar_user; //Rui
        public event MetodosCom2Strings respostaProcurarUtilizadorPorTipo;
        public event MetodosCom2Strings respostaProcurarUtilizadorPorTipoParaLista;
        public event MetodosSemParametros respostaApagarUser;
        public event MetodosComUser respostaPedidoPorID;
        public event MetodosComInt respostaGerarIDRegistos;
        public event MetodosSemParametros respostaCriarRegisto;
        public event MetodosComString respostaNomeUser;
        public event MetodosComUser respostaPedidoUser;
        public event MetodosSemParametros respostaApagarRegisto;
        public event MetodosSemParametros respostaFiltragem;
        public event MetodosSemParametros respostaFiltragem2;

        public ModelBit()//construtor
        {
            listaUtilizadores = new List<Utilizador>();
            listaRegistos = new List<Registo>();
        }


        //ordenar registos
        public void OrdenarLista(int i)
        {
            if(i == 0)
            {
                Registo temp = new Registo();

                for (int n = 1; n < listaRegistos.Count; n++)
                {
                    for (int j = 0; j < listaRegistos.Count - n; j++)
                    {
                        if (listaRegistos[j].Id_Registo > listaRegistos[j + 1].Id_Registo)
                        {
                            temp = listaRegistos[j];
                            listaRegistos[j] = listaRegistos[j + 1];
                            listaRegistos[j + 1] = temp;
                        }
                    }
                }
                if (flagID == false)
                    flagID = true;
                else
                {
                    listaRegistos.Reverse();
                    flagID = false;
                }
            }
            

            if (i == 1)
            {
                Registo temp = new Registo();

                for (int n = 1; n < listaRegistos.Count; n++)
                {
                    for (int j = 0; j < listaRegistos.Count - n; j++)
                    {
                        if (listaRegistos[j].tiposExame.CompareTo(listaRegistos[j + 1].tiposExame) > 0)
                        {
                            temp = listaRegistos[j];
                            listaRegistos[j] = listaRegistos[j + 1];
                            listaRegistos[j + 1] = temp;
                        }
                    }
                }
                if(flagTipo == false)
                    flagTipo = true;
                else
                {
                    listaRegistos.Reverse();
                    flagTipo = false;
                }
            }
           

            if (i == 2)
            {
                Registo temp = new Registo();

                for (int n = 1; n < listaRegistos.Count; n++)
                {
                    for (int j = 0; j < listaRegistos.Count - n; j++)
                    {
                        if (listaRegistos[j].dataInicio > listaRegistos[j + 1].dataInicio)
                        {
                            temp = listaRegistos[j];
                            listaRegistos[j] = listaRegistos[j + 1];
                            listaRegistos[j + 1] = temp;
                        }
                    }
                }
                if (flagDI == false)
                    flagDI = true;
                else
                {
                    listaRegistos.Reverse();
                    flagDI = false;
                }
            }
            

            if (i == 3)
            {
                Registo temp = new Registo();

                for (int n = 1; n < listaRegistos.Count; n++)
                {
                    for (int j = 0; j < listaRegistos.Count - n; j++)
                    {
                        if (listaRegistos[j].dataFim > listaRegistos[j + 1].dataFim)
                        {
                            temp = listaRegistos[j];
                            listaRegistos[j] = listaRegistos[j + 1];
                            listaRegistos[j + 1] = temp;
                        }
                    }
                }
                if (flagDF == false)
                    flagDF = true;
                else
                {
                    listaRegistos.Reverse();
                    flagDF = false;
                }
            }

            if (AtualizarDataRegistos != null)
                AtualizarDataRegistos();
        }

        //filtrarRegistos
        public void filtrarLista(int index, string filtro)
        {
            aux = new List<Registo>();
            aux.Clear();
            foreach (Registo r in listaRegistos)
                if (r.Estado == 0)
                    aux.Add(r);

            if (index == 0)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].Id_Registo != Convert.ToInt32(filtro))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            if(index == 1)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].tiposExame != filtro)
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (index == 2)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].dataInicio < Convert.ToDateTime(filtro, CultureInfo.InvariantCulture))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (index == 3)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].dataFim < Convert.ToDateTime(filtro, CultureInfo.InvariantCulture))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (respostaFiltragem != null)
                respostaFiltragem();
        }

        public void filtrarLista2(int index, string filtro)
        {
            aux = new List<Registo>();
            aux.Clear();
            foreach (Registo r in listaRegistos)
                if (r.Estado == 0 && r.Id_utilizador == lastID)
                    aux.Add(r);

            if (index == 0)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].Id_Registo != Convert.ToInt32(filtro))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (index == 1)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].tiposExame != filtro)
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (index == 2)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].dataInicio < Convert.ToDateTime(filtro, CultureInfo.InvariantCulture))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (index == 3)
            {
                for (int i = 0; i < aux.Count(); i++)
                {
                    if (aux[i].dataFim < Convert.ToDateTime(filtro, CultureInfo.InvariantCulture))
                    {
                        aux.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (respostaFiltragem2 != null)
                respostaFiltragem2();
        }

        public void resetOrdem()
        {
            Registo temp = new Registo();

            for (int n = 1; n < listaRegistos.Count; n++)
            {
                for (int j = 0; j < listaRegistos.Count - n; j++)
                {
                    if (listaRegistos[j].Id_Registo > listaRegistos[j + 1].Id_Registo)
                    {
                        temp = listaRegistos[j];
                        listaRegistos[j] = listaRegistos[j + 1];
                        listaRegistos[j + 1] = temp;
                    }
                }
            }
        }

        //Criar Registo
        public void guardarRegisto(string idRegisto, string idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, string fra)
        {
            Registo R = new Registo(Convert.ToInt32(idRegisto), Convert.ToInt32(idUtilizador), tiposE, inicio, fim, f, fra, 0);

            listaRegistos.Add(R);

            atualizaFicheiroRegistos();

            if (respostaCriarRegisto != null)
                respostaCriarRegisto();

        }

        internal void nomeUser(int a)
        {
            foreach (Utilizador u in listaUtilizadores)
                if (u.Id_utilizador == a && respostaNomeUser!=null) respostaNomeUser(u.Nome);
        }

        //Guardar Registo em ficheiro
        private async void atualizaFicheiroRegistos()
        {
            var registos = listaRegistos;

            var serializer = new DataContractSerializer(typeof(List<Registo>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                              "DadosRegistos.xml",
                              CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, registos);
            }



            StorageFolder pasta = Windows.Storage.ApplicationData.Current.LocalFolder;
            
                foreach (Registo r in listaRegistos)
                {
                    Stream streamFicheiroR = await pasta.OpenStreamForWriteAsync(Convert.ToString(r.Id_Registo) + ".txt", CreationCollisionOption.ReplaceExisting);
                    using (StreamWriter escritorR = new StreamWriter(streamFicheiroR))
                    {
                        foreach (FrameRegisto f in r.Resultados)
                            escritorR.WriteLine(f.resultado0 + ";" + f.resultado1 + ";" + f.resultado2 + ";" + f.resultado3 + ";" + f.resultado4 + ";" + f.resultado5);
                    }

                }
            
        }

        public void AtualizaDataRegistosMetodo()
        {
            if (AtualizarDataRegistos != null)
                AtualizarDataRegistos();
        }

        //adicionar registos lista. Controller chama este metodo n vezes
        public void AdicionarNovoRegistoLista(Registo novo) //ler do ficheiro
        {
            listaRegistos.Add(novo); //adicionar à lista
        }

        public void id_newRegistos()
        {
            int id;
            if (listaRegistos.Count > 0)
                id = 1000 + listaRegistos.Count;
            else
                id = 1000;

            if (respostaGerarIDRegistos != null)
                respostaGerarIDRegistos(id);
        }



        public void ProcurarValorPorTipoViewListagem(string tipo, string valor)
        {
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
            if (respostaProcurarUtilizadorPorTipoParaLista != null)
                respostaProcurarUtilizadorPorTipoParaLista(cor, texto);
        }

        public void ProcurarValorPorTipo(string tipo, string valor)
        {
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
            if (respostaProcurarUtilizadorPorTipo != null)
                respostaProcurarUtilizadorPorTipo(cor, texto);
        }

        //0 - Carregar a pagina para editar com dados.

        //apagar user
        public void Delete_user(int id)
        {
            int index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Utilizador a = listaUtilizadores[index];
            listaUtilizadores[index].Estado = 1;
            savefile();
            if (respostaApagarUser != null)
                respostaApagarUser();
        }

        //apagar registo
        public void Delete_registo(int id)
        {
            int index = listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == id);
            Registo r = listaRegistos[index];
            listaRegistos[index].Estado = 1;
            atualizaFicheiroRegistos();
            if (respostaApagarRegisto != null)
                respostaApagarRegisto();
        }

        //1 - id
        public void id_new()
        {
            int id;
            if (listaUtilizadores.Count > 0)
                id = 1000 + listaUtilizadores.Count;
            else
                id = 1000;

            if (resposta_gerar_id != null)
                resposta_gerar_id(id);
        }

        //2 - criar
        public void GuardarUtilizador(string id, string nome, DateTime data, string genero, string altura, string peso, BitmapImage path)
        {
            Utilizador user = new Utilizador(Convert.ToInt32(id), nome, data, genero, Convert.ToInt32(altura), Convert.ToInt32(peso), path, 0);
            listaUtilizadores.Add(user);
            savefile();
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

        public void calcula_idadeEditar(DateTime date)
        {
            int anos = DateTime.Today.Year - date.Year; //assumindo que já fez anos este ano

            if (date.Month > DateTime.Today.Month) //se ainda nao fez retira um
                anos = anos - 1;
            if (date.Month == DateTime.Today.Month && date.Day > DateTime.Today.Day)  //se os meses forem iguais mas ainda nao fez anos retira um
                anos = anos - 1;

            if (resposta_calcular_idade_editar != null)
                resposta_calcular_idade_editar(anos);
        }

        internal void calcula_idadeVisualizar(DateTime date)
        {
            int anos = DateTime.Today.Year - date.Year; //assumindo que já fez anos este ano

            if (date.Month > DateTime.Today.Month) //se ainda nao fez retira um
                anos = anos - 1;
            if (date.Month == DateTime.Today.Month && date.Day > DateTime.Today.Day)  //se os meses forem iguais mas ainda nao fez anos retira um
                anos = anos - 1;

            if (resposta_calcular_idade_visualizar != null)
                resposta_calcular_idade_visualizar(anos);
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


        //4-ler do ficheiro
        //controller ativa este metodo n vezes
        public void AdicionarNovoUtilizadorLista(Utilizador novo) //ler do ficheiro
        {
            listaUtilizadores.Add(novo); //adicionar à lista
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
            int index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Convert.ToInt32(id));

            Utilizador aux = new Utilizador(Convert.ToInt32(id), nome, DataNascimento, genero, Convert.ToInt32(altura), Convert.ToInt32(peso), caminhofoto,0); //Utilizador Auxiliar.
            aux.Idade = calcular_idade_Retornar(aux.DataNascimento);

            listaUtilizadores[index] = aux;

            savefile();
            if (resposta_editar_user != null)
                resposta_editar_user(aux);
            if (AtualizarDataUtilizadores != null)
                AtualizarDataUtilizadores();

        }

        //6 - Gravar ficheiro.  rui - 18/04

        public async void savefile()
        {
            var users = listaUtilizadores;

            var serializer = new DataContractSerializer(typeof(List<Utilizador>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                              "DadosUtilizadores.xml",
                              CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, users);
            }
            /*MemoryStream sessionData = new MemoryStream();	//New

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("dados_utilizador.xml", CreationCollisionOption.OpenIfExists);

            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(List<Utilizador>));	//Serialization da lista utilizadores.
                dcs.WriteObject(sessionData, listaUtilizadores);

                sessionData.Seek(0, SeekOrigin.Begin);
                await sessionData.CopyToAsync(fileStream);
                await fileStream.FlushAsync();*/

            /*StorageFolder pasta = Windows.Storage.ApplicationData.Current.LocalFolder;

            Stream streamFicheiro = await pasta.OpenStreamForWriteAsync("DadosUtilizadores.txt", CreationCollisionOption.ReplaceExisting);

            using (StreamWriter escritor = new StreamWriter(streamFicheiro))
            {
                foreach (Utilizador user in listaUtilizadores)
                {
                    escritor.WriteLine(user.Id_utilizador + ";" + user.Nome + ";" + user.DataNascimento.Day + "/" + user.DataNascimento.Month + "/" + user.DataNascimento.Year + ";" + user.Genero + ";" + user.Altura + ";" + user.Peso + ";" + user.Fotografia + ";" + user.Estado);
                }

            }*/

        }

        //7 - Ler do ficheiro.  rui - 18/04

        public async void loadfile()
        {
            using (Stream fileStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("dados_utilizador.xml"))//abre
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(List<Utilizador>));
                listaUtilizadores = (List<Utilizador>)dcs.ReadObject(fileStream);   //le para a lista.
            }

        }

        //pesquisar user por ID
        public void pesquisaID(int id)
        {
            int index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Utilizador a = listaUtilizadores[index];
            if (respostaPedidoPorID != null)
                respostaPedidoPorID(a);
        }

        public void pesquisaUserParaRegisto(int id)
        {
            int index = listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Utilizador a = listaUtilizadores[index];

            if (respostaPedidoUser != null)
                respostaPedidoUser(a);
        }

        public void pesquisaRegistosParaUser(int id)
        {
            lastID = id;
            Program.v_main.gridView_registos_User.Items.Clear();

            foreach(Registo r in listaRegistos)
            {
                if (r.Id_utilizador == id && r.Estado == 0)
                    Program.v_main.gridView_registos_User.Items.Add(r);
            }
        }

    }
}