using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DershaneOtomasyonu
{


    public partial class LoginForm : Form
    {

        
        public LoginForm()
        {
            InitializeComponent();

            
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void MenuYonlendir(int rolId)
        {
            Form hedefForm = null;

            switch (rolId)
            {
                case 1: // Öğrenci
                    hedefForm = new OgrenciMainForm();
                    break;

                case 2: // Öğretmen
                case 5: // Danışman Öğretmen
                    hedefForm = new OgretmenMainForm();
                    break;

                case 3: // Müdür
                    hedefForm = new MudurMainForm();
                    break;

                case 4: // İdare
                    hedefForm = new PersonelMainForm();
                    break;

                default:
                    MessageBox.Show("Tanımsız rol!", "Hata");
                    return;
            }

            hedefForm.Show();
        }
        private void girisYapButton_Click(object sender, EventArgs e)
        {
            using (var conn = Veritabani.BaglantiGetir())
            {
                try
                {
                    string sql = @"
                        SELECT 
                            k.KullaniciID,
                            k.Ad,
                            k.Soyad,
                            k.RolID
                        FROM Kullanicilar k
                        WHERE k.TCNo = @tc 
                          AND k.Sifre = @sifre 
                          AND k.AktifMi = TRUE";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("tc", tcNoTextBox.Text);
                        cmd.Parameters.AddWithValue("sifre", sifreTextBox.Text);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int kullaniciID = reader.GetInt32(0);
                                string ad = reader.GetString(1);
                                string soyad = reader.GetString(2);
                                int rolID = reader.GetInt32(3);

                                string adSoyad = ad + " " + soyad;

                                // 🔹 OTURUM BİLGİLERİ
                                Oturum.KullaniciID = kullaniciID;
                                Oturum.RolID = rolID;
                                Oturum.AdSoyad = adSoyad;

                                MessageBox.Show(
                                    $"Hoş geldiniz, {adSoyad}",
                                    "Giriş Başarılı",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );

                                this.Hide();
                                MenuYonlendir(rolID);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "TC No veya Şifre hatalı ya da hesabınız aktif değil!",
                                    "Hata",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sistem hatası: " + ex.Message);
                }
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void sifreGosterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // CheckBox işaretli ise (true) gizleme kapansın (false), 
            // İşaretli değilse (false) gizleme açılsın (true).
            sifreTextBox.UseSystemPasswordChar = !sifreGosterCheckBox.Checked;
        }
    }
}
