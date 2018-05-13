namespace Bitalino___Projeto_LAB3
{
    partial class ViewListarUtilizadores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewListarUtilizadores));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewUtilizadores = new System.Windows.Forms.DataGridView();
            this.ColumnFotografia = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIdade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGénero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAltura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPeso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVisualizar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnApagar = new System.Windows.Forms.DataGridViewImageColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRemoverFiltro = new System.Windows.Forms.Button();
            this.comboBoxGenero = new System.Windows.Forms.ComboBox();
            this.checkBoxIdade = new System.Windows.Forms.CheckBox();
            this.checkBoxPeso = new System.Windows.Forms.CheckBox();
            this.checkBoxGenero = new System.Windows.Forms.CheckBox();
            this.checkBoxNome = new System.Windows.Forms.CheckBox();
            this.checkBoxAltura = new System.Windows.Forms.CheckBox();
            this.checkBoxID = new System.Windows.Forms.CheckBox();
            this.textBoxPeso = new System.Windows.Forms.TextBox();
            this.textBoxAltura = new System.Windows.Forms.TextBox();
            this.textBoxNome = new System.Windows.Forms.TextBox();
            this.textBoxIdade = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUtilizadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewUtilizadores);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 298);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Utilizadores";
            // 
            // dataGridViewUtilizadores
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUtilizadores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUtilizadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUtilizadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFotografia,
            this.ColumnID,
            this.ColumnNome,
            this.ColumnIdade,
            this.ColumnGénero,
            this.ColumnAltura,
            this.ColumnPeso,
            this.ColumnVisualizar,
            this.ColumnEditar,
            this.ColumnApagar});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUtilizadores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewUtilizadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUtilizadores.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewUtilizadores.Name = "dataGridViewUtilizadores";
            this.dataGridViewUtilizadores.ReadOnly = true;
            this.dataGridViewUtilizadores.Size = new System.Drawing.Size(694, 279);
            this.dataGridViewUtilizadores.TabIndex = 26;
            this.dataGridViewUtilizadores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUtilizadores_CellContentClick);
            this.dataGridViewUtilizadores.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewUtilizadores_CellMouseClick);
            this.dataGridViewUtilizadores.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUtilizadores_CellMouseEnter);
            this.dataGridViewUtilizadores.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUtilizadores_CellMouseLeave);
            // 
            // ColumnFotografia
            // 
            this.ColumnFotografia.FillWeight = 60F;
            this.ColumnFotografia.HeaderText = "Fotografia";
            this.ColumnFotografia.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnFotografia.Name = "ColumnFotografia";
            this.ColumnFotografia.ReadOnly = true;
            this.ColumnFotografia.Width = 60;
            // 
            // ColumnID
            // 
            this.ColumnID.FillWeight = 60F;
            this.ColumnID.HeaderText = "ID";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.ReadOnly = true;
            this.ColumnID.Width = 60;
            // 
            // ColumnNome
            // 
            this.ColumnNome.FillWeight = 140F;
            this.ColumnNome.HeaderText = "Nome";
            this.ColumnNome.Name = "ColumnNome";
            this.ColumnNome.ReadOnly = true;
            this.ColumnNome.Width = 140;
            // 
            // ColumnIdade
            // 
            this.ColumnIdade.FillWeight = 40F;
            this.ColumnIdade.HeaderText = "Idade";
            this.ColumnIdade.Name = "ColumnIdade";
            this.ColumnIdade.ReadOnly = true;
            this.ColumnIdade.Width = 40;
            // 
            // ColumnGénero
            // 
            this.ColumnGénero.FillWeight = 80F;
            this.ColumnGénero.HeaderText = "Género";
            this.ColumnGénero.Name = "ColumnGénero";
            this.ColumnGénero.ReadOnly = true;
            this.ColumnGénero.Width = 80;
            // 
            // ColumnAltura
            // 
            this.ColumnAltura.FillWeight = 45F;
            this.ColumnAltura.HeaderText = "Altura (Cm)";
            this.ColumnAltura.Name = "ColumnAltura";
            this.ColumnAltura.ReadOnly = true;
            this.ColumnAltura.Width = 45;
            // 
            // ColumnPeso
            // 
            this.ColumnPeso.FillWeight = 45F;
            this.ColumnPeso.HeaderText = "Peso (Kg)";
            this.ColumnPeso.Name = "ColumnPeso";
            this.ColumnPeso.ReadOnly = true;
            this.ColumnPeso.Width = 45;
            // 
            // ColumnVisualizar
            // 
            this.ColumnVisualizar.HeaderText = "Visualizar";
            this.ColumnVisualizar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnVisualizar.Name = "ColumnVisualizar";
            this.ColumnVisualizar.ReadOnly = true;
            this.ColumnVisualizar.Width = 80;
            // 
            // ColumnEditar
            // 
            this.ColumnEditar.FillWeight = 50F;
            this.ColumnEditar.HeaderText = "Editar";
            this.ColumnEditar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnEditar.Name = "ColumnEditar";
            this.ColumnEditar.ReadOnly = true;
            this.ColumnEditar.Width = 50;
            // 
            // ColumnApagar
            // 
            this.ColumnApagar.FillWeight = 50F;
            this.ColumnApagar.HeaderText = "Apagar";
            this.ColumnApagar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnApagar.Name = "ColumnApagar";
            this.ColumnApagar.ReadOnly = true;
            this.ColumnApagar.Width = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 34);
            this.button1.TabIndex = 40;
            this.button1.Text = "Filtrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(661, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 30);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonRemoverFiltro);
            this.groupBox2.Controls.Add(this.comboBoxGenero);
            this.groupBox2.Controls.Add(this.checkBoxIdade);
            this.groupBox2.Controls.Add(this.checkBoxPeso);
            this.groupBox2.Controls.Add(this.checkBoxGenero);
            this.groupBox2.Controls.Add(this.checkBoxNome);
            this.groupBox2.Controls.Add(this.checkBoxAltura);
            this.groupBox2.Controls.Add(this.checkBoxID);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBoxPeso);
            this.groupBox2.Controls.Add(this.textBoxAltura);
            this.groupBox2.Controls.Add(this.textBoxNome);
            this.groupBox2.Controls.Add(this.textBoxIdade);
            this.groupBox2.Controls.Add(this.textBoxID);
            this.groupBox2.Location = new System.Drawing.Point(90, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 72);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Kgs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(406, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Cms";
            // 
            // buttonRemoverFiltro
            // 
            this.buttonRemoverFiltro.Location = new System.Drawing.Point(499, 21);
            this.buttonRemoverFiltro.Name = "buttonRemoverFiltro";
            this.buttonRemoverFiltro.Size = new System.Drawing.Size(60, 34);
            this.buttonRemoverFiltro.TabIndex = 42;
            this.buttonRemoverFiltro.Text = "Remover Filtro";
            this.buttonRemoverFiltro.UseVisualStyleBackColor = true;
            this.buttonRemoverFiltro.Click += new System.EventHandler(this.buttonRemoverFiltro_Click);
            // 
            // comboBoxGenero
            // 
            this.comboBoxGenero.FormattingEnabled = true;
            this.comboBoxGenero.Items.AddRange(new object[] {
            "Masculino",
            "Feminino",
            "Outros"});
            this.comboBoxGenero.Location = new System.Drawing.Point(184, 40);
            this.comboBoxGenero.Name = "comboBoxGenero";
            this.comboBoxGenero.Size = new System.Drawing.Size(88, 21);
            this.comboBoxGenero.TabIndex = 43;
            this.comboBoxGenero.SelectedIndexChanged += new System.EventHandler(this.comboBoxGenero_SelectedIndexChanged);
            // 
            // checkBoxIdade
            // 
            this.checkBoxIdade.AutoSize = true;
            this.checkBoxIdade.Location = new System.Drawing.Point(7, 40);
            this.checkBoxIdade.Name = "checkBoxIdade";
            this.checkBoxIdade.Size = new System.Drawing.Size(53, 17);
            this.checkBoxIdade.TabIndex = 42;
            this.checkBoxIdade.Text = "Idade";
            this.checkBoxIdade.UseVisualStyleBackColor = true;
            // 
            // checkBoxPeso
            // 
            this.checkBoxPeso.AutoSize = true;
            this.checkBoxPeso.Location = new System.Drawing.Point(309, 40);
            this.checkBoxPeso.Name = "checkBoxPeso";
            this.checkBoxPeso.Size = new System.Drawing.Size(50, 17);
            this.checkBoxPeso.TabIndex = 42;
            this.checkBoxPeso.Text = "Peso";
            this.checkBoxPeso.UseVisualStyleBackColor = true;
            // 
            // checkBoxGenero
            // 
            this.checkBoxGenero.AutoSize = true;
            this.checkBoxGenero.Location = new System.Drawing.Point(124, 41);
            this.checkBoxGenero.Name = "checkBoxGenero";
            this.checkBoxGenero.Size = new System.Drawing.Size(61, 17);
            this.checkBoxGenero.TabIndex = 42;
            this.checkBoxGenero.Text = "Género";
            this.checkBoxGenero.UseVisualStyleBackColor = true;
            // 
            // checkBoxNome
            // 
            this.checkBoxNome.AutoSize = true;
            this.checkBoxNome.Location = new System.Drawing.Point(124, 17);
            this.checkBoxNome.Name = "checkBoxNome";
            this.checkBoxNome.Size = new System.Drawing.Size(54, 17);
            this.checkBoxNome.TabIndex = 42;
            this.checkBoxNome.Text = "Nome";
            this.checkBoxNome.UseVisualStyleBackColor = true;
            // 
            // checkBoxAltura
            // 
            this.checkBoxAltura.AutoSize = true;
            this.checkBoxAltura.Location = new System.Drawing.Point(309, 21);
            this.checkBoxAltura.Name = "checkBoxAltura";
            this.checkBoxAltura.Size = new System.Drawing.Size(53, 17);
            this.checkBoxAltura.TabIndex = 42;
            this.checkBoxAltura.Text = "Altura";
            this.checkBoxAltura.UseVisualStyleBackColor = true;
            // 
            // checkBoxID
            // 
            this.checkBoxID.AutoSize = true;
            this.checkBoxID.Location = new System.Drawing.Point(7, 17);
            this.checkBoxID.Name = "checkBoxID";
            this.checkBoxID.Size = new System.Drawing.Size(37, 17);
            this.checkBoxID.TabIndex = 42;
            this.checkBoxID.Text = "ID";
            this.checkBoxID.UseVisualStyleBackColor = true;
            // 
            // textBoxPeso
            // 
            this.textBoxPeso.Location = new System.Drawing.Point(362, 40);
            this.textBoxPeso.Name = "textBoxPeso";
            this.textBoxPeso.Size = new System.Drawing.Size(38, 20);
            this.textBoxPeso.TabIndex = 37;
            this.textBoxPeso.TextChanged += new System.EventHandler(this.textBoxPeso_TextChanged);
            // 
            // textBoxAltura
            // 
            this.textBoxAltura.Location = new System.Drawing.Point(362, 19);
            this.textBoxAltura.Name = "textBoxAltura";
            this.textBoxAltura.Size = new System.Drawing.Size(38, 20);
            this.textBoxAltura.TabIndex = 37;
            this.textBoxAltura.TextChanged += new System.EventHandler(this.textBoxAltura_TextChanged);
            // 
            // textBoxNome
            // 
            this.textBoxNome.Location = new System.Drawing.Point(184, 15);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.Size = new System.Drawing.Size(110, 20);
            this.textBoxNome.TabIndex = 37;
            this.textBoxNome.TextChanged += new System.EventHandler(this.textBoxNome_TextChanged);
            // 
            // textBoxIdade
            // 
            this.textBoxIdade.Location = new System.Drawing.Point(60, 38);
            this.textBoxIdade.Name = "textBoxIdade";
            this.textBoxIdade.Size = new System.Drawing.Size(47, 20);
            this.textBoxIdade.TabIndex = 37;
            this.textBoxIdade.TextChanged += new System.EventHandler(this.textBoxIdade_TextChanged);
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(44, 15);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(63, 20);
            this.textBoxID.TabIndex = 37;
            this.textBoxID.TextChanged += new System.EventHandler(this.textBoxID_TextChanged);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.BackColor = System.Drawing.Color.White;
            this.buttonAddUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAddUser.BackgroundImage")));
            this.buttonAddUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAddUser.Location = new System.Drawing.Point(10, 5);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 70);
            this.buttonAddUser.TabIndex = 43;
            this.buttonAddUser.UseVisualStyleBackColor = false;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            this.buttonAddUser.MouseEnter += new System.EventHandler(this.buttonAddUser_MouseEnter);
            // 
            // ViewListarUtilizadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 378);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(716, 417);
            this.MinimumSize = new System.Drawing.Size(716, 417);
            this.Name = "ViewListarUtilizadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Listar Utilizadores";
            this.Load += new System.EventHandler(this.ViewListarUtilizadores_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUtilizadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridViewUtilizadores;
        private System.Windows.Forms.DataGridViewImageColumn ColumnApagar;
        private System.Windows.Forms.DataGridViewImageColumn ColumnEditar;
        private System.Windows.Forms.DataGridViewImageColumn ColumnVisualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAltura;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGénero;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdade;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewImageColumn ColumnFotografia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemoverFiltro;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.CheckBox checkBoxPeso;
        private System.Windows.Forms.CheckBox checkBoxGenero;
        private System.Windows.Forms.CheckBox checkBoxNome;
        private System.Windows.Forms.CheckBox checkBoxAltura;
        private System.Windows.Forms.CheckBox checkBoxIdade;
        private System.Windows.Forms.CheckBox checkBoxID;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxGenero;
        private System.Windows.Forms.TextBox textBoxPeso;
        private System.Windows.Forms.TextBox textBoxAltura;
        private System.Windows.Forms.TextBox textBoxIdade;
        private System.Windows.Forms.TextBox textBoxID;
    }
}