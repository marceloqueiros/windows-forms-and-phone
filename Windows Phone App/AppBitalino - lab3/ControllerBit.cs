using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Globalization;//formato da data CultureInfo.InvariantCulture;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;
using System.Diagnostics;

namespace AppBitalino___lab3
{
    public class ControllerBit
    {
        Stopwatch timer = new Stopwatch();
        int EndTime;
        public bool readContinue;
        public DateTime fim = new DateTime();
        public DateTime inicio = new DateTime();
        public List<FrameRegisto> temp = new List<FrameRegisto>();
        Bitalino dev;

        App Program = App.Current as App;

        public ControllerBit()
        {
            Program.v_main.carregaLista += V_main_carregaLista;
            Program.v_main.carregaLista_registos += V_main_carregaLista_registos;
            Program.v_main.atualizaLista += V_main_atualizaLista;
            Program.v_main.pedidoID += V_main_pedidoID;
            Program.v_main.pedidoIdade += V_main_pedidoIdade;
            Program.v_main.pedidoGuardarUser += V_main_pedidoGuardarUser; ;
            Program.v_main.pedidoEditarUser += V_main_pedidoEditarUser;
            Program.v_main.pedidoIdadeEditar += V_main_pedidoIdadeEditar;
            Program.v_main.pedidoIdadeVisualizar += V_main_pedidoIdadeVisualizar;
            Program.v_main.pedidoApagarUser += V_main_pedidoApagarUser;
            Program.v_main.PedidoPesquisarID += V_main_PedidoPesquisarID;
            Program.v_main.PedidoIDRegisto += V_main_PedidoIDRegisto;
            Program.v_main.PedidoGuardarRegisto += V_main_PedidoGuardarRegisto;
            Program.v_main.PedidoConetarBitalino += V_main_PedidoConetarBitalino;
            Program.v_main.PedidoDadosBit += V_main_PedidoDadosBit;
            Program.v_main.pedidoNomeUser += V_main_pedidoNomeUser;
            Program.v_main.pedidoUserParaRegisto += V_main_pedidoUserParaRegisto;
            Program.v_main.pedidoRegistosUser += V_main_pedidoRegistosUser;
            Program.v_main.pedidoApagarRegisto += V_main_pedidoApagarRegisto;
            Program.v_main.atualizaLista_registos += V_main_atualizaLista_registos;
            Program.v_main.pedidoOrdenarLista += V_main_pedidoOrdenarLista;
            Program.v_main.pedidoResetOrdem += V_main_pedidoResetOrdem;
            Program.v_main.pedidoFiltragem += V_main_pedidoFiltragem;
            Program.v_main.pedidoFiltragem2 += V_main_pedidoFiltragem2;
        }

        private void V_main_pedidoFiltragem2(int index, string filtro)
        {
            Program.model.filtrarLista2(index, filtro);
        }

        private void V_main_pedidoFiltragem(int index, string filtro)
        {
            Program.model.filtrarLista(index, filtro);
        }

        private void V_main_pedidoResetOrdem()
        {
            Program.model.resetOrdem();
        }

        private void V_main_pedidoOrdenarLista(int a)
        {
            Program.model.OrdenarLista(a);
        }

        private void V_main_atualizaLista_registos()
        {
            Program.model.AtualizaDataRegistosMetodo();
        }

        private void V_main_pedidoApagarRegisto(int a)
        {
            Program.model.Delete_registo(a);
        }

        private void V_main_pedidoRegistosUser(int a)
        {
            Program.model.pesquisaRegistosParaUser(a);
        }

        private void V_main_pedidoUserParaRegisto(int a)
        {
            Program.model.pesquisaUserParaRegisto(a);
        }

        private void V_main_pedidoNomeUser(int a)
        {
            Program.model.nomeUser(a);
        }

        private void V_main_PedidoConetarBitalino(string s)
        {
            //dev = new Bitalino(s);
        }

        private void V_main_PedidoGuardarRegisto(string idRegisto, string idUtilizador, string tiposE, string frames)
        {
            Program.model.guardarRegisto(idRegisto, idUtilizador, tiposE, inicio, fim, temp, frames);
        }


        private void V_main_PedidoDadosBit(int duracao)
        {
            temp.Clear();
            inicio = DateTime.Now;
            timer.Start();
            EndTime = duracao;
            Task readThread = new Task(Ler); //Thread(Ler);
            readContinue = true;
            readThread.Start();
            if (!readContinue)
            {
                fim = DateTime.Now;
                timer.Stop();
                timer.Reset();
            }
        }

        private async void Ler()
        {


            //dev.start(1000, new int[] { 0, 1, 2, 3, 4, 5 });   // start acquisition of all channels at 1000 Hz
            //bool ledState = false;

            //Bitalino.Frame[] frames = new Bitalino.Frame[Convert.ToInt32(Program.v_main.comboBox_Frame.SelectedItem)];
            //for (int i = 0; i < frames.Length; i++)
            //{
            //    frames[i] = new Bitalino.Frame();   // must initialize all elements in the array
            //}

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///OUTPUTS.txt"));
            StreamReader sr = new StreamReader(await file.OpenStreamForReadAsync());

            string str;

            do
            {
                //ledState = !ledState;   // toggle LED state
                //dev.trigger(new bool[] { false, false, ledState, false });

                //dev.read(frames); // get the number of frames selected from device
                //Bitalino.Frame f = frames[0];  // get a reference to the first frame of each 100 frames block
                //                               //Console.WriteLine("{0} : {1} {2} {3} {4} ; {5} {6} {7} {8} {9} {10}",   // dump the first frame
                //                               //                  f.seq,
                //                               //                  f.digital[0], f.digital[1], f.digital[2], f.digital[3],
                //                               //                  f.analog[0], f.analog[1], f.analog[2], f.analog[3], f.analog[4], f.analog[5]);


                ////passar para a ListBoxLogs e para o nosso frame
                //foreach (Bitalino.Frame frame in frames)
                //{
                //    string[] s = frame.ToString().Split(';');
                //    FrameRegisto fra = new FrameRegisto();
                //    fra.resultado0 = Convert.ToInt32(s[0]);
                //    fra.resultado1 = Convert.ToInt32(s[1]);
                //    fra.resultado2 = Convert.ToInt32(s[2]);
                //    fra.resultado3 = Convert.ToInt32(s[3]);
                //    fra.resultado4 = Convert.ToInt32(s[4]);
                //    fra.resultado5 = Convert.ToInt32(s[5]);
                //    temp.Add(fra);
                //    await Program.v_main.listBox_logs.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => Program.v_main.atualizaLOGS("time: " + timer.ElapsedMilliseconds + " - " + frame.ToString()));
                //}

                //_-------temp 
                str = sr.ReadLine();


                //Apresenta dados nos LOGS
                //listBox.Invoke(new Action(() => listBox.Items.Add("time: " + timer.ElapsedMilliseconds + " - " + str.ToString())));
                await Program.v_main.listBox_logs.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => Program.v_main.atualizaLOGS("time: " + timer.ElapsedMilliseconds + " - " + str.ToString()));

                string[] st = str.Split(';');
                FrameRegisto fr = new FrameRegisto();
                fr.resultado0 = Convert.ToInt32(st[0]);
                fr.resultado1 = Convert.ToInt32(st[1]);
                fr.resultado2 = Convert.ToInt32(st[2]);
                fr.resultado3 = Convert.ToInt32(st[3]);
                fr.resultado4 = Convert.ToInt32(st[4]);
                fr.resultado5 = Convert.ToInt32(st[5]);

                Task.Delay(50).Wait(); //System.Threading.Tasks.Task.Delay(50).Wait(); //Thread.Sleep(50);
                temp.Add(fr);
                //-----
                if (timer.ElapsedMilliseconds > EndTime * 1000) readContinue = false;
                //falta por duração limite
            } while (readContinue);
            //PÁRA QUANDO PRESSIONAR STOP OU A DURAÇÃO ATINGIR O LIMITE (SE O UTILIZADOR POS DURAÇÃO)
            fim = DateTime.Now;
            timer.Stop();
            //duracaoReal = timer.ElapsedMilliseconds;
            timer.Reset();


            //dev.stop(); // stop acquisition
            //dev.Dispose(); // disconnect from device

        }

        private void V_main_PedidoIDRegisto()
        {
            Program.model.id_newRegistos();
        }

        private void V_main_PedidoPesquisarID(int ID)
        {
            try
            {
                Program.model.pesquisaID(ID);
            }
            catch(ArgumentOutOfRangeException)
            {
                Program.v_main.respostaPedidoID();
            }
        }

        private void V_main_pedidoIdadeVisualizar(DateTime x)
        {
            Program.model.calcula_idadeVisualizar(x);
        }

        private void V_main_pedidoGuardarUser(string id, string nome, DateTime D, string genero,string altura, string peso, BitmapImage path)
        {
            Program.model.GuardarUtilizador(id, nome, D, genero, altura, peso, path);
        }

        private void V_main_pedidoApagarUser(int a)
        {
            Program.model.Delete_user(a);
        }

        private void V_main_pedidoIdadeEditar(DateTime x)
        {
            Program.model.calcula_idadeEditar(x);
        }

        private void V_main_pedidoEditarUser(string x1, string x2, DateTime D, string x3, string x4, string x5, string x6)
        {
            Program.model.EditarUtilizador(x1, x2, D, x3, x4, x5, x6);
        }

        private void V_main_atualizaLista()
        {
            Program.model.AtualizarDataUtilizadoresMetodo();
        }

        private void V_main_pedidoIdade(DateTime x)
        {
            Program.model.calcula_idade(x);
        }

        private void V_main_pedidoID()
        {
            Program.model.id_new();
        }

        private async void V_main_carregaLista()
        {
            try
            {
                using (Stream fileStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("DadosUtilizadores.xml"))//abre
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(List<Utilizador>));
                    Program.model.listaUtilizadores = (List<Utilizador>)dcs.ReadObject(fileStream);   //le para a lista.
                }
                //StorageFolder pasta = Windows.Storage.ApplicationData.Current.LocalFolder;

                // Stream streamFicheiro = await pasta.OpenStreamForReadAsync("DadosUtilizadores.txt");

                // Utilizador novo;
                // using (StreamReader sr = new StreamReader(streamFicheiro))
                // {
                //     for (int i = 0; !sr.EndOfStream; i++)
                //     {
                //         string[] separar = sr.ReadLine().Split(';');

                //         string[] datan = separar[2].Split('/');
                //         DateTime data = new DateTime(Convert.ToInt32(datan[2]), Convert.ToInt32(datan[1]), Convert.ToInt32(datan[0]));

                //         novo = new Utilizador(Convert.ToInt32(separar[0]), separar[1], data, separar[3], Convert.ToInt32(separar[4]), Convert.ToInt32(separar[5]), separar[6], Convert.ToInt32(separar[7]));
                //         novo.Idade = Program.model.calcular_idade_Retornar(data);
                //         Program.model.AdicionarNovoUtilizadorLista(novo);
                //     }
                // }
            }
            catch(FileNotFoundException)
            {

            }

            Program.model.AtualizarDataUtilizadoresMetodo();
        }

        private async void V_main_carregaLista_registos()
        {
            try
            {
                using (Stream fileStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("DadosRegistos.xml"))//abre
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(List<Registo>));
                    Program.model.listaRegistos = (List<Registo>)dcs.ReadObject(fileStream);   //le para a lista.
                }

                StorageFolder pasta = Windows.Storage.ApplicationData.Current.LocalFolder;
                List<FrameRegisto> fr = new List<FrameRegisto>();

                foreach (Registo r in Program.model.listaRegistos)
                {
                    Stream streamFicheiro = await pasta.OpenStreamForReadAsync(r.Id_utilizador+".txt");
                    StreamReader leitor = new StreamReader(streamFicheiro);
                    fr.Clear();
                    while (!leitor.EndOfStream)
                    {
                        string[] st = leitor.ReadLine().Split(';');
                        FrameRegisto frame = new FrameRegisto();
                        frame.resultado0 = Convert.ToInt32(st[0]);
                        frame.resultado1 = Convert.ToInt32(st[1]);
                        frame.resultado2 = Convert.ToInt32(st[2]);
                        frame.resultado3 = Convert.ToInt32(st[3]);
                        frame.resultado4 = Convert.ToInt32(st[4]);
                        frame.resultado5 = Convert.ToInt32(st[5]);
                        fr.Add(frame);
                    }
                    r.Resultados = fr;
                }

                //StorageFolder pasta = Windows.Storage.ApplicationData.Current.LocalFolder;

                //Stream streamFicheiro = await pasta.OpenStreamForReadAsync("DadosRegistos.txt");

                //Registo novo;
                //List<FrameRegisto> fr;
                //using (StreamReader sr = new StreamReader(streamFicheiro))
                //{
                //    for (int i = 0; !sr.EndOfStream; i++)
                //    {
                //        fr = new List<FrameRegisto>();
                //        string[] separar = sr.ReadLine().Split(';');

                //        //string[] dataI = separar[3].Split('/');
                //        //string[] dataF = separar[4].Split('/');
                //        //DateTime dataInicio = new DateTime(Convert.ToDateTime(separar[3]));
                //        //DateTime dataFim = new DateTime(Convert.ToInt32(dataF[2]), Convert.ToInt32(dataF[1]), Convert.ToInt32(dataF[0]));

                //        Stream streamFicheiroR = await pasta.OpenStreamForReadAsync(separar[0]+".txt");
                //        StreamReader leitor = new StreamReader(streamFicheiroR);
                //        while (!leitor.EndOfStream)
                //        {
                //            string[] st = leitor.ReadLine().Split(';');
                //            FrameRegisto frame = new FrameRegisto();
                //            frame.resultado0 = Convert.ToInt32(st[0]);
                //            frame.resultado1 = Convert.ToInt32(st[1]);
                //            frame.resultado2 = Convert.ToInt32(st[2]);
                //            frame.resultado3 = Convert.ToInt32(st[3]);
                //            frame.resultado4 = Convert.ToInt32(st[4]);
                //            frame.resultado5 = Convert.ToInt32(st[5]);
                //            fr.Add(frame);
                //        }

                //        novo = new Registo(Convert.ToInt32(separar[0]), Convert.ToInt32(separar[1]), separar[2], Convert.ToDateTime(separar[3], CultureInfo.InvariantCulture), Convert.ToDateTime(separar[4], CultureInfo.InvariantCulture), fr, separar[5], Convert.ToInt32(separar[6]));
                //        Program.model.AdicionarNovoRegistoLista(novo);
                //    }
                //}
            }
            catch (FileNotFoundException)
            {

            }

            Program.model.AtualizaDataRegistosMetodo();
        }

        //public void conectBITalino(string mac)
        //{
        //    // uncomment this block to search for Bluetooth devices
        //    /*
        //    Bitalino.DevInfo[] devs = Bitalino.find();   
        //    foreach (Bitalino.DevInfo d in devs)
        //       Console.WriteLine("{0} - {1}", d.macAddr, d.name);
        //    return;
        //    */

        //    //Console.WriteLine("Connecting to device...");

        //    Bitalino dev = new Bitalino(mac);  // device MAC address
        //                                                       //Bitalino dev = new Bitalino("COM7");  // Bluetooth virtual COM port or USB-UART COM port

        //    //Console.WriteLine("Connected to device. Press Enter to exit.");

        //    string ver = dev.version();    // get device version string
        //    //Console.WriteLine("BITalino version: {0}", ver);

        //    dev.battery(10);  // set battery threshold (optional)

        //    dev.start(1000, new int[] { 0, 1, 2, 3, 4, 5 });   // start acquisition of all channels at 1000 Hz

        //    bool ledState = false;

        //    Bitalino.Frame[] frames = new Bitalino.Frame[100];
        //    for (int i = 0; i < frames.Length; i++)
        //        frames[i] = new Bitalino.Frame();   // must initialize all elements in the array

        //    do
        //    {
        //        ledState = !ledState;   // toggle LED state
        //        dev.trigger(new bool[] { false, false, ledState, false });

        //        dev.read(frames); // get 100 frames from device
        //        Bitalino.Frame f = frames[0];  // get a reference to the first frame of each 100 frames block
        //        /*Console.WriteLine("{0} : {1} {2} {3} {4} ; {5} {6} {7} {8} {9} {10}",   // dump the first frame
        //                          f.seq,
        //                          f.digital[0], f.digital[1], f.digital[2], f.digital[3],
        //                          f.analog[0], f.analog[1], f.analog[2], f.analog[3], f.analog[4], f.analog[5]);*/

        //    } while (true);//!Console.KeyAvailable); // until a key is pressed

        //    dev.stop(); // stop acquisition

        //    dev.Dispose(); // disconnect from device
        //}

    }
}
