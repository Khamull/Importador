namespace ECOLABOR.Apresentacao.Menu
{
    partial class frmListaModelos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaModelos));
            this.dtGVModelos = new System.Windows.Forms.DataGridView();
            this.btnexcluir = new System.Windows.Forms.Button();
            this.btnNovaPlanilha = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGVModelos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGVModelos
            // 
            this.dtGVModelos.AllowUserToAddRows = false;
            this.dtGVModelos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGVModelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGVModelos.Location = new System.Drawing.Point(5, 75);
            this.dtGVModelos.Name = "dtGVModelos";
            this.dtGVModelos.ReadOnly = true;
            this.dtGVModelos.Size = new System.Drawing.Size(441, 152);
            this.dtGVModelos.TabIndex = 0;
            this.dtGVModelos.SelectionChanged += new System.EventHandler(this.dtGVModelos_SelectionChanged);
            // 
            // btnexcluir
            // 
            this.btnexcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnexcluir.Image")));
            this.btnexcluir.Location = new System.Drawing.Point(314, 10);
            this.btnexcluir.Name = "btnexcluir";
            this.btnexcluir.Size = new System.Drawing.Size(63, 59);
            this.btnexcluir.TabIndex = 1;
            this.btnexcluir.Text = "Excluir";
            this.btnexcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnexcluir.UseVisualStyleBackColor = true;
            this.btnexcluir.Click += new System.EventHandler(this.btnexcluir_Click);
            // 
            // btnNovaPlanilha
            // 
            this.btnNovaPlanilha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovaPlanilha.Image = global::ECOLABOR.Properties.Resources.add1__1_;
            this.btnNovaPlanilha.Location = new System.Drawing.Point(245, 10);
            this.btnNovaPlanilha.Name = "btnNovaPlanilha";
            this.btnNovaPlanilha.Size = new System.Drawing.Size(63, 59);
            this.btnNovaPlanilha.TabIndex = 2;
            this.btnNovaPlanilha.Text = "Nova";
            this.btnNovaPlanilha.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNovaPlanilha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNovaPlanilha.UseVisualStyleBackColor = true;
            this.btnNovaPlanilha.Visible = false;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(383, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(63, 59);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Importar";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // frmListaModelos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 229);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnNovaPlanilha);
            this.Controls.Add(this.btnexcluir);
            this.Controls.Add(this.dtGVModelos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "frmListaModelos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SIP - Modelos Cadastrados";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmListaModelos_FormClosing);
            this.Load += new System.EventHandler(this.frmListaModelos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGVModelos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGVModelos;
        private System.Windows.Forms.Button btnexcluir;
        private System.Windows.Forms.Button btnNovaPlanilha;
        private System.Windows.Forms.Button btnImport;
    }
}