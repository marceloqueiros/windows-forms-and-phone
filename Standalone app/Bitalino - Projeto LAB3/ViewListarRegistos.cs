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
    public partial class ViewListarRegistos : Form
    {
        public event MetodosSemParametros NovoRegisto;
        public event MetodosSemParametros PedidoHome;
        public event MetodosComInt apagarRegisto;
        public event MetodosComInt visualizarRegisto;
        public ViewListarRegistos()
        {
            InitializeComponent();
            Program.Model.AtualizarDataRegistos += Model_bit_resposta_carregar_lista_registos;
        }
       

        private void Model_bit_resposta_carregar_lista_registos()
        {
            dataGridViewRegistos.AllowUserToAddRows = true;
            int i;
            dataGridViewRegistos.Rows.Clear();  //limpar tudo

            int indexdata = -1;
            for (i = 0; i < Program.Model.listaRegistos.Count; i++)
            {
                if (Program.Model.listaRegistos[i].Estado == 0)
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
            linha.Cells[4].Value = foto; //adicionar foto à linha
            linha.Cells[5].Value = Program.Model.listaUtilizadores[indexutilizador].Id_utilizador;
            linha.Cells[6].Value = Program.Model.listaUtilizadores[indexutilizador].Nome;
            linha.Cells[7].Value = fotovisualizar;
            linha.Cells[8].Value = fotoremove;

            dataGridViewRegistos.Rows.Add(linha);

            return indexdata;
        }

        private void ViewListarRegistos_Load(object sender, EventArgs e)
        {
            checkBoxECG.Checked = false;
            checkBoxEDA.Checked = false;
            checkBoxLux.Checked = false;
            checkBoxNome.Checked = false;
            checkBoxAAC.Checked = false;
            checkBoxBATT.Checked = false;
            checkBoxEMG.Checked = false;
            Model_bit_resposta_carregar_lista_registos();
        }

        private void listViewRegisto_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (NovoRegisto != null)
                NovoRegisto();
        }

        private void dataGridViewRegistos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox_home_Click(object sender, EventArgs e)
        {
            if (PedidoHome != null)
                PedidoHome();
        }

        private void dataGridViewRegistos_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
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

        private void dataGridViewRegistos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 6)
                this.Cursor = Cursors.Hand;
            if (e.RowIndex >= 0 && e.ColumnIndex <= 6)
                this.Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridViewRegistos.AllowUserToAddRows = true;
            List<Registo> lista = Program.Model.listaRegistos; //lista como propriedade do model
            int i;
            dataGridViewRegistos.Rows.Clear();  //limpar tudo
            Model_bit_resposta_carregar_lista_registos();


            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxIDUtilizador.Checked; i++)
            {
                if (dataGridViewRegistos.Rows[i].Cells[5].Value.ToString() != textBoxIDutilizador.Text)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxIdRegisto.Checked; i++)
            {
                if (dataGridViewRegistos.Rows[i].Cells[0].Value.ToString() != textBoxRegisto.Text)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }
            for (i = 0; i < dataGridViewRegistos.RowCount && checkBoxNome.Checked; i++)
            {
                if (dataGridViewRegistos.Rows[i].Cells[6].Value.ToString() != textBoxNome.Text)
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
                if (Program.Model.listaRegistos[inb].dataFim > dateTimePickerfim.Value)
                {
                    dataGridViewRegistos.Rows.Remove(dataGridViewRegistos.Rows[i]);
                    i--;
                }
            }

            dataGridViewRegistos.AllowUserToAddRows = false;
            if (!Visible) ShowDialog();
        }

        private void buttonRemoverFiltro_Click(object sender, EventArgs e)
        {
            checkBoxECG.Checked = false;
            checkBoxEDA.Checked = false;
            checkBoxLux.Checked = false;
            checkBoxNome.Checked = false;
            checkBoxAAC.Checked = false;
            checkBoxBATT.Checked = false;
            checkBoxEMG.Checked = false;
            Model_bit_resposta_carregar_lista_registos();
        }
    }
}
