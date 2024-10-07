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

using System.Text.RegularExpressions;

namespace Login_Users
{
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
        }

        //Instancia de conexion
        conexionBD nuevo = new conexionBD();


        private void frmRegistro_Load(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Validar(lblCheckNombre, txtNombre);
        }

        //Funcion Validacion
        public void Validar(Label lbl, TextBox txt)
        {
            // Expresión regular para letras y espacios
            string validacion = @"^[a-zA-Z\s]+$";

            if (!Regex.IsMatch(txt.Text, validacion))
            {
                lbl.ForeColor = Color.Red;
                lbl.Text = "❌";
            }
            else
            {
                lbl.ForeColor = Color.Blue;
                lbl.Text = "✅";
            }
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            Validar(lblCheckApellido, txtApellido);
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            ValidarC(lblCheckCorreo, txtCorreo);
        }

        //Validar correo
        public void ValidarC(Label lbl, TextBox txt)
        {
            // Expresión regular Para validar correo
            string validacion = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(txt.Text, validacion))
            {
                lbl.ForeColor = Color.Red;
                lbl.Text = "❌";
            }
            else
            {
                lbl.ForeColor = Color.Blue;
                lbl.Text = "✅";
            }
        }

        //Validar numero
        public void ValidarTelf(Label lbl, TextBox txt)
        {
            // Expresión regular para telefono de 8 caracteres y empieze con 3
            string validacion = "^3\\d{9}$";

            if (!Regex.IsMatch(txt.Text, validacion))
            {
                lbl.ForeColor = Color.Red;
                lbl.Text = "❌";
            }
            else
            {
                lbl.ForeColor = Color.Blue;
                lbl.Text = "✅";

            }
        }
        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            ValidarTelf(lblCheckTelefono, txtTelefono);
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            ValidarUser(lblCheckUsuario, txtUsuario);
        }

        //Validar Usuario
        public void ValidarUser(Label lbl, TextBox txt)
        {
            // Expresión regular para letras y numeros
            string validacion = "^[a-zA-Z0-9]{5,}$";

            if (!Regex.IsMatch(txt.Text, validacion))
            {
                lbl.ForeColor = Color.Red;
                lbl.Text = "❌";
            }
            else
            {
                lbl.ForeColor = Color.Blue;
                lbl.Text = "✅";

            }
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            ValidarPass(lblCheckContra, txtContraseña);
        }

        //Validar Contraseña
        public void ValidarPass(Label lbl, TextBox txt)
        {
            // Expresión regular para letras y espacios
            string validacion = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$";

            if (!Regex.IsMatch(txt.Text, validacion))
            {
                lbl.ForeColor = Color.Red;
                lbl.Text = "❌";
            }
            else
            {
                lbl.ForeColor = Color.Blue;
                lbl.Text = "✅";

            }
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked)
            {
                if (txtContraseña.PasswordChar == '●') txtContraseña.PasswordChar = '\0';
            }
            else txtContraseña.PasswordChar = '●';
        }


        //Agrega Codigo AQUI  ✅
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtCorreo.Text != "" && 
                txtTelefono.Text != "" && txtUsuario.Text != "" && txtContraseña.Text != "")
            {
                string usuario = txtUsuario.Text;
                string contraseña = txtContraseña.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string correo = txtCorreo.Text;
                string telefono = txtTelefono.Text;

                nuevo.AgregarCuenta(usuario, contraseña, nombre, apellido, correo, telefono);
                txtUsuario.Text = "";
                txtContraseña.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtCorreo.Text = "";
                txtTelefono.Text = "";                
            }
            else
            {
                MessageBox.Show("Debe rellenar todos los campos para registrar la cuenta.", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}