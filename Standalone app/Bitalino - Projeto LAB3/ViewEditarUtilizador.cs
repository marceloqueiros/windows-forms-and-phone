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
    public partial class ViewEditarUtilizador : Form
    {
        public event MetodosCom6StringsE1Date editar_user;//y (y=verificado)
        public event MetodosComDate calcular_idade;//y 
        public event MetodosSemParametros PedidoHome;
        

        public ViewEditarUtilizador()
        { 
            InitializeComponent();
            //Recebe os dados da gridview e vai passalos para a view.
            //Program.Model.resposta_abrir_com_dados += Model_resposta_abrir_com_dados;
            Program.Model.resposta_calcular_idade += Model_resposta_calcular_idade;
            Program.Model.resposta_editar_user += Model_resposta_editar_user;
            //textBoxID.Text = id;
            //textBoxNome.Text = nome;
            //textBoxIdade.Text = idade;
            //comboBoxGenero.Text = genero;
            //textBoxAltura.Text = altura;
            //textBoxPeso.Text = peso;
            //pictureBoxFoto.Image = img;
        }

        public void VisualizarEdição(int index)
        {
            Utilizador a = Program.Model.listaUtilizadores[index];

            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = a.Nome;
            textBoxIdade.Text = a.Idade.ToString();
            dateTimePickerBirth.Value = a.DataNascimento.Date;
            comboBoxGenero.Text = a.Genero;
            textBoxAltura.Text = a.Altura.ToString();
            textBoxPeso.Text = a.Peso.ToString();
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + a.Fotografia;
            ShowDialog();
        }

        private void Model_resposta_calcular_idade(int idade)
        {
            textBoxIdade.Text = idade.ToString();
        }

        private void Model_resposta_editar_user()
        {
            MessageBox.Show("Utilizador alterado com sucesso!");
            textBoxID.Text = "";
            textBoxNome.Text = "";
            dateTimePickerBirth.Value = dateTimePickerBirth.MaxDate;
            comboBoxGenero.Text = "";
            textBoxAltura.Text = "";
            textBoxPeso.Text = "";
            textBoxFotografia.Text = "";
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\imageProfile.png";
            Close();
        }

        //cancelar - não é necessário evento porque a view apenas se altera
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxNome.Text = "";
            dateTimePickerBirth.Value = dateTimePickerBirth.MaxDate;
            comboBoxGenero.Text = "";
            textBoxAltura.Text = "";
            textBoxPeso.Text = "";
            textBoxFotografia.Text = "";
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\imageProfile.png";
            Close();
        }

        private void buttonFotografia_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxFotografia.Text = dlg.FileName;
            }
        }

        private void textBoxFotografia_TextChanged(object sender, EventArgs e)
        {
            pictureBoxFoto.ImageLocation = textBoxFotografia.Text;
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {
            //metodo não usado pra nada mas dá erro se eliminar
        }

        private void buttonGuardar_Click(object sender, EventArgs e)//IMPORTANTE!!
        {
            if (textBoxID.Text == "" || textBoxNome.Text == "" || comboBoxGenero.Text == "" || textBoxAltura.Text == "" || textBoxPeso.Text == "")
                MessageBox.Show("Faltam campos!");
            else
            {
                if(textBoxFotografia.Text=="") textBoxFotografia.Text=pictureBoxFoto.ImageLocation;
                if (editar_user != null)
                    editar_user(textBoxID.Text, textBoxNome.Text, dateTimePickerBirth.Value, comboBoxGenero.Text, textBoxAltura.Text, textBoxPeso.Text, textBoxFotografia.Text);
            }
        }

        private void dateTimePickerBirth_ValueChanged(object sender, EventArgs e)
        {
            if (calcular_idade != null)
                calcular_idade(dateTimePickerBirth.Value);
        }

        private void ViewEditarUtilizador_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            if (PedidoHome != null)
                PedidoHome();
        }

        private void textBoxAltura_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxAltura.Text, "[^0-9]"))
            {
                MessageBox.Show("Insira apenas números para a Altura.");
                textBoxAltura.Text.Remove(textBoxAltura.Text.Length - 1);
                textBoxAltura.Clear();
            }
            
        }

        private void textBoxPeso_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxPeso.Text, "[^0-9]"))
            {
                MessageBox.Show("Insira apenas números para a Altura.");
                textBoxPeso.Text.Remove(textBoxPeso.Text.Length - 1);
                textBoxPeso.Clear();
            }
           
        }
    }
}
