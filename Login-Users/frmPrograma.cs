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
    public partial class frmPrograma : Form
    {
        public frmPrograma()
        {
            InitializeComponent();
            cmbProvincia.SelectedIndexChanged += new EventHandler(cmbProvincia_SelectedIndexChanged);
        }
        conexionBD ObjPrograma = new conexionBD();
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrograma_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void frmPrograma_Load(object sender, EventArgs e)
        {
            DatosCliente();
            ObjPrograma.CargarSitIVA(cmbSituacionIVA);
            ObjPrograma.CargarRubros(cmbRubro);
            ObjPrograma.CargarTipos(cmbTipo);
            ObjPrograma.CargarProvincias(cmbProvincia);
            cmbEstado.SelectedIndex = 0;
        }
        private void DatosCliente()
        {
            DataTable datosCliente = ObjPrograma.DatosCliente();
            txtUsuario.Text = datosCliente.Rows[0]["Usuario"].ToString();
            txtContraseña.Text = datosCliente.Rows[0]["Contraseña"].ToString();
            txtNombre.Text = datosCliente.Rows[0]["Nombre"].ToString();
            txtApellido.Text = datosCliente.Rows[0]["Apellido"].ToString();
            txtCorreo.Text = datosCliente.Rows[0]["Correo"].ToString();
            txtTelefono.Text = datosCliente.Rows[0]["Telefono"].ToString();
        }

        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombreEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtNombreCliente.Text != "" && txtDocumento.Text != "" && txtCuit.Text != "" &&
                txtNombreEmpresa.Text != "" && txtDireccion.Text != "")
            {
                if (cmbSituacionIVA.Text != "" && cmbRubro.Text != "" && cmbTipo.Text != "" && 
                    cmbProvincia.Text != "" && cmbLocalidad.Text != "")
                {
                    string nombreCliente = txtNombreCliente.Text;
                    string dni = txtDocumento.Text;
                    string cuit = txtCuit.Text;
                    string nombreEmpresa = txtNombreEmpresa.Text;
                    int situacionIVA = Convert.ToInt32(cmbSituacionIVA.SelectedValue);
                    int rubro = Convert.ToInt32(cmbRubro.SelectedValue);
                    int tipo = Convert.ToInt32(cmbTipo.SelectedValue);
                    string direccion = txtDireccion.Text;
                    int provincia = Convert.ToInt32(cmbProvincia.SelectedValue);
                    int localidad = Convert.ToInt32(cmbLocalidad.SelectedValue);

                    ObjPrograma.AgregarCliente(nombreCliente, nombreEmpresa, dni, cuit, direccion, provincia, rubro, situacionIVA, true, tipo, localidad);

                }
            }
        }

        private void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue != null && cmbProvincia.SelectedValue is int)
            {
                int idProvincia = Convert.ToInt32(cmbProvincia.SelectedValue);
                ObjPrograma.CargarLocalidades(cmbLocalidad, idProvincia);
            }
        }
    }
}
