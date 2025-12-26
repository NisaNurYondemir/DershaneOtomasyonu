namespace DershaneOtomasyonu
{
    partial class AnaSayfaUC
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
            dgvYakinOdevler = new DataGridView();
            dgvDuyurular = new DataGridView();
            dgvYeniOdevler = new DataGridView();
            lblKullaniciAdi = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvYakinOdevler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDuyurular).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvYeniOdevler).BeginInit();
            SuspendLayout();
            // 
            // dgvYakinOdevler
            // 
            dgvYakinOdevler.AllowUserToAddRows = false;
            dgvYakinOdevler.AllowUserToDeleteRows = false;
            dgvYakinOdevler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvYakinOdevler.Location = new Point(26, 293);
            dgvYakinOdevler.Name = "dgvYakinOdevler";
            dgvYakinOdevler.ReadOnly = true;
            dgvYakinOdevler.RowHeadersWidth = 51;
            dgvYakinOdevler.RowTemplate.Height = 29;
            dgvYakinOdevler.Size = new Size(822, 204);
            dgvYakinOdevler.TabIndex = 0;
            // 
            // dgvDuyurular
            // 
            dgvDuyurular.AllowUserToAddRows = false;
            dgvDuyurular.AllowUserToDeleteRows = false;
            dgvDuyurular.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDuyurular.Location = new Point(26, 67);
            dgvDuyurular.Name = "dgvDuyurular";
            dgvDuyurular.ReadOnly = true;
            dgvDuyurular.RowHeadersWidth = 51;
            dgvDuyurular.RowTemplate.Height = 29;
            dgvDuyurular.Size = new Size(822, 204);
            dgvDuyurular.TabIndex = 1;
            // 
            // dgvYeniOdevler
            // 
            dgvYeniOdevler.AllowUserToAddRows = false;
            dgvYeniOdevler.AllowUserToDeleteRows = false;
            dgvYeniOdevler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvYeniOdevler.Location = new Point(26, 525);
            dgvYeniOdevler.Name = "dgvYeniOdevler";
            dgvYeniOdevler.ReadOnly = true;
            dgvYeniOdevler.RowHeadersWidth = 51;
            dgvYeniOdevler.RowTemplate.Height = 29;
            dgvYeniOdevler.Size = new Size(822, 204);
            dgvYeniOdevler.TabIndex = 2;
            // 
            // lblKullaniciAdi
            // 
            lblKullaniciAdi.AutoSize = true;
            lblKullaniciAdi.Location = new Point(409, 20);
            lblKullaniciAdi.Name = "lblKullaniciAdi";
            lblKullaniciAdi.Size = new Size(50, 20);
            lblKullaniciAdi.TabIndex = 3;
            lblKullaniciAdi.Text = "label1";
            // 
            // AnaSayfaUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblKullaniciAdi);
            Controls.Add(dgvYeniOdevler);
            Controls.Add(dgvDuyurular);
            Controls.Add(dgvYakinOdevler);
            Name = "AnaSayfaUC";
            Size = new Size(879, 752);
            Load += AnaSayfaUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvYakinOdevler).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDuyurular).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvYeniOdevler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvYakinOdevler;
        private DataGridView dgvDuyurular;
        private DataGridView dgvYeniOdevler;
        private Label lblKullaniciAdi;
    }
}
