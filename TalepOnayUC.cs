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
    public partial class TalepOnayUC : UserControl
    {
        
        public TalepOnayUC()
        {
            InitializeComponent();
        }

        private void TalepDurumGuncelle(int talepID, string yeniDurum)
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = "UPDATE Talepler SET Durum = @durum WHERE TalepID = @talepID";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("durum", yeniDurum);
                        cmd.Parameters.AddWithValue("talepID", talepID);

                        int affected = cmd.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show($"Talep {yeniDurum.ToLower()}.");
                            TalepleriYukle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Talep güncellenirken hata: " + ex.Message);
            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            if (dgvTalepler.SelectedRows.Count > 0 && dgvTalepler.Columns.Contains("TalepID"))
            {
                int talepID = Convert.ToInt32(dgvTalepler.SelectedRows[0].Cells["TalepID"].Value);
                TalepDurumGuncelle(talepID, "Reddedildi");
            }
            else
            {
                MessageBox.Show("Lütfen reddetmek için bir talep seçin.");
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (dgvTalepler.SelectedRows.Count > 0 && dgvTalepler.Columns.Contains("TalepID"))
            {
                int talepID = Convert.ToInt32(dgvTalepler.SelectedRows[0].Cells["TalepID"].Value);
                TalepDurumGuncelle(talepID, "Onaylandi");
            }
            else
            {
                MessageBox.Show("Lütfen onaylamak için bir talep seçin.");
            }
        }

        private void TalepOnayUC_Load(object sender, EventArgs e)
        {

            // Rol kontrolü: sadece yetkili roller
            if (Oturum.RolID >= 2 && Oturum.RolID <= 5)
            {
                TalepleriYukle();
            }
            else
            {
                MessageBox.Show("Bu sayfaya erişim yetkiniz yok.");
                this.ParentForm?.Close();
            }
        }

        private void TalepleriYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    int kullaniciID = Oturum.KullaniciID;
                    string sql = "";

                    if (Oturum.RolID == 3 || Oturum.RolID == 4) // Müdür / İdare - tüm talepler
                    {
                        sql = @"
                            SELECT 
                                t.TalepID,
                                k.Ad || ' ' || k.Soyad as TalepEden,
                                t.TalepTuru,
                                LEFT(t.Aciklama, 100) as Aciklama,
                                t.Durum,
                                TO_CHAR(t.OlusturmaTarihi, 'DD.MM.YYYY HH24:MI') as Tarih
                            FROM Talepler t
                            JOIN Kullanicilar k ON t.TalepEdenID = k.KullaniciID
                            ORDER BY t.OlusturmaTarihi DESC";
                    }
                    else if (Oturum.RolID == 2) // Öğretmen - sadece kendisine gelen talepler
                    {
                        sql = @"
                            SELECT 
                                t.TalepID,
                                k.Ad || ' ' || k.Soyad as TalepEden,
                                t.TalepTuru,
                                LEFT(t.Aciklama, 100) as Aciklama,
                                t.Durum,
                                TO_CHAR(t.OlusturmaTarihi, 'DD.MM.YYYY HH24:MI') as Tarih
                            FROM Talepler t
                            JOIN Kullanicilar k ON t.TalepEdenID = k.KullaniciID
                            WHERE t.IlgiliOgretmenID = @ogretmenID
                            ORDER BY t.OlusturmaTarihi DESC";
                    }
                    else if (Oturum.RolID == 5) // Rehberlik - tüm öğrenci talepleri
                    {
                        sql = @"
                            SELECT 
                                t.TalepID,
                                k.Ad || ' ' || k.Soyad as TalepEden,
                                t.TalepTuru,
                                LEFT(t.Aciklama, 100) as Aciklama,
                                t.Durum,
                                TO_CHAR(t.OlusturmaTarihi, 'DD.MM.YYYY HH24:MI') as Tarih
                            FROM Talepler t
                            JOIN Kullanicilar k ON t.TalepEdenID = k.KullaniciID
                            WHERE k.RolID = 1
                            ORDER BY t.OlusturmaTarihi DESC";
                    }

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (Oturum.RolID == 2)
                            cmd.Parameters.AddWithValue("ogretmenID", kullaniciID);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvTalepler.DataSource = dt;

                        // Sütun başlıkları
                        if (dt.Columns.Contains("TalepEden"))
                            dgvTalepler.Columns["TalepEden"].HeaderText = "Talep Eden";
                        if (dt.Columns.Contains("TalepTuru"))
                            dgvTalepler.Columns["TalepTuru"].HeaderText = "Talep Türü";
                        if (dt.Columns.Contains("Aciklama"))
                            dgvTalepler.Columns["Aciklama"].HeaderText = "Açıklama";
                        if (dt.Columns.Contains("Durum"))
                            dgvTalepler.Columns["Durum"].HeaderText = "Durum";
                        if (dt.Columns.Contains("Tarih"))
                            dgvTalepler.Columns["Tarih"].HeaderText = "Tarih";

                        if (dgvTalepler.Columns.Contains("TalepID"))
                            dgvTalepler.Columns["TalepID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Talepler yüklenirken hata: " + ex.Message);
            }
        }

    }
}

