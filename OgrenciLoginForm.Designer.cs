namespace DershaneOtomasyonu
{
    partial class OgrenciLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OgrenciLoginForm));
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            ogrenciNoTextBox = new TextBox();
            ogrenciSifreTextBox = new TextBox();
            ogrenciSifreGosterCheckBox = new CheckBox();
            ogrenciGirisYapButton = new Button();
            pictureBox1 = new PictureBox();
            closeButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(222, 200, 230);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 631);
            panel1.TabIndex = 8;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(497, 189);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 9;
            label1.Text = "Öğrenci Girişi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(394, 261);
            label3.Name = "label3";
            label3.Size = new Size(128, 20);
            label3.TabIndex = 10;
            label3.Text = "Öğrenci Numarası";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(394, 346);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 11;
            label4.Text = "Şifre";
            // 
            // ogrenciNoTextBox
            // 
            ogrenciNoTextBox.Location = new Point(394, 284);
            ogrenciNoTextBox.Name = "ogrenciNoTextBox";
            ogrenciNoTextBox.Size = new Size(295, 27);
            ogrenciNoTextBox.TabIndex = 12;
            // 
            // ogrenciSifreTextBox
            // 
            ogrenciSifreTextBox.Location = new Point(394, 369);
            ogrenciSifreTextBox.Name = "ogrenciSifreTextBox";
            ogrenciSifreTextBox.Size = new Size(295, 27);
            ogrenciSifreTextBox.TabIndex = 13;
            // 
            // ogrenciSifreGosterCheckBox
            // 
            ogrenciSifreGosterCheckBox.AutoSize = true;
            ogrenciSifreGosterCheckBox.Location = new Point(570, 426);
            ogrenciSifreGosterCheckBox.Name = "ogrenciSifreGosterCheckBox";
            ogrenciSifreGosterCheckBox.Size = new Size(119, 24);
            ogrenciSifreGosterCheckBox.TabIndex = 14;
            ogrenciSifreGosterCheckBox.Text = "Şifreyi Göster";
            ogrenciSifreGosterCheckBox.UseVisualStyleBackColor = true;
            // 
            // ogrenciGirisYapButton
            // 
            ogrenciGirisYapButton.Location = new Point(496, 510);
            ogrenciGirisYapButton.Name = "ogrenciGirisYapButton";
            ogrenciGirisYapButton.Size = new Size(98, 41);
            ogrenciGirisYapButton.TabIndex = 15;
            ogrenciGirisYapButton.Text = "GİRİŞ YAP";
            ogrenciGirisYapButton.UseVisualStyleBackColor = true;
            ogrenciGirisYapButton.Click += ogrenciGirisYapButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(526, 116);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(45, 45);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(758, 12);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(30, 30);
            closeButton.TabIndex = 25;
            closeButton.Text = "X";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // OgrenciLoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 631);
            Controls.Add(closeButton);
            Controls.Add(pictureBox1);
            Controls.Add(ogrenciGirisYapButton);
            Controls.Add(ogrenciSifreGosterCheckBox);
            Controls.Add(ogrenciSifreTextBox);
            Controls.Add(ogrenciNoTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OgrenciLoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SigninForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox ogrenciNoTextBox;
        private TextBox ogrenciSifreTextBox;
        private CheckBox ogrenciSifreGosterCheckBox;
        private Button ogrenciGirisYapButton;
        private PictureBox pictureBox1;
        private Button closeButton;
    }
}