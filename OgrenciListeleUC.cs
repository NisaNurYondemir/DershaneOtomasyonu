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
    public partial class OgrenciListeleUC : UserControl
    {
        public OgrenciListeleUC()
        {
            InitializeComponent();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string tc = txtAraTC.Text.Trim();
            if (tc.Length == 11)
            {
                OgrencileriYukle(tc);
            }
            else
            {
                MessageBox.Show("Arama için 11 haneli TC girin.");
            }
        }

        private void OgrenciListeleUC_Load(object sender, EventArgs e)
        {
            if (Oturum.RolID != 3 && Oturum.RolID != 4)
            {
                MessageBox.Show("Bu sayfa sadece müdür veya idare personeli içindir.");
                this.ParentForm?.Close();
                return;
            }

            OgrencileriYukle();
        }

        private void OgrencileriYukle(string tcAra = "")
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            k.KullaniciID,
                            k.Ad,
                            k.Soyad,
                            k.TCNo,
                            k.AktifMi,
                            od.SinifSeviyesi,
                            od.VeliAdSoyad,
                            od.VeliTel,
                            COALESCE(d.Ad || ' ' || d.Soyad, 'Atanmadı') as DanismanAdi
                        FROM Kullanicilar k
                        JOIN OgrenciDetay od ON k.KullaniciID = od.OgrenciID
                        LEFT JOIN Kullanicilar d ON od.DanismanOgretmenID = d.KullaniciID
                        WHERE k.RolID = 1
                        " + (string.IsNullOrEmpty(tcAra) ? "" : " AND k.TCNo = @tc") + @"
                        ORDER BY k.Ad, k.Soyad";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(tcAra))
                            cmd.Parameters.AddWithValue("tc", tcAra);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvOgrenciler.DataSource = dt;

                        // Kolon başlıkları
                        if (dt.Columns.Contains("KullaniciID"))
                            dgvOgrenciler.Columns["KullaniciID"].Visible = false;
                        if (dt.Columns.Contains("Ad"))
                            dgvOgrenciler.Columns["Ad"].HeaderText = "Ad";
                        if (dt.Columns.Contains("Soyad"))
                            dgvOgrenciler.Columns["Soyad"].HeaderText = "Soyad";
                        if (dt.Columns.Contains("TCNo"))
                            dgvOgrenciler.Columns["TCNo"].HeaderText = "TC No";
                        if (dt.Columns.Contains("AktifMi"))
                            dgvOgrenciler.Columns["AktifMi"].HeaderText = "Aktif";
                        if (dt.Columns.Contains("SinifSeviyesi"))
                            dgvOgrenciler.Columns["SinifSeviyesi"].HeaderText = "Sınıf";
                        if (dt.Columns.Contains("VeliAdSoyad"))
                            dgvOgrenciler.Columns["VeliAdSoyad"].HeaderText = "Veli";
                        if (dt.Columns.Contains("VeliTel"))
                            dgvOgrenciler.Columns["VeliTel"].HeaderText = "Veli Tel";
                        if (dt.Columns.Contains("DanismanAdi"))
                            dgvOgrenciler.Columns["DanismanAdi"].HeaderText = "Danışman";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenciler yüklenirken hata: " + ex.Message);
            }



        }
    }
}
