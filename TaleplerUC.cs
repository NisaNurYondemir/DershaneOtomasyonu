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
    public partial class TaleplerUC : UserControl
    {

        public TaleplerUC()
        {
            InitializeComponent();
        }



        public void TalepleriYukle()
        {
            using (var conn = Veritabani.BaglantiGetir())
            {
                string sql = @"
            SELECT 
                TalepID,
                TalepTuru AS ""Talep Türü"",
                Aciklama AS ""Açıklama"",
                Durum,
                OlusturmaTarihi AS ""Tarih""
            FROM Talepler
            WHERE TalepEdenID = @kullaniciID
            ORDER BY OlusturmaTarihi DESC";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("kullaniciID", Oturum.KullaniciID);

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTalepler.DataSource = dt;
                    dgvTalepler.Columns["TalepID"].Visible = false;
                    dgvTalepler.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }
            }
        }


        private void cmbTalepTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilen = cmbTalepTuru.SelectedItem.ToString();
            cmbOgretmenler.Enabled =
                secilen == "OzelDers" || secilen == "DanismanGorusmesi";
        }
        private void TaleplerUC_Load(object sender, EventArgs e)
        {
            

            cmbTalepTuru.Items.AddRange(new string[]
            {
                "OzelDers",
                "DanismanGorusmesi",
                "DersDegisimi",
                "EkDers"
            });

            cmbTalepTuru.SelectedIndex = 0;

            OgretmenleriDoldur();
            TalepleriYukle();

        }

        private void OgretmenleriDoldur()
        {
            using (var conn = Veritabani.BaglantiGetir())
            {
                string sql = @"
                    SELECT KullaniciID, Ad || ' ' || Soyad AS AdSoyad
                    FROM Kullanicilar
                    WHERE RolID IN (2,5) AND AktifMi = TRUE
                    ORDER BY Ad";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    cmbOgretmenler.Items.Clear();
                    while (reader.Read())
                    {
                        cmbOgretmenler.Items.Add(new ComboboxItem
                        {
                            Text = reader.GetString(1),
                            Value = reader.GetInt32(0)
                        });
                    }
                }
            }
        }

        private void btnTalepOlustur_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show("Açıklama giriniz.");
                return;
            }

            int? ogretmenID = null;
            if (cmbOgretmenler.Enabled && cmbOgretmenler.SelectedItem != null)
            {
                ogretmenID = ((ComboboxItem)cmbOgretmenler.SelectedItem).Value;
            }

            using (var conn = Veritabani.BaglantiGetir())
            {
                string sql = @"
                    INSERT INTO Talepler (TalepTuru, Aciklama, IlgiliOgretmenID)
                    VALUES (@tur, @aciklama, @ogretmenID)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("tur", cmbTalepTuru.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("aciklama", txtAciklama.Text.Trim());
                    cmd.Parameters.AddWithValue("ogretmenID",
                        ogretmenID.HasValue ? (object)ogretmenID.Value : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Talep oluşturuldu.");
            txtAciklama.Clear();
            TalepleriYukle();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
