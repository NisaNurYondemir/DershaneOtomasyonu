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

    public partial class MudurMainForm : Form
    {
        public string suAnkiKullaniciTC { get; set; }
        private void SayfaGetir(UserControl yeniSayfa)
        {
            pnlIcerik.Controls.Clear();
            yeniSayfa.Dock = DockStyle.Fill;
            pnlIcerik.Controls.Add(yeniSayfa);
        }
        public MudurMainForm()
        {
            InitializeComponent();
        }

        private void MudurMainForm_Load(object sender, EventArgs e)
        {
            // Oturum bilgisini form açılışında ata
            Oturum.TC = suAnkiKullaniciTC;
            Oturum.RolID = 3; // Müdür
        }

        private void denemelerButton_Click(object sender, EventArgs e)
        {
            DenemelerUC uc = new DenemelerUC();
            SayfaGetir(uc); // UC artık Oturum.RolID üzerinden kontrol yapacak
        }

        private void onaylarButton_Click(object sender, EventArgs e)
        {
            DersProgramOnayUC uc = new DersProgramOnayUC();
            SayfaGetir(uc);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void anaSayfaButton_Click(object sender, EventArgs e)
        {
            AnaSayfaUC uc = new AnaSayfaUC();
            SayfaGetir(uc);
        }

        // Oturum sınıfı: uygulama genelinde geçerli kullanıcı bilgilerini saklar
        public static class Oturum
        {
            public static string TC { get; set; }
            public static int RolID { get; set; }
        }

        private void ogretmenlerButton_Click(object sender, EventArgs e)
        {
            OgretmenListeleUC uc = new OgretmenListeleUC();
            SayfaGetir(uc);
        }

        private void ogrencilerButton_Click(object sender, EventArgs e)
        {
            OgrenciListeleUC uc = new OgrenciListeleUC();
            SayfaGetir(uc);
        }
    }
}
