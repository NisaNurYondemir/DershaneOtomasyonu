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
    public partial class DenemelerUC : UserControl
    {

        public DenemelerUC()
        {
            InitializeComponent();

        }


        private void DenemeSonuclariYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            ds.SonucID,
                            d.DenemeAdi AS ""Deneme Adı"",
                            d.Tarih,
                            k.Ad || ' ' || k.Soyad AS ""Öğrenci"",
                            ds.DogruSayisi AS ""Doğru"",
                            ds.YanlisSayisi AS ""Yanlış"",
                            ds.NetSayisi AS ""Net"",
                            ds.Puan
                        FROM DenemeSonuclari ds
                        JOIN Denemeler d ON ds.DenemeID = d.DenemeID
                        JOIN Kullanicilar k ON ds.OgrenciID = k.KullaniciID
                        ORDER BY d.Tarih DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDenemeler.DataSource = dt;

                        // Tarih formatı
                        if (dgvDenemeler.Columns["Tarih"] != null)
                        {
                            dgvDenemeler.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy";
                        }

                        // SonucID gizle
                        if (dgvDenemeler.Columns.Contains("SonucID"))
                        {
                            dgvDenemeler.Columns["SonucID"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deneme sonuçları yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void DenemelerUC_Load(object sender, EventArgs e)
        {
            DenemeSonuclariYukle();
        }
    }
}
