namespace ECOLABOR.Apresentacao.Administração
{
    partial class frmListaUsuários
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaUsuários));
            this.dtUsuarios = new System.Windows.Forms.DataGridView();
            this.sIPDataSet = new ECOLABOR.SIPDataSet();
            this.sIPDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sIPDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sIPDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtUsuarios
            // 
            this.dtUsuarios.AllowUserToAddRows = false;
            this.dtUsuarios.AllowUserToDeleteRows = false;
            this.dtUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtUsuarios.Location = new System.Drawing.Point(12, 77);
            this.dtUsuarios.Name = "dtUsuarios";
            this.dtUsuarios.ReadOnly = true;
            this.dtUsuarios.Size = new System.Drawing.Size(542, 190);
            this.dtUsuarios.TabIndex = 0;
            this.dtUsuarios.SelectionChanged += new System.EventHandler(this.dtUsuarios_SelectionChanged);
            // 
            // sIPDataSet
            // 
            this.sIPDataSet.DataSetName = "SIPDataSet";
            this.sIPDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sIPDataSetBindingSource
            // 
            this.sIPDataSetBindingSource.DataSource = this.sIPDataSet;
            this.sIPDataSetBindingSource.Position = 0;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(485, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(69, 59);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Selecionar";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // frmListaUsuários
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 279);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dtUsuarios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmListaUsuários";
            this.Text = "SIP - Lista de Usuários";
            this.Load += new System.EventHandler(this.frmListaUsuários_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sIPDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sIPDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtUsuarios;
        private System.Windows.Forms.BindingSource sIPDataSetBindingSource;
        private SIPDataSet sIPDataSet;
        private System.Windows.Forms.Button btnImport;

    }
}