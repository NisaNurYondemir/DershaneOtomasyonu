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
    public partial class OgretmenDersProgramiForm : Form
    {
        public OgretmenDersProgramiForm()
        {
            InitializeComponent();
        }

        private void anaSayfaButton_Click(object sender, EventArgs e)
        {
            OgretmenMainForm ogretmenMainForm = new OgretmenMainForm();
            ogretmenMainForm.Show();
            this.Close();
        }

        private void dersProgramıButton_Click(object sender, EventArgs e)
        {

        }

        private void denemelerButton_Click(object sender, EventArgs e)
        {
            
        }

        private void odevlerButton_Click(object sender, EventArgs e)
        {
            
        }

        private void taleplerButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
