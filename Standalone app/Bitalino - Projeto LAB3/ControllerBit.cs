using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bitalino___Projeto_LAB3
{
    class ControllerBit
    {
        public Thread readThread { get; set; }
        public long duração { get; set; }
        public long duraçãoReal { set; get; }
        public DateTime fim { get; set; }
        public DateTime inicio { get; set; }
        public Bitalino dev { get; set; }
        

        public ControllerBit()
        {
            fim = new DateTime();
            inicio = new DateTime();
            Program.V_CriarUtilizador.criar_user += V_CriarUtilizador_criar_user; 
            Program.V_CriarUtilizador.calcular_idade += V_CriarUtilizador_calcular_idade; 
            Program.V_CriarUtilizador.gerar_id += V_CriarUtilizador_gerar_id; 
            Program.V_EditarUtilizador.editar_user += V_EditarUtilizador_editar_user;
            Program.V_ListarUtilizadores.procuraUser += V_ListarUtilizadores_procuraUser;
            Program.V_ListarUtilizadores.apagaUser += V_ListarUtilizadores_apagaUser;
            Program.V_VisualizarUtilizador.mostraEditar += V_VisualizarUtilizador_mostraEditar;
            Program.V_VisualizarUtilizador.apagaUser += V_VisualizarUtilizador_apagaUser;
            Program.V_VisualizarUtilizador.novoRegisto += V_VisualizarUtilizador_novoRegisto;
            Program.V_EditarUtilizador.calcular_idade += V_EditarUtilizador_calcular_idade;
            Program.V_Menu.PedidoAbrirPerfil += V_Menu_PedidoAbrirPerfil;
            Program.V_Menu.NovoRegisto += V_Menu_NovoRegisto;
            Program.V_ListarUtilizadores.PedidoHome += home;
            Program.V_EditarUtilizador.PedidoHome += home;
            Program.V_VisualizarUtilizador.PedidoHome += home;
            Program.V_CriarRegisto.PedidoHome += home;
            Program.V_CriarUtilizador.PedidoHome += home;
            Program.V_Menu.PedidoProcurarUtilizadores += V_Menu_PedidoProcurarUtilizadores;
            Program.V_Menu.AbrirListagemUtilizadores += V_Menu_AbrirListagemUtilizadores;
            Program.V_Menu.NovoUser += V_Menu_NovoUser;
            Program.V_Menu.PedidoAbrirUltimoUser += V_Menu_PedidoAbrirUltimoUser;
            Program.V_Menu.PedidoAbrirUltimoRegisto += V_Menu_PedidoAbrirUltimoRegisto;
            Program.V_ListarUtilizadores.PedidoAddUser += V_Menu_NovoUser;
            Program.V_ListarUtilizadores.PedidoAbrirPerfil += V_ListarUtilizadores_PedidoAbrirPerfil;
            Program.V_Menu.PedidoAbrirListagemRegistos += V_Menu_PedidoAbrirListagemRegistos;
            Program.V_ListarRegistos.NovoRegisto += V_ListarRegistos_NovoRegisto;
            Program.V_ListarRegistos.PedidoHome += home;
            Program.V_CriarRegisto.gerarId += V_CriarRegisto_gerarId;
            Program.V_CriarRegisto.PedidoProcurarUtilizadores += V_CriarRegisto_PedidoProcurarUtilizadores;
            Program.V_CriarRegisto.criarRegisto += V_CriarRegisto_criarRegisto;
            Program.V_ListarRegistos.apagarRegisto += V_ListarRegistos_apagarRegisto;
            Program.V_ListarRegistos.visualizarRegisto += V_ListarRegistos_visualizarRegisto;
            Program.V_VisualizarUtilizador.apagarRegisto += V_ListarRegistos_apagarRegisto;
            Program.V_VisualizarUtilizador.visualizarRegisto += V_ListarRegistos_visualizarRegisto;
            Program.V_VisualizarRegisto.apagarRegisto += V_ListarRegistos_apagarRegisto;
            Program.V_Menu.PedidoAbrirRegisto += V_ListarRegistos_visualizarRegisto;
            Program.V_Menu.fechar += V_Menu_fechar;
            LerFicheiroUtilizadores();
            LerFicheiroRegistos();
        }

        private void V_Menu_fechar()
        {
            Program.Model.fechar();
        }

        public void Ler()
        {
            try
            {
                dev.start(1000, new int[] { 0, 1, 2, 3, 4, 5 });   // start acquisition of all channels at 1000 Hz
                bool ledState = false;
                Bitalino.Frame[] frames = new Bitalino.Frame[Program.V_CriarRegisto.nbFrames];
                for (int i = 0; i < frames.Length; i++)
                {
                    frames[i] = new Bitalino.Frame();   // must initialize all elements in the array
                }
                //StreamReader sr = new StreamReader("OUTPUTS.txt");
                //    string str = "";
                do
                {
                    ledState = !ledState;   // toggle LED state
                    dev.trigger(new bool[] { false, false, ledState, false });

                    dev.read(frames); // get 100 frames from device
                    Bitalino.Frame f = frames[0];  // get a reference to the first frame of each 100 frames block
                    //Console.WriteLine("{0} : {1} {2} {3} {4} ; {5} {6} {7} {8} {9} {10}",   // dump the first frame
                    //                  f.seq,
                    //                  f.digital[0], f.digital[1], f.digital[2], f.digital[3],
                    //                  f.analog[0], f.analog[1], f.analog[2], f.analog[3], f.analog[4], f.analog[5]);


                    //passar para a ListBoxLogs e para o nosso frame
                    foreach (Bitalino.Frame frame in frames)
                    {

                        //string[] st = frame.ToString().Split(';');
                        FrameRegisto fr = new FrameRegisto();

                        fr.resultado0 = f.analog[0];
                        fr.resultado1 = f.analog[1];
                        fr.resultado2 = f.analog[2];
                        fr.resultado3 = f.analog[3];
                        fr.resultado4 = f.analog[4];
                        fr.resultado5 = f.analog[5];
                        Program.V_CriarRegisto.temp.Add(fr);
                        string c = "";

                        if (Program.V_CriarRegisto.CAAC) {
                            if (Program.V_CriarRegisto.aac == "0") { c += "AAC: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.aac == "1") { c += "AAC: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.aac == "2") { c += "AAC: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.aac == "3") { c += "AAC: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.aac == "4") { c += "AAC: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.aac == "5") { c += "AAC: " + fr.resultado5.ToString() + "  "; }
                        }
                        if (Program.V_CriarRegisto.CBATT)
                        {
                            if (Program.V_CriarRegisto.batt == "0") { c += "BATT: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.batt == "1") { c += "BATT: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.batt == "2") { c += "BATT: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.batt == "3") { c += "BATT: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.batt == "4") { c += "BATT: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.batt == "5") { c += "BATT: " + fr.resultado5.ToString() + "  "; }
                        }
                        if (Program.V_CriarRegisto.CECG)
                        {
                            if (Program.V_CriarRegisto.ecg == "0") { c += "ECG: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.ecg == "1") { c += "ECG: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.ecg == "2") { c += "ECG: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.ecg == "3") { c += "ECG: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.ecg == "4") { c += "ECG: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.ecg == "5") { c += "ECG: " + fr.resultado5.ToString() + "  "; }
                        }
                        if (Program.V_CriarRegisto.CEMG)
                        {
                            if (Program.V_CriarRegisto.emg == "0") { c += "EMG: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.emg == "1") { c += "EMG: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.emg == "2") { c += "EMG: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.emg == "3") { c += "EMG: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.emg == "4") { c += "EMG: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.emg == "5") { c += "EMG: " + fr.resultado5.ToString() + "  "; }
                        }
                        if (Program.V_CriarRegisto.CLUX)
                        {
                            if (Program.V_CriarRegisto.lux == "0") { c += "LUX: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.lux == "1") { c += "LUX: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.lux == "2") { c += "LUX: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.lux == "3") { c += "LUX: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.lux == "4") { c += "LUX: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.lux == "5") { c += "LUX: " + fr.resultado5.ToString() + "  "; }
                        }
                        if (Program.V_CriarRegisto.CEDA)
                        {
                            if (Program.V_CriarRegisto.eda == "0") { c += "EDA: " + fr.resultado0.ToString() + "  "; }
                            if (Program.V_CriarRegisto.eda == "1") { c += "EDA: " + fr.resultado1.ToString() + "  "; }
                            if (Program.V_CriarRegisto.eda == "2") { c += "EDA: " + fr.resultado2.ToString() + "  "; }
                            if (Program.V_CriarRegisto.eda == "3") { c += "EDA: " + fr.resultado3.ToString() + "  "; }
                            if (Program.V_CriarRegisto.eda == "4") { c += "EDA: " + fr.resultado4.ToString() + "  "; }
                            if (Program.V_CriarRegisto.eda == "5") { c += "EDA: " + fr.resultado5.ToString() + "  "; }
                        }
                        Program.V_CriarRegisto.mostraLog(c);
                    }

                    //_-------temp 
                    //str = sr.ReadLine();



                    //listBoxLogs.Invoke(new Action(() => listBoxLogs.Items.Add("time: " + timer.ElapsedMilliseconds + " - " + str.ToString())));



                    //string[] st = str.Split(';');
                    //if (st[0] != "") { 
                    //FrameRegisto fr = new FrameRegisto();
                    //fr.resultado0 = Convert.ToInt32(st[0]);
                    //fr.resultado1 = Convert.ToInt32(st[1]);
                    //fr.resultado2 = Convert.ToInt32(st[2]);
                    //fr.resultado3 = Convert.ToInt32(st[3]);
                    //fr.resultado4 = Convert.ToInt32(st[4]);
                    //fr.resultado5 = Convert.ToInt32(st[5]);
                    //Thread.Sleep(50);
                    //temp.Add(fr);
                    //}    

                    //-----
                    if (Program.V_CriarRegisto.timer.ElapsedMilliseconds > duração) Program.V_CriarRegisto.readContinue = false;

                //falta por duração limite
            } while (Program.V_CriarRegisto.readContinue);

                Program.V_CriarRegisto.readContinue = false;
            Program.V_CriarRegisto.BUTstart();

            fim = DateTime.Now;
            Program.V_CriarRegisto.timer.Stop();
            duraçãoReal = Program.V_CriarRegisto.timer.ElapsedMilliseconds;
                Program.V_CriarRegisto.timer.Reset();
            Program.V_CriarRegisto.guardarTh();
                // PÁRA QUANDO PRESSIONAR STOP OU A DURAÇÃO ATINGIR O LIMITE (SE O UTILIZADOR POS DURAÇÃO)

                dev.stop(); // stop acquisition
                dev.Dispose(); // disconnect from device
            }
            catch (Bitalino.Exception ex)
            {
                Program.V_CriarRegisto.ERRO(ex);
                Program.V_CriarRegisto.readContinue = false;
            }
            readThread.Abort();
        }

        private void V_ListarRegistos_visualizarRegisto(int id)
        {
            int index = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == id);
            Program.V_VisualizarRegisto.VisualizarRegisto(index);
        }

        private void V_ListarRegistos_apagarRegisto(int id)
        {
            Program.Model.Delete_Registo(id);
        }

        private void V_CriarRegisto_criarRegisto(int idRegisto, int idUtilizador, string tiposE, DateTime inicio, DateTime fim, List<FrameRegisto> f, long d, string fra, bool a, bool b, bool c, bool dd, bool e, bool ff, string vT)
        {
            Program.Model.guardarRegisto(idRegisto, idUtilizador, tiposE, inicio, fim, f, d, fra, a, b, c, dd, e, ff, vT);
        }

        private void V_CriarRegisto_PedidoProcurarUtilizadores(string s1, string s2)
        {
            Program.Model.ProcurarValorPorTipoViewListagem(s1,s2);
        }

        private void V_CriarRegisto_gerarId()
        {
            Program.Model.id_newRegistos();
        }

        private void V_ListarRegistos_NovoRegisto()
        {
            Program.V_CriarRegisto.ShowDialog();
        }

        private void V_Menu_PedidoAbrirListagemRegistos()
        {
            Program.V_ListarRegistos.ShowDialog();
        }

        private void V_ListarUtilizadores_PedidoAbrirPerfil(int id)
        {
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Program.V_VisualizarUtilizador.Visualizar(index);
        }

        private void V_Menu_PedidoAbrirUltimoRegisto()
        {
            if (Program.Model.listaRegistos.Count > 0)
                Program.V_VisualizarRegisto.VisualizarRegisto(Program.Model.listaRegistos.FindLast(
                    delegate (Registo R)
                    {
                        int estado = 0;
                        return R.Estado == estado;
                    }));
        }

        private void V_Menu_PedidoAbrirUltimoUser()
        {
            if (Program.Model.listaUtilizadores.Count > 0)
                Program.V_VisualizarUtilizador.VisualizarUtilizador(Program.Model.listaUtilizadores.FindLast(
                    delegate (Utilizador U)
                    {
                        int estado = 0;
                        return U.Estado == estado;
                    }));
        }

        private void V_ListarUtilizadores_PedidoProcurarUtilizadores(string tipo, string valor)
        {
            Program.Model.ProcurarValorPorTipoViewListagem(tipo, valor);
        }

        private void V_Menu_NovoUser()
        {
            Program.V_CriarUtilizador.novoUserVazio();
        }

        private void V_Menu_AbrirListagemUtilizadores()
        {
            Program.V_ListarUtilizadores.Model_AtualizarDataUtilizadores();
            Program.V_ListarUtilizadores.ShowDialog();
        }

        private void V_Menu_PedidoProcurarUtilizadores(string tipo, string valor)
        {
            Program.Model.ProcurarValorPorTipo(tipo, valor);
        }
        private void V_Menu_NovoRegisto()
        {
            Program.V_CriarRegisto.novoRegistoVazio();
        }

        private void V_VisualizarUtilizador_novoRegisto(int id)
        {
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Program.V_CriarRegisto.novoRegisto(index);
        }

        

        private void home()
        {
            Program.V_CriarRegisto.Visible = false;
            Program.V_CriarUtilizador.Visible = false;
            Program.V_EditarUtilizador.Visible = false;
            Program.V_ListarRegistos.Visible = false;
            Program.V_ListarUtilizadores.Visible = false;
            Program.V_VisualizarRegisto.Visible = false;
            Program.V_VisualizarUtilizador.Visible = false;
        }

        private void V_Menu_PedidoAbrirPerfil(string tipo, string valor)
        {
            Program.V_ListarUtilizadores.ListaFiltrada(tipo, valor);
        }

        private void V_VisualizarUtilizador_apagaUser(int a)
        {
            Program.Model.Delete_user(a);
        }

        private void V_VisualizarUtilizador_mostraEditar(int id)
        {
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            Program.V_EditarUtilizador.VisualizarEdição(index);
        }

        private void V_ListarUtilizadores_apagaUser(int id)
        {
            Program.Model.Delete_user(id);
            Program.Model.AtualizarDataRegistosMetodo();
        }

        private void V_EditarUtilizador_calcular_idade(DateTime x)
        {
            Program.Model.calcula_idade(x);
        }

        private void V_ListarUtilizadores_procuraUser(int id, int index)
        {
            int ind = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == id);
            if (index == 7) Program.V_VisualizarUtilizador.Visualizar(ind);
            if (index == 8) Program.V_EditarUtilizador.VisualizarEdição(ind);
        }

        private void V_EditarUtilizador_editar_user(string x1, string x2, DateTime D, string x3, string x4, string x5, string x6)
        {
            Program.Model.EditarUtilizador(x1, x2, D, x3, x4, x5, x6);
            Program.Model.AtualizarDataRegistosMetodo();
        }

        public void LerFicheiroUtilizadores()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Utilizador>));
            if (System.IO.File.Exists("listaUtilizadores.xml"))
            {
                using (FileStream stream = File.OpenRead("listaUtilizadores.xml"))
                {

                    Program.Model.listaUtilizadores = (List<Utilizador>)serializer.Deserialize(stream);
                }
            }
            Program.Model.AtualizarDataUtilizadoresMetodo();
        }
        

        public void LerFicheiroRegistos()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Registo>));
            if (System.IO.File.Exists("listaRegistos.xml"))
            {
                using (FileStream stream = File.OpenRead("listaRegistos.xml"))
                {
                    Program.Model.listaRegistos = (List<Registo>)serializer.Deserialize(stream);
                }
            }
            Program.Model.AtualizarDataRegistosMetodo();
        }

        //y
        private void V_CriarUtilizador_criar_user(string x1, string x2, DateTime D, string x3, string x4, string x5, string x6)
        {
            Program.Model.GuardarUtilizador(x1, x2, D, x3, x4, x5, x6);
        }

        //y
        private void V_CriarUtilizador_gerar_id()
        {
            Program.Model.id_new();
        }
        //y
        private void V_CriarUtilizador_calcular_idade(DateTime x)
        {
            Program.Model.calcula_idade(x);
        }

    }
}
