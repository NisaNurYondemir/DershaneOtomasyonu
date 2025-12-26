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
using static DershaneOtomasyonu.TaleplerUC;

namespace DershaneOtomasyonu
{
    public partial class DersProgramiOlusturUC : UserControl
    {

        public DersProgramiOlusturUC()
        {
            InitializeComponent();
        }

        private void DersProgramiOlusturUC_Load(object sender, EventArgs e)
        {
            if (Oturum.RolID == 4) // Sadece İdare
            {
                ComboBoxlariDoldur();
            }
            else
            {
                MessageBox.Show("Bu sayfa sadece idare personeli içindir.");
                this.ParentForm?.Close();
            }
        }

        private void ComboBoxlariDoldur()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    // Dersleri doldur
                    string sqlDersler = "SELECT DersID, DersAdi FROM Dersler ORDER BY DersAdi";
                    using (var cmd = new NpgsqlCommand(sqlDersler, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbDersler.Items.Clear();
                            cmbDersler.Items.Add(new ComboboxItem { Text = "-- Ders Seçin --", Value = 0 });
                            while (reader.Read())
                            {
                                cmbDersler.Items.Add(new ComboboxItem
                                {
                                    Text = reader.GetString(1),
                                    Value = reader.GetInt32(0)
                                });
                            }
                            cmbDersler.SelectedIndex = 0;
                        }
                    }

                    // Öğretmenleri doldur
                    string sqlOgretmenler = @"
                        SELECT k.KullaniciID, k.Ad || ' ' || k.Soyad as AdSoyad
                        FROM Kullanicilar k
                        WHERE k.RolID = 2 AND k.AktifMi = TRUE
                        ORDER BY k.Ad";

                    using (var cmd = new NpgsqlCommand(sqlOgretmenler, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbOgretmenler.Items.Clear();
                            cmbOgretmenler.Items.Add(new ComboboxItem { Text = "-- Öğretmen Seçin --", Value = 0 });
                            while (reader.Read())
                            {
                                cmbOgretmenler.Items.Add(new ComboboxItem
                                {
                                    Text = reader.GetString(1),
                                    Value = reader.GetInt32(0)
                                });
                            }
                            cmbOgretmenler.SelectedIndex = 0;
                        }
                    }

                    // Derslikleri doldur
                    string sqlDerslikler = "SELECT DerslikID, DerslikAdi FROM Derslikler ORDER BY DerslikAdi";
                    using (var cmd = new NpgsqlCommand(sqlDerslikler, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbDerslik.Items.Clear();
                            cmbDerslik.Items.Add(new ComboboxItem { Text = "-- Derslik Seçin --", Value = 0 });
                            while (reader.Read())
                            {
                                cmbDerslik.Items.Add(new ComboboxItem
                                {
                                    Text = reader.GetString(1),
                                    Value = reader.GetInt32(0)
                                });
                            }
                            cmbDerslik.SelectedIndex = 0;
                        }
                    }

                    // Günleri doldur
                    cmbGun.Items.Clear();
                    cmbGun.Items.AddRange(new string[] {
                        "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar"
                    });
                    if (cmbGun.Items.Count > 0)
                        cmbGun.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Combobox'lar doldurulurken hata: " + ex.Message);
            }
        }

        private void btnProgramEkle_Click(object sender, EventArgs e)
        {
            if (Oturum.RolID != 4) return; // Sadece idare

            // Kontroller
            if (cmbDersler.SelectedIndex <= 0)
            {
                MessageBox.Show("Lütfen bir ders seçin.");
                return;
            }
            if (cmbOgretmenler.SelectedIndex <= 0)
            {
                MessageBox.Show("Lütfen bir öğretmen seçin.");
                return;
            }
            if (cmbDerslik.SelectedIndex <= 0)
            {
                MessageBox.Show("Lütfen bir derslik seçin.");
                return;
            }
            if (cmbGun.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir gün seçin.");
                return;
            }
            if (string.IsNullOrEmpty(txtBaslangic.Text) || string.IsNullOrEmpty(txtBitis.Text))
            {
                MessageBox.Show("Lütfen başlangıç ve bitiş saatlerini girin.");
                return;
            }

            if (!TimeSpan.TryParse(txtBaslangic.Text, out TimeSpan baslangicSaati) ||
                !TimeSpan.TryParse(txtBitis.Text, out TimeSpan bitisSaati))
            {
                MessageBox.Show("Saat formatı hatalı. Örnek: 09:00, 14:30");
                return;
            }

            if (bitisSaati <= baslangicSaati)
            {
                MessageBox.Show("Bitiş saati başlangıç saatinden sonra olmalıdır.");
                return;
            }

            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    var dersItem = (ComboboxItem)cmbDersler.SelectedItem;
                    var ogretmenItem = (ComboboxItem)cmbOgretmenler.SelectedItem;
                    var derslikItem = (ComboboxItem)cmbDerslik.SelectedItem;

                    int dersID = dersItem.Value;
                    int ogretmenID = ogretmenItem.Value;
                    int derslikID = derslikItem.Value;
                    string gun = cmbGun.SelectedItem.ToString();

                    // Derslik ve öğretmen çakışma kontrolleri
                    string cakismaKontrol = @"
                        SELECT COUNT(*) 
                        FROM DersProgrami 
                        WHERE DerslikID = @derslikID 
                        AND Gun = @gun 
                        AND (
                            (BaslangicSaati <= @baslangic AND BitisSaati > @baslangic) OR
                            (BaslangicSaati < @bitis AND BitisSaati >= @bitis) OR
                            (@baslangic <= BaslangicSaati AND @bitis >= BitisSaati)
                        )";

                    using (var cmdKontrol = new NpgsqlCommand(cakismaKontrol, conn))
                    {
                        cmdKontrol.Parameters.AddWithValue("derslikID", derslikID);
                        cmdKontrol.Parameters.AddWithValue("gun", gun);
                        cmdKontrol.Parameters.AddWithValue("baslangic", baslangicSaati);
                        cmdKontrol.Parameters.AddWithValue("bitis", bitisSaati);

                        int cakismaSayisi = Convert.ToInt32(cmdKontrol.ExecuteScalar());
                        if (cakismaSayisi > 0)
                        {
                            MessageBox.Show("Bu derslikte seçilen saatte başka ders var!");
                            return;
                        }
                    }

                    string ogretmenKontrol = @"
                        SELECT COUNT(*) 
                        FROM DersProgrami 
                        WHERE OgretmenID = @ogretmenID 
                        AND Gun = @gun 
                        AND (
                            (BaslangicSaati <= @baslangic AND BitisSaati > @baslangic) OR
                            (BaslangicSaati < @bitis AND BitisSaati >= @bitis) OR
                            (@baslangic <= BaslangicSaati AND @bitis >= BitisSaati)
                        )";

                    using (var cmdOgretmen = new NpgsqlCommand(ogretmenKontrol, conn))
                    {
                        cmdOgretmen.Parameters.AddWithValue("ogretmenID", ogretmenID);
                        cmdOgretmen.Parameters.AddWithValue("gun", gun);
                        cmdOgretmen.Parameters.AddWithValue("baslangic", baslangicSaati);
                        cmdOgretmen.Parameters.AddWithValue("bitis", bitisSaati);

                        int ogretmenCakisma = Convert.ToInt32(cmdOgretmen.ExecuteScalar());
                        if (ogretmenCakisma > 0)
                        {
                            MessageBox.Show("Bu öğretmenin seçilen saatte başka dersi var!");
                            return;
                        }
                    }

                    // Ders programı ekle
                    string sql = @"
                        INSERT INTO DersProgrami 
                            (DersID, OgretmenID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi)
                        VALUES 
                            (@dersID, @ogretmenID, @derslikID, @gun, @baslangic, @bitis, FALSE)
                        RETURNING ProgramID";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("dersID", dersID);
                        cmd.Parameters.AddWithValue("ogretmenID", ogretmenID);
                        cmd.Parameters.AddWithValue("derslikID", derslikID);
                        cmd.Parameters.AddWithValue("gun", gun);
                        cmd.Parameters.AddWithValue("baslangic", baslangicSaati);
                        cmd.Parameters.AddWithValue("bitis", bitisSaati);

                        var newProgramID = cmd.ExecuteScalar();
                        if (newProgramID != null)
                        {
                            MessageBox.Show($"Ders programı eklendi. (ID: {newProgramID})\nMüdür onayı bekleniyor.",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormuTemizle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void FormuTemizle()
        {
            if (cmbDersler.Items.Count > 0) cmbDersler.SelectedIndex = 0;
            if (cmbOgretmenler.Items.Count > 0) cmbOgretmenler.SelectedIndex = 0;
            if (cmbDerslik.Items.Count > 0) cmbDerslik.SelectedIndex = 0;
            if (cmbGun.Items.Count > 0) cmbGun.SelectedIndex = 0;
            txtBaslangic.Clear();
            txtBitis.Clear();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            ComboBoxlariDoldur();
        }

        private void txtBaslangic_Leave(object sender, EventArgs e)
        {
            SaatFormatla(txtBaslangic);
        }

        private void txtBitis_Leave(object sender, EventArgs e)
        {
            SaatFormatla(txtBitis);
        }

        private void SaatFormatla(TextBox txt)
        {
            if (!string.IsNullOrEmpty(txt.Text))
            {
                string saat = txt.Text.Replace(":", "");
                if (saat.Length == 4)
                {
                    txt.Text = saat.Insert(2, ":");
                }
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString() => Text;
    }
}
