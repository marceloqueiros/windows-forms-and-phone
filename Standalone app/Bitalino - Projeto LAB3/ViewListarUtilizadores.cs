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
    public partial class ViewListarUtilizadores : Form
    {
        //public event MetodosComArrayUtilizadores carregar_lista_utilizadores;
        public event MetodosCom2Int procuraUser; //Abre com dados a view editar.
        public event MetodosComInt apagaUser;
        public event MetodosSemParametros PedidoHome;
        public event MetodosSemParametros PedidoAddUser;
        public event MetodosComInt PedidoAbrirPerfil;
        public ViewListarUtilizadores()
        {
            InitializeComponent();
            Program.Model.AtualizarDataUtilizadores += Model_AtualizarDataUtilizadores;
            Program.Model.resposta_criar_user += Model_AtualizarDataUtilizadores;
        }

        public void ListaFiltrada(string tipo, string valor)
        {
            dataGridViewUtilizadores.AllowUserToAddRows = true;
            List<Utilizador> lista = Program.Model.listaUtilizadores; //lista como propriedade do model
            int i;
            dataGridViewUtilizadores.Rows.Clear();  //limpar tudo

            int indexdata = -1;

            for (i = 0; i < lista.Count; i++)
            {
                switch (tipo)  //esta verificação tem que ser feita na view
                {
                    case "ID":
                        if (lista[i].Estado == 0 && lista[i].Id_utilizador == Convert.ToInt32(valor))
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                    case "Nome":
                        if (lista[i].Estado == 0 && lista[i].Nome == valor)
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                    case "Idade":
                        if (lista[i].Estado == 0 && lista[i].Idade == Convert.ToInt32(valor))
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                    case "Género":
                        if (lista[i].Estado == 0 && lista[i].Genero == valor)
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                    case "Peso":
                        if (lista[i].Estado == 0 && lista[i].Peso == Convert.ToInt32(valor))
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                    case "Altura":
                        if (lista[i].Estado == 0 && lista[i].Altura == Convert.ToInt32(valor))
                            indexdata = escreveNaDataGrid(i, indexdata);
                        break;
                }
            }
            dataGridViewUtilizadores.AllowUserToAddRows = false;

            if (PedidoAbrirPerfil != null && dataGridViewUtilizadores.RowCount == 1)
                PedidoAbrirPerfil(Convert.ToInt32(dataGridViewUtilizadores.Rows[0].Cells[1].Value));
            else
                ShowDialog();
        }



        public void ListaFiltradaComCheckds(bool ID, bool Nome, bool Idade, bool Genero, bool Peso, bool Altura, string ValorID, string ValorNome, string ValorIdade, string ValorGenero, string ValorPeso, string ValorAltura)
        {
            dataGridViewUtilizadores.AllowUserToAddRows = true;
            List<Utilizador> lista = Program.Model.listaUtilizadores; //lista como propriedade do model
            int i;
            dataGridViewUtilizadores.Rows.Clear();  //limpar tudo
            Model_AtualizarDataUtilizadores();

            for (i = 0; i < dataGridViewUtilizadores.RowCount && ID; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[1].Value.ToString() != ValorID) { 
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewUtilizadores.RowCount && Nome; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[2].Value.ToString() != ValorNome)
                {
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewUtilizadores.RowCount && Idade; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[3].Value.ToString() != ValorIdade)
                {
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewUtilizadores.RowCount && Genero; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[4].Value.ToString() != ValorGenero)
                {
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewUtilizadores.RowCount && Peso; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[6].Value.ToString() != ValorPeso)
                {
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewUtilizadores.RowCount && Altura; i++)
            {
                if (dataGridViewUtilizadores.Rows[i].Cells[5].Value.ToString() != ValorAltura)
                {
                    dataGridViewUtilizadores.Rows.Remove(dataGridViewUtilizadores.Rows[i]);
                    i--;
                }
            }
            
            dataGridViewUtilizadores.AllowUserToAddRows = false;
            if (!Visible) ShowDialog();
        }

    //a view altera-se a si própria:
    public void Model_AtualizarDataUtilizadores() //chamado tbm depois de criar user
        {
            dataGridViewUtilizadores.AllowUserToAddRows = true;
            List<Utilizador> lista = Program.Model.listaUtilizadores;
            int i;
            dataGridViewUtilizadores.Rows.Clear();  //limpar tudo

            int indexdata=-1;
            for (i = 0; i < lista.Count;i++)
            {
                if (lista[i].Estado == 0)
                {
                    indexdata=escreveNaDataGrid(i, indexdata);
                }     
            }
            dataGridViewUtilizadores.AllowUserToAddRows = false;
        }

        private int escreveNaDataGrid(int i, int indexdata)
        {
            DataGridViewRow linha;
            DataGridViewImageColumn imgcoluna;
            Image foto;
            string caminho, caminhovisualizar, caminhoeditar, caminhoremove;
            this.dataGridViewUtilizadores.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridViewUtilizadores.MultiSelect = false;

            caminhovisualizar = Application.StartupPath + @"\imagemUtilizadores\" + "Lupa.png";
            Image fotovisualizar = Image.FromFile(caminhovisualizar);

            caminhoeditar = Application.StartupPath + @"\imagemUtilizadores\" + "user_edit.png";
            Image fotoeditar = Image.FromFile(caminhoeditar);

            caminhoremove = Application.StartupPath + @"\imagemUtilizadores\" + "Remove.png";
            Image fotoremove = Image.FromFile(caminhoremove);

            List<Utilizador> lista = Program.Model.listaUtilizadores;
            indexdata++;
            linha = (DataGridViewRow)dataGridViewUtilizadores.Rows[indexdata].Clone(); //criar linha
            imgcoluna = new DataGridViewImageColumn();

            caminho = Application.StartupPath + @"\imagemUtilizadores\" + lista[i].Fotografia;
            foto = Image.FromFile(caminho);

            imgcoluna.Image = foto;
            linha.Cells[0].Value = foto; //adicionar foto à linha
            linha.Cells[1].Value = lista[i].Id_utilizador;
            linha.Cells[2].Value = lista[i].Nome;
            linha.Cells[3].Value = lista[i].Idade;
            linha.Cells[4].Value = lista[i].Genero;
            linha.Cells[5].Value = lista[i].Altura;
            linha.Cells[6].Value = lista[i].Peso;
            linha.Cells[7].Value = fotovisualizar;
            linha.Cells[8].Value = fotoeditar;
            linha.Cells[9].Value = fotoremove;
            //inserir simbolos editar e apagar

            dataGridViewUtilizadores.Rows.Add(linha);

            return indexdata;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {//ir para menu
            if (PedidoHome != null)
                PedidoHome();
        }

        private void dataGridViewUtilizadores_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 6)
                this.Cursor = Cursors.Hand;
            if (e.RowIndex >= 0 && e.ColumnIndex <= 6)
                this.Cursor = Cursors.Default;
        }

        private void dataGridViewUtilizadores_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void dataGridViewUtilizadores_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = this.dataGridViewUtilizadores.Rows[e.RowIndex];
                string aux = row.Cells[1].Value.ToString();
                int id = Convert.ToInt32(aux);
                if (e.RowIndex >= 0 && e.ColumnIndex == 7) //editar
                {
                    if (procuraUser != null)
                        procuraUser(id, e.ColumnIndex);
                }

                if (e.RowIndex >= 0 && e.ColumnIndex == 8) //editar
                {
                    if (procuraUser != null)
                        procuraUser(id, e.ColumnIndex);
                }

                if (e.RowIndex >= 0 && e.ColumnIndex == 9) //apagar
                {
                    string msg = "Deseja apagar o Utilizador com o ID " + id + '?';
                    DialogResult dialogResult = MessageBox.Show(msg, "Atenção", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (apagaUser != null)
                            apagaUser(id);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxID.Checked && textBoxID.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else if (checkBoxNome.Checked && textBoxNome.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else if (checkBoxIdade.Checked && textBoxIdade.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else if (checkBoxAltura.Checked && textBoxAltura.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else if (checkBoxPeso.Checked && textBoxPeso.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else if (checkBoxGenero.Checked && comboBoxGenero.Text == "")
                MessageBox.Show("Um campo de filtragem está ativado mas não está preenchido!");
            else
                ListaFiltradaComCheckds(checkBoxID.Checked, checkBoxNome.Checked, checkBoxIdade.Checked, checkBoxGenero.Checked, checkBoxPeso.Checked, checkBoxAltura.Checked, textBoxID.Text, textBoxNome.Text, textBoxIdade.Text, comboBoxGenero.Text, textBoxPeso.Text, textBoxAltura.Text);
        }

        private void buttonRemoverFiltro_Click(object sender, EventArgs e)
        {
            checkBoxAltura.Checked = false;
            checkBoxPeso.Checked = false;
            checkBoxID.Checked = false;
            checkBoxNome.Checked = false;
            checkBoxIdade.Checked = false;
            checkBoxGenero.Checked = false;
            Model_AtualizarDataUtilizadores();
        }

        private void buttonAddUser_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.buttonAddUser, "Criar Utilizador");
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (PedidoAddUser != null)
                PedidoAddUser();
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxID.Text == "") checkBoxID.Checked = false;
            else checkBoxID.Checked = true;
        }

        private void textBoxIdade_TextChanged(object sender, EventArgs e)
        {
            if (textBoxIdade.Text == "") checkBoxIdade.Checked = false;
            else checkBoxIdade.Checked = true;
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNome.Text == "") checkBoxNome.Checked = false;
            else checkBoxNome.Checked = true;
        }

        private void comboBoxGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGenero.Text == "") checkBoxGenero.Checked = false;
            else checkBoxGenero.Checked = true;
        }

        private void textBoxAltura_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAltura.Text == "") checkBoxAltura.Checked = false;
            else checkBoxAltura.Checked = true;
        }

        private void textBoxPeso_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPeso.Text == "") checkBoxPeso.Checked = false;
            else checkBoxPeso.Checked = true;
        }

        private void ViewListarUtilizadores_Load(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxIdade.Text = "";
            textBoxAltura.Text = "";
            comboBoxGenero.Text = "Masculino";
            textBoxPeso.Text = "";
            textBoxNome.Text = "";
            checkBoxID.Checked = false;
            checkBoxNome.Checked = false;
            checkBoxIdade.Checked = false;
            checkBoxAltura.Checked = false;
            checkBoxPeso.Checked = false;
            checkBoxGenero.Checked = false;
        }

        private void dataGridViewUtilizadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}