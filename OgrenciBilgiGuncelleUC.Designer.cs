namespace DershaneOtomasyonu
{
    partial class OgrenciBilgiGuncelleUC
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
            cmbDanisman = new ComboBox();
            dgvOgrenciler = new DataGridView();
            txtSoyad = new TextBox();
            txtTC = new TextBox();
            txtAd = new TextBox();
            chkAktif = new CheckBox();
            txtSinif = new TextBox();
            btnYenile = new Button();
            btnTemizle = new Button();
            btnAra = new Button();
            txtAraTC = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnGuncelle = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOgrenciler).BeginInit();
            SuspendLayout();
            // 
            // cmbDanisman
            // 
            cmbDanisman.FormattingEnabled = true;
            cmbDanisman.Location = new Point(36, 697);
            cmbDanisman.Name = "cmbDanisman";
            cmbDanisman.Size = new Size(151, 28);
            cmbDanisman.TabIndex = 0;
            // 
            // dgvOgrenciler
            // 
            dgvOgrenciler.AllowUserToAddRows = false;
            dgvOgrenciler.AllowUserToDeleteRows = false;
            dgvOgrenciler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOgrenciler.Location = new Point(34, 24);
            dgvOgrenciler.Name = "dgvOgrenciler";
            dgvOgrenciler.ReadOnly = true;
            dgvOgrenciler.RowHeadersWidth = 51;
            dgvOgrenciler.RowTemplate.Height = 29;
            dgvOgrenciler.Size = new Size(815, 473);
            dgvOgrenciler.TabIndex = 1;
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new Point(36, 623);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Size = new Size(125, 27);
            txtSoyad.TabIndex = 12;
            // 
            // txtTC
            // 
            txtTC.Location = new Point(183, 623);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(125, 27);
            txtTC.TabIndex = 9;
            // 
            // txtAd
            // 
            txtAd.Location = new Point(36, 545);
            txtAd.Name = "txtAd";
            txtAd.Size = new Size(125, 27);
            txtAd.TabIndex = 8;
            // 
            // chkAktif
            // 
            chkAktif.AutoSize = true;
            chkAktif.Location = new Point(218, 701);
            chkAktif.Name = "chkAktif";
            chkAktif.Size = new Size(90, 24);
            chkAktif.TabIndex = 13;
            chkAktif.Text = "Aktif mi?";
            chkAktif.UseVisualStyleBackColor = true;
            // 
            // txtSinif
            // 
            txtSinif.Location = new Point(183, 545);
            txtSinif.Name = "txtSinif";
            txtSinif.Size = new Size(125, 27);
            txtSinif.TabIndex = 14;
            // 
            // btnYenile
            // 
            btnYenile.Location = new Point(355, 545);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(94, 29);
            btnYenile.TabIndex = 17;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = true;
            btnYenile.Click += btnYenile_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.Location = new Point(355, 622);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(94, 29);
            btnTemizle.TabIndex = 16;
            btnTemizle.Text = "Temizle";
            btnTemizle.UseVisualStyleBackColor = true;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // btnAra
            // 
            btnAra.Location = new Point(725, 545);
            btnAra.Name = "btnAra";
            btnAra.Size = new Size(94, 29);
            btnAra.TabIndex = 15;
            btnAra.Text = "Ara";
            btnAra.UseVisualStyleBackColor = true;
            btnAra.Click += btnAra_Click;
            // 
            // txtAraTC
            // 
            txtAraTC.Location = new Point(574, 545);
            txtAraTC.Name = "txtAraTC";
            txtAraTC.Size = new Size(125, 27);
            txtAraTC.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 674);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 19;
            label1.Text = "Danışman:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 522);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 20;
            label2.Text = "İsim:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 600);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 21;
            label3.Text = "Soyisim:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(574, 511);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 22;
            label4.Text = "TC ile Ara:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(183, 600);
            label5.Name = "label5";
            label5.Size = new Size(28, 20);
            label5.TabIndex = 23;
            label5.Text = "TC:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(183, 522);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 24;
            label6.Text = "Sınıf:";
            // 
            // btnGuncelle
            // 
            btnGuncelle.Location = new Point(355, 696);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(94, 29);
            btnGuncelle.TabIndex = 25;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = true;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // OgrenciBilgiGuncelleUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnGuncelle);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtAraTC);
            Controls.Add(btnYenile);
            Controls.Add(btnTemizle);
            Controls.Add(btnAra);
            Controls.Add(txtSinif);
            Controls.Add(chkAktif);
            Controls.Add(txtSoyad);
            Controls.Add(txtTC);
            Controls.Add(txtAd);
            Controls.Add(dgvOgrenciler);
            Controls.Add(cmbDanisman);
            Name = "OgrenciBilgiGuncelleUC";
            Size = new Size(879, 752);
            Load += OgrenciBilgiGuncelleUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOgrenciler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbDanisman;
        private DataGridView dgvOgrenciler;
        private TextBox txtSoyad;
        private TextBox txtTC;
        private TextBox txtAd;
        private CheckBox chkAktif;
        private TextBox txtSinif;
        private Button btnYenile;
        private Button btnTemizle;
        private Button btnAra;
        private TextBox txtAraTC;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnGuncelle;
    }
}
