namespace DershaneOtomasyonu
{
    partial class OgrenciKayitUC
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
            txtSifre = new TextBox();
            cmbDanisman = new ComboBox();
            cmbSinif = new ComboBox();
            txtAd = new TextBox();
            txtTC = new TextBox();
            txtSoyad = new TextBox();
            btnOgrenciKaydet = new Button();
            btnTemizle = new Button();
            btnYenile = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(492, 201);
            txtSifre.Name = "txtSifre";
            txtSifre.Size = new Size(125, 27);
            txtSifre.TabIndex = 0;
            // 
            // cmbDanisman
            // 
            cmbDanisman.FormattingEnabled = true;
            cmbDanisman.Location = new Point(275, 122);
            cmbDanisman.Name = "cmbDanisman";
            cmbDanisman.Size = new Size(151, 28);
            cmbDanisman.TabIndex = 1;
            // 
            // cmbSinif
            // 
            cmbSinif.FormattingEnabled = true;
            cmbSinif.Location = new Point(275, 200);
            cmbSinif.Name = "cmbSinif";
            cmbSinif.Size = new Size(151, 28);
            cmbSinif.TabIndex = 2;
            // 
            // txtAd
            // 
            txtAd.Location = new Point(81, 123);
            txtAd.Name = "txtAd";
            txtAd.Size = new Size(125, 27);
            txtAd.TabIndex = 3;
            // 
            // txtTC
            // 
            txtTC.Location = new Point(492, 123);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(125, 27);
            txtTC.TabIndex = 4;
            txtTC.KeyPress += txtTC_KeyPress;
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new Point(81, 201);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Size = new Size(125, 27);
            txtSoyad.TabIndex = 7;
            // 
            // btnOgrenciKaydet
            // 
            btnOgrenciKaydet.Location = new Point(81, 302);
            btnOgrenciKaydet.Name = "btnOgrenciKaydet";
            btnOgrenciKaydet.Size = new Size(94, 29);
            btnOgrenciKaydet.TabIndex = 8;
            btnOgrenciKaydet.Text = "Kaydet";
            btnOgrenciKaydet.UseVisualStyleBackColor = true;
            btnOgrenciKaydet.Click += btnOgrenciKaydet_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.Location = new Point(362, 302);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(94, 29);
            btnTemizle.TabIndex = 9;
            btnTemizle.Text = "Temizle";
            btnTemizle.UseVisualStyleBackColor = true;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // btnYenile
            // 
            btnYenile.Location = new Point(222, 302);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(94, 29);
            btnYenile.TabIndex = 10;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = true;
            btnYenile.Click += btnYenile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(492, 178);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 11;
            label1.Text = "Şifre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(275, 88);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 12;
            label2.Text = "Danışman:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(81, 88);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 14;
            label4.Text = "İsim:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(275, 168);
            label5.Name = "label5";
            label5.Size = new Size(41, 20);
            label5.TabIndex = 15;
            label5.Text = "Sınıf:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(492, 88);
            label6.Name = "label6";
            label6.Size = new Size(28, 20);
            label6.TabIndex = 16;
            label6.Text = "TC:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(81, 168);
            label8.Name = "label8";
            label8.Size = new Size(63, 20);
            label8.TabIndex = 18;
            label8.Text = "Soyisim:";
            // 
            // OgrenciKayitUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnYenile);
            Controls.Add(btnTemizle);
            Controls.Add(btnOgrenciKaydet);
            Controls.Add(txtSoyad);
            Controls.Add(txtTC);
            Controls.Add(txtAd);
            Controls.Add(cmbSinif);
            Controls.Add(cmbDanisman);
            Controls.Add(txtSifre);
            Name = "OgrenciKayitUC";
            Size = new Size(879, 752);
            Load += OgrenciKayitUC_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSifre;
        private ComboBox cmbDanisman;
        private ComboBox cmbSinif;
        private TextBox txtAd;
        private TextBox txtTC;
        private TextBox txtSoyad;
        private Button btnOgrenciKaydet;
        private Button btnTemizle;
        private Button btnYenile;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
    }
}
