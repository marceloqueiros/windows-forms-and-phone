using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bitalino___Projeto_LAB3
{
    public partial class ViewCriarUtilizador : Form
    {
        public event MetodosCom6StringsE1Date criar_user;//y (y=verificado)
        public event MetodosComDate calcular_idade;//y 
        public event MetodosSemParametros gerar_id;//y
        public event MetodosSemParametros PedidoHome;

        public ViewCriarUtilizador()
        {
            InitializeComponent();
            Program.Model.resposta_criar_user += Model_bit_resposta_criar_user;//y
            Program.Model.resposta_calcular_idade += Model_bit_resposta_calcular_idade;//y
            Program.Model.resposta_gerar_id += Model_bit_resposta_gerar_id; //y
        }

        public void novoUserVazio()
        {
            textBoxID.Text = "";
            textBoxNome.Text = "";
            dateTimePickerBirth.Value = dateTimePickerBirth.MaxDate;
            comboBoxGenero.Text = "";
            textBoxAltura.Text = "";
            textBoxPeso.Text = "";
            textBoxFotografia.Text = "";
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\imageProfile.png";
            ShowDialog();
        }

        //1
        private void ViewCriarUtilizador_Load(object sender, EventArgs e)
        {
            if (gerar_id != null)
                gerar_id();
        }

        private void Model_bit_resposta_gerar_id(int id)
        {
            textBoxID.Text = id.ToString();
        }



        //2 - criar
        private void buttonCriar_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || textBoxNome.Text == "" || comboBoxGenero.Text == "" || textBoxAltura.Text == "" || textBoxPeso.Text == "" || textBoxFotografia.Text == "")
                MessageBox.Show("Faltam campos!");
            else if (criar_user != null)
                criar_user(textBoxID.Text, textBoxNome.Text, dateTimePickerBirth.Value, comboBoxGenero.Text, textBoxAltura.Text, textBoxPeso.Text, textBoxFotografia.Text);
        }

        private void Model_bit_resposta_criar_user()
        {
            MessageBox.Show("Utilizador criado com sucesso!");
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



        //3- idade
        private void dateTimePickerBirth_ValueChanged(object sender, EventArgs e)
        {
            if (calcular_idade != null)
                calcular_idade(dateTimePickerBirth.Value);
        }

        private void Model_bit_resposta_calcular_idade(int idade)
        {
            textBoxIdade.Text = idade.ToString();
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
        


       //não faz sentido ter evento pra isto
       private void buttonFotografia_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxFotografia.Text = dlg.FileName;
            }
        }
        
        private void textBoxFotografia_TextChanged(object sender, EventArgs e)  //EVENTO QUE DISPARA QUANDO O TEXTO MUDA NESTA TEXTBOX...
        {
            pictureBoxFoto.ImageLocation = textBoxFotografia.Text;
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {
            //metodo não usado pra nada mas dá erro se eliminar
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
