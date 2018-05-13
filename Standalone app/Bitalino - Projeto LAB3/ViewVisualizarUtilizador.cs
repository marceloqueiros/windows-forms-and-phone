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
    public partial class ViewVisualizarUtilizador : Form
    {
        public event MetodosComInt mostraEditar;
        public event MetodosComInt apagaUser;
        public event MetodosComInt novoRegisto;
        public event MetodosSemParametros PedidoHome;
        public event MetodosComInt apagarRegisto;
        public event MetodosComInt visualizarRegisto;

        public ViewVisualizarUtilizador()
        {
            InitializeComponent();
        }

        private void Model_bit_resposta_carregar_lista_registos()
        {
            dataGridViewRegistos.AllowUserToAddRows = true;
            int i;
            dataGridViewRegistos.Rows.Clear();  //limpar tudo

            int indexdata = -1;
            for (i = 0; i < Program.Model.listaRegistos.Count; i++)
            {
                if (Program.Model.listaRegistos[i].Estado == 0 && Program.Model.listaRegistos[i].Id_utilizador == Convert.ToInt32(textBoxID.Text))
                {
                    indexdata = escreveNaDataGrid(i, indexdata);
                }
            }
            dataGridViewRegistos.AllowUserToAddRows = false;

        }

        private int escreveNaDataGrid(int i, int indexdata)
        {
            DataGridViewRow linha;
            DataGridViewImageColumn imgcoluna;
            Image foto;
            string caminho, caminhovisualizar, caminhoremove;
            this.dataGridViewRegistos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridViewRegistos.MultiSelect = false;

            caminhovisualizar = Application.StartupPath + @"\imagemUtilizadores\" + "Lupa.png";
            Image fotovisualizar = Image.FromFile(caminhovisualizar);

            caminhoremove = Application.StartupPath + @"\imagemUtilizadores\" + "Remove.png";
            Image fotoremove = Image.FromFile(caminhoremove);

            indexdata++;
            linha = (DataGridViewRow)dataGridViewRegistos.Rows[indexdata].Clone(); //criar linha
            imgcoluna = new DataGridViewImageColumn();
            int indexutilizador = Program.Model.listaUtilizadores.FindIndex(listaUtilizadores => listaUtilizadores.Id_utilizador == Program.Model.listaRegistos[i].Id_utilizador);
            caminho = Application.StartupPath + @"\imagemUtilizadores\" + Program.Model.listaUtilizadores[indexutilizador].Fotografia;
            foto = Image.FromFile(caminho);


            imgcoluna.Image = foto;
            linha.Cells[0].Value = Program.Model.listaRegistos[i].Id_Registo;
            linha.Cells[1].Value = Program.Model.listaRegistos[i].tiposExame;
            linha.Cells[2].Value = Program.Model.listaRegistos[i].dataInicio;
            linha.Cells[3].Value = Program.Model.listaRegistos[i].dataFim;
            linha.Cells[4].Value = fotovisualizar;
            linha.Cells[5].Value = fotoremove;

            dataGridViewRegistos.Rows.Add(linha);

            return indexdata;
        }

        public void Visualizar(int index)
        {
            Utilizador a = Program.Model.listaUtilizadores[index];

            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = a.Nome;
            textBoxBirth.Text = a.DataNascimento.Date.Day.ToString() + '/' + a.DataNascimento.Date.Month.ToString() + '/' + a.DataNascimento.Date.Year.ToString();
            textBoxIdade.Text = a.Idade.ToString();
            textBoxGenero.Text = a.Genero;
            textBoxAltura.Text = a.Altura.ToString();
            textBoxPeso.Text = a.Peso.ToString();
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + a.Fotografia;
            ShowDialog();
        }

        public void VisualizarUtilizador(Utilizador a)
        {
            textBoxID.Text = a.Id_utilizador.ToString();
            textBoxNome.Text = a.Nome;
            textBoxBirth.Text = a.DataNascimento.Date.Day.ToString() + '/' + a.DataNascimento.Date.Month.ToString() + '/' + a.DataNascimento.Date.Year.ToString();
            textBoxIdade.Text = a.Idade.ToString();
            textBoxGenero.Text = a.Genero;
            textBoxAltura.Text = a.Altura.ToString();
            textBoxPeso.Text = a.Peso.ToString();
            pictureBoxFoto.ImageLocation = Application.StartupPath + @"\imagemUtilizadores\" + a.Fotografia;
            ShowDialog();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (mostraEditar != null)
                mostraEditar(Convert.ToInt32(textBoxID.Text));
        }

        private void buttonApagar_Click(object sender, EventArgs e)
        {
            string msg = "Deseja apagar o utilizador com o ID " + textBoxID.Text + '?';
            DialogResult dialogResult = MessageBox.Show(msg, "Atenção", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (apagaUser != null)
                    apagaUser(Convert.ToInt32(textBoxID.Text));
            }
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            if (PedidoHome != null)
                PedidoHome();
        }

        private void ViewVisualizarUtilizador_Load(object sender, EventArgs e)
        {
            checkBoxECG.Checked = false;
            checkBoxEDA.Checked = false;
            checkBoxLux.Checked = false;
            checkBoxAAC.Checked = false;
            checkBoxBATT.Checked = false;
            checkBoxEMG.Checked = false;
            Model_bit_resposta_carregar_lista_registos();
        }

        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            if (novoRegisto != null)
                novoRegisto(Convert.ToInt32(textBoxID.Text));
        }

        private void buttonNewUser_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.buttonNewUser, "Novo Registo");
        }

        private void dataGridViewRegistos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = this.dataGridViewRegistos.Rows[e.RowIndex];
                string aux = row.Cells[0].Value.ToString();
                int id = Convert.ToInt32(aux);
                if (e.RowIndex >= 0 && e.ColumnIndex == 7) //visualizar
                {
                    if (visualizarRegisto != null)
                        visualizarRegisto(id);
                }

                if (e.RowIndex >= 0 && e.ColumnIndex == 8) //apagar
                {
                    string msg = "Deseja apagar o Registo com o ID " + id + '?';
                    DialogResult dialogResult = MessageBox.Show(msg, "Atenção", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (apagarRegisto != null)
                            apagarRegisto(id);
                    }
                }
            }
        }

        private void dataGridViewRegistos_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void dataGridViewRegistos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 6)
                this.Cursor = Cursors.Hand;
            if (e.RowIndex >= 0 && e.ColumnIndex <= 6)
                this.Cursor = Cursors.Default;
        }

        private void buttonFiltrar_Click(object sender, EventArgs e)
        {
            dataGridViewRegistos.AllowUserToAddRows = true;
            List<Registo> lista = Program.Model.listaRegistos; //lista como propriedade do model
            int i;
            dataGridViewRegistos.Rows.Clear();  //limpar tudo
            Model_bit_resposta_carregar_lista_registos();

            
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxIdRegisto.Checked; i++)
            {
                if (dataGridViewRegistos.Rows[i].Cells[0].Value.ToString() != textBoxRegisto.Text)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }

            //_-------------------------
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxAAC.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].AAC == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxECG.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].ECG == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxBATT.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].BATT == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxEDA.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].EDA == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxLux.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].LUX == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxEMG.Checked; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].EMG == false)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].dataInicio < dateTimePickerini.Value)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount; i++)
            {
                int inb = Program.Model.listaRegistos.FindIndex(listaRegistos => listaRegistos.Id_Registo == Convert.ToInt32(dataGridViewRegistos.Rows[i].Cells[0].Value.ToString()));
                if (Program.Model.listaRegistos[inb].dataFim > dateTimePickerini.Value)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }

            dataGridViewRegistos.AllowUserToAddRows = false;
            if (!Visible) ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            checkBoxECG.Checked = false;
            checkBoxEDA.Checked = false;
            checkBoxLux.Checked = false;
            checkBoxAAC.Checked = false;
            checkBoxBATT.Checked = false;
            checkBoxEMG.Checked = false;
            Model_bit_resposta_carregar_lista_registos();
    }
    }
}
