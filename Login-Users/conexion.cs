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
    internal class conexion
    {
        OleDbCommand comando;
        OleDbConnection conectar;
        OleDbDataAdapter adaptador;

        string cadena;

        public conexion()
        {
             cadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../BD/SistemaGestion.accdb";  // <--- Agregar la ruta de la Base de Datos
        }

        //Metodo para probar si la conexion y ruta esta bien
        public void Conect()
        {
            try
            {
                conectar = new OleDbConnection(cadena);
                conectar.Open();
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
            conectar = new OleDbConnection(cadena);
            comando = new OleDbCommand(consulta, conectar);
            try
            {
                conectar.Open();
                int count = (int)comando.ExecuteScalar();
                return count > 0;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
                return false;
            }
        }

    }
}
