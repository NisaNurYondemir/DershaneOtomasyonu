namespace DershaneOtomasyonu
{
    partial class DenemelerUC
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
            dgvDenemeler = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDenemeler).BeginInit();
            SuspendLayout();
            // 
            // dgvDenemeler
            // 
            dgvDenemeler.AllowUserToAddRows = false;
            dgvDenemeler.AllowUserToDeleteRows = false;
            dgvDenemeler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDenemeler.Location = new Point(0, 0);
            dgvDenemeler.Name = "dgvDenemeler";
            dgvDenemeler.ReadOnly = true;
            dgvDenemeler.RowHeadersWidth = 51;
            dgvDenemeler.RowTemplate.Height = 29;
            dgvDenemeler.Size = new Size(879, 752);
            dgvDenemeler.TabIndex = 0;
            // 
            // DenemelerUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvDenemeler);
            Name = "DenemelerUC";
            Size = new Size(879, 752);
            Load += DenemelerUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDenemeler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDenemeler;
    }
}
