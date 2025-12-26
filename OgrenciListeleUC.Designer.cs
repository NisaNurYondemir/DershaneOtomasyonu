namespace DershaneOtomasyonu
{
    partial class OgrenciListeleUC
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
            dgvOgrenciler = new DataGridView();
            btnAra = new Button();
            txtAraTC = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOgrenciler).BeginInit();
            SuspendLayout();
            // 
            // dgvOgrenciler
            // 
            dgvOgrenciler.AllowUserToAddRows = false;
            dgvOgrenciler.AllowUserToDeleteRows = false;
            dgvOgrenciler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOgrenciler.Location = new Point(29, 26);
            dgvOgrenciler.Name = "dgvOgrenciler";
            dgvOgrenciler.ReadOnly = true;
            dgvOgrenciler.RowHeadersWidth = 51;
            dgvOgrenciler.RowTemplate.Height = 29;
            dgvOgrenciler.Size = new Size(824, 594);
            dgvOgrenciler.TabIndex = 0;
            // 
            // btnAra
            // 
            btnAra.Location = new Point(298, 682);
            btnAra.Name = "btnAra";
            btnAra.Size = new Size(94, 29);
            btnAra.TabIndex = 1;
            btnAra.Text = "Ara";
            btnAra.UseVisualStyleBackColor = true;
            btnAra.Click += btnAra_Click;
            // 
            // txtAraTC
            // 
            txtAraTC.Location = new Point(50, 684);
            txtAraTC.Name = "txtAraTC";
            txtAraTC.Size = new Size(182, 27);
            txtAraTC.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 649);
            label1.Name = "label1";
            label1.Size = new Size(28, 20);
            label1.TabIndex = 3;
            label1.Text = "TC:";
            // 
            // OgrenciListeleUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(txtAraTC);
            Controls.Add(btnAra);
            Controls.Add(dgvOgrenciler);
            Name = "OgrenciListeleUC";
            Size = new Size(879, 752);
            Load += OgrenciListeleUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOgrenciler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOgrenciler;
        private Button btnAra;
        private TextBox txtAraTC;
        private Label label1;
    }
}
