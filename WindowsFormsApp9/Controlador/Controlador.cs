using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp9.Modelo;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WindowsFormsApp9.Controlador
{
    public class ControlConfig
    {   //Atributos:
        private string _DBMS;       // El motor de base de datos 
        private string _cadconn;    // La cadena de conexión a la base de datos

        public string DBMS { get { return _DBMS; } }        //Propiedad de solo lectura
        public string cadconn { get { return _cadconn; } }  //Propiedad de solo lectura


        //Métodos:
        public ControlConfig(string DBMS, string cadconn)
        {
            this._DBMS = DBMS;
            this._cadconn = cadconn;
        }
    }

    public abstract class ControlBD
    {   //Atributos:
        protected BDMySQL bd;   // Objeto base de datos
        protected string iSql;

        //Métodos:
        public ControlBD()  // Constructor vacío (requisito de la herencia).
        { }
    }

    public class controladorUsuarios:ControlBD
    {
        public controladorUsuarios(ControlConfig _cfg)  // Constructor que asocia un archivo de configuración que ya
        {                                            // fue leído. (Para no releer el config.ini innecesariamente)
            bd = new BDMySQL(_cfg.cadconn);
        }
        private string obtieneMD5(string _txt)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(_txt));
            string result = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return result;
        }

        public bool validarUsuario(string _usu, string _cve)
        {
            string error = "";

            DataRow dr = bd.Leer1Registro("SELECT * FROM usuarios WHERE cuenta = '" + _usu + "';", ref error);
            if (error.Contains("Unable to connect"))
            {
                MessageBox.Show("Error al conectar con el servidor de la base de datos");
                Application.Exit();
                return false;
            }
            if (dr == null)
            {
                if (error.Contains("Authentication"))
                {
                    MessageBox.Show("Error al conectar con el servidor, revice la cadena de conexion");
                    return false;
                }
                else
                {
                    MessageBox.Show("Error el usuario no esta registrado");
                    return false;
                }
            }
            else if (error.Contains("No hay ninguna fila"))
            {
                MessageBox.Show("Error el usuario no esta registrado");
                return false;
            }
            else
            {
                //if(_cve == dr["clave"].ToString())
                if (obtieneMD5(_cve) == dr["clave"].ToString())
                {
                    MessageBox.Show("Bienvenido(a) " + _usu);
                    return true;
                }
                else
                {
                    MessageBox.Show("Eroor contraseña invalida");
                    return false;
                }
                //return true;
            }
        }

        public DataSet listarUsuariosCtl()
        {
            iSql = "SELECT nombres, apellidos, cuenta FROM usuarios ORDER BY nombres ASC";
            return (bd.LeerRegistros(iSql));
        }

        public DataSet listarUsuarios2Ctl(string _dato_a_buscar)
        {
            iSql = "SELECT  nombres, apellidos, cuenta FROM usuarios WHERE nombres LIKE ('" + _dato_a_buscar + "%') ORDER BY nombres ASC";
            return (bd.LeerRegistros(iSql));
        }
        public bool agregarUsuarioCtl(object[] _datos)
        {
            //mandar el nombre de la tabla y de los datos de la tabla al metodo al modelo
            _datos[3] = obtieneMD5(_datos[3].ToString());
            return (bd.InsertarRegistro("usuarios", _datos));
        }
    }
}
