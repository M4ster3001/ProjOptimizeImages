namespace ImageTools
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbPastas = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.boxDestino = new System.Windows.Forms.TextBox();
            this.boxOrigem = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cbQualidade = new System.Windows.Forms.ComboBox();
            this.gbQualidade = new System.Windows.Forms.GroupBox();
            this.folderBrowserOrigem = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbProgressBar = new System.Windows.Forms.Label();
            this.lbQtde = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerBar = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDestino = new System.Windows.Forms.FolderBrowserDialog();
            this.gpCompressao = new System.Windows.Forms.GroupBox();
            this.cbCompression = new System.Windows.Forms.ComboBox();
            this.gbPastas.SuspendLayout();
            this.gbQualidade.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gpCompressao.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPastas
            // 
            this.gbPastas.Controls.Add(this.label4);
            this.gbPastas.Controls.Add(this.label3);
            this.gbPastas.Controls.Add(this.label2);
            this.gbPastas.Controls.Add(this.label1);
            this.gbPastas.Controls.Add(this.boxDestino);
            this.gbPastas.Controls.Add(this.boxOrigem);
            this.gbPastas.Location = new System.Drawing.Point(12, 12);
            this.gbPastas.Name = "gbPastas";
            this.gbPastas.Size = new System.Drawing.Size(430, 153);
            this.gbPastas.TabIndex = 0;
            this.gbPastas.TabStop = false;
            this.gbPastas.Text = "Pastas";
            this.gbPastas.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(401, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "...";
            this.label4.Click += new System.EventHandler(this.BrowserDialogDestino);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(401, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "...";
            this.label3.Click += new System.EventHandler(this.BrowserDialogOrigem);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Origem";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destino";
            // 
            // boxDestino
            // 
            this.boxDestino.Location = new System.Drawing.Point(62, 83);
            this.boxDestino.Name = "boxDestino";
            this.boxDestino.Size = new System.Drawing.Size(333, 23);
            this.boxDestino.TabIndex = 1;
            // 
            // boxOrigem
            // 
            this.boxOrigem.Location = new System.Drawing.Point(62, 25);
            this.boxOrigem.Name = "boxOrigem";
            this.boxOrigem.Size = new System.Drawing.Size(333, 23);
            this.boxOrigem.TabIndex = 0;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(12, 403);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(110, 35);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Executar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar1.Location = new System.Drawing.Point(6, 381);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(334, 39);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // cbQualidade
            // 
            this.cbQualidade.FormattingEnabled = true;
            this.cbQualidade.Items.AddRange(new object[] {
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%"});
            this.cbQualidade.Location = new System.Drawing.Point(6, 39);
            this.cbQualidade.Name = "cbQualidade";
            this.cbQualidade.Size = new System.Drawing.Size(158, 23);
            this.cbQualidade.TabIndex = 3;
            // 
            // gbQualidade
            // 
            this.gbQualidade.Controls.Add(this.cbQualidade);
            this.gbQualidade.Location = new System.Drawing.Point(12, 218);
            this.gbQualidade.Name = "gbQualidade";
            this.gbQualidade.Size = new System.Drawing.Size(190, 91);
            this.gbQualidade.TabIndex = 4;
            this.gbQualidade.TabStop = false;
            this.gbQualidade.Text = "Qualidade(.jpeg, .jpg ou .png)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbProgressBar);
            this.groupBox1.Controls.Add(this.lbQtde);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(482, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 426);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagens carregadas";
            // 
            // lbProgressBar
            // 
            this.lbProgressBar.AutoSize = true;
            this.lbProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.lbProgressBar.Location = new System.Drawing.Point(129, 391);
            this.lbProgressBar.Name = "lbProgressBar";
            this.lbProgressBar.Size = new System.Drawing.Size(0, 15);
            this.lbProgressBar.TabIndex = 6;
            // 
            // lbQtde
            // 
            this.lbQtde.AutoSize = true;
            this.lbQtde.Location = new System.Drawing.Point(6, 348);
            this.lbQtde.Name = "lbQtde";
            this.lbQtde.Size = new System.Drawing.Size(0, 15);
            this.lbQtde.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(334, 323);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timerBar
            // 
            this.timerBar.Tick += new System.EventHandler(this.timerBar_Tick);
            // 
            // gpCompressao
            // 
            this.gpCompressao.Controls.Add(this.cbCompression);
            this.gpCompressao.Location = new System.Drawing.Point(238, 218);
            this.gpCompressao.Name = "gpCompressao";
            this.gpCompressao.Size = new System.Drawing.Size(204, 91);
            this.gpCompressao.TabIndex = 6;
            this.gpCompressao.TabStop = false;
            this.gpCompressao.Text = "Tipo de compressão(.tiff)";
            // 
            // cbCompression
            // 
            this.cbCompression.FormattingEnabled = true;
            this.cbCompression.Items.AddRange(new object[] {
            "LZW",
            "CCITT3",
            "CCITT4",
            "Rle"});
            this.cbCompression.Location = new System.Drawing.Point(6, 39);
            this.cbCompression.Name = "cbCompression";
            this.cbCompression.Size = new System.Drawing.Size(192, 23);
            this.cbCompression.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 450);
            this.Controls.Add(this.gpCompressao);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbQualidade);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.gbPastas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbPastas.ResumeLayout(false);
            this.gbPastas.PerformLayout();
            this.gbQualidade.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpCompressao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPastas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxDestino;
        private System.Windows.Forms.TextBox boxOrigem;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cbQualidade;
        private System.Windows.Forms.GroupBox gbQualidade;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserOrigem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lbQtde;
        private System.Windows.Forms.Timer timerBar;
        private System.Windows.Forms.Label lbProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDestino;
        private System.Windows.Forms.GroupBox gpCompressao;
        private System.Windows.Forms.ComboBox cbCompression;
    }
}

