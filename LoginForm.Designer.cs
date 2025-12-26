namespace DershaneOtomasyonu
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            girisYapButton = new Button();
            sifreGosterCheckBox = new CheckBox();
            sifreTextBox = new TextBox();
            tcNoTextBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            closeButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // girisYapButton
            // 
            girisYapButton.Location = new Point(502, 472);
            girisYapButton.Name = "girisYapButton";
            girisYapButton.Size = new Size(98, 41);
            girisYapButton.TabIndex = 23;
            girisYapButton.Text = "GİRİŞ YAP";
            girisYapButton.UseVisualStyleBackColor = true;
            girisYapButton.Click += girisYapButton_Click;
            // 
            // sifreGosterCheckBox
            // 
            sifreGosterCheckBox.AutoSize = true;
            sifreGosterCheckBox.Location = new Point(576, 407);
            sifreGosterCheckBox.Name = "sifreGosterCheckBox";
            sifreGosterCheckBox.Size = new Size(119, 24);
            sifreGosterCheckBox.TabIndex = 22;
            sifreGosterCheckBox.Text = "Şifreyi Göster";
            sifreGosterCheckBox.UseVisualStyleBackColor = true;
            sifreGosterCheckBox.CheckedChanged += sifreGosterCheckBox_CheckedChanged;
            // 
            // sifreTextBox
            // 
            sifreTextBox.Location = new Point(400, 348);
            sifreTextBox.Name = "sifreTextBox";
            sifreTextBox.Size = new Size(295, 27);
            sifreTextBox.TabIndex = 21;
            sifreTextBox.UseSystemPasswordChar = true;
            // 
            // tcNoTextBox
            // 
            tcNoTextBox.Location = new Point(400, 244);
            tcNoTextBox.Name = "tcNoTextBox";
            tcNoTextBox.Size = new Size(295, 27);
            tcNoTextBox.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(400, 316);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 19;
            label4.Text = "Şifre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(400, 212);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 18;
            label3.Text = "TC NO";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(222, 200, 230);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 631);
            panel1.TabIndex = 16;
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
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 631);
            Controls.Add(closeButton);
            Controls.Add(girisYapButton);
            Controls.Add(sifreGosterCheckBox);
            Controls.Add(sifreTextBox);
            Controls.Add(tcNoTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OgretmenLoginForm";
            Load += LoginForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button girisYapButton;
        private CheckBox sifreGosterCheckBox;
        private TextBox sifreTextBox;
        private TextBox tcNoTextBox;
        private Label label4;
        private Label label3;
        private Panel panel1;
        private PictureBox pictureBox4;
        private Label label2;
        private Button closeButton;
    }
}