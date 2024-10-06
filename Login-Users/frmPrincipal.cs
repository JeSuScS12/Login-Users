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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //Instanciar conexion
        conexionBD nuevo = new conexionBD();

        //Funcion Abrir Formularios
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

        private void button2_Click(object sender, EventArgs e)
        {
            ColorBtn(panelUp);
            btnLogin.BackColor = Color.FromArgb(60, 121, 124);
            
            panelContenedor.Visible = false;
            panelContenedor.Dock = DockStyle.None;

            panelLogin.Visible = true;
            panelLogin.Dock = DockStyle.Fill;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorBtn(panelUp);
            btnRegistro.BackColor = Color.FromArgb(60, 121, 124);

            panelLogin.Visible = false;
            panelLogin.Dock = DockStyle.None;

            panelContenedor.Visible= true;
            panelContenedor.Dock = DockStyle.Fill;
            AbrirFrm(new frmRegistro());
        }



        //Control de colores de Botones
        public void ColorBtn(Panel panel)
        {
            foreach (Control btn in panel.Controls)
            {
                if (btn is Button)
                {
                    if(btn.Text != "X")
                    {
                        if (btn.BackColor == Color.FromArgb(60, 121, 124))
                        {
                            btn.BackColor = Color.FromArgb(7, 88, 91);
                        }
                    }
                }
            }

        }

        //-----------------------------------------------------------------
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            panelLogin.Dock = DockStyle.Fill;
        }

        //-----------------------------------------------------------------

        private void frm_close(object sender, FormClosingEventArgs e)
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            this.Show();
            txtUsuario.Focus();
        }


        //Boton de validacion del Ususario en la BD
        private void btnIngreso_Click_1(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtContraseña.Text.Trim();

            if (txtUsuario.Text != "")
            {
                if (txtContraseña.Text != "")
                {
                    lblError.Text = "";
                    lblError.Visible = false;

                    if (nuevo.ValidarUsuario(user, pass))
                    {
                        frmPrograma frm = new frmPrograma();

                        frm.Show();
                        this.Hide();
                        frm.FormClosing += frm_close;
                    }
                    else lblError.Text = "El usuario no existe";
                }
                else lblError.Text = "Ingrese Contraseña";
                lblError.Visible = true;
            }
            else lblError.Text = "Ingrese Usuario";
            lblError.Visible = true;
        }


        //Cambiar el formato de la contraseña
        private void chkMostrar_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkMostrar.Checked)
            {
                if (txtContraseña.PasswordChar == '●') txtContraseña.PasswordChar = '\0';
            }
            else txtContraseña.PasswordChar = '●';
        }
    }
}
