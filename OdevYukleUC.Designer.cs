namespace DershaneOtomasyonu
{
    partial class OdevYukleUC
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
            dgvOdevler = new DataGridView();
            odevEkleBtn = new Button();
            odevAciklamaTxt = new TextBox();
            teslimTarihi = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvOdevler).BeginInit();
            SuspendLayout();
            // 
            // dgvOdevler
            // 
            dgvOdevler.AllowUserToAddRows = false;
            dgvOdevler.AllowUserToDeleteRows = false;
            dgvOdevler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOdevler.Location = new Point(64, 53);
            dgvOdevler.Name = "dgvOdevler";
            dgvOdevler.ReadOnly = true;
            dgvOdevler.RowHeadersWidth = 51;
            dgvOdevler.RowTemplate.Height = 29;
            dgvOdevler.Size = new Size(753, 200);
            dgvOdevler.TabIndex = 0;
            // 
            // odevEkleBtn
            // 
            odevEkleBtn.Location = new Point(358, 473);
            odevEkleBtn.Name = "odevEkleBtn";
            odevEkleBtn.Size = new Size(94, 29);
            odevEkleBtn.TabIndex = 1;
            odevEkleBtn.Text = "button1";
            odevEkleBtn.UseVisualStyleBackColor = true;
            odevEkleBtn.Click += odevEkleBtn_Click;
            // 
            // odevAciklamaTxt
            // 
            odevAciklamaTxt.Location = new Point(518, 348);
            odevAciklamaTxt.Name = "odevAciklamaTxt";
            odevAciklamaTxt.Size = new Size(125, 27);
            odevAciklamaTxt.TabIndex = 2;
            // 
            // teslimTarihi
            // 
            teslimTarihi.Location = new Point(104, 348);
            teslimTarihi.Name = "teslimTarihi";
            teslimTarihi.Size = new Size(250, 27);
            teslimTarihi.TabIndex = 3;
            // 
            // OdevYukleUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(teslimTarihi);
            Controls.Add(odevAciklamaTxt);
            Controls.Add(odevEkleBtn);
            Controls.Add(dgvOdevler);
            Name = "OdevYukleUC";
            Size = new Size(879, 752);
            ((System.ComponentModel.ISupportInitialize)dgvOdevler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOdevler;
        private Button odevEkleBtn;
        private TextBox odevAciklamaTxt;
        private DateTimePicker teslimTarihi;
    }
}
