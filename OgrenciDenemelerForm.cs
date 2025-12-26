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
    public partial class OgrenciDenemelerForm : Form
    {
        public OgrenciDenemelerForm()
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

        }

        private void odevlerButton_Click(object sender, EventArgs e)
        {
            OgrenciOdevlerForm ogrenciOdevlerForm = new OgrenciOdevlerForm();
            ogrenciOdevlerForm.Show();
            this.Close();
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
