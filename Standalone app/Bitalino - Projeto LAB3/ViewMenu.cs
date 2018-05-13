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
    public partial class ViewMenu : Form
    {
        public event MetodosCom2Strings PedidoAbrirPerfil;
        public event MetodosCom2Strings PedidoProcurarUtilizadores;
        public event MetodosSemParametros NovoRegisto;
        public event MetodosSemParametros AbrirListagemUtilizadores;
        public event MetodosSemParametros NovoUser;
        public event MetodosSemParametros PedidoAbrirUltimoUser;
        public event MetodosSemParametros PedidoAbrirUltimoRegisto;
        public event MetodosSemParametros PedidoAbrirListagemRegistos;
        public event MetodosComInt PedidoAbrirRegisto;
        public event MetodosSemParametros fechar;
        public ViewMenu()
        {
            InitializeComponent();
            Program.Model.respostaProcurarUtilizadorPorTipo += Model_respostaProcurarUtilizadorPorTipo;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (NovoUser != null)
                NovoUser();
        }

        private void pictureBoxListarUtilizador_Click(object sender, EventArgs e)
        {
            if (AbrirListagemUtilizadores != null)
                AbrirListagemUtilizadores();
        }

        private void pictureBoxCriarRegisto_Click(object sender, EventArgs e)
        {
            if (NovoRegisto != null)
                NovoRegisto();
        }

        private void pictureBoxListarRegisto_Click(object sender, EventArgs e)
        {
            if (PedidoAbrirListagemRegistos != null)
                PedidoAbrirListagemRegistos();
        }

        private void Model_respostaProcurarUtilizadorPorTipo(string cor, string texto)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (PedidoProcurarUtilizadores != null)
                PedidoProcurarUtilizadores(comboBoxProcura.Text, textBoxProcura.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PedidoAbrirRegisto != null)
                PedidoAbrirRegisto(Convert.ToInt32(textBoxProcuraRegisto.Text));
        }

        private void comboBoxProcura_TextChanged(object sender, EventArgs e)
        {
            if (PedidoProcurarUtilizadores != null)
                PedidoProcurarUtilizadores(comboBoxProcura.Text, textBoxProcura.Text);
        }
        

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gestão de Recolhas de Dados Biométricos. \n\n - Esta aplicação permite a gestão de dados biométricos recolhidos para vários utilizadores. \n \n- A  recolha dos dados biométricos é efetuada com recurso ao dispositivo BiTalino. \n \n Esta aplicação foi desenvolvida no âmbito da unidade curricular Laboratório Integrado III, pertencente à licenciatura de Engenharia Informática da Universidade de Trás-os-Montes e Alto Douro. \n \n- Esta aplicação foi desenvolvida pelos alunos: \n- Marcelo Pinto - Al60102 \n- Ricardo Cardoso - Al28382 \n- Rui Carvalho - Al61054 \n\n\n Informação de referência: www.bitalino.com");
        }

        private void pictureBoxUltimoUtilizador_Click(object sender, EventArgs e)
        {
            if (PedidoAbrirUltimoUser != null) PedidoAbrirUltimoUser();
        }

        private void pictureBoxUltimoRegisto_Click(object sender, EventArgs e)
        {
            if (PedidoAbrirUltimoRegisto != null) PedidoAbrirUltimoRegisto();
        }

        private void labelProcura_Click(object sender, EventArgs e)
        {

        }

        private void ViewMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            fechar();
        }
    }
}
