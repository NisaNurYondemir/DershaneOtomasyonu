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
    public partial class OgrenciBilgiGuncelleUC : UserControl
    {


        private int seciliOgrenciID = -1;

        public OgrenciBilgiGuncelleUC()
        {
            InitializeComponent();
        }

        private void OgrenciBilgiGuncelleUC_Load(object sender, EventArgs e)
        {
            if (Oturum.RolID == 3 || Oturum.RolID == 4)
            {
                DanismanlariDoldur();
                OgrencileriYukle();
                FormuTemizle();
            }
            else
            {
                MessageBox.Show("Bu sayfa sadece müdür ve idare personeli içindir.");
                this.ParentForm?.Close();
            }
        }

        private void DanismanlariDoldur()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT k.KullaniciID, k.Ad || ' ' || k.Soyad as AdSoyad
                        FROM Kullanicilar k
                        WHERE k.RolID IN (2, 5) AND k.AktifMi = TRUE
                        ORDER BY k.Ad";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbDanisman.Items.Clear();
                        cmbDanisman.Items.Add(new ComboboxItem { Text = "Danışman Atanmadı", Value = 0 });

                        while (reader.Read())
                        {
                            cmbDanisman.Items.Add(new ComboboxItem
                            {
                                Text = reader.GetString(1),
                                Value = reader.GetInt32(0)
                            });
                        }

                        if (cmbDanisman.Items.Count > 0)
                            cmbDanisman.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Danışmanlar yüklenirken hata: " + ex.Message);
            }
        }

        private void OgrencileriYukle()
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
                            COALESCE(danisman.Ad || ' ' || danisman.Soyad, 'Atanmadı') as DanismanAdi
                        FROM Kullanicilar k
                        JOIN OgrenciDetay od ON k.KullaniciID = od.OgrenciID
                        LEFT JOIN Kullanicilar danisman ON od.DanismanOgretmenID = danisman.KullaniciID
                        WHERE k.RolID = 1
                        ORDER BY k.Ad, k.Soyad";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvOgrenciler.DataSource = dt;

                        // Sütun başlıkları
                        if (dt.Columns.Contains("KullaniciID"))
                        {
                            dgvOgrenciler.Columns["KullaniciID"].HeaderText = "ID";
                            dgvOgrenciler.Columns["KullaniciID"].Width = 50;
                        }
                        if (dt.Columns.Contains("Ad")) dgvOgrenciler.Columns["Ad"].HeaderText = "Ad";
                        if (dt.Columns.Contains("Soyad")) dgvOgrenciler.Columns["Soyad"].HeaderText = "Soyad";
                        if (dt.Columns.Contains("TCNo")) dgvOgrenciler.Columns["TCNo"].HeaderText = "TC No";
                        if (dt.Columns.Contains("AktifMi"))
                        {
                            dgvOgrenciler.Columns["AktifMi"].HeaderText = "Aktif";
                            dgvOgrenciler.Columns["AktifMi"].Width = 50;
                        }
                        if (dt.Columns.Contains("SinifSeviyesi")) dgvOgrenciler.Columns["SinifSeviyesi"].HeaderText = "Sınıf";
                        if (dt.Columns.Contains("VeliAdSoyad")) dgvOgrenciler.Columns["VeliAdSoyad"].HeaderText = "Veli";
                        if (dt.Columns.Contains("VeliTel")) dgvOgrenciler.Columns["VeliTel"].HeaderText = "Veli Tel";
                        if (dt.Columns.Contains("DanismanAdi")) dgvOgrenciler.Columns["DanismanAdi"].HeaderText = "Danışman";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenciler yüklenirken hata: " + ex.Message);
            }
        }

        private void dgvOgrenciler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvOgrenciler.Rows.Count > 0)
            {
                DataGridViewRow row = dgvOgrenciler.Rows[e.RowIndex];
                seciliOgrenciID = Convert.ToInt32(row.Cells["KullaniciID"].Value);

                txtAd.Text = row.Cells["Ad"].Value?.ToString();
                txtSoyad.Text = row.Cells["Soyad"].Value?.ToString();
                txtTC.Text = row.Cells["TCNo"].Value?.ToString();
                chkAktif.Checked = Convert.ToBoolean(row.Cells["AktifMi"].Value);

                string sinif = row.Cells["SinifSeviyesi"].Value?.ToString();
                txtSinif.Text = sinif == "0" ? "Mezun" : sinif;

                string danismanAdi = row.Cells["DanismanAdi"].Value?.ToString();
                foreach (ComboboxItem item in cmbDanisman.Items)
                {
                    if (item.Text == danismanAdi || (danismanAdi == "Atanmadı" && item.Value == 0))
                    {
                        cmbDanisman.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliOgrenciID <= 0)
            {
                MessageBox.Show("Lütfen güncellemek için bir öğrenci seçin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Lütfen ad ve soyad girin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTC.Text) || txtTC.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik No 11 haneli olmalıdır.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSinif.Text))
            {
                MessageBox.Show("Lütfen sınıf girin.");
                return;
            }

            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlKullanici = @"
                            UPDATE Kullanicilar 
                            SET Ad = @ad, Soyad = @soyad, TCNo = @tc, AktifMi = @aktif
                            WHERE KullaniciID = @ogrenciID";

                        using (var cmdKullanici = new NpgsqlCommand(sqlKullanici, conn, transaction))
                        {
                            cmdKullanici.Parameters.AddWithValue("ad", txtAd.Text.Trim());
                            cmdKullanici.Parameters.AddWithValue("soyad", txtSoyad.Text.Trim());
                            cmdKullanici.Parameters.AddWithValue("tc", txtTC.Text.Trim());
                            cmdKullanici.Parameters.AddWithValue("aktif", chkAktif.Checked);
                            cmdKullanici.Parameters.AddWithValue("ogrenciID", seciliOgrenciID);
                            cmdKullanici.ExecuteNonQuery();
                        }

                        string sqlDetay = @"
                            UPDATE OgrenciDetay 
                            SET VeliAdSoyad = @veliAd, VeliTel = @veliTel, 
                                DanismanOgretmenID = @danismanID, SinifSeviyesi = @sinif
                            WHERE OgrenciID = @ogrenciID";

                        using (var cmdDetay = new NpgsqlCommand(sqlDetay, conn, transaction))
                        {
                            if (cmbDanisman.SelectedItem is ComboboxItem selectedItem)
                                cmdDetay.Parameters.AddWithValue("danismanID", selectedItem.Value);
                            else
                                cmdDetay.Parameters.AddWithValue("danismanID", DBNull.Value);

                            int sinifSeviyesi = txtSinif.Text == "Mezun" ? 0 : Convert.ToInt32(txtSinif.Text);
                            cmdDetay.Parameters.AddWithValue("sinif", sinifSeviyesi);
                            cmdDetay.Parameters.AddWithValue("ogrenciID", seciliOgrenciID);

                            cmdDetay.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Öğrenci bilgileri güncellendi.", "Başarılı");
                        OgrencileriYukle();
                        FormuTemizle();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Güncelleme sırasında hata: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            seciliOgrenciID = -1;
            txtAd.Clear();
            txtSoyad.Clear();
            txtTC.Clear();
            txtSinif.Clear();
            chkAktif.Checked = true;

            if (cmbDanisman.Items.Count > 0)
                cmbDanisman.SelectedIndex = 0;


        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            DanismanlariDoldur();
            OgrencileriYukle();
            FormuTemizle();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAraTC.Text) && txtAraTC.Text.Length == 11)
            {
                foreach (DataGridViewRow row in dgvOgrenciler.Rows)
                {
                    if (row.Cells["TCNo"].Value?.ToString() == txtAraTC.Text)
                    {
                        dgvOgrenciler.ClearSelection();
                        row.Selected = true;
                        dgvOgrenciler.CurrentCell = row.Cells[0];
                        dgvOgrenciler_CellClick(null, new DataGridViewCellEventArgs(0, row.Index));
                        return;
                    }
                }
                MessageBox.Show("Öğrenci bulunamadı.");
            }
        }
    }




}
