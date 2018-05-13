using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitalino___Projeto_LAB3
{
    public partial class ViewVisualizarRegisto : Form
    {public event MetodosComInt apagarRegisto;
        public ViewVisualizarRegisto()
        {
            InitializeComponent();
        }

        public void VisualizarRegisto(Registo a)
        {
            textBoxIDRegisto.Text = a.Id_Registo.ToString();
            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxTipos.Text = a.tiposExame;
            textBoxDataInicio.Text = a.dataInicio.ToString();
            textBoxDataFim.Text = a.dataFim.ToString();
            textBoxDuracao.Text = a.duração.ToString();
            textBoxFrameRate.Text = a.frames;
            for(int i = 0;i< a.Resultados.Count; i++) {
                string str = a.Resultados[i].resultado0.ToString() + ';' + a.Resultados[i].resultado1.ToString() + ';' + a.Resultados[i].resultado2.ToString() + ';' + a.Resultados[i].resultado3.ToString() + ';' + a.Resultados[i].resultado4.ToString() + ';' + a.Resultados[i].resultado5.ToString();
                listBoxLogs.Items.Add(i + ": " +  str.ToString());
            }
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == a.Id_utilizador);

            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = Program.Model.listaUtilizadores[index].Nome;

            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + Program.Model.listaUtilizadores[index].Fotografia;
            ShowDialog();
        }

        public void VisualizarRegisto(int ind)
        {
            Registo a = Program.Model.listaRegistos[ind];

            textBoxIDRegisto.Text = a.Id_Registo.ToString();
            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxTipos.Text = a.tiposExame;
            textBoxDataInicio.Text = a.dataInicio.ToString();
            textBoxDataFim.Text = a.dataFim.ToString();
            textBoxDuracao.Text = a.duração.ToString();
            textBoxFrameRate.Text = a.frames;

            //for (int i = 0; i < a.Resultados.Count; i++)
            //{
            //    string str = a.Resultados[i].resultado0.ToString() + ';' + a.Resultados[i].resultado1.ToString() + ';' + a.Resultados[i].resultado2.ToString() + ';' + a.Resultados[i].resultado3.ToString() + ';' + a.Resultados[i].resultado4.ToString() + ';' + a.Resultados[i].resultado5.ToString();
            //    listBoxLogs.Items.Add(i + ": " + str.ToString());
            //}
            int index = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == a.Id_utilizador);
            listBoxLogs.Text = a.valoresTexto;
            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = Program.Model.listaUtilizadores[index].Nome;

            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + Program.Model.listaUtilizadores[index].Fotografia;
            ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (apagarRegisto != null)
                apagarRegisto(Convert.ToInt32(textBoxIDRegisto.Text));
            Visible = false;
        }

        private void ViewVisualizarRegisto_Load(object sender, EventArgs e)
        {
            tabPage2.Parent = null;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
