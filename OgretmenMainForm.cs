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
    public partial class OgretmenMainForm : Form
    {

        // LoginForm'dan set edilecek
        public string SuAnkiKullaniciTC { get; set; }
        public int SuAnkiRolID { get; set; } = 2; // 2: Öğretmen, 5: Danışman

        public OgretmenMainForm()
        {
            InitializeComponent();

        }

        // Ortak sayfa yükleme metodu
        private void SayfaGetir(UserControl yeniSayfa)
        {
            pnlIcerik.Controls.Clear();
            yeniSayfa.Dock = DockStyle.Fill;
            pnlIcerik.Controls.Add(yeniSayfa);
            yeniSayfa.BringToFront();
        }
        // Ana sayfa
        private void AnaSayfaGoster()
        {
            AnaSayfaUC uc = new AnaSayfaUC();
            SayfaGetir(uc);
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }

        private void anaSayfaButton_Click(object sender, EventArgs e)
        {
            AnaSayfaGoster();
        }


        private void odevlerButton_Click(object sender, EventArgs e)
        {
            OdevYukleUC uc = new OdevYukleUC();
            SayfaGetir(uc);
        }

        private void dersProgramıButton_Click(object sender, EventArgs e)
        {
            DersProgramiUC uc = new DersProgramiUC();
            SayfaGetir(uc);
        }

        private void OgretmenMainForm_Load(object sender, EventArgs e)
        {
            AnaSayfaGoster();
        }

        private void onaylarButton_Click(object sender, EventArgs e)
        {
            TalepOnayUC uc = new TalepOnayUC();
            SayfaGetir(uc);
        }
    }
}
