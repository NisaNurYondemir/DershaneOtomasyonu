namespace DershaneOtomasyonu
{
    partial class DersProgramOnayUC
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
            dgvDersProgram = new DataGridView();
            btnOnayla = new Button();
            btnReddet = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDersProgram).BeginInit();
            SuspendLayout();
            // 
            // dgvDersProgram
            // 
            dgvDersProgram.AllowUserToAddRows = false;
            dgvDersProgram.AllowUserToDeleteRows = false;
            dgvDersProgram.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDersProgram.Location = new Point(28, 34);
            dgvDersProgram.Name = "dgvDersProgram";
            dgvDersProgram.ReadOnly = true;
            dgvDersProgram.RowHeadersWidth = 51;
            dgvDersProgram.RowTemplate.Height = 29;
            dgvDersProgram.Size = new Size(823, 322);
            dgvDersProgram.TabIndex = 0;
            dgvDersProgram.CellClick += dgvDersProgram_CellClick;
            // 
            // btnOnayla
            // 
            btnOnayla.Location = new Point(143, 481);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(94, 29);
            btnOnayla.TabIndex = 1;
            btnOnayla.Text = "Onayla";
            btnOnayla.UseVisualStyleBackColor = true;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // btnReddet
            // 
            btnReddet.Location = new Point(316, 483);
            btnReddet.Name = "btnReddet";
            btnReddet.Size = new Size(94, 29);
            btnReddet.TabIndex = 2;
            btnReddet.Text = "Reddet";
            btnReddet.UseVisualStyleBackColor = true;
            btnReddet.Click += btnReddet_Click;
            // 
            // DersProgramOnayUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnReddet);
            Controls.Add(btnOnayla);
            Controls.Add(dgvDersProgram);
            Name = "DersProgramOnayUC";
            Size = new Size(879, 752);
            Load += DersProgramOnayUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDersProgram).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDersProgram;
        private Button btnOnayla;
        private Button btnReddet;
    }
}
