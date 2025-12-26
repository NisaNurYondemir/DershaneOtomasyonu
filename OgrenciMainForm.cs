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
    public partial class OgrenciMainForm : Form
    {
        

        public OgrenciMainForm()
        {
            InitializeComponent();
        }

        private void SayfaGetir(UserControl uc)
        {
            pnlIcerik.Controls.Clear();

            if (!pnlIcerik.Controls.Contains(uc))
            {
                uc.Dock = DockStyle.Fill;
                pnlIcerik.Controls.Add(uc);
            }

            uc.BringToFront();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
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

        private void dersProgramıButton_Click(object sender, EventArgs e)
        {
            DersProgramiUC uc = new DersProgramiUC();
            SayfaGetir(uc);
        }

        private void denemelerButton_Click(object sender, EventArgs e)
        {

            DenemelerUC uc = new DenemelerUC();
            SayfaGetir(uc);

        }

        private void odevlerButton_Click(object sender, EventArgs e)
        {
            OdevlerUC uc = new OdevlerUC();
            SayfaGetir(uc);
        }

       

        private void taleplerButton_Click(object sender, EventArgs e)
        {
            TaleplerUC uc = new TaleplerUC();
            SayfaGetir(uc);

        }


        private void OgrenciMainForm_Load(object sender, EventArgs e)
        {
            AnaSayfaGoster();
        }
    }
}
