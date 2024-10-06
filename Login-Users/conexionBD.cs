using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace ConexionBD
{
    internal class conexionBD
    {
        OleDbCommand comando;
        OleDbConnection conexion;
        OleDbDataAdapter adaptador;

        string cadena;

        public conexionBD()
        {
             cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../BD/SistemaGestion.accdb";  // <--- Agregar la ruta de la Base de Datos
        }

        //Metodo para probar si la conexion y ruta esta bien
        public void Conect()
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                conexion.Open();
                MessageBox.Show("Se conecto a BD");
            }
            catch (Exception x)
            {
                MessageBox.Show("Error" + x, "😒");
            }
        }

        //Metodo de validacion del usuario
        public bool ValidarUsuario(string user, string pass)
        {
            string consulta = $"select count(*) from Cuentas where Usuario = '{user}' and Contraseña = '{pass}'";  //Query de Validacion (Mejorar metodo) 
            conexion = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conexion);
            try
            {
                conexion.Open();
                int count = (int)comando.ExecuteScalar();
                if (count > 0)
                {
                    SesionActual.Usuario = user;
                    return true;
                }else
                {
                    return false;
                }               
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
                return false;
            }
        }
        public void AgregarCuenta(string usuario, string contraseña, string nombre, string apellido, string correo, string telefono)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Cuentas (Usuario, Contraseña, Nombre, Apellido, Correo, Telefono) VALUES (?, ?, ?, ?, ?, ?)";

                comando.Parameters.AddWithValue("@Usuario", usuario);
                comando.Parameters.AddWithValue("@Contraseña", contraseña);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Apellido", apellido);
                comando.Parameters.AddWithValue("@Correo", correo);
                comando.Parameters.AddWithValue("@Telefono", telefono);

                conexion.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("La cuenta ha sido registrada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AgregarCliente(string nombreCliente, string nombreEmpresa, string dni, string cuit, string direccion, int provincia, int rubro, int situacionIva, bool estado, int tipo, int localidad)
        {
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();

                
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO ClientesGeneral (NombreCliente, NombreEmpresa, Documento, CUIT, Direccion, idProvincia, idRubro, idSituacionIVA, Estado, idCuenta, idTipo, idLocalidad) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                comando.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                comando.Parameters.AddWithValue("@NombreEmpresa", nombreEmpresa);
                comando.Parameters.AddWithValue("@Documento", dni);
                comando.Parameters.AddWithValue("@CUIT", cuit);
                comando.Parameters.AddWithValue("@Direccion", direccion);
                comando.Parameters.AddWithValue("@idProvincia", provincia);
                comando.Parameters.AddWithValue("@idRubro", rubro);
                comando.Parameters.AddWithValue("@idSituacionIVA", situacionIva);
                comando.Parameters.AddWithValue("@Estado", estado);
                int idCuenta = idCliente();
                comando.Parameters.AddWithValue("@idCuenta", idCuenta);
                comando.Parameters.AddWithValue("@idTipo", tipo);
                comando.Parameters.AddWithValue("@idLocalidad", localidad);

                conexion.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Los datos del cliente han sido guardados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static class SesionActual
        {
            public static string Usuario { get; set; }  // para guardar el usuario
        }
        public DataTable DatosCliente()
        {
            DataTable datosCliente = new DataTable();
            try
            {
                conexion = new OleDbConnection(cadena);
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT Usuario, Contraseña, Nombre, Apellido, Correo, Telefono FROM Cuentas WHERE Usuario = ?";               
                comando.Parameters.AddWithValue("@Usuario", SesionActual.Usuario);
                conexion.Open();

                adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(datosCliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            return datosCliente;
        }
        public int idCliente()
        {
            int idCliente = -1;
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    string consulta = "SELECT idCuenta FROM Cuentas WHERE Usuario = @Usuario";
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", SesionActual.Usuario);
                        conexion.Open();
                        object resultado = comando.ExecuteScalar();
                        if (resultado != null)
                        {
                            idCliente = Convert.ToInt32(resultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID de la cuenta: " + ex.Message);
            }
            return idCliente;
        }
        public void CargarRubros(ComboBox cmbRubros)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT idRubro, Rubro FROM Rubros";
                        conexion.Open();

                        using (OleDbDataReader reader = comando.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cmbRubros.DataSource = dt;
                            cmbRubros.DisplayMember = "Rubro";
                            cmbRubros.ValueMember = "idRubro";
                            cmbRubros.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarProvincias(ComboBox cmbProvincias)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT idProvincia, Provincia FROM Provincias";
                        conexion.Open();

                        using (OleDbDataReader reader = comando.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cmbProvincias.DataSource = dt;
                            cmbProvincias.DisplayMember = "Provincia";
                            cmbProvincias.ValueMember = "idProvincia";
                            cmbProvincias.SelectedIndex = -1;  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarSitIVA(ComboBox cmbIVA)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT idSituacionIVA, SituacionIVA FROM SituacionesIVA";
                        conexion.Open();

                        using (OleDbDataReader reader = comando.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cmbIVA.DataSource = dt;
                            cmbIVA.DisplayMember = "SituacionIVA";
                            cmbIVA.ValueMember = "idSituacionIVA";
                            cmbIVA.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarTipos(ComboBox cmbTipos)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT idTipo, Tipo FROM Tipos";
                        conexion.Open();

                        using (OleDbDataReader reader = comando.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cmbTipos.DataSource = dt;
                            cmbTipos.DisplayMember = "Tipo";
                            cmbTipos.ValueMember = "idTipo";
                            cmbTipos.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarLocalidades(ComboBox cmbLocalidades, int provincia)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT idLocalidad, Localidad FROM Localidades WHERE idProvincia = ?";
                        comando.Parameters.AddWithValue("@idProvincia", provincia);
                        conexion.Open();

                        using (OleDbDataReader reader = comando.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cmbLocalidades.DataSource = dt;
                            cmbLocalidades.DisplayMember = "Localidad";
                            cmbLocalidades.ValueMember = "idLocalidad";
                            cmbLocalidades.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
