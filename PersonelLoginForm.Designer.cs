namespace DershaneOtomasyonu
{
    partial class PersonelLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonelLoginForm));
            personelGirisYapButton = new Button();
            ogrenciSifreGosterCheckBox = new CheckBox();
            ogrenciSifreTextBox = new TextBox();
            ogrenciNoTextBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            closeButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // personelGirisYapButton
            // 
            personelGirisYapButton.Location = new Point(503, 511);
            personelGirisYapButton.Name = "personelGirisYapButton";
            personelGirisYapButton.Size = new Size(98, 41);
            personelGirisYapButton.TabIndex = 31;
            personelGirisYapButton.Text = "GİRİŞ YAP";
            personelGirisYapButton.UseVisualStyleBackColor = true;
            // 
            // ogrenciSifreGosterCheckBox
            // 
            ogrenciSifreGosterCheckBox.AutoSize = true;
            ogrenciSifreGosterCheckBox.Location = new Point(577, 427);
            ogrenciSifreGosterCheckBox.Name = "ogrenciSifreGosterCheckBox";
            ogrenciSifreGosterCheckBox.Size = new Size(119, 24);
            ogrenciSifreGosterCheckBox.TabIndex = 30;
            ogrenciSifreGosterCheckBox.Text = "Şifreyi Göster";
            ogrenciSifreGosterCheckBox.UseVisualStyleBackColor = true;
            // 
            // ogrenciSifreTextBox
            // 
            ogrenciSifreTextBox.Location = new Point(401, 370);
            ogrenciSifreTextBox.Name = "ogrenciSifreTextBox";
            ogrenciSifreTextBox.Size = new Size(295, 27);
            ogrenciSifreTextBox.TabIndex = 29;
            // 
            // ogrenciNoTextBox
            // 
            ogrenciNoTextBox.Location = new Point(401, 285);
            ogrenciNoTextBox.Name = "ogrenciNoTextBox";
            ogrenciNoTextBox.Size = new Size(295, 27);
            ogrenciNoTextBox.TabIndex = 28;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(401, 347);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 27;
            label4.Text = "Şifre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(401, 262);
            label3.Name = "label3";
            label3.Size = new Size(131, 20);
            label3.TabIndex = 26;
            label3.Text = "Personel Numarası";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(498, 189);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 25;
            label1.Text = "Personel Girişi";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(222, 200, 230);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 632);
            panel1.TabIndex = 24;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(94, 84);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(94, 228);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 0;
            label2.Text = "CEP DERSHANE";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(528, 128);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(45, 45);
            pictureBox3.TabIndex = 32;
            pictureBox3.TabStop = false;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(758, 12);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(30, 30);
            closeButton.TabIndex = 33;
            closeButton.Text = "X";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // PersonelLoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 631);
            Controls.Add(closeButton);
            Controls.Add(pictureBox3);
            Controls.Add(personelGirisYapButton);
            Controls.Add(ogrenciSifreGosterCheckBox);
            Controls.Add(ogrenciSifreTextBox);
            Controls.Add(ogrenciNoTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PersonelLoginForm";
            Text = "PersonelLoginForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button personelGirisYapButton;
        private CheckBox ogrenciSifreGosterCheckBox;
        private TextBox ogrenciSifreTextBox;
        private TextBox ogrenciNoTextBox;
        private Label label4;
        private Label label3;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox4;
        private Label label2;
        private PictureBox pictureBox3;
        private Button closeButton;
    }
}