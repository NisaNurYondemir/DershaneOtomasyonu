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
    public partial class OgretmenListeleUC : UserControl
    {
        public int OgretmenID { get; set; } // Giriş yapan öğretmenin KullaniciID'si

        public OgretmenListeleUC()
        {
            InitializeComponent();
        }

        private void OgretmenListeleUC_Load(object sender, EventArgs e)
        {
            if (OgretmenID <= 0)
            {
                MessageBox.Show("Geçersiz öğretmen bilgisi.");
                this.ParentForm?.Close();
                return;
            }

            OgretmenleriYukle();
        }


        private void OgretmenleriYukle(string tcAra = "")
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                SELECT 
                    KullaniciID,
                    Ad,
                    Soyad,
                    TCNo,
                    AktifMi
                FROM Kullanicilar
                WHERE RolID = 2 " + (string.IsNullOrEmpty(tcAra) ? "" : " AND TCNo = @tc") + @"
                ORDER BY Ad, Soyad";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(tcAra))
                            cmd.Parameters.AddWithValue("tc", tcAra);

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvOgretmenler.DataSource = dt;

                        // Kolon başlıkları
                        if (dt.Columns.Contains("KullaniciID"))
                            dgvOgretmenler.Columns["KullaniciID"].Visible = false;
                        if (dt.Columns.Contains("Ad"))
                            dgvOgretmenler.Columns["Ad"].HeaderText = "Ad";
                        if (dt.Columns.Contains("Soyad"))
                            dgvOgretmenler.Columns["Soyad"].HeaderText = "Soyad";
                        if (dt.Columns.Contains("TCNo"))
                            dgvOgretmenler.Columns["TCNo"].HeaderText = "TC No";
                        if (dt.Columns.Contains("AktifMi"))
                            dgvOgretmenler.Columns["AktifMi"].HeaderText = "Aktif";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmenler yüklenirken hata: " + ex.Message);
            }
        }


        private void btnAra_Click(object sender, EventArgs e)
        {
            string tc = txtAraTC.Text.Trim();
            if (tc.Length == 11)
            {
                OgretmenleriYukle(tc);
            }
            else
            {
                MessageBox.Show("Arama için 11 haneli TC girin.");
            }
        }
    }
}
