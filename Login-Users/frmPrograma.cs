using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Users
{
    public partial class frmPrograma : Form
    {
        public frmPrograma()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrograma_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

    }
}
