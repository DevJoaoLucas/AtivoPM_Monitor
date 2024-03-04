namespace AtivoPM
{
    partial class FormMonitor
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonitor));
            this.panelCabecalho = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lblStatusAtivoPM = new System.Windows.Forms.Label();
            this.lblInformativo = new System.Windows.Forms.Label();
            this.buttonSuporte = new System.Windows.Forms.Button();
            this.txtBoxSupPss = new System.Windows.Forms.TextBox();
            this.panelCabecalho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCabecalho
            // 
            this.panelCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(68)))));
            this.panelCabecalho.Controls.Add(this.lblTitulo);
            this.panelCabecalho.Controls.Add(this.buttonExit);
            this.panelCabecalho.Location = new System.Drawing.Point(-15, 0);
            this.panelCabecalho.Margin = new System.Windows.Forms.Padding(0);
            this.panelCabecalho.Name = "panelCabecalho";
            this.panelCabecalho.Size = new System.Drawing.Size(632, 36);
            this.panelCabecalho.TabIndex = 1;
            this.panelCabecalho.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCabecalho_MouseMove);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(27, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(158, 19);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Ativo Process Monitor";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(68)))));
            this.buttonExit.BackgroundImage = global::AtivoPM.Properties.Resources.kisspng_basic_ui_set_icon_close_icon_5d40760a2b36f6_483324381564505610177;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.CausesValidation = false;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(68)))));
            this.buttonExit.Location = new System.Drawing.Point(600, 5);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(19, 27);
            this.buttonExit.TabIndex = 0;
            this.buttonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonExit.UseMnemonic = false;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 69);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(446, 259);
            this.dataGridView.TabIndex = 7;
            // 
            // lblStatusAtivoPM
            // 
            this.lblStatusAtivoPM.AutoSize = true;
            this.lblStatusAtivoPM.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusAtivoPM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusAtivoPM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStatusAtivoPM.Location = new System.Drawing.Point(12, 47);
            this.lblStatusAtivoPM.Name = "lblStatusAtivoPM";
            this.lblStatusAtivoPM.Size = new System.Drawing.Size(59, 19);
            this.lblStatusAtivoPM.TabIndex = 8;
            this.lblStatusAtivoPM.Text = "STATUS";
            // 
            // lblInformativo
            // 
            this.lblInformativo.AutoSize = true;
            this.lblInformativo.BackColor = System.Drawing.Color.Transparent;
            this.lblInformativo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInformativo.Location = new System.Drawing.Point(12, 331);
            this.lblInformativo.Name = "lblInformativo";
            this.lblInformativo.Size = new System.Drawing.Size(249, 19);
            this.lblInformativo.TabIndex = 9;
            this.lblInformativo.Text = "Desenvolvedor responsável: João Lucas";
            // 
            // buttonSuporte
            // 
            this.buttonSuporte.Location = new System.Drawing.Point(463, 294);
            this.buttonSuporte.Name = "buttonSuporte";
            this.buttonSuporte.Size = new System.Drawing.Size(140, 23);
            this.buttonSuporte.TabIndex = 10;
            this.buttonSuporte.Text = "Suporte Desenvolvimento";
            this.buttonSuporte.UseVisualStyleBackColor = true;
            this.buttonSuporte.Click += new System.EventHandler(this.buttonSuporte_Click);
            // 
            // txtBoxSupPss
            // 
            this.txtBoxSupPss.Location = new System.Drawing.Point(479, 323);
            this.txtBoxSupPss.Name = "txtBoxSupPss";
            this.txtBoxSupPss.PasswordChar = '*';
            this.txtBoxSupPss.Size = new System.Drawing.Size(100, 22);
            this.txtBoxSupPss.TabIndex = 11;
            // 
            // FormMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(615, 353);
            this.Controls.Add(this.txtBoxSupPss);
            this.Controls.Add(this.buttonSuporte);
            this.Controls.Add(this.lblInformativo);
            this.Controls.Add(this.lblStatusAtivoPM);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panelCabecalho);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMonitor";
            this.Text = "Form1";
            this.panelCabecalho.ResumeLayout(false);
            this.panelCabecalho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelCabecalho;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblStatusAtivoPM;
        private System.Windows.Forms.Label lblInformativo;
        private System.Windows.Forms.Button buttonSuporte;
        private System.Windows.Forms.TextBox txtBoxSupPss;
    }
}

