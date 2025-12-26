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
    public partial class DuyuruOlusturUC : UserControl
    {

        
        public DuyuruOlusturUC()
        {
            InitializeComponent();
        }

        private void DuyuruOlusturUC_Load(object sender, EventArgs e)
        {
            if (Oturum.RolID == 3 || Oturum.RolID == 4) // Müdür veya İdare
            {
                cmbHedefKitle.Items.AddRange(new string[] { "Tumu", "Ogretmen", "Ogrenci" });
                if (cmbHedefKitle.Items.Count > 0)
                    cmbHedefKitle.SelectedIndex = 0;

                DuyurulariYukle();
            }
            else
            {
                MessageBox.Show("Bu sayfa sadece müdür ve idare personeli içindir.");
                this.ParentForm?.Close();
            }

        }

        private void DuyurulariYukle()
        {
            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        SELECT 
                            DuyuruID,
                            Baslik,
                            LEFT(Icerik, 150) as Icerik,
                            TO_CHAR(Tarih, 'DD.MM.YYYY HH24:MI') as Tarih,
                            HedefKitle
                        FROM Duyurular
                        ORDER BY Tarih DESC
                        LIMIT 20";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDuyurular.DataSource = dt;

                        if (dt.Columns.Contains("Baslik"))
                            dgvDuyurular.Columns["Baslik"].HeaderText = "Başlık";
                        if (dt.Columns.Contains("Icerik"))
                            dgvDuyurular.Columns["Icerik"].HeaderText = "İçerik";
                        if (dt.Columns.Contains("Tarih"))
                            dgvDuyurular.Columns["Tarih"].HeaderText = "Tarih";
                        if (dt.Columns.Contains("HedefKitle"))
                            dgvDuyurular.Columns["HedefKitle"].HeaderText = "Hedef Kitle";

                        if (dgvDuyurular.Columns.Contains("DuyuruID"))
                            dgvDuyurular.Columns["DuyuruID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duyurular yüklenirken hata: " + ex.Message);
            }
        }

        private void btnDuyuruEkle_Click(object sender, EventArgs e)
        {
            if (Oturum.RolID != 3 && Oturum.RolID != 4) return;

            if (string.IsNullOrWhiteSpace(txtBaslik.Text))
            {
                MessageBox.Show("Lütfen duyuru başlığı girin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtIcerik.Text))
            {
                MessageBox.Show("Lütfen duyuru içeriği girin.");
                return;
            }

            if (cmbHedefKitle.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen hedef kitle seçin.");
                return;
            }

            try
            {
                using (var conn = Veritabani.BaglantiGetir())
                {
                    string sql = @"
                        INSERT INTO Duyurular (Baslik, Icerik, HedefKitle)
                        VALUES (@baslik, @icerik, @hedefKitle)
                        RETURNING DuyuruID";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("baslik", txtBaslik.Text.Trim());
                        cmd.Parameters.AddWithValue("icerik", txtIcerik.Text.Trim());
                        cmd.Parameters.AddWithValue("hedefKitle", cmbHedefKitle.SelectedItem.ToString());

                        var newDuyuruID = cmd.ExecuteScalar();
                        if (newDuyuruID != null)
                        {
                            MessageBox.Show($"Duyuru başarıyla eklendi. (ID: {newDuyuruID})",
                                "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtBaslik.Clear();
                            txtIcerik.Clear();
                            if (cmbHedefKitle.Items.Count > 0)
                                cmbHedefKitle.SelectedIndex = 0;

                            DuyurulariYukle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duyuru eklenirken hata: " + ex.Message);
            }
        }




    }
}
