namespace DershaneOtomasyonu
{
    partial class OdevlerUC
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
            ((System.ComponentModel.ISupportInitialize)dgvOdevler).BeginInit();
            SuspendLayout();
            // 
            // dgvOdevler
            // 
            dgvOdevler.AllowUserToAddRows = false;
            dgvOdevler.AllowUserToDeleteRows = false;
            dgvOdevler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOdevler.Location = new Point(0, 0);
            dgvOdevler.Name = "dgvOdevler";
            dgvOdevler.ReadOnly = true;
            dgvOdevler.RowHeadersWidth = 51;
            dgvOdevler.RowTemplate.Height = 29;
            dgvOdevler.Size = new Size(879, 752);
            dgvOdevler.TabIndex = 0;
            // 
            // OdevlerUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvOdevler);
            Name = "OdevlerUC";
            Size = new Size(879, 752);
            Load += OdevlerUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOdevler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvOdevler;
    }
}
