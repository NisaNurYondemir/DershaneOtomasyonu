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
    public partial class DersProgramiUC : UserControl
    {
        
        public DersProgramiUC()
        {
            InitializeComponent();
        }

        private void DersProgramiUC_Load(object sender, EventArgs e)
        {
            ProgramiGetir();
        }
        public void ProgramiGetir()
        {
            using (var conn = Veritabani.BaglantiGetir())
            {
                try
                {
                    // BURADA AÇ
                    conn.Open();

                    MessageBox.Show("✓ Bağlantı AÇILDI!");

                    // HİÇ WHERE YOK - BASİT SORGU
                    string sql = "SELECT * FROM DersProgrami";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Kaç kayıt var?
                        MessageBox.Show($"✓ {dt.Rows.Count} kayıt getirildi!");

                        // DataGridView'e bağla
                        dgvDersProgrami.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"✗ Hata: {ex.Message}");
                }



            }
        }
    }

}
