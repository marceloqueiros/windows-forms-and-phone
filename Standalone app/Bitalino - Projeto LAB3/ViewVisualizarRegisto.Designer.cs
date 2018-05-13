namespace Bitalino___Projeto_LAB3
{
    partial class ViewVisualizarRegisto
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewVisualizarRegisto));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelIDRegisto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDataInicio = new System.Windows.Forms.Label();
            this.labelDataFim = new System.Windows.Forms.Label();
            this.textBoxIDRegisto = new System.Windows.Forms.TextBox();
            this.textBoxTipos = new System.Windows.Forms.TextBox();
            this.textBoxDataInicio = new System.Windows.Forms.TextBox();
            this.textBoxDataFim = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxFrameRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxDuracao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxValores = new System.Windows.Forms.GroupBox();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelNome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxNome = new System.Windows.Forms.TextBox();
            this.pictureBoxFoto = new System.Windows.Forms.PictureBox();
            this.labelProcura = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxValores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(716, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gráfico";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(134, 6);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(416, 293);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.pictureBox3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.groupBoxValores);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.textBoxDataFim);
            this.tabPage1.Controls.Add(this.textBoxDataInicio);
            this.tabPage1.Controls.Add(this.textBoxTipos);
            this.tabPage1.Controls.Add(this.textBoxIDRegisto);
            this.tabPage1.Controls.Add(this.labelDataFim);
            this.tabPage1.Controls.Add(this.labelDataInicio);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.labelIDRegisto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(716, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Registo";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // labelIDRegisto
            // 
            this.labelIDRegisto.AutoSize = true;
            this.labelIDRegisto.Location = new System.Drawing.Point(234, 18);
            this.labelIDRegisto.Name = "labelIDRegisto";
            this.labelIDRegisto.Size = new System.Drawing.Size(60, 13);
            this.labelIDRegisto.TabIndex = 36;
            this.labelIDRegisto.Text = "ID Registo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Tipos:";
            // 
            // labelDataInicio
            // 
            this.labelDataInicio.AutoSize = true;
            this.labelDataInicio.Location = new System.Drawing.Point(388, 19);
            this.labelDataInicio.Name = "labelDataInicio";
            this.labelDataInicio.Size = new System.Drawing.Size(60, 13);
            this.labelDataInicio.TabIndex = 41;
            this.labelDataInicio.Text = "Data inicio:";
            // 
            // labelDataFim
            // 
            this.labelDataFim.AutoSize = true;
            this.labelDataFim.Location = new System.Drawing.Point(399, 55);
            this.labelDataFim.Name = "labelDataFim";
            this.labelDataFim.Size = new System.Drawing.Size(49, 13);
            this.labelDataFim.TabIndex = 42;
            this.labelDataFim.Text = "Data fim:";
            // 
            // textBoxIDRegisto
            // 
            this.textBoxIDRegisto.Location = new System.Drawing.Point(304, 16);
            this.textBoxIDRegisto.Name = "textBoxIDRegisto";
            this.textBoxIDRegisto.ReadOnly = true;
            this.textBoxIDRegisto.Size = new System.Drawing.Size(52, 20);
            this.textBoxIDRegisto.TabIndex = 38;
            // 
            // textBoxTipos
            // 
            this.textBoxTipos.Location = new System.Drawing.Point(115, 355);
            this.textBoxTipos.Name = "textBoxTipos";
            this.textBoxTipos.ReadOnly = true;
            this.textBoxTipos.Size = new System.Drawing.Size(224, 20);
            this.textBoxTipos.TabIndex = 38;
            // 
            // textBoxDataInicio
            // 
            this.textBoxDataInicio.Location = new System.Drawing.Point(454, 16);
            this.textBoxDataInicio.Name = "textBoxDataInicio";
            this.textBoxDataInicio.ReadOnly = true;
            this.textBoxDataInicio.Size = new System.Drawing.Size(208, 20);
            this.textBoxDataInicio.TabIndex = 43;
            // 
            // textBoxDataFim
            // 
            this.textBoxDataFim.Location = new System.Drawing.Point(454, 52);
            this.textBoxDataFim.Name = "textBoxDataFim";
            this.textBoxDataFim.ReadOnly = true;
            this.textBoxDataFim.Size = new System.Drawing.Size(208, 20);
            this.textBoxDataFim.TabIndex = 44;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxFrameRate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(249, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(107, 85);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frame rate";
            // 
            // textBoxFrameRate
            // 
            this.textBoxFrameRate.Location = new System.Drawing.Point(16, 30);
            this.textBoxFrameRate.Name = "textBoxFrameRate";
            this.textBoxFrameRate.ReadOnly = true;
            this.textBoxFrameRate.Size = new System.Drawing.Size(74, 22);
            this.textBoxFrameRate.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 49;
            this.label4.Text = "por segundo";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxDuracao);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(249, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(107, 87);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Duração";
            // 
            // textBoxDuracao
            // 
            this.textBoxDuracao.Location = new System.Drawing.Point(16, 31);
            this.textBoxDuracao.Name = "textBoxDuracao";
            this.textBoxDuracao.ReadOnly = true;
            this.textBoxDuracao.Size = new System.Drawing.Size(74, 22);
            this.textBoxDuracao.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 49;
            this.label2.Text = "milisegundos";
            // 
            // groupBoxValores
            // 
            this.groupBoxValores.Controls.Add(this.listBoxLogs);
            this.groupBoxValores.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxValores.Location = new System.Drawing.Point(388, 84);
            this.groupBoxValores.Name = "groupBoxValores";
            this.groupBoxValores.Size = new System.Drawing.Size(302, 346);
            this.groupBoxValores.TabIndex = 48;
            this.groupBoxValores.TabStop = false;
            this.groupBoxValores.Text = "Valores";
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.ItemHeight = 16;
            this.listBoxLogs.Location = new System.Drawing.Point(3, 18);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(296, 325);
            this.listBoxLogs.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 55);
            this.button1.TabIndex = 55;
            this.button1.Text = "Eliminar Registo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(677, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 30);
            this.pictureBox3.TabIndex = 25;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelProcura);
            this.groupBox4.Controls.Add(this.pictureBoxFoto);
            this.groupBox4.Controls.Add(this.textBoxNome);
            this.groupBox4.Controls.Add(this.textBoxID);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.labelNome);
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 212);
            this.groupBox4.TabIndex = 57;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Utilizador";
            // 
            // labelNome
            // 
            this.labelNome.AutoSize = true;
            this.labelNome.Location = new System.Drawing.Point(6, 170);
            this.labelNome.Name = "labelNome";
            this.labelNome.Size = new System.Drawing.Size(38, 13);
            this.labelNome.TabIndex = 47;
            this.labelNome.Text = "Nome:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "ID:";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(48, 142);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(60, 20);
            this.textBoxID.TabIndex = 54;
            // 
            // textBoxNome
            // 
            this.textBoxNome.Location = new System.Drawing.Point(48, 167);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.ReadOnly = true;
            this.textBoxNome.Size = new System.Drawing.Size(145, 20);
            this.textBoxNome.TabIndex = 54;
            // 
            // pictureBoxFoto
            // 
            this.pictureBoxFoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxFoto.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFoto.Image")));
            this.pictureBoxFoto.Location = new System.Drawing.Point(39, 16);
            this.pictureBoxFoto.Name = "pictureBoxFoto";
            this.pictureBoxFoto.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFoto.TabIndex = 55;
            this.pictureBoxFoto.TabStop = false;
            // 
            // labelProcura
            // 
            this.labelProcura.AutoSize = true;
            this.labelProcura.BackColor = System.Drawing.Color.White;
            this.labelProcura.ForeColor = System.Drawing.Color.White;
            this.labelProcura.Location = new System.Drawing.Point(73, 191);
            this.labelProcura.Name = "labelProcura";
            this.labelProcura.Size = new System.Drawing.Size(35, 13);
            this.labelProcura.TabIndex = 56;
            this.labelProcura.Text = "label4";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(724, 462);
            this.tabControl1.TabIndex = 2;
            // 
            // ViewVisualizarRegisto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 467);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(741, 447);
            this.Name = "ViewVisualizarRegisto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualizar";
            this.Load += new System.EventHandler(this.ViewVisualizarRegisto_Load);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxValores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelProcura;
        private System.Windows.Forms.PictureBox pictureBoxFoto;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelNome;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxValores;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDuracao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFrameRate;
        private System.Windows.Forms.TextBox textBoxDataFim;
        private System.Windows.Forms.TextBox textBoxDataInicio;
        private System.Windows.Forms.TextBox textBoxTipos;
        private System.Windows.Forms.TextBox textBoxIDRegisto;
        private System.Windows.Forms.Label labelDataFim;
        private System.Windows.Forms.Label labelDataInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelIDRegisto;
        private System.Windows.Forms.TabControl tabControl1;
    }
}