namespace DershaneOtomasyonu
{
    partial class TaleplerUC
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            dgvTalepler = new DataGridView();
            cmbOgretmenler = new ComboBox();
            cmbTalepTuru = new ComboBox();
            txtAciklama = new TextBox();
            btnTalepOlustur = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTalepler).BeginInit();
            SuspendLayout();
            // 
            // dgvTalepler
            // 
            dgvTalepler.AllowUserToAddRows = false;
            dgvTalepler.AllowUserToDeleteRows = false;
            dgvTalepler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTalepler.Location = new Point(46, 46);
            dgvTalepler.Name = "dgvTalepler";
            dgvTalepler.ReadOnly = true;
            dgvTalepler.RowHeadersWidth = 51;
            dgvTalepler.RowTemplate.Height = 29;
            dgvTalepler.Size = new Size(790, 231);
            dgvTalepler.TabIndex = 0;
            // 
            // cmbOgretmenler
            // 
            cmbOgretmenler.FormattingEnabled = true;
            cmbOgretmenler.Location = new Point(79, 437);
            cmbOgretmenler.Name = "cmbOgretmenler";
            cmbOgretmenler.Size = new Size(151, 28);
            cmbOgretmenler.TabIndex = 1;
            // 
            // cmbTalepTuru
            // 
            cmbTalepTuru.FormattingEnabled = true;
            cmbTalepTuru.Location = new Point(285, 437);
            cmbTalepTuru.Name = "cmbTalepTuru";
            cmbTalepTuru.Size = new Size(151, 28);
            cmbTalepTuru.TabIndex = 2;
            cmbTalepTuru.SelectedIndexChanged += cmbTalepTuru_SelectedIndexChanged;
            // 
            // txtAciklama
            // 
            txtAciklama.Location = new Point(488, 438);
            txtAciklama.Name = "txtAciklama";
            txtAciklama.Size = new Size(125, 27);
            txtAciklama.TabIndex = 3;
            // 
            // btnTalepOlustur
            // 
            btnTalepOlustur.Location = new Point(663, 436);
            btnTalepOlustur.Name = "btnTalepOlustur";
            btnTalepOlustur.Size = new Size(151, 29);
            btnTalepOlustur.TabIndex = 4;
            btnTalepOlustur.Text = "Talep Ekle";
            btnTalepOlustur.UseVisualStyleBackColor = true;
            btnTalepOlustur.Click += btnTalepOlustur_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 403);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 5;
            label1.Text = "Öğretmen:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(292, 403);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 6;
            label2.Text = "Talep Türü:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(488, 403);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 7;
            label3.Text = "Açıklama:";
            // 
            // TaleplerUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnTalepOlustur);
            Controls.Add(txtAciklama);
            Controls.Add(cmbTalepTuru);
            Controls.Add(cmbOgretmenler);
            Controls.Add(dgvTalepler);
            Name = "TaleplerUC";
            Size = new Size(879, 752);
            Load += TaleplerUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTalepler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTalepler;
        private ComboBox cmbOgretmenler;
        private ComboBox cmbTalepTuru;
        private TextBox txtAciklama;
        private Button btnTalepOlustur;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
