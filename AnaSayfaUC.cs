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
    public partial class AnaSayfaUC : UserControl
    {

        public AnaSayfaUC()
        {
            InitializeComponent();
        }
       
        private void OgrenciAnaSayfaYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    // YAKLAŞAN ÖDEVLER
                    string sqlYakinOdevler = @"
                        SELECT 
                            LEFT(o.Aciklama, 50) || '...' AS Odev,
                            d.DersAdi AS Ders,
                            TO_CHAR(o.TeslimTarihi, 'DD.MM.YYYY') AS Teslim_Tarihi,
                            CASE 
                                WHEN o.TeslimTarihi < CURRENT_DATE THEN 'GEÇ KALDI'
                                WHEN o.TeslimTarihi = CURRENT_DATE THEN 'BUGÜN'
                                WHEN o.TeslimTarihi = CURRENT_DATE + 1 THEN 'YARIN'
                                ELSE (o.TeslimTarihi - CURRENT_DATE)::TEXT || ' GÜN'
                            END AS Kalan
                        FROM Odevler o
                        JOIN Dersler d ON o.DersID = d.DersID
                        WHERE d.SinifSeviyesi = (
                            SELECT SinifSeviyesi 
                            FROM OgrenciDetay 
                            WHERE OgrenciID = @ogrenciID
                        )
                        ORDER BY o.TeslimTarihi
                        LIMIT 5";

                    using (var cmd = new NpgsqlCommand(sqlYakinOdevler, conn))
                    {
                        cmd.Parameters.AddWithValue("ogrenciID", Oturum.KullaniciID);
                        DataTable dt = new DataTable();
                        new NpgsqlDataAdapter(cmd).Fill(dt);
                        dgvYakinOdevler.DataSource = dt;
                    }

                    // DUYURULAR
                    string sqlDuyurular = @"
                        SELECT 
                            Baslik,
                            LEFT(Icerik, 100) || '...' AS Icerik,
                            TO_CHAR(Tarih, 'DD.MM.YYYY') AS Tarih
                        FROM Duyurular
                        WHERE HedefKitle IN ('Tumu', 'Ogrenci')
                        ORDER BY Tarih DESC
                        LIMIT 5";

                    DataTable duyuruDt = new DataTable();
                    new NpgsqlDataAdapter(sqlDuyurular, conn).Fill(duyuruDt);
                    dgvDuyurular.DataSource = duyuruDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci ana sayfa hatası:\n" + ex.Message);
            }
        }

        private void OgretmenAnaSayfaYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            LEFT(o.Aciklama, 50) || '...' AS Odev,
                            d.DersAdi AS Ders,
                            TO_CHAR(o.TeslimTarihi, 'DD.MM.YYYY') AS Teslim_Tarihi,
                            (SELECT COUNT(*) FROM OdevTeslim ot WHERE ot.OdevID = o.OdevID) AS Teslim_Eden
                        FROM Odevler o
                        JOIN Dersler d ON o.DersID = d.DersID
                        WHERE o.OgretmenID = @ogretmenID
                        ORDER BY o.VerilisTarihi DESC
                        LIMIT 5";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("ogretmenID", Oturum.KullaniciID);
                        DataTable dt = new DataTable();
                        new NpgsqlDataAdapter(cmd).Fill(dt);
                        dgvYakinOdevler.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmen ana sayfa hatası:\n" + ex.Message);
            }
        }

        private void AnaSayfaUC_Load(object sender, EventArgs e)
        {
            lblKullaniciAdi.Text = $"Hoş Geldiniz, {Oturum.AdSoyad}";

            switch (Oturum.RolID)
            {
                case 1:
                    OgrenciAnaSayfaYukle();
                    break;
                case 2:
                case 5:
                    OgretmenAnaSayfaYukle();
                    break;
            }
        }


    }
}
