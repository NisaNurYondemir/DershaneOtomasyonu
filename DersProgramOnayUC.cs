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
    public partial class DersProgramOnayUC : UserControl
    {
        private int seciliProgramID = -1;
        public DersProgramOnayUC()
        {
            InitializeComponent();
        }


        private void DersProgramOnayUC_Load(object sender, EventArgs e)
        {
            // Sadece Müdür rolü görebilsin
            if (Oturum.RolID != 3)
            {
                MessageBox.Show("Bu sayfa sadece müdür için geçerlidir.");
                this.ParentForm?.Close();
                return;
            }

            DersProgramlariniYukle();
        }

        private void DersProgramlariniYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            dp.ProgramID,
                            d.DersAdi,
                            k.Ad || ' ' || k.Soyad AS Ogretmen,
                            dl.DerslikAdi,
                            dp.Gun,
                            TO_CHAR(dp.BaslangicSaati, 'HH24:MI') AS Baslangic,
                            TO_CHAR(dp.BitisSaati, 'HH24:MI') AS Bitis,
                            dp.OnaylandiMi
                        FROM DersProgrami dp
                        JOIN Dersler d ON dp.DersID = d.DersID
                        JOIN Kullanicilar k ON dp.OgretmenID = k.KullaniciID
                        JOIN Derslikler dl ON dp.DerslikID = dl.DerslikID
                        WHERE dp.OnaylandiMi IS FALSE
                        ORDER BY dp.Gun, dp.BaslangicSaati";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDersProgram.DataSource = dt;

                    // Kolon başlıkları
                    dgvDersProgram.Columns["ProgramID"].Visible = false;
                    dgvDersProgram.Columns["DersAdi"].HeaderText = "Ders";
                    dgvDersProgram.Columns["Ogretmen"].HeaderText = "Öğretmen";
                    dgvDersProgram.Columns["DerslikAdi"].HeaderText = "Derslik";
                    dgvDersProgram.Columns["Gun"].HeaderText = "Gün";
                    dgvDersProgram.Columns["Baslangic"].HeaderText = "Başlangıç";
                    dgvDersProgram.Columns["Bitis"].HeaderText = "Bitiş";
                    dgvDersProgram.Columns["OnaylandiMi"].HeaderText = "Onay Durumu";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ders programları yüklenirken hata: " + ex.Message);
            }
        }

        private void dgvDersProgram_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDersProgram.Rows.Count > 0)
            {
                DataGridViewRow row = dgvDersProgram.Rows[e.RowIndex];
                seciliProgramID = Convert.ToInt32(row.Cells["ProgramID"].Value);
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (seciliProgramID <= 0)
            {
                MessageBox.Show("Lütfen onaylamak için bir ders programı seçin.");
                return;
            }

            OnayDurumunuGuncelle(true);
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            if (seciliProgramID <= 0)
            {
                MessageBox.Show("Lütfen reddetmek için bir ders programı seçin.");
                return;
            }

            OnayDurumunuGuncelle(false);
        }

        private void OnayDurumunuGuncelle(bool onaylandiMi)
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = "UPDATE DersProgrami SET OnaylandiMi = @onaylandi WHERE ProgramID = @programID";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("onaylandi", onaylandiMi);
                        cmd.Parameters.AddWithValue("programID", seciliProgramID);

                        int sonuc = cmd.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show(onaylandiMi ? "Ders programı onaylandı." : "Ders programı reddedildi.");
                            seciliProgramID = -1;
                            DersProgramlariniYukle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata: " + ex.Message);
            }
        }
    }
}
