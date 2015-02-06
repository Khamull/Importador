namespace ECOLABOR.Apresentacao
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tSMenu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.novaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listaDePlanilhasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroDeUsuáriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senhaArquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseDeDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMenu1,
            this.administraçãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.tSMenu1;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(485, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.TabStop = true;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tSMenu1
            // 
            this.tSMenu1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaToolStripMenuItem,
            this.importarToolStripMenuItem,
            this.listaDePlanilhasToolStripMenuItem});
            this.tSMenu1.Name = "tSMenu1";
            this.tSMenu1.Size = new System.Drawing.Size(50, 20);
            this.tSMenu1.Text = "Menu";
            // 
            // novaToolStripMenuItem
            // 
            this.novaToolStripMenuItem.Name = "novaToolStripMenuItem";
            this.novaToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.novaToolStripMenuItem.Text = "Cadastrar Nova Planilha";
            this.novaToolStripMenuItem.Click += new System.EventHandler(this.novaToolStripMenuItem_Click);
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.importarToolStripMenuItem.Text = "Importar";
            this.importarToolStripMenuItem.Click += new System.EventHandler(this.importarToolStripMenuItem_Click);
            // 
            // listaDePlanilhasToolStripMenuItem
            // 
            this.listaDePlanilhasToolStripMenuItem.Name = "listaDePlanilhasToolStripMenuItem";
            this.listaDePlanilhasToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.listaDePlanilhasToolStripMenuItem.Text = "Lista de Planilhas";
            this.listaDePlanilhasToolStripMenuItem.Click += new System.EventHandler(this.listaDePlanilhasToolStripMenuItem_Click);
            // 
            // administraçãoToolStripMenuItem
            // 
            this.administraçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroDeUsuáriosToolStripMenuItem,
            this.configuraçõesToolStripMenuItem});
            this.administraçãoToolStripMenuItem.Name = "administraçãoToolStripMenuItem";
            this.administraçãoToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.administraçãoToolStripMenuItem.Text = "Administração";
            // 
            // cadastroDeUsuáriosToolStripMenuItem
            // 
            this.cadastroDeUsuáriosToolStripMenuItem.Name = "cadastroDeUsuáriosToolStripMenuItem";
            this.cadastroDeUsuáriosToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.cadastroDeUsuáriosToolStripMenuItem.Text = "Cadastro de Usuários";
            this.cadastroDeUsuáriosToolStripMenuItem.Click += new System.EventHandler(this.cadastroDeUsuáriosToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.senhaArquivosToolStripMenuItem,
            this.baseDeDadosToolStripMenuItem});
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.configuraçõesToolStripMenuItem_Click);
            // 
            // senhaArquivosToolStripMenuItem
            // 
            this.senhaArquivosToolStripMenuItem.Name = "senhaArquivosToolStripMenuItem";
            this.senhaArquivosToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.senhaArquivosToolStripMenuItem.Text = "Senha Arquivos";
            this.senhaArquivosToolStripMenuItem.Click += new System.EventHandler(this.senhaArquivosToolStripMenuItem_Click);
            // 
            // baseDeDadosToolStripMenuItem
            // 
            this.baseDeDadosToolStripMenuItem.Name = "baseDeDadosToolStripMenuItem";
            this.baseDeDadosToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.baseDeDadosToolStripMenuItem.Text = "Base de Dados";
            this.baseDeDadosToolStripMenuItem.Click += new System.EventHandler(this.baseDeDadosToolStripMenuItem_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(485, 262);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIP - Ecolabor - 1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tSMenu1;
        private System.Windows.Forms.ToolStripMenuItem novaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listaDePlanilhasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administraçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastroDeUsuáriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem senhaArquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseDeDadosToolStripMenuItem;
    }
}