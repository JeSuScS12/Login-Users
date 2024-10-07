using ConexionBD;
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
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        //Instanciar conexion
        conexionBD nuevo = new conexionBD();

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Funcion Abrir en Contenedor
        private void AbrirFrm(object frmHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
            {
                this.panelContenedor.Controls.RemoveAt(0);
            }

            Form frm = frmHijo as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(frm);
            this.panelContenedor.Tag = frm;
            frm.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            AbrirFrm(new frmPrograma());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            nuevo.ValidarCargaDatos();
        }
    }
}
