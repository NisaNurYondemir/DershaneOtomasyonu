namespace DershaneOtomasyonu
{
    partial class DuyuruOlusturUC
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
            cmbHedefKitle = new ComboBox();
            dgvDuyurular = new DataGridView();
            txtBaslik = new TextBox();
            txtIcerik = new TextBox();
            btnDuyuruEkle = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDuyurular).BeginInit();
            SuspendLayout();
            // 
            // cmbHedefKitle
            // 
            cmbHedefKitle.FormattingEnabled = true;
            cmbHedefKitle.Location = new Point(54, 397);
            cmbHedefKitle.Name = "cmbHedefKitle";
            cmbHedefKitle.Size = new Size(151, 28);
            cmbHedefKitle.TabIndex = 0;
            // 
            // dgvDuyurular
            // 
            dgvDuyurular.AllowUserToAddRows = false;
            dgvDuyurular.AllowUserToDeleteRows = false;
            dgvDuyurular.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDuyurular.Location = new Point(54, 64);
            dgvDuyurular.Name = "dgvDuyurular";
            dgvDuyurular.ReadOnly = true;
            dgvDuyurular.RowHeadersWidth = 51;
            dgvDuyurular.RowTemplate.Height = 29;
            dgvDuyurular.Size = new Size(767, 265);
            dgvDuyurular.TabIndex = 1;
            // 
            // txtBaslik
            // 
            txtBaslik.Location = new Point(272, 398);
            txtBaslik.Name = "txtBaslik";
            txtBaslik.Size = new Size(125, 27);
            txtBaslik.TabIndex = 2;
            // 
            // txtIcerik
            // 
            txtIcerik.Location = new Point(473, 398);
            txtIcerik.Name = "txtIcerik";
            txtIcerik.Size = new Size(125, 27);
            txtIcerik.TabIndex = 3;
            // 
            // btnDuyuruEkle
            // 
            btnDuyuruEkle.Location = new Point(676, 397);
            btnDuyuruEkle.Name = "btnDuyuruEkle";
            btnDuyuruEkle.Size = new Size(145, 28);
            btnDuyuruEkle.TabIndex = 4;
            btnDuyuruEkle.Text = "Duyuru Ekle";
            btnDuyuruEkle.UseVisualStyleBackColor = true;
            btnDuyuruEkle.Click += btnDuyuruEkle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 368);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 5;
            label1.Text = "Hedef Kitle:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(272, 368);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 6;
            label2.Text = "Başlık:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(473, 368);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 7;
            label3.Text = "İçerik:";
            // 
            // DuyuruOlusturUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDuyuruEkle);
            Controls.Add(txtIcerik);
            Controls.Add(txtBaslik);
            Controls.Add(dgvDuyurular);
            Controls.Add(cmbHedefKitle);
            Name = "DuyuruOlusturUC";
            Size = new Size(879, 752);
            Load += DuyuruOlusturUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDuyurular).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbHedefKitle;
        private DataGridView dgvDuyurular;
        private TextBox txtBaslik;
        private TextBox txtIcerik;
        private Button btnDuyuruEkle;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
