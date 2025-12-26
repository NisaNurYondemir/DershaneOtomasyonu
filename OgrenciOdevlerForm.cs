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
    public partial class OgrenciOdevlerForm : Form
    {
        public OgrenciOdevlerForm()
        {
            InitializeComponent();
        }

        private void anaSayfaButton_Click(object sender, EventArgs e)
        {
            OgrenciMainForm ogrenciMainForm = new OgrenciMainForm();
            ogrenciMainForm.Show();
            this.Close();
        }

        private void dersProgramıButton_Click(object sender, EventArgs e)
        {
            OgrenciDersProgramiForm ogrenciDersProgramiForm = new OgrenciDersProgramiForm();
            ogrenciDersProgramiForm.Show();
            this.Close();
        }

        private void denemelerButton_Click(object sender, EventArgs e)
        {
            OgrenciDenemelerForm ogrenciDenemelerForm = new OgrenciDenemelerForm();
            ogrenciDenemelerForm.Show();
            this.Close();
        }

        private void odevlerButton_Click(object sender, EventArgs e)
        {

        }

        private void taleplerButton_Click(object sender, EventArgs e)
        {
            OgrenciTaleplerForm ogrenciTaleplerForm = new OgrenciTaleplerForm();
            ogrenciTaleplerForm.Show();
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
