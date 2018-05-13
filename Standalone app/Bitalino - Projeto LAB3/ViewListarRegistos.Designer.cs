namespace Bitalino___Projeto_LAB3
{
    partial class ViewListarRegistos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewListarRegistos));
            this.pictureBox_home = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewRegistos = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoverFiltro = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxEMG = new System.Windows.Forms.CheckBox();
            this.checkBoxBATT = new System.Windows.Forms.CheckBox();
            this.checkBoxEDA = new System.Windows.Forms.CheckBox();
            this.dateTimePickerini = new System.Windows.Forms.DateTimePicker();
            this.checkBoxAAC = new System.Windows.Forms.CheckBox();
            this.dateTimePickerfim = new System.Windows.Forms.DateTimePicker();
            this.checkBoxLux = new System.Windows.Forms.CheckBox();
            this.checkBoxECG = new System.Windows.Forms.CheckBox();
            this.checkBoxNome = new System.Windows.Forms.CheckBox();
            this.checkBoxIdRegisto = new System.Windows.Forms.CheckBox();
            this.checkBoxIDUtilizador = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxRegisto = new System.Windows.Forms.TextBox();
            this.textBoxIDutilizador = new System.Windows.Forms.TextBox();
            this.textBoxNome = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.ColumnIDRegisto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDataInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDataFim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFoto = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnIDUtilizador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVisualizar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnApagar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_home)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_home
            // 
            this.pictureBox_home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_home.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_home.Image")));
            this.pictureBox_home.Location = new System.Drawing.Point(855, 11);
            this.pictureBox_home.Name = "pictureBox_home";
            this.pictureBox_home.Size = new System.Drawing.Size(31, 30);
            this.pictureBox_home.TabIndex = 24;
            this.pictureBox_home.TabStop = false;
            this.pictureBox_home.Click += new System.EventHandler(this.pictureBox_home_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewRegistos);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Location = new System.Drawing.Point(5, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(894, 308);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registos";
            // 
            // dataGridViewRegistos
            // 
            this.dataGridViewRegistos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegistos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIDRegisto,
            this.ColumnTipo,
            this.ColumnDataInicio,
            this.ColumnDataFim,
            this.ColumnFoto,
            this.ColumnIDUtilizador,
            this.ColumnNome,
            this.ColumnVisualizar,
            this.ColumnApagar});
            this.dataGridViewRegistos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRegistos.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewRegistos.Name = "dataGridViewRegistos";
            this.dataGridViewRegistos.Size = new System.Drawing.Size(888, 289);
            this.dataGridViewRegistos.TabIndex = 25;
            this.dataGridViewRegistos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRegistos_CellContentClick);
            this.dataGridViewRegistos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewRegistos_CellMouseClick);
            this.dataGridViewRegistos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRegistos_CellMouseEnter);
            this.dataGridViewRegistos.MouseLeave += new System.EventHandler(this.dataGridViewRegistos_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(544, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(544, 46);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRemoverFiltro);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBoxEMG);
            this.groupBox2.Controls.Add(this.checkBoxBATT);
            this.groupBox2.Controls.Add(this.checkBoxEDA);
            this.groupBox2.Controls.Add(this.dateTimePickerini);
            this.groupBox2.Controls.Add(this.checkBoxAAC);
            this.groupBox2.Controls.Add(this.dateTimePickerfim);
            this.groupBox2.Controls.Add(this.checkBoxLux);
            this.groupBox2.Controls.Add(this.checkBoxECG);
            this.groupBox2.Controls.Add(this.checkBoxNome);
            this.groupBox2.Controls.Add(this.checkBoxIdRegisto);
            this.groupBox2.Controls.Add(this.checkBoxIDUtilizador);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBoxRegisto);
            this.groupBox2.Controls.Add(this.textBoxIDutilizador);
            this.groupBox2.Controls.Add(this.textBoxNome);
            this.groupBox2.Location = new System.Drawing.Point(109, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 72);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por:";
            // 
            // buttonRemoverFiltro
            // 
            this.buttonRemoverFiltro.Location = new System.Drawing.Point(674, 25);
            this.buttonRemoverFiltro.Name = "buttonRemoverFiltro";
            this.buttonRemoverFiltro.Size = new System.Drawing.Size(60, 34);
            this.buttonRemoverFiltro.TabIndex = 42;
            this.buttonRemoverFiltro.Text = "Remover Filtro";
            this.buttonRemoverFiltro.UseVisualStyleBackColor = true;
            this.buttonRemoverFiltro.Click += new System.EventHandler(this.buttonRemoverFiltro_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Entre:";
            // 
            // checkBoxEMG
            // 
            this.checkBoxEMG.AutoSize = true;
            this.checkBoxEMG.Location = new System.Drawing.Point(279, 47);
            this.checkBoxEMG.Name = "checkBoxEMG";
            this.checkBoxEMG.Size = new System.Drawing.Size(50, 17);
            this.checkBoxEMG.TabIndex = 42;
            this.checkBoxEMG.Text = "EMG";
            this.checkBoxEMG.UseVisualStyleBackColor = true;
            // 
            // checkBoxBATT
            // 
            this.checkBoxBATT.AutoSize = true;
            this.checkBoxBATT.Location = new System.Drawing.Point(219, 47);
            this.checkBoxBATT.Name = "checkBoxBATT";
            this.checkBoxBATT.Size = new System.Drawing.Size(54, 17);
            this.checkBoxBATT.TabIndex = 42;
            this.checkBoxBATT.Text = "BATT";
            this.checkBoxBATT.UseVisualStyleBackColor = true;
            // 
            // checkBoxEDA
            // 
            this.checkBoxEDA.AutoSize = true;
            this.checkBoxEDA.Location = new System.Drawing.Point(170, 47);
            this.checkBoxEDA.Name = "checkBoxEDA";
            this.checkBoxEDA.Size = new System.Drawing.Size(48, 17);
            this.checkBoxEDA.TabIndex = 42;
            this.checkBoxEDA.Text = "EDA";
            this.checkBoxEDA.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerini
            // 
            this.dateTimePickerini.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerini.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerini.Location = new System.Drawing.Point(390, 42);
            this.dateTimePickerini.MaxDate = new System.DateTime(2016, 2, 27, 0, 0, 0, 0);
            this.dateTimePickerini.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerini.Name = "dateTimePickerini";
            this.dateTimePickerini.Size = new System.Drawing.Size(95, 20);
            this.dateTimePickerini.TabIndex = 29;
            this.dateTimePickerini.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // checkBoxAAC
            // 
            this.checkBoxAAC.AutoSize = true;
            this.checkBoxAAC.Location = new System.Drawing.Point(117, 47);
            this.checkBoxAAC.Name = "checkBoxAAC";
            this.checkBoxAAC.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAAC.TabIndex = 42;
            this.checkBoxAAC.Text = "AAC";
            this.checkBoxAAC.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerfim
            // 
            this.dateTimePickerfim.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerfim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerfim.Location = new System.Drawing.Point(498, 42);
            this.dateTimePickerfim.MaxDate = new System.DateTime(2016, 2, 27, 0, 0, 0, 0);
            this.dateTimePickerfim.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerfim.Name = "dateTimePickerfim";
            this.dateTimePickerfim.Size = new System.Drawing.Size(95, 20);
            this.dateTimePickerfim.TabIndex = 29;
            this.dateTimePickerfim.Value = new System.DateTime(2016, 2, 27, 0, 0, 0, 0);
            // 
            // checkBoxLux
            // 
            this.checkBoxLux.AutoSize = true;
            this.checkBoxLux.Location = new System.Drawing.Point(64, 47);
            this.checkBoxLux.Name = "checkBoxLux";
            this.checkBoxLux.Size = new System.Drawing.Size(47, 17);
            this.checkBoxLux.TabIndex = 42;
            this.checkBoxLux.Text = "LUX";
            this.checkBoxLux.UseVisualStyleBackColor = true;
            // 
            // checkBoxECG
            // 
            this.checkBoxECG.AutoSize = true;
            this.checkBoxECG.Location = new System.Drawing.Point(10, 47);
            this.checkBoxECG.Name = "checkBoxECG";
            this.checkBoxECG.Size = new System.Drawing.Size(48, 17);
            this.checkBoxECG.TabIndex = 42;
            this.checkBoxECG.Text = "ECG";
            this.checkBoxECG.UseVisualStyleBackColor = true;
            // 
            // checkBoxNome
            // 
            this.checkBoxNome.AutoSize = true;
            this.checkBoxNome.Location = new System.Drawing.Point(202, 19);
            this.checkBoxNome.Name = "checkBoxNome";
            this.checkBoxNome.Size = new System.Drawing.Size(54, 17);
            this.checkBoxNome.TabIndex = 42;
            this.checkBoxNome.Text = "Nome";
            this.checkBoxNome.UseVisualStyleBackColor = true;
            // 
            // checkBoxIdRegisto
            // 
            this.checkBoxIdRegisto.AutoSize = true;
            this.checkBoxIdRegisto.Location = new System.Drawing.Point(420, 19);
            this.checkBoxIdRegisto.Name = "checkBoxIdRegisto";
            this.checkBoxIdRegisto.Size = new System.Drawing.Size(76, 17);
            this.checkBoxIdRegisto.TabIndex = 42;
            this.checkBoxIdRegisto.Text = "ID Registo";
            this.checkBoxIdRegisto.UseVisualStyleBackColor = true;
            // 
            // checkBoxIDUtilizador
            // 
            this.checkBoxIDUtilizador.AutoSize = true;
            this.checkBoxIDUtilizador.Location = new System.Drawing.Point(10, 19);
            this.checkBoxIDUtilizador.Name = "checkBoxIDUtilizador";
            this.checkBoxIDUtilizador.Size = new System.Drawing.Size(83, 17);
            this.checkBoxIDUtilizador.TabIndex = 42;
            this.checkBoxIDUtilizador.Text = "ID Utilizador";
            this.checkBoxIDUtilizador.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(612, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 34);
            this.button4.TabIndex = 40;
            this.button4.Text = "Filtrar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBoxRegisto
            // 
            this.textBoxRegisto.Location = new System.Drawing.Point(498, 16);
            this.textBoxRegisto.Name = "textBoxRegisto";
            this.textBoxRegisto.Size = new System.Drawing.Size(91, 20);
            this.textBoxRegisto.TabIndex = 37;
            // 
            // textBoxIDutilizador
            // 
            this.textBoxIDutilizador.Location = new System.Drawing.Point(99, 16);
            this.textBoxIDutilizador.Name = "textBoxIDutilizador";
            this.textBoxIDutilizador.Size = new System.Drawing.Size(91, 20);
            this.textBoxIDutilizador.TabIndex = 37;
            // 
            // textBoxNome
            // 
            this.textBoxNome.Location = new System.Drawing.Point(262, 16);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.Size = new System.Drawing.Size(144, 20);
            this.textBoxNome.TabIndex = 37;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(33, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 73);
            this.button3.TabIndex = 102;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ColumnIDRegisto
            // 
            this.ColumnIDRegisto.FillWeight = 60F;
            this.ColumnIDRegisto.HeaderText = "ID Registo";
            this.ColumnIDRegisto.Name = "ColumnIDRegisto";
            this.ColumnIDRegisto.Width = 60;
            // 
            // ColumnTipo
            // 
            this.ColumnTipo.FillWeight = 165F;
            this.ColumnTipo.HeaderText = "Tipo(s)";
            this.ColumnTipo.Name = "ColumnTipo";
            this.ColumnTipo.Width = 165;
            // 
            // ColumnDataInicio
            // 
            this.ColumnDataInicio.FillWeight = 120F;
            this.ColumnDataInicio.HeaderText = "Data de Inicio";
            this.ColumnDataInicio.Name = "ColumnDataInicio";
            this.ColumnDataInicio.Width = 120;
            // 
            // ColumnDataFim
            // 
            this.ColumnDataFim.FillWeight = 120F;
            this.ColumnDataFim.HeaderText = "Data de Fim";
            this.ColumnDataFim.Name = "ColumnDataFim";
            this.ColumnDataFim.Width = 120;
            // 
            // ColumnFoto
            // 
            this.ColumnFoto.FillWeight = 60F;
            this.ColumnFoto.HeaderText = "Utilizador";
            this.ColumnFoto.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnFoto.Name = "ColumnFoto";
            this.ColumnFoto.Width = 60;
            // 
            // ColumnIDUtilizador
            // 
            this.ColumnIDUtilizador.FillWeight = 60F;
            this.ColumnIDUtilizador.HeaderText = "ID Utilizador";
            this.ColumnIDUtilizador.Name = "ColumnIDUtilizador";
            this.ColumnIDUtilizador.Width = 60;
            // 
            // ColumnNome
            // 
            this.ColumnNome.FillWeight = 120F;
            this.ColumnNome.HeaderText = "Nome";
            this.ColumnNome.Name = "ColumnNome";
            this.ColumnNome.Width = 120;
            // 
            // ColumnVisualizar
            // 
            this.ColumnVisualizar.FillWeight = 70F;
            this.ColumnVisualizar.HeaderText = "Visualizar Registo";
            this.ColumnVisualizar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnVisualizar.Name = "ColumnVisualizar";
            this.ColumnVisualizar.Width = 70;
            // 
            // ColumnApagar
            // 
            this.ColumnApagar.FillWeight = 70F;
            this.ColumnApagar.HeaderText = "Apagar Registo";
            this.ColumnApagar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnApagar.Name = "ColumnApagar";
            this.ColumnApagar.Width = 70;
            // 
            // ViewListarRegistos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 406);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox_home);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(641, 445);
            this.Name = "ViewListarRegistos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Listar Registos";
            this.Load += new System.EventHandler(this.ViewListarRegistos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_home)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox_home;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewRegistos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerfim;
        private System.Windows.Forms.DateTimePicker dateTimePickerini;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemoverFiltro;
        private System.Windows.Forms.CheckBox checkBoxEMG;
        private System.Windows.Forms.CheckBox checkBoxBATT;
        private System.Windows.Forms.CheckBox checkBoxEDA;
        private System.Windows.Forms.CheckBox checkBoxAAC;
        private System.Windows.Forms.CheckBox checkBoxLux;
        private System.Windows.Forms.CheckBox checkBoxECG;
        private System.Windows.Forms.CheckBox checkBoxNome;
        private System.Windows.Forms.CheckBox checkBoxIDUtilizador;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.CheckBox checkBoxIdRegisto;
        private System.Windows.Forms.TextBox textBoxRegisto;
        private System.Windows.Forms.TextBox textBoxIDutilizador;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIDRegisto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDataInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDataFim;
        private System.Windows.Forms.DataGridViewImageColumn ColumnFoto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIDUtilizador;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNome;
        private System.Windows.Forms.DataGridViewImageColumn ColumnVisualizar;
        private System.Windows.Forms.DataGridViewImageColumn ColumnApagar;
    }
}