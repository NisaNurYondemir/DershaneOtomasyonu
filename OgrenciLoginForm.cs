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
using Npgsql;

namespace DershaneOtomasyonu
{
    public partial class OgrenciLoginForm : Form
    {
        public OgrenciLoginForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ogrenciGirisYapButton_Click(object sender, EventArgs e)
        {


            /*OgrenciMainForm ogrenciMainForm = new OgrenciMainForm();
            ogrenciMainForm.Show();
                this.Close();*/
        }
    }
}
