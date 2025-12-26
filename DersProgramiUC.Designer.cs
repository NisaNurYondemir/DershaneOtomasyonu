namespace DershaneOtomasyonu
{
    partial class DersProgramiUC
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
            dgvDersProgrami = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDersProgrami).BeginInit();
            SuspendLayout();
            // 
            // dgvDersProgrami
            // 
            dgvDersProgrami.AllowUserToAddRows = false;
            dgvDersProgrami.AllowUserToDeleteRows = false;
            dgvDersProgrami.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDersProgrami.Location = new Point(0, 0);
            dgvDersProgrami.Name = "dgvDersProgrami";
            dgvDersProgrami.ReadOnly = true;
            dgvDersProgrami.RowHeadersWidth = 51;
            dgvDersProgrami.RowTemplate.Height = 29;
            dgvDersProgrami.Size = new Size(879, 752);
            dgvDersProgrami.TabIndex = 0;
            // 
            // DersProgramiUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvDersProgrami);
            Name = "DersProgramiUC";
            Size = new Size(879, 752);
            Load += DersProgramiUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDersProgrami).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDersProgrami;
    }
}
