namespace DershaneOtomasyonu
{
    partial class OgretmenListeleUC
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
            label1 = new Label();
            txtAraTC = new TextBox();
            btnAra = new Button();
            dgvOgretmenler = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOgretmenler).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 657);
            label1.Name = "label1";
            label1.Size = new Size(28, 20);
            label1.TabIndex = 7;
            label1.Text = "TC:";
            // 
            // txtAraTC
            // 
            txtAraTC.Location = new Point(48, 692);
            txtAraTC.Name = "txtAraTC";
            txtAraTC.Size = new Size(182, 27);
            txtAraTC.TabIndex = 6;
            // 
            // btnAra
            // 
            btnAra.Location = new Point(296, 690);
            btnAra.Name = "btnAra";
            btnAra.Size = new Size(94, 29);
            btnAra.TabIndex = 5;
            btnAra.Text = "Ara";
            btnAra.UseVisualStyleBackColor = true;
            btnAra.Click += btnAra_Click;
            // 
            // dgvOgretmenler
            // 
            dgvOgretmenler.AllowUserToAddRows = false;
            dgvOgretmenler.AllowUserToDeleteRows = false;
            dgvOgretmenler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOgretmenler.Location = new Point(27, 34);
            dgvOgretmenler.Name = "dgvOgretmenler";
            dgvOgretmenler.ReadOnly = true;
            dgvOgretmenler.RowHeadersWidth = 51;
            dgvOgretmenler.RowTemplate.Height = 29;
            dgvOgretmenler.Size = new Size(824, 594);
            dgvOgretmenler.TabIndex = 4;
            // 
            // OgretmenListeleUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(txtAraTC);
            Controls.Add(btnAra);
            Controls.Add(dgvOgretmenler);
            Name = "OgretmenListeleUC";
            Size = new Size(879, 752);
            Load += OgretmenListeleUC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOgretmenler).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtAraTC;
        private Button btnAra;
        private DataGridView dgvOgretmenler;
    }
}
