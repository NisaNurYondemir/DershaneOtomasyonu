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
    public partial class OdevlerUC : UserControl
    {
        public OdevlerUC()
        {
            InitializeComponent();
        }
        private void OdevleriYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = "";

                    // ÖĞRENCİ
                    if (Oturum.RolID == 1)
                    {
                        sql = @"
                            SELECT 
                                o.OdevID,
                                d.DersAdi AS ""Ders"",
                                k.Ad || ' ' || k.Soyad AS ""Öğretmen"",
                                o.Aciklama AS ""Açıklama"",
                                o.VerilisTarihi AS ""Veriliş Tarihi"",
                                o.TeslimTarihi AS ""Teslim Tarihi"",
                                o.Durum
                            FROM vw_odevler o
                            JOIN Dersler d ON o.DersID = d.DersID
                            JOIN Kullanicilar k ON o.OgretmenID = k.KullaniciID
                            WHERE d.SinifSeviyesi = (
                                SELECT SinifSeviyesi 
                                FROM OgrenciDetay 
                                WHERE OgrenciID = @ogrenciID
                            )
                            ORDER BY o.TeslimTarihi ASC";
                    }
                    // ÖĞRETMEN / DANIŞMAN
                    else if (Oturum.RolID == 2 || Oturum.RolID == 5)
                    {
                        sql = @"
                            SELECT 
                                o.OdevID,
                                d.DersAdi AS ""Ders"",
                                o.Aciklama AS ""Açıklama"",
                                o.VerilisTarihi AS ""Veriliş Tarihi"",
                                o.TeslimTarihi AS ""Teslim Tarihi"",
                                o.TeslimEdenSayisi AS ""Teslim Eden"",
                                o.Durum
                            FROM vw_odevler o
                            JOIN Dersler d ON o.DersID = d.DersID
                            WHERE o.OgretmenID = @ogretmenID
                            ORDER BY o.TeslimTarihi ASC";
                    }

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (Oturum.RolID == 1)
                            cmd.Parameters.AddWithValue("ogrenciID", Oturum.KullaniciID);
                        else
                            cmd.Parameters.AddWithValue("ogretmenID", Oturum.KullaniciID);

                        DataTable dt = new DataTable();
                        new NpgsqlDataAdapter(cmd).Fill(dt);
                        dgvOdevler.DataSource = dt;

                        // Tarih formatları
                        if (dgvOdevler.Columns.Contains("Veriliş Tarihi"))
                            dgvOdevler.Columns["Veriliş Tarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";

                        if (dgvOdevler.Columns.Contains("Teslim Tarihi"))
                            dgvOdevler.Columns["Teslim Tarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";

                        // OdevID gizle
                        if (dgvOdevler.Columns.Contains("OdevID"))
                            dgvOdevler.Columns["OdevID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödevler yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void OdevlerUC_Load(object sender, EventArgs e)
        {
            OdevleriYukle();
        }
    }
}
