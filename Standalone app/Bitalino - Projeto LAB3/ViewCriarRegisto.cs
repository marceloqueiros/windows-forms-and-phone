using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bitalino___Projeto_LAB3
{
    public partial class ViewCriarRegisto : Form
    {
        static int[] analogChannels;
        string exames = "";
        int minimo; int maximo;
        public int nbFrames { get; set; }
        public bool readContinue { set; get; }
        public event MetodosSemParametros PedidoHome;
        public event MetodosSemParametros gerarId;
        public event MetodosCom2Strings PedidoProcurarUtilizadores;
        public event MetodoCriarRegisto criarRegisto;
        public bool CECG { set; get; }
        public bool CAAC { set; get; }
        public bool CBATT { set; get; }
        public bool CEDA { set; get; }
        public bool CEMG { set; get; }
        public bool CLUX { set; get; }
        public string ecg { get; set; }
        public string aac { get; set; }
        public string emg { get; set; }
        public string eda { get; set; }
        public string batt { get; set; }
        public string lux { get; set; }
        public Stopwatch timer { get; set; }
        public List<FrameRegisto> temp { get; set; }
        private Thread addDataRunnerecg;
        private Thread addDataRunnerbatt;
        private Thread addDataRunneremg;
        private Thread addDataRunnereda;
        private Thread addDataRunneraac;
        private Thread addDataRunnerlux;
        public delegate void AddDataDelegate();
        public AddDataDelegate addDataDelecg;
        public AddDataDelegate addDataDelbatt;
        public AddDataDelegate addDataDelemg;
        public AddDataDelegate addDataDeleda;
        public AddDataDelegate addDataDelaac;
        public AddDataDelegate addDataDellux;
        public ViewCriarRegisto()
        {
            InitializeComponent();
            Program.Model.respostaGerarIDRegistos += Model_respostaGerarIDRegistos;
            Program.Model.respostaProcurarUserParaRegisto += Model_respostaProcurarUserParaRegisto;
            Program.Model.respostaCriarRegisto += Model_respostaCriarRegisto;
            temp = new List<FrameRegisto>();
            timer = new Stopwatch();
        }

        private void Model_respostaCriarRegisto()
        {
            MessageBox.Show("Registo guardado com sucesso!");
        }
        
        private void Model_respostaProcurarUserParaRegisto(string cor, string texto)
        {
            switch (cor)
            {
                case "red":
                    labelProcura.ForeColor = Color.Red;
                    break;
                case "green":
                    labelProcura.ForeColor = Color.Green;
                    break;
                case "black":
                    labelProcura.ForeColor = Color.Black;
                    break;
            }
            labelProcura.Text = texto;
        }
        public void guardarTh()
        {
            if (labelProcura.Text == "Encontrado!")
            {
                DialogResult dr2 = MessageBox.Show("Exame(s) realizado(s) com sucesso! Quer guardar já os dados?", "", MessageBoxButtons.YesNo);
                if (dr2 == DialogResult.Yes)
                    Invoke(new Action(() => guardar()));
            }
            else
            {
                MessageBox.Show("Exame(s) realizado(s) com sucesso!");
            }
        }
        public void ERRO(Bitalino.Exception ex)
        {
            MessageBox.Show("Erro ao ler exame(s): " + ex.Message);
        }
        public void BUTstart()
        {
            buttonStart.Invoke(new Action(() => buttonStart.Text = "START"));
        }
        private void Model_respostaGerarIDRegistos(int id)
        {
            textBoxIDRegisto.Text = id.ToString();
        }

        public void novoRegisto(int index)
        {
            Utilizador a = Program.Model.listaUtilizadores[index];

            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = a.Nome;

            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + a.Fotografia;
            ShowDialog();
        }

        public void novoRegistoVazio()
        {
            textBoxID.Text = "";
            textBoxNome.Text = "";
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\imageProfile.png";
            ShowDialog();
        }

        private void ViewCriarRegisto_Load(object sender, EventArgs e)
        {
            //this.tabPage1.Parent = null; // hide    
            //this.tabPage1.Parent = this.tabControl1; //show
            this.tabPage0.Parent = null; // hide    
            this.tabPage1.Parent = null; // hide    
            this.tabPage2.Parent = null; // hide    
            this.tabPage3.Parent = null; // hide    
            this.tabPage4.Parent = null; // hide    
            this.tabPage5.Parent = null; // hide    
            string[] ports = SerialPort.GetPortNames();

            for(int i=0;i<ports.Count();i++)
                comboBoxConectar.Items.Add(ports[i]);

            if (gerarId != null)
                gerarId();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (PedidoHome != null)
                PedidoHome();
        }

        //falta por em mvc o start!
        private void button2_Click(object sender, EventArgs e)//START
        {
            if (buttonStart.Text == "START")
            {
                int a = Convert.ToInt32(comboBoxPAAC.Text), b = Convert.ToInt32(comboBoxPBATT.Text), c = Convert.ToInt32(comboBoxPECG.Text), d = Convert.ToInt32(comboBoxPEDA.Text), e2 = Convert.ToInt32(comboBoxPEMG.Text), f = Convert.ToInt32(comboBoxPLUX.Text);
                int flagP = 0;
                int[] v;
                if (a == b || a == c || a == d || a == e2 || a == f) flagP = 1;
                if (b == c || b == d || b == e2 || b == f) flagP = 1;
                if (c == d || c == e2 || c == f) flagP = 1;
                if (d == e2 || d == f) flagP = 1;
                if (e2 == f) flagP = 1;

                if (flagP == 1 || labelConectar.Text != "Conectado" || comboBoxFrames.Text == "" || (checkBoxAAC.Checked == false && checkBoxBATT.Checked == false && checkBoxECG.Checked == false && checkBoxEDA.Checked == false && checkBoxEMG.Checked == false && checkBoxLUX.Checked == false))
                {
                    if (flagP == 1) MessageBox.Show("Canais selecionados não podem ser iguais!");
                    if (checkBoxAAC.Checked == false && checkBoxBATT.Checked == false && checkBoxECG.Checked == false && checkBoxEDA.Checked == false && checkBoxEMG.Checked == false && checkBoxLUX.Checked == false)
                        MessageBox.Show("Selecione tipo(s) de exames.");
                    if (labelConectar.Text != "Conectado")
                        MessageBox.Show("Aparelho desconectado.");
                    if (comboBoxFrames.Text == "")
                        MessageBox.Show("Selecione número de frames por segundo.");
                }
                else
                {
                    if (textBoxDuração.Text == "") Program.Controller.duração = 9999999999999999;
                    else
                        Program.Controller.duração = Convert.ToInt64(textBoxDuração.Text) * 1000;

                    int flag = 1;
                    //....
                    if (listBoxLogs.Items.Count!=1 && listBoxLogs.Items.Count != 0 && labelGuardar.Text != "Guardado")//...
                    {
                        DialogResult dr = MessageBox.Show("Atenção, os dados não foram guardados e vão ser apagados!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Cancel)
                            flag = 0;
                    }
                    //

                    if (flag == 1)
                    {//ler exames---------------------------------------------
                        buttonClear_Click(sender, e);
                        listBoxLogs.Items.Clear(); //limpar
                        buttonStart.Text = "STOP";
                        exames = "";

                        if (checkBoxAAC.Checked) exames += "AAC ";
                        if (checkBoxBATT.Checked) exames += "BATT ";
                        if (checkBoxECG.Checked) exames += "ECG ";
                        if (checkBoxEDA.Checked) exames += "EDA ";
                        if (checkBoxEMG.Checked) exames += "EMG ";
                        if (checkBoxLUX.Checked) exames += "LUX ";
                        nbFrames = Convert.ToInt32(comboBoxFrames.Text);
                        Program.Controller.inicio = DateTime.Now;
                        timer.Start();

                        Program.Controller.readThread = new Thread(Program.Controller.Ler);
                        readContinue = true;
                        Program.Controller.readThread.Start();
                        aac = comboBoxPAAC.Text;
                        batt = comboBoxPBATT.Text;
                        ecg = comboBoxPECG.Text;
                        eda = comboBoxPEDA.Text;
                        emg = comboBoxPEMG.Text;
                        lux = comboBoxPLUX.Text;
                        CAAC = checkBoxAAC.Checked;
                        CEMG = checkBoxEMG.Checked;
                        CEDA = checkBoxEDA.Checked;
                        CLUX = checkBoxAAC.Checked;
                        CBATT = checkBoxBATT.Checked;
                        CECG = checkBoxECG.Checked;
                        if (checkBoxAAC.Checked){
                            if (aac == "0") { tabPage0.Text = "Gráfico AAC"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }
                            if (aac == "1") { tabPage1.Text = "Gráfico AAC"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }
                            if (aac == "2") { tabPage2.Text = "Gráfico AAC"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }
                            if (aac == "3") { tabPage3.Text = "Gráfico AAC"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }
                            if (aac == "4") { tabPage4.Text = "Gráfico AAC"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }
                            if (aac == "5") { tabPage5.Text = "Gráfico AAC"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficoaac(sender, e); }

                        }
                        if (checkBoxBATT.Checked) {
                            if (comboBoxPBATT.Text == "0") { tabPage0.Text = "Gráfico BATT"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                            if (comboBoxPBATT.Text == "1") { tabPage1.Text = "Gráfico BATT"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                            if (comboBoxPBATT.Text == "2") { tabPage2.Text = "Gráfico BATT"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                            if (comboBoxPBATT.Text == "3") { tabPage3.Text = "Gráfico BATT"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                            if (comboBoxPBATT.Text == "4") { tabPage4.Text = "Gráfico BATT"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                            if (comboBoxPBATT.Text == "5") { tabPage5.Text = "Gráfico BATT"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficobatt(sender, e); }
                        }
                        if (checkBoxECG.Checked) {
                            if (comboBoxPECG.Text == "0") { tabPage0.Text = "Gráfico ECG"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                            if (comboBoxPECG.Text == "1") { tabPage1.Text = "Gráfico ECG"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                            if (comboBoxPECG.Text == "2") { tabPage2.Text = "Gráfico ECG"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                            if (comboBoxPECG.Text == "3") { tabPage3.Text = "Gráfico ECG"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                            if (comboBoxPECG.Text == "4") { tabPage4.Text = "Gráfico ECG"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                            if (comboBoxPECG.Text == "5") { tabPage5.Text = "Gráfico ECG"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficoecg(sender, e); }
                        }
                        if (checkBoxEDA.Checked) {
                            if (comboBoxPEDA.Text == "0") { tabPage0.Text = "Gráfico EDA"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                            if (comboBoxPEDA.Text == "1") { tabPage1.Text = "Gráfico EDA"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                            if (comboBoxPEDA.Text == "2") { tabPage2.Text = "Gráfico EDA"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                            if (comboBoxPEDA.Text == "3") { tabPage3.Text = "Gráfico EDA"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                            if (comboBoxPEDA.Text == "4") { tabPage4.Text = "Gráfico EDA"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                            if (comboBoxPEDA.Text == "5") { tabPage5.Text = "Gráfico EDA"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficoeda(sender, e); }
                        }
                        if (checkBoxEMG.Checked)
                        {
                            if (comboBoxPEMG.Text == "0") { tabPage0.Text = "Gráfico EMG"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                            if (comboBoxPEMG.Text == "1") { tabPage1.Text = "Gráfico EMG"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                            if (comboBoxPEMG.Text == "2") { tabPage2.Text = "Gráfico EMG"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                            if (comboBoxPEMG.Text == "3") { tabPage3.Text = "Gráfico EMG"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                            if (comboBoxPEMG.Text == "4") { tabPage4.Text = "Gráfico EMG"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                            if (comboBoxPEMG.Text == "5") { tabPage5.Text = "Gráfico EMG"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficoemg(sender, e); }
                        }
                        if (checkBoxLUX.Checked)
                        {
                            if (comboBoxPLUX.Text == "0") { tabPage0.Text = "Gráfico LUX"; this.tabPage0.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                            if (comboBoxPLUX.Text == "1") { tabPage1.Text = "Gráfico LUX"; this.tabPage1.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                            if (comboBoxPLUX.Text == "2") { tabPage2.Text = "Gráfico LUX"; this.tabPage2.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                            if (comboBoxPLUX.Text == "3") { tabPage3.Text = "Gráfico LUX"; this.tabPage3.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                            if (comboBoxPLUX.Text == "4") { tabPage4.Text = "Gráfico LUX"; this.tabPage4.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                            if (comboBoxPLUX.Text == "5") { tabPage5.Text = "Gráfico LUX"; this.tabPage5.Parent = this.tabControlRegisto; começarGráficolux(sender, e); }
                        }
    }//---------------------------
                }
            }
            else {
                buttonStart.Text = "START";
                readContinue = false;
            }
        }

        public void mostraLog(string c) {
            listBoxLogs.Invoke(new Action(() => listBoxLogs.Items.Insert(0, "time: " + timer.ElapsedMilliseconds + " " + c)));
        }

        private void buttonSaveDataTxt_Click(object sender, EventArgs e)
        {
            guardar();
        }

        public void guardar()
        {
            if (buttonStart.Text == "START")
            {
                if (listBoxLogs.Items[0].ToString() == "LOGS..." || labelProcura.Text != "Encontrado!" || labelConectar.Text != "Conectado" || comboBoxFrames.Text == "" || (checkBoxAAC.Checked == false && checkBoxBATT.Checked == false && checkBoxECG.Checked == false && checkBoxEDA.Checked == false && checkBoxEMG.Checked == false && checkBoxLUX.Checked == false))
                {
                    if (listBoxLogs.Items[0].ToString() == "LOGS...")
                        MessageBox.Show("Não há dados.");
                    if (labelProcura.Text != "Encontrado!")
                        MessageBox.Show("Selecione utilizador válido.");
                    if (checkBoxAAC.Checked == false && checkBoxBATT.Checked == false && checkBoxECG.Checked == false && checkBoxEDA.Checked == false && checkBoxEMG.Checked == false && checkBoxLUX.Checked == false)
                        MessageBox.Show("Selecione tipo(s) de exames.");
                    if (labelConectar.Text != "Conectado")
                        MessageBox.Show("Aparelho desconectado.");
                    if (comboBoxFrames.Text == "")
                        MessageBox.Show("Selecione número de frames por segundo.");
                }
                else
                {
                    string valoresTexto = listBoxLogs.Text;
                    if (criarRegisto != null)
                        criarRegisto(Convert.ToInt32(textBoxIDRegisto.Text), Convert.ToInt32(textBoxID.Text), exames, Program.Controller.inicio, Program.Controller.fim, temp, Program.Controller.duraçãoReal, comboBoxFrames.Text, checkBoxECG.Checked, checkBoxAAC.Checked, checkBoxEDA.Checked, checkBoxLUX.Checked, checkBoxBATT.Checked, checkBoxEMG.Checked, valoresTexto);
                    labelGuardar.Text = "Guardado";
                    labelGuardar.ForeColor = Color.Green;
                    gerarId();
                }
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            if (PedidoProcurarUtilizadores != null)
                PedidoProcurarUtilizadores("ID", textBoxID.Text);
            if (labelProcura.Text == "Encontrado!")
            {
                int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Convert.ToInt32(textBoxID.Text));
                pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + Program.Model.listaUtilizadores[index].Fotografia;
                textBoxNome.Text = Program.Model.listaUtilizadores[index].Nome;
            }
            else
            {
                pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\imageProfile.png";
                textBoxNome.Text = "";
            }
                
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text!="START") return;
            listBoxLogs.Items.Clear();
            labelGuardar.Text = "Não Guardado";
            labelGuardar.ForeColor = Color.Red;
            listBoxLogs.Items.Add("LOGS...");
            chart0.Series.Clear();
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            chart4.Series.Clear();
            chart5.Series.Clear();
            temp.Clear();
            this.tabPage0.Parent = null; // hide    
            this.tabPage1.Parent = null; // hide    
            this.tabPage2.Parent = null; // hide    
            this.tabPage3.Parent = null; // hide    
            this.tabPage4.Parent = null; // hide    
            this.tabPage5.Parent = null; // hide   
        }

        private void button1_Click(object sender, EventArgs e)//CONECTAR
        {
            string[] ports = SerialPort.GetPortNames();
            if (buttonConectar.Text == "Desconectar") {
                buttonConectar.Text = "Conectar";
                labelConectar.Text = "Desconectado";
                labelConectar.ForeColor = Color.Red;
                return;
            }

            labelConectar.Text = "Conectando...";
            labelConectar.ForeColor = Color.Yellow;

            //----- temporário
            //labelConectar.Text = "Conectado";
            //labelConectar.ForeColor = Color.Green;
            //------
            try
            {
                labelConectar.Text = "Conectado";
                labelConectar.ForeColor = Color.Green;
                Program.Controller.dev = new Bitalino(comboBoxConectar.Text);
                string ver = Program.Controller.dev.version();    // get device version string
                labelVersão.Text = ver;
                labelVersão.ForeColor = Color.Green;
                //dev.battery(10);  // set battery threshold (optional)

                

                buttonConectar.Text = "Desconectar";
            }
            catch (Bitalino.Exception ex)
            {
                buttonConectar.Text = "Conectar";
                labelConectar.Text = "Desconectado";
                labelConectar.ForeColor = Color.Red;
                MessageBox.Show("Erro na conexão: " + ex.Message);
            }
        }

        private void textBoxDuração_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxDuração.Text, "[^0-9]"))
            {
                MessageBox.Show("Insira apenas números.");
                textBoxDuração.Text.Remove(textBoxDuração.Text.Length - 1);
                textBoxDuração.Clear();
            }
        }

        private void ViewCriarRegisto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(labelGuardar.Text=="Não Guardado" && listBoxLogs.Items.Count > 2 ) {
                //devemos perguntar se não deseja guardar os dados se estes ainda nao tiverem sido guardados
                DialogResult dr = MessageBox.Show("Tem a certeza? Os dados não foram guardados e vão ser apagados!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel)
                     e.Cancel=true;
                buttonClear_Click(sender, e);
                //limpar dados ao sair
                chart0.Series.Clear();
                chart1.Series.Clear();
                chart2.Series.Clear();
                chart3.Series.Clear();
                chart4.Series.Clear();
                chart5.Series.Clear();
                labelProcura.Text = "Não Encontrado";
            }
        }



        //private void Txt(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        StreamWriter file = new StreamWriter(manager.pathTxt);

        //        foreach (string sample in listBoxLogs.Items)
        //        {
        //            file.WriteLine(sample);
        //        }

        //        file.Close();

        //        MessageBox.Show("Guardado como txt.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show( "Erro: " + ex.Message);
        //    }
        //}

        //void Csv(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        StreamWriter file = new StreamWriter(manager.pathCsv);
                
        //        file.WriteLine("Time;D1;D2;D3;D4;D5;D6");

        //        foreach (string sample in listBoxLog.Items)
        //        {
        //            string temp = CSV_Parser.ToCSV(sample);

        //            if (temp != null)
        //            {
        //                file.WriteLine(temp);
        //            }

        //        }

        //        file.Close();

        //        MessageBox.Show("DONE - sample saved");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        

        //protected override void Dispose(bool disposing)
        //{
        //    if ((addDataRunner.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
        //    {
        //        addDataRunner.Resume();
        //    }
        //    addDataRunner.Abort();

        //    if (disposing)
        //    {
        //        if (components != null)
        //        {
        //            components.Dispose();
        //        }
        //    }
        //    base.Dispose(disposing);
        //}

        private void começarGráficoecg(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoopecg);
            addDataRunnerecg = new Thread(addDataThreadStart);
            addDataDelecg += new AddDataDelegate(AddDataecg);

            //add series into chart
            startTrending_Clickecg(null, new EventArgs());
        }
        private void começarGráficolux(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLooplux);
            addDataRunnerlux = new Thread(addDataThreadStart);
            addDataDellux += new AddDataDelegate(AddDatalux);

            //add series into chart
            startTrending_Clicklux(null, new EventArgs());
        }
        private void começarGráficobatt(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoopbatt);
            addDataRunnerbatt = new Thread(addDataThreadStart);
            addDataDelbatt += new AddDataDelegate(AddDatabatt);

            //add series into chart
            startTrending_Clickbatt(null, new EventArgs());
        }
        private void começarGráficoeda(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoopeda);
            addDataRunnereda = new Thread(addDataThreadStart);
            addDataDeleda += new AddDataDelegate(AddDataeda);

            //add series into chart
            startTrending_Clickeda(null, new EventArgs());
        }
        private void começarGráficoemg(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoopemg);
            addDataRunneremg = new Thread(addDataThreadStart);
            addDataDelemg += new AddDataDelegate(AddDataemg);

            //add series into chart
            startTrending_Clickemg(null, new EventArgs());
        }
        private void começarGráficoaac(object sender, EventArgs e)
        {
            minimo = 20000; maximo = -1000;
            //define a thread to add values into chart
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoopaac);
            addDataRunneraac = new Thread(addDataThreadStart);
            addDataDelaac += new AddDataDelegate(AddDataaac);

            //add series into chart
            startTrending_Clickaac(null, new EventArgs());
        }






        private void startTrending_Clickeda(object sender, System.EventArgs e)
        {
            //add series from min to max
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(120);
                if (eda == "0")
                {
                    chart0.ChartAreas[0].AxisX.Minimum = 0;
                    chart0.ChartAreas[0].AxisX.Maximum = 8000;
                    chart0.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart0.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart0.Series.Add(newSeries);
                }
                if (eda == "1")
                {
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.Maximum = 8000;
                    chart1.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart1.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart1.Series.Add(newSeries);
                }
                if (eda == "2")
                {
                    chart2.ChartAreas[0].AxisX.Minimum = 0;
                    chart2.ChartAreas[0].AxisX.Maximum = 8000;
                    chart2.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart2.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart2.Series.Add(newSeries);
                }
                if (eda == "3")
                {
                    chart3.ChartAreas[0].AxisX.Minimum = 0;
                    chart3.ChartAreas[0].AxisX.Maximum = 8000;
                    chart3.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart3.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart3.Series.Add(newSeries);
                }
                if (eda == "4")
                {
                    chart4.ChartAreas[0].AxisX.Minimum = 0;
                    chart4.ChartAreas[0].AxisX.Maximum = 8000;
                    chart4.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart4.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart4.Series.Add(newSeries);
                }
                if (eda == "5")
                {
                    chart5.ChartAreas[0].AxisX.Minimum = 0;
                    chart5.ChartAreas[0].AxisX.Maximum = 8000;
                    chart5.Series.Clear();
                    Series newSeries = new Series("Valores exame");
                    chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                    chart5.ChartAreas[0].AxisY.Title = "Valores exame";
                    newSeries.ChartType = SeriesChartType.Line;
                    newSeries.BorderWidth = 2;
                    newSeries.Color = Color.OrangeRed;
                    newSeries.XValueType = ChartValueType.Int32;
                    chart5.Series.Add(newSeries);
                }
                //start thread to add data into chart
                addDataRunnereda.Start();
            //addDataRunnereda.Abort();
        }
        private void startTrending_Clickemg(object sender, System.EventArgs e)
        {
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(120);
            if (emg == "0")
            {
                chart0.ChartAreas[0].AxisX.Minimum = 0;
                chart0.ChartAreas[0].AxisX.Maximum = 8000;
                chart0.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart0.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart0.Series.Add(newSeries);
            }
            if (emg == "1")
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 8000;
                chart1.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart1.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart1.Series.Add(newSeries);
            }
            if (emg == "2")
            {
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = 8000;
                chart2.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart2.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart2.Series.Add(newSeries);
            }
            if (emg == "3")
            {
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisX.Maximum = 8000;
                chart3.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart3.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart3.Series.Add(newSeries);
            }
            if (emg == "4")
            {
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                chart4.ChartAreas[0].AxisX.Maximum = 8000;
                chart4.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart4.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart4.Series.Add(newSeries);
            }
            if (emg == "5")
            {
                chart5.ChartAreas[0].AxisX.Minimum = 0;
                chart5.ChartAreas[0].AxisX.Maximum = 8000;
                chart5.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart5.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart5.Series.Add(newSeries);
            }
            //start thread to add data into chart
            addDataRunneremg.Start();
            //addDataRunneremg.Abort();
        }
        private void startTrending_Clickecg(object sender, System.EventArgs e)
        {
        DateTime minValue = DateTime.Now;
        DateTime maxValue = minValue.AddSeconds(120);
        if (ecg == "0")
        {
            chart0.ChartAreas[0].AxisX.Minimum = 0;
            chart0.ChartAreas[0].AxisX.Maximum = 8000;
            chart0.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart0.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart0.Series.Add(newSeries);
        }
        if (ecg == "1")
        {
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 8000;
            chart1.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart1.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart1.Series.Add(newSeries);
        }
        if (ecg == "2")
        {
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 8000;
            chart2.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart2.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart2.Series.Add(newSeries);
        }
        if (ecg == "3")
        {
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Maximum = 8000;
            chart3.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart3.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart3.Series.Add(newSeries);
        }
        if (ecg == "4")
        {
            chart4.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisX.Maximum = 8000;
            chart4.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart4.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart4.Series.Add(newSeries);
        }
        if (ecg == "5")
        {
            chart5.ChartAreas[0].AxisX.Minimum = 0;
            chart5.ChartAreas[0].AxisX.Maximum = 8000;
            chart5.Series.Clear();
            Series newSeries = new Series("Valores exame");
            chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
            chart5.ChartAreas[0].AxisY.Title = "Valores exame";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Int32;
            chart5.Series.Add(newSeries);
        }
            //start thread to add data into chart
            addDataRunnerecg.Start();
            //addDataRunnerecg.Abort();
        }
        private void startTrending_Clickaac(object sender, System.EventArgs e)
        {
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(120);
            if (aac == "0")
            {
                chart0.ChartAreas[0].AxisX.Minimum = 0;
                chart0.ChartAreas[0].AxisX.Maximum = 8000;
                chart0.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart0.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart0.Series.Add(newSeries);
            }
            if (aac == "1")
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 8000;
                chart1.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart1.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart1.Series.Add(newSeries);
            }
            if (aac == "2")
            {
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = 8000;
                chart2.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart2.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart2.Series.Add(newSeries);
            }
            if (aac == "3")
            {
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisX.Maximum = 8000;
                chart3.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart3.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart3.Series.Add(newSeries);
            }
            if (aac == "4")
            {
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                chart4.ChartAreas[0].AxisX.Maximum = 8000;
                chart4.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart4.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart4.Series.Add(newSeries);
            }
            if (aac == "5")
            {
                chart5.ChartAreas[0].AxisX.Minimum = 0;
                chart5.ChartAreas[0].AxisX.Maximum = 8000;
                chart5.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart5.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart5.Series.Add(newSeries);
            }
            //start thread to add data into chart
            addDataRunneraac.Start();
            //addDataRunneraac.Abort();
        }
        private void startTrending_Clicklux(object sender, System.EventArgs e)
        {
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(120);
            if (lux == "0")
            {
                chart0.ChartAreas[0].AxisX.Minimum = 0;
                chart0.ChartAreas[0].AxisX.Maximum = 8000;
                chart0.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart0.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart0.Series.Add(newSeries);
            }
            if (lux == "1")
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 8000;
                chart1.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart1.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart1.Series.Add(newSeries);
            }
            if (lux == "2")
            {
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = 8000;
                chart2.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart2.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart2.Series.Add(newSeries);
            }
            if (lux == "3")
            {
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisX.Maximum = 8000;
                chart3.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart3.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart3.Series.Add(newSeries);
            }
            if (lux == "4")
            {
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                chart4.ChartAreas[0].AxisX.Maximum = 8000;
                chart4.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart4.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart4.Series.Add(newSeries);
            }
            if (lux == "5")
            {
                chart5.ChartAreas[0].AxisX.Minimum = 0;
                chart5.ChartAreas[0].AxisX.Maximum = 8000;
                chart5.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart5.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart5.Series.Add(newSeries);
            }
            //start thread to add data into chart
            addDataRunnerlux.Start();
            //addDataRunnerlux.Abort();
        }
        private void startTrending_Clickbatt(object sender, System.EventArgs e)
        {
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(120);
            if (batt == "0")
            {
                chart0.ChartAreas[0].AxisX.Minimum = 0;
                chart0.ChartAreas[0].AxisX.Maximum = 8000;
                chart0.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart0.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart0.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart0.Series.Add(newSeries);
            }
            if (batt == "1")
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 8000;
                chart1.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart1.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart1.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart1.Series.Add(newSeries);
            }
            if (batt == "2")
            {
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Maximum = 8000;
                chart2.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart2.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart2.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart2.Series.Add(newSeries);
            }
            if (batt == "3")
            {
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisX.Maximum = 8000;
                chart3.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart3.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart3.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart3.Series.Add(newSeries);
            }
            if (batt == "4")
            {
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                chart4.ChartAreas[0].AxisX.Maximum = 8000;
                chart4.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart4.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart4.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart4.Series.Add(newSeries);
            }
            if (batt == "5")
            {
                chart5.ChartAreas[0].AxisX.Minimum = 0;
                chart5.ChartAreas[0].AxisX.Maximum = 8000;
                chart5.Series.Clear();
                Series newSeries = new Series("Valores exame");
                chart5.ChartAreas[0].AxisX.Title = "Tempo em milisegundos";
                chart5.ChartAreas[0].AxisY.Title = "Valores exame";
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 2;
                newSeries.Color = Color.OrangeRed;
                newSeries.XValueType = ChartValueType.Int32;
                chart5.Series.Add(newSeries);
            }
            //start thread to add data into chart
            addDataRunnerbatt.Start();
            //addDataRunnerbatt.Abort();
        }





        int milesimos = 0;
        private void AddDataThreadLoopemg()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible) {
                    if (emg == "0") chart0.Invoke(addDataDelemg);
                    if (emg == "1") chart1.Invoke(addDataDelemg);
                    if (emg == "2") chart2.Invoke(addDataDelemg);
                    if (emg == "3") chart3.Invoke(addDataDelemg);
                    if (emg == "4") chart4.Invoke(addDataDelemg);
                    if (emg == "5") chart5.Invoke(addDataDelemg);
                }
                
                if (nbFrames == 1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }
        private void AddDataThreadLoopaac()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible)
                {
                    if (aac == "0") chart0.Invoke(addDataDelaac);
                    if (aac == "1") chart1.Invoke(addDataDelaac);
                    if (aac == "2") chart2.Invoke(addDataDelaac);
                    if (aac == "3") chart3.Invoke(addDataDelaac);
                    if (aac == "4") chart4.Invoke(addDataDelaac);
                    if (aac == "5") chart5.Invoke(addDataDelaac);
                }
                
                if (nbFrames==1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }
        private void AddDataThreadLoopecg()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible)
                {
                    if (ecg == "0") chart0.Invoke(addDataDelecg);
                    if (ecg == "1") chart1.Invoke(addDataDelecg);
                    if (ecg == "2") chart2.Invoke(addDataDelecg);
                    if (ecg == "3") chart3.Invoke(addDataDelecg);
                    if (ecg == "4") chart4.Invoke(addDataDelecg);
                    if (ecg == "5") chart5.Invoke(addDataDelecg);
                }
                
                if (nbFrames == 1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }
        private void AddDataThreadLoopeda()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible)
                {
                    if (eda == "0") chart0.Invoke(addDataDeleda);
                    if (eda == "1") chart1.Invoke(addDataDeleda);
                    if (eda == "2") chart2.Invoke(addDataDeleda);
                    if (eda == "3") chart3.Invoke(addDataDeleda);
                    if (eda == "4") chart4.Invoke(addDataDeleda);
                    if (eda == "5") chart5.Invoke(addDataDeleda);
                }
                
                if (nbFrames == 1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }
        private void AddDataThreadLoopbatt()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible)
                {
                    if (batt == "0") chart0.Invoke(addDataDelbatt);
                    if (batt == "1") chart1.Invoke(addDataDelbatt);
                    if (batt == "2") chart2.Invoke(addDataDelbatt);
                    if (batt == "3") chart3.Invoke(addDataDelbatt);
                    if (batt == "4") chart4.Invoke(addDataDelbatt);
                    if (batt == "5") chart5.Invoke(addDataDelbatt);
                }
                
                if (nbFrames == 1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }
        private void AddDataThreadLooplux()
        {
            Thread.Sleep(500);
            while (readContinue)
            {
                if (Visible)
                {
                    if (lux == "0") chart0.Invoke(addDataDellux);
                    if (lux == "1") chart1.Invoke(addDataDellux);
                    if (lux == "2") chart2.Invoke(addDataDellux);
                    if (lux == "3") chart3.Invoke(addDataDellux);
                    if (lux == "4") chart4.Invoke(addDataDellux);
                    if (lux == "5") chart5.Invoke(addDataDellux);
                }
               
                if (nbFrames == 1) milesimos = 1050;
                if (nbFrames == 10) milesimos = 110;
                if (nbFrames == 100) milesimos = 30;
                Thread.Sleep(milesimos);
            }
        }






        public void AddDataecg()
        {
            DateTime timeStamp = DateTime.Now;
            if (ecg == "0") {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
            if (ecg == "1") {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
            if (ecg == "2") {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
            if (ecg == "3") {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
            if (ecg == "4") {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
            if  ( ecg == "5") {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointecg(timeStamp, ptSeries);
                }
            }
        }
        public void AddDatabatt()
        {
            DateTime timeStamp = DateTime.Now;
            if (batt == "0")
            {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
            if (batt == "1")
            {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
            if (batt == "2")
            {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
            if (batt == "3")
            {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
            if (batt == "4")
            {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
            if (batt == "5")
            {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointbatt(timeStamp, ptSeries);
                }
            }
        }
        public void AddDataeda()
        {
            DateTime timeStamp = DateTime.Now;
            if (eda == "0")
            {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
            if (eda == "1")
            {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
            if (eda == "2")
            {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
            if (eda == "3")
            {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
            if (eda == "4")
            {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
            if (eda == "5")
            {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointeda(timeStamp, ptSeries);
                }
            }
        }
        public void AddDataemg()
        {
            DateTime timeStamp = DateTime.Now;
            if (emg == "0")
            {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
            if (emg == "1")
            {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
            if (emg == "2")
            {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
            if (emg == "3")
            {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
            if (emg == "4")
            {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
            if (emg == "5")
            {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointemg(timeStamp, ptSeries);
                }
            }
        }
        public void AddDatalux()
        {
            DateTime timeStamp = DateTime.Now;
            if (lux == "0")
            {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
            if (lux== "1")
            {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
            if (lux == "2")
            {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
            if (lux == "3")
            {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
            if (lux == "4")
            {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
            if (lux == "5")
            {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointlux(timeStamp, ptSeries);
                }
            }
        }
        public void AddDataaac()
        {
            DateTime timeStamp = DateTime.Now;
            if (aac == "0")
            {
                foreach (Series ptSeries in chart0.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
            if (aac == "1")
            {
                foreach (Series ptSeries in chart1.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
            if (aac == "2")
            {
                foreach (Series ptSeries in chart2.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
            if (aac == "3")
            {
                foreach (Series ptSeries in chart3.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
            if (aac == "4")
            {
                foreach (Series ptSeries in chart4.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
            if (aac == "5")
            {
                foreach (Series ptSeries in chart5.Series)
                {
                    AddNewPointaac(timeStamp, ptSeries);
                }
            }
        }



        private void chart0_Click(object sender, EventArgs e)
        {

        }

        private void tabPageRegisto_Click(object sender, EventArgs e)
        {

        }

        public void AddNewPointaac(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxAAC.Checked)
            {
                if (aac == "0") res = temp.Last().resultado0;
                if (aac == "1") res = temp.Last().resultado1;
                if (aac == "2") res = temp.Last().resultado2;
                if (aac == "3") res = temp.Last().resultado3;
                if (aac == "4") res = temp.Last().resultado4;
                if (aac == "5") res = temp.Last().resultado5;
            }
            
            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);
            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if ( minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (aac == "0")
            {
                
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos+5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos+5;
                }
                chart0.Invalidate();
            }
            if (aac == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos+5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos+5;
                }
                chart1.Invalidate();
            }
            if (aac == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (aac == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (aac == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (aac == "5")
            {
                if(chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos+5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos+5;
                }
                chart5.Invalidate();
            }
            
            double x=0;
            //if ( ti.ElapsedMilliseconds > 5000)
            //{
            //    x += 0.03;
            //    chart0.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddMinutes(0.1+x).ToOADate();
            //}


            
        }
        public void AddNewPointbatt(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxBATT.Checked)
            {
                if (batt == "0") res = temp.Last().resultado0;
                if (batt == "1") res = temp.Last().resultado1;
                if (batt == "2") res = temp.Last().resultado2;
                if (batt == "3") res = temp.Last().resultado3;
                if (batt == "4") res = temp.Last().resultado4;
                if (batt == "5") res = temp.Last().resultado5;
            }

            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);
            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if (minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (batt == "0")
            {
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart0.Invalidate();
            }
            if (batt == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart1.Invalidate();
            }
            if (batt == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (batt == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (batt == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (batt == "5")
            {
                if (chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart5.Invalidate();
            }
        }
        public void AddNewPointlux(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxLUX.Checked)
            {
                if (lux == "0") res = temp.Last().resultado0;
                if (lux == "1") res = temp.Last().resultado1;
                if (lux == "2") res = temp.Last().resultado2;
                if (lux == "3") res = temp.Last().resultado3;
                if (lux == "4") res = temp.Last().resultado4;
                if (lux == "5") res = temp.Last().resultado5;
            }

            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);
            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if (minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (lux == "0")
            {
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart0.Invalidate();
            }
            if (lux == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart1.Invalidate();
            }
            if (lux == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (lux == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (lux == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (lux == "5")
            {
                if (chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart5.Invalidate();
            }
        }
        public void AddNewPointemg(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxEMG.Checked)
            {
                if (emg == "0") res = temp.Last().resultado0;
                if (emg == "1") res = temp.Last().resultado1;
                if (emg == "2") res = temp.Last().resultado2;
                if (emg == "3") res = temp.Last().resultado3;
                if (emg == "4") res = temp.Last().resultado4;
                if (emg == "5") res = temp.Last().resultado5;
            }

            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);
            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if (minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (emg == "0")
            {
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart0.Invalidate();
            }
            if (emg == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart1.Invalidate();
            }
            if (emg == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (emg == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (emg == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (emg == "5")
            {
                if (chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart5.Invalidate();
            }
        }
        public void AddNewPointeda(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxEDA.Checked)
            {
                if (eda == "0") res = temp.Last().resultado0;
                if (eda == "1") res = temp.Last().resultado1;
                if (eda == "2") res = temp.Last().resultado2;
                if (eda == "3") res = temp.Last().resultado3;
                if (eda == "4") res = temp.Last().resultado4;
                if (eda == "5") res = temp.Last().resultado5;
            }

            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);
            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if (minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (eda == "0")
            {
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart0.Invalidate();
            }
            if (eda == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart1.Invalidate();
            }
            if (eda == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (eda == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (eda == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (eda == "5")
            {
                if (chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart5.Invalidate();
            }
        }
        public void AddNewPointecg(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            //double newVal = 0;

            //if (ptSeries.Points.Count > 0)
            //{
            //    newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + temp.Last().resultado0;
            //}
            //DateTime t = DateTime.Now;
            int res = 0;

            if (checkBoxECG.Checked)
            {
                if (ecg == "0") res = temp.Last().resultado0;
                if (ecg == "1") res = temp.Last().resultado1;
                if (ecg == "2") res = temp.Last().resultado2;
                if (ecg == "3") res = temp.Last().resultado3;
                if (ecg == "4") res = temp.Last().resultado4;
                if (ecg == "5") res = temp.Last().resultado5;
            }

            ptSeries.Points.AddXY(timer.ElapsedMilliseconds, res);

            //double removeBefore = timer.ElapsedMilliseconds;
            //while (ptSeries.Points[0].XValue < removeBefore)
            //{
            //    ptSeries.Points.RemoveAt(0);
            //}

            if (minimo > res) minimo = res;
            if (maximo < res) maximo = res;
            if (ecg == "0")
            {
                if (chart0.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart0.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart0.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart0.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart0.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart0.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart0.Invalidate();
            }
            if (ecg == "1")
            {
                if (chart1.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart1.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart1.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart1.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += milesimos+5;
                    chart1.ChartAreas[0].AxisX.Maximum += milesimos+5;
                }
                chart1.Invalidate();
            }
            if (ecg == "2")
            {
                if (chart2.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart2.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart2.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart2.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart2.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart2.Invalidate();
            }
            if (ecg == "3")
            {
                if (chart3.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart3.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart3.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart3.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart3.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart3.Invalidate();
            }
            if (ecg == "4")
            {
                if (chart4.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart4.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart4.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart4.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart4.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart4.Invalidate();
            }
            if (ecg == "5")
            {
                if (chart5.ChartAreas[0].AxisY.Minimum - 200 > minimo)
                    chart5.ChartAreas[0].AxisY.Minimum = minimo - 200;
                if (chart5.ChartAreas[0].AxisY.Minimum + 200 < maximo)
                    chart5.ChartAreas[0].AxisY.Maximum = maximo + 200;
                if (timer.ElapsedMilliseconds > 6000)
                {
                    chart5.ChartAreas[0].AxisX.Minimum += milesimos + 5;
                    chart5.ChartAreas[0].AxisX.Maximum += milesimos + 5;
                }
                chart5.Invalidate();
            }
        }



        //private void plot(List<double[]> Data, List<string> Names)
        //{
        //    chart0.Titles.Add("Boxplot");
        //    string names = "series0";            //Define theboxplotseries. No data is bound to this           
        //    Series BoxPlotSeries = new Series();
        //    BoxPlotSeries.Name = "BoxPlotSeries";
        //    BoxPlotSeries.ChartType = SeriesChartType.BoxPlot;
        //    BoxPlotSeries.ChartArea = "Box PlotArea";
        //    ChartArea plotArea = new ChartArea();
        //    ChartArea boxArea = new ChartArea();            //Add thecharting areas to the chart            
        //    chart0.ChartAreas.Add(plotArea);
        //    chart0.ChartAreas.Add(boxArea);
        //    plotArea.Name = "Data Chart Area";
        //    boxArea.Name = "Box Plot Area";            //Showthe Y grid but make it a bit more subtle than default           
        //    boxArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //    boxArea.AxisY.MajorGrid.LineColor = Color.LightGray;            //Turnoff the x grid as it looks awful            

        //    boxArea.AxisX.MajorGrid.Enabled = false;            //Have toadd data as series to the chart, the type isn't important           
        //    for (inti = 0; i < Data.Count; i++)
        //    {
        //        Series newSeries = new Series();
        //        newSeries.Points.DataBindY(Data[i]);
        //        newSeries.Name = "series" + i.ToString();
        //        newSeries.Color = Color.Blue;
        //        if (i != 0)
        //        {
        //            names = names + ";" + newSeries.Name;
        //        }
        //        chart0.Series.Add(newSeries);                //Needa custom label for the x axis otherwise it is just numeric               
        //        CustomLabel myLabel = new CustomLabel();
        //        myLabel.FromPosition = i + 0.5;
        //        myLabel.ToPosition = i + 1.5;
        //        myLabel.Text = Names[i];
        //        boxArea.AxisX.CustomLabels.Add(myLabel);
        //    }
        //    chart0.Series.Add(BoxPlotSeries);            // Definethe settings for the Box plot           
        //    chart0.Series["BoxPlotSeries"]["BoxPlotSeries"] = names;
        //    chart0.Series["BoxPlotSeries"]["BoxPlotWhiskerPercentile"] = "5";
        //    chart0.Series["BoxPlotSeries"]["BoxPlotShowAverage"] = "false";
        //    chart0.Series["BoxPlotSeries"]["BoxPlotShowMedian"] = "true";
        //    chart0.Series["BoxPlotSeries"]["BoxPlotShowUnusualValues"] = "true";            //Hidethe plot of the values so just box plot is shown           
        //    plotArea.Visible = false;
        //}
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    List<double[]> data = new List<double[]>();
        //    data.Add(convertToDouble(new string[] { "10.5", "10.7", "20.5", "25.4" }));
        //    data.Add(convertToDouble(new string[] { "19.5", "20.7", "32.5", "33.4" }));

        //    List<string> Names = new List<string>();
        //    Names.Add("Name1");
        //    Names.Add("Name2");
        //    plot(data, Names);
        //}

        //public static double[] convertToDouble(string[] elems)
        //{
        //    double[] arrDouble = new double[elems.Length];

        //    for (int i = 0; i < elems.Length; i++)
        //    {
        //        arrDouble[i] = Convert.ToDouble(elems[i]);
        //    }
        //    return arrDouble;
        //}
    }
}
