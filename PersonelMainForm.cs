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
    public partial class PersonelMainForm : Form
    {
        
        public PersonelMainForm()
        {
            InitializeComponent();
        }

        private void SayfaGetir(UserControl yeniSayfa)
        {
            pnlIcerik.Controls.Clear();
            yeniSayfa.Dock = DockStyle.Fill;
            pnlIcerik.Controls.Add(yeniSayfa);
            yeniSayfa.BringToFront();
        }

        private void PersonelMainForm_Load(object sender, EventArgs e)
        {
            AnaSayfaGoster();
        }

        private void AnaSayfaGoster()
        {
            AnaSayfaUC uc = new AnaSayfaUC();
            SayfaGetir(uc);
        }

        private void anaSayfaButton_Click(object sender, EventArgs e)
        {
            AnaSayfaGoster();
        }

        private void dersProgramıOlusturBtn_Click(object sender, EventArgs e)
        {
            DersProgramiOlusturUC uc = new DersProgramiOlusturUC();
            SayfaGetir(uc);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }

        private void duyurularButton_Click(object sender, EventArgs e)
        {
            DuyuruOlusturUC uc = new DuyuruOlusturUC();
            SayfaGetir(uc);

        }

        private void kayitButton_Click(object sender, EventArgs e)
        {
            OgrenciKayitUC uc = new OgrenciKayitUC();
            SayfaGetir(uc);
        }

        private void OgrencilerButton_Click(object sender, EventArgs e)
        {
            OgrenciBilgiGuncelleUC uc = new OgrenciBilgiGuncelleUC();
            SayfaGetir(uc);
        }
    }
}
