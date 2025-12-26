using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DershaneOtomasyonu
{
    public partial class OgrenciKayitUC : UserControl
    {
       

        public OgrenciKayitUC()
        {
            InitializeComponent();
        }

        private void OgrenciKayitUC_Load(object sender, EventArgs e)
        {
            if (Oturum.RolID == 3 || Oturum.RolID == 4) // Müdür veya İdare
            {
                DanismanOgretmenleriDoldur();
                SinifSeviyeleriniDoldur();
                txtSifre.Text = "123456"; // Varsayılan şifre
            }
            else
            {
                MessageBox.Show("Bu sayfa sadece müdür ve idare personeli içindir.");
                this.ParentForm?.Close();
            }
        }
        private void DanismanOgretmenleriDoldur()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT k.KullaniciID, k.Ad || ' ' || k.Soyad as AdSoyad, r.RolAdi
                        FROM Kullanicilar k
                        JOIN Roller r ON k.RolID = r.RolID
                        WHERE k.RolID IN (2, 5) AND k.AktifMi = TRUE
                        ORDER BY k.Ad";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbDanisman.Items.Clear();
                        cmbDanisman.Items.Add(new ComboboxItem { Text = "-- Danışman Seçin --", Value = 0 });

                        while (reader.Read())
                        {
                            int kullaniciID = Convert.ToInt32(reader["KullaniciID"]);
                            string adSoyad = reader["AdSoyad"].ToString();
                            string rolAdi = reader["RolAdi"].ToString();

                            cmbDanisman.Items.Add(new ComboboxItem
                            {
                                Text = $"{adSoyad} ({rolAdi})",
                                Value = kullaniciID
                            });
                        }

                        if (cmbDanisman.Items.Count > 0)
                            cmbDanisman.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Danışman öğretmenler yüklenirken hata: " + ex.Message);
            }
        }

        private void SinifSeviyeleriniDoldur()
        {
            cmbSinif.Items.Clear();
            cmbSinif.Items.AddRange(new string[] { "9", "10", "11", "12", "Mezun" });
            if (cmbSinif.Items.Count > 0)
                cmbSinif.SelectedIndex = 0;
        }

        private void btnOgrenciKaydet_Click(object sender, EventArgs e)
        {
            if (Oturum.RolID != 3 && Oturum.RolID != 4) return;

            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Lütfen öğrenci adı ve soyadı girin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTC.Text) || txtTC.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik No 11 haneli olmalıdır.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen şifre girin.");
                return;
            }

            if (cmbSinif.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen sınıf seviyesi seçin.");
                return;
            }

            if (!long.TryParse(txtTC.Text, out _))
            {
                MessageBox.Show("TC Kimlik No sadece rakamlardan oluşmalıdır.");
                return;
            }

            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string tcKontrol = "SELECT COUNT(*) FROM Kullanicilar WHERE TCNo = @tc";
                    using (var cmdKontrol = new NpgsqlCommand(tcKontrol, conn))
                    {
                        cmdKontrol.Parameters.AddWithValue("tc", txtTC.Text.Trim());
                        int kayitSayisi = Convert.ToInt32(cmdKontrol.ExecuteScalar());
                        if (kayitSayisi > 0)
                        {
                            MessageBox.Show("Bu TC Kimlik No ile kayıtlı öğrenci zaten var!");
                            return;
                        }
                    }

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string sqlKullanici = @"
                                INSERT INTO Kullanicilar (Ad, Soyad, TCNo, Sifre, RolID, AktifMi)
                                VALUES (@ad, @soyad, @tc, @sifre, 1, TRUE)
                                RETURNING KullaniciID";

                            int yeniOgrenciID;
                            using (var cmdKullanici = new NpgsqlCommand(sqlKullanici, conn, transaction))
                            {
                                cmdKullanici.Parameters.AddWithValue("ad", txtAd.Text.Trim());
                                cmdKullanici.Parameters.AddWithValue("soyad", txtSoyad.Text.Trim());
                                cmdKullanici.Parameters.AddWithValue("tc", txtTC.Text.Trim());
                                cmdKullanici.Parameters.AddWithValue("sifre", txtSifre.Text.Trim());

                                yeniOgrenciID = Convert.ToInt32(cmdKullanici.ExecuteScalar());
                            }

                            string sqlDetay = @"
                                INSERT INTO OgrenciDetay 
                                    (OgrenciID, VeliAdSoyad, VeliTel, DanismanOgretmenID, SinifSeviyesi)
                                VALUES 
                                    (@ogrenciID, @veliAd, @veliTel, @danismanID, @sinif)";

                            using (var cmdDetay = new NpgsqlCommand(sqlDetay, conn, transaction))
                            {
                                cmdDetay.Parameters.AddWithValue("ogrenciID", yeniOgrenciID);
                                

                                if (cmbDanisman.SelectedIndex > 0)
                                {
                                    var danismanItem = (ComboboxItem)cmbDanisman.SelectedItem;
                                    cmdDetay.Parameters.AddWithValue("danismanID", danismanItem.Value);
                                }
                                else
                                {
                                    cmdDetay.Parameters.AddWithValue("danismanID", DBNull.Value);
                                }

                                int sinifSeviyesi = cmbSinif.SelectedItem.ToString() == "Mezun" ? 0 :
                                                   Convert.ToInt32(cmbSinif.SelectedItem.ToString());
                                cmdDetay.Parameters.AddWithValue("sinif", sinifSeviyesi);

                                cmdDetay.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show($"Öğrenci başarıyla kaydedildi.\nÖğrenci ID: {yeniOgrenciID}\nTC: {txtTC.Text}",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            FormuTemizle();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Kayıt işlemi sırasında hata: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci kaydı sırasında hata: " + ex.Message);
            }
        }

        private void FormuTemizle()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTC.Clear();
            txtSifre.Text = "123456";

            if (cmbSinif.Items.Count > 0) cmbSinif.SelectedIndex = 0;
            if (cmbDanisman.Items.Count > 0) cmbDanisman.SelectedIndex = 0;

            txtAd.Focus();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            DanismanOgretmenleriDoldur();
        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam ve control tuşlarına izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString() => Text;
        }


    }


}