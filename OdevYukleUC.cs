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
    public partial class OdevYukleUC : UserControl
    {

        public string GelenTC { get; set; }
        public int GelenRolID { get; set; }
        private int ogretmenDersID = -1;
        

        private void OdevYukleUC_Load(object sender, EventArgs e)
        {
            if (GelenRolID == 2 || GelenRolID == 5)
            {
                // DateTimePicker formatı
                teslimTarihi.Format = DateTimePickerFormat.Custom;
                teslimTarihi.CustomFormat = "dd.MM.yyyy";
                teslimTarihi.MinDate = DateTime.Today;
                teslimTarihi.Value = DateTime.Today; // İlk açılışta bugün

                OgretmenDersiniAl();
                if (ogretmenDersID > 0)
                {
                    OdevleriYukle();
                }
            }
            else
            {
                MessageBox.Show("Bu sayfa sadece öğretmenler içindir.");
                this.ParentForm?.Close();
            }
        }

        private void OgretmenDersiniAl()
{
    try
    {
        using (var conn = Veritabani.BaglantiGetir())
        {
            // DEBUG 1: TC var mı?
            string debugSql1 = "SELECT KullaniciID, Ad, Soyad FROM Kullanicilar WHERE TCNo = @tc";
            using (var cmd1 = new NpgsqlCommand(debugSql1, conn))
            {
                cmd1.Parameters.AddWithValue("tc", GelenTC);
                using (var reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int kid = reader.GetInt32(0);
                        string ad = reader.GetString(1);
                        string soyad = reader.GetString(2);
                        MessageBox.Show($"DEBUG 1: TC bulundu - ID:{kid}, Ad:{ad} {soyad}");
                    }
                    else
                    {
                        MessageBox.Show($"DEBUG 1: TC {GelenTC} bulunamadı!");
                        return;
                    }
                }
            }

            // DEBUG 2: Bu öğretmenin DersProgrami'nde dersi var mı?
            string debugSql2 = @"
                SELECT COUNT(*) as DersSayisi 
                FROM DersProgrami 
                WHERE OgretmenID = (
                    SELECT KullaniciID FROM Kullanicilar WHERE TCNo = @tc
                )";
            using (var cmd2 = new NpgsqlCommand(debugSql2, conn))
            {
                cmd2.Parameters.AddWithValue("tc", GelenTC);
                int dersSayisi = Convert.ToInt32(cmd2.ExecuteScalar());
                MessageBox.Show($"DEBUG 2: DersProgrami'nde {dersSayisi} ders var");
            }

            // DEBUG 3: Hangi dersler?
            string debugSql3 = @"
                SELECT dp.DersID, d.DersAdi 
                FROM DersProgrami dp
                JOIN Dersler d ON dp.DersID = d.DersID
                WHERE dp.OgretmenID = (
                    SELECT KullaniciID FROM Kullanicilar WHERE TCNo = @tc
                )
                GROUP BY dp.DersID, d.DersAdi";
            using (var cmd3 = new NpgsqlCommand(debugSql3, conn))
            {
                cmd3.Parameters.AddWithValue("tc", GelenTC);
                using (var reader = cmd3.ExecuteReader())
                {
                    string dersler = "";
                    while (reader.Read())
                    {
                        dersler += $"DersID: {reader.GetInt32(0)}, Ad: {reader.GetString(1)}\n";
                    }
                    MessageBox.Show($"DEBUG 3: Dersler:\n{dersler}");
                }
            }

            // Normal sorgu
            string sql = @"
                SELECT DISTINCT d.DersID
                FROM Dersler d
                WHERE d.DersID IN (
                    SELECT dp.DersID 
                    FROM DersProgrami dp 
                    WHERE dp.OgretmenID = (
                        SELECT KullaniciID FROM Kullanicilar WHERE TCNo = @tc
                    )
                )
                LIMIT 1";
                
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("tc", GelenTC);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    ogretmenDersID = Convert.ToInt32(result);
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Hata: " + ex.Message);
    }
}
        private void OdevleriYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            o.OdevID,
                            LEFT(o.Aciklama, 100) as Aciklama,
                            TO_CHAR(o.VerilisTarihi, 'DD.MM.YYYY') as Verilis_Tarihi,
                            TO_CHAR(o.TeslimTarihi, 'DD.MM.YYYY') as Teslim_Tarihi
                        FROM Odevler o
                        WHERE o.OgretmenID = (
                            SELECT KullaniciID FROM Kullanicilar WHERE TCNo = @tc
                        )
                        ORDER BY o.VerilisTarihi DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("tc", GelenTC);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvOdevler.DataSource = dt;

                        if (dt.Columns.Contains("Aciklama"))
                            dgvOdevler.Columns["Aciklama"].HeaderText = "Ödev Açıklaması";
                        if (dt.Columns.Contains("Verilis_Tarihi"))
                            dgvOdevler.Columns["Verilis_Tarihi"].HeaderText = "Veriliş Tarihi";
                        if (dt.Columns.Contains("Teslim_Tarihi"))
                            dgvOdevler.Columns["Teslim_Tarihi"].HeaderText = "Teslim Tarihi";

                        if (dgvOdevler.Columns.Contains("OdevID"))
                            dgvOdevler.Columns["OdevID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödevler yüklenirken hata: " + ex.Message);
            }
        }

        public OdevYukleUC()
        {
            InitializeComponent();
        }

        private void odevEkleBtn_Click(object sender, EventArgs e)
        {
            if (ogretmenDersID <= 0)
            {
                MessageBox.Show("Ders bilgisi bulunamadı.");
                return;
            }

            if (string.IsNullOrWhiteSpace(odevAciklamaTxt.Text))
            {
                MessageBox.Show("Lütfen ödev açıklaması girin.");
                return;
            }

            // HOCANIN SEÇTİĞİ TARİH
            DateTime secilenTarih = teslimTarihi.Value.Date;

            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    // Öğretmen ID'sini al
                    string ogretmenSql = "SELECT KullaniciID FROM Kullanicilar WHERE TCNo = @tc";
                    int ogretmenID;

                    using (var cmdOgretmen = new NpgsqlCommand(ogretmenSql, conn))
                    {
                        cmdOgretmen.Parameters.AddWithValue("tc", GelenTC);
                        ogretmenID = Convert.ToInt32(cmdOgretmen.ExecuteScalar());
                    }

                    // Ödev ekle
                    string sql = @"
                        INSERT INTO Odevler (DersID, OgretmenID, Aciklama, TeslimTarihi, VerilisTarihi)
                        VALUES (@dersID, @ogretmenID, @aciklama, @teslimTarihi, CURRENT_DATE)
                        RETURNING OdevID";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("dersID", ogretmenDersID);
                        cmd.Parameters.AddWithValue("ogretmenID", ogretmenID);
                        cmd.Parameters.AddWithValue("aciklama", odevAciklamaTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("teslimTarihi", secilenTarih);

                        var newOdevID = cmd.ExecuteScalar();

                        if (newOdevID != null)
                        {
                            MessageBox.Show($"Ödev başarıyla eklendi.\nTeslim Tarihi: {secilenTarih:dd.MM.yyyy}",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // SADECE TextBox temizlensin
                            odevAciklamaTxt.Clear();

                            // DateTimePicker AYNI KALSIN (hoca ne seçtiyse)

                            OdevleriYukle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödev eklenirken hata: " + ex.Message);
            }
        }
    }
}

