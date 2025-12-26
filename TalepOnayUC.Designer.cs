namespace DershaneOtomasyonu
{
    partial class TalepOnayUC
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
            btnOnayla = new Button();
            btnReddet = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTalepler).BeginInit();
            SuspendLayout();
            // 
            // dgvTalepler
            // 
            dgvTalepler.AllowUserToAddRows = false;
            dgvTalepler.AllowUserToDeleteRows = false;
            dgvTalepler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTalepler.Location = new Point(34, 27);
            dgvTalepler.Name = "dgvTalepler";
            dgvTalepler.ReadOnly = true;
            dgvTalepler.RowHeadersWidth = 51;
            dgvTalepler.RowTemplate.Height = 29;
            dgvTalepler.Size = new Size(818, 365);
            dgvTalepler.TabIndex = 0;
            // 
            // btnOnayla
            // 
            btnOnayla.Location = new Point(295, 435);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(94, 29);
            btnOnayla.TabIndex = 1;
            btnOnayla.Text = "Onayla";
            btnOnayla.UseVisualStyleBackColor = true;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // btnReddet
            // 
            btnReddet.Location = new Point(482, 435);
            btnReddet.Name = "btnReddet";
            btnReddet.Size = new Size(94, 29);
            btnReddet.TabIndex = 2;
            btnReddet.Text = "Reddet";
            btnReddet.UseVisualStyleBackColor = true;
            btnReddet.Click += btnReddet_Click;
            // 
            // TalepOnayUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnReddet);
            Controls.Add(btnOnayla);
            Controls.Add(dgvTalepler);
            Name = "TalepOnayUC";
            Size = new Size(879, 752);
            Load += TalepOnayUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTalepler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTalepler;
        private Button btnOnayla;
        private Button btnReddet;
    }
}
