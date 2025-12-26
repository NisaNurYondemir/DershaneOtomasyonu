namespace DershaneOtomasyonu
{
    partial class DersProgramiOlusturUC
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
            cmbDersler = new ComboBox();
            cmbOgretmenler = new ComboBox();
            cmbDerslik = new ComboBox();
            cmbGun = new ComboBox();
            txtBaslangic = new TextBox();
            txtBitis = new TextBox();
            btnProgramEkle = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnYenile = new Button();
            SuspendLayout();
            // 
            // cmbDersler
            // 
            cmbDersler.FormattingEnabled = true;
            cmbDersler.Location = new Point(112, 196);
            cmbDersler.Name = "cmbDersler";
            cmbDersler.Size = new Size(151, 28);
            cmbDersler.TabIndex = 0;
            // 
            // cmbOgretmenler
            // 
            cmbOgretmenler.FormattingEnabled = true;
            cmbOgretmenler.Location = new Point(326, 196);
            cmbOgretmenler.Name = "cmbOgretmenler";
            cmbOgretmenler.Size = new Size(151, 28);
            cmbOgretmenler.TabIndex = 1;
            // 
            // cmbDerslik
            // 
            cmbDerslik.FormattingEnabled = true;
            cmbDerslik.Location = new Point(515, 196);
            cmbDerslik.Name = "cmbDerslik";
            cmbDerslik.Size = new Size(151, 28);
            cmbDerslik.TabIndex = 2;
            // 
            // cmbGun
            // 
            cmbGun.FormattingEnabled = true;
            cmbGun.Location = new Point(695, 196);
            cmbGun.Name = "cmbGun";
            cmbGun.Size = new Size(151, 28);
            cmbGun.TabIndex = 3;
            // 
            // txtBaslangic
            // 
            txtBaslangic.Location = new Point(112, 329);
            txtBaslangic.Name = "txtBaslangic";
            txtBaslangic.Size = new Size(125, 27);
            txtBaslangic.TabIndex = 4;
            txtBaslangic.Leave += txtBaslangic_Leave;
            // 
            // txtBitis
            // 
            txtBitis.Location = new Point(326, 329);
            txtBitis.Name = "txtBitis";
            txtBitis.Size = new Size(125, 27);
            txtBitis.TabIndex = 5;
            txtBitis.Leave += txtBitis_Leave;
            // 
            // btnProgramEkle
            // 
            btnProgramEkle.Location = new Point(606, 327);
            btnProgramEkle.Name = "btnProgramEkle";
            btnProgramEkle.Size = new Size(94, 29);
            btnProgramEkle.TabIndex = 6;
            btnProgramEkle.Text = "Ders Ekle";
            btnProgramEkle.UseVisualStyleBackColor = true;
            btnProgramEkle.Click += btnProgramEkle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(112, 163);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 7;
            label1.Text = "Ders:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(326, 163);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 8;
            label2.Text = "Öğretmen:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(515, 163);
            label3.Name = "label3";
            label3.Size = new Size(57, 20);
            label3.TabIndex = 9;
            label3.Text = "Derslik:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(695, 163);
            label4.Name = "label4";
            label4.Size = new Size(38, 20);
            label4.TabIndex = 10;
            label4.Text = "Gün:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(112, 281);
            label5.Name = "label5";
            label5.Size = new Size(112, 20);
            label5.TabIndex = 11;
            label5.Text = "Başlangıç Saati:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(326, 281);
            label6.Name = "label6";
            label6.Size = new Size(77, 20);
            label6.TabIndex = 12;
            label6.Text = "Bitiş Saati:";
            // 
            // btnYenile
            // 
            btnYenile.Location = new Point(724, 327);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(94, 29);
            btnYenile.TabIndex = 13;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = true;
            btnYenile.Click += btnYenile_Click;
            // 
            // DersProgramiOlusturUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnYenile);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnProgramEkle);
            Controls.Add(txtBitis);
            Controls.Add(txtBaslangic);
            Controls.Add(cmbGun);
            Controls.Add(cmbDerslik);
            Controls.Add(cmbOgretmenler);
            Controls.Add(cmbDersler);
            Name = "DersProgramiOlusturUC";
            Size = new Size(879, 752);
            Load += DersProgramiOlusturUC_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbDersler;
        private ComboBox cmbOgretmenler;
        private ComboBox cmbDerslik;
        private ComboBox cmbGun;
        private TextBox txtBaslangic;
        private TextBox txtBitis;
        private Button btnProgramEkle;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnYenile;
    }
}
