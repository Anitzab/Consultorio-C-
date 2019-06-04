using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;


namespace WindowsFormsApp9.Modelo
{
    public class BDMySQL
    {   //Atributos:
        private string _cadconn = "";   // la cadena de conexión
        public string cadconn { get { return _cadconn; } }  //propiedad get para solo lectura de _cadconn

        //Métodos:
        public BDMySQL(string cadena_de_conexion)  //Constructor
        {
            _cadconn = cadena_de_conexion;
        }


        /// <summary>
        /// para Ejecutar consultas SELECT-FROM que devuelvan 1 tabla COMPLETA,  
        /// <para>Ejemplo: DataTable dt = bd.LeerTabla("Libros");</para>
        /// </summary>
        /// <param name="_tabla">la tabla que se quiere leer desde la BD</param>
        /// <returns>Retorna una tabla completa en un DataTable, ó Null si la tabla no se encuentra</returns>
        public DataTable LeerTabla(string _tabla)
        {
            string iSql = "SELECT * FROM " + _tabla + "; ";
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                cnn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(iSql, cnn);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dt.TableName = _tabla;
                    return (dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Instrucción SQL: " + iSql +
                        "\n\nEl DBMS ha devuelto el siguiente error:\n" +
                        ex.Message, "Error en la instrucción SQL",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
            }
        }


        /// <summary>
        /// para Ejecutar consultas SELECT-FROM-WHERE que devuelvan 1 solo registro, 
        /// <para>Ejemplo:  DataRow dr = bd.Leer1Registro("SELECT * FROM Usuarios WHERE Usuario='" + textBox1.Text.Trim() + "';");</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <returns>Retorna 1 registro en un DataRow ó Null si no se puede obtener el registro.</returns>
        public DataRow Leer1Registro(string _sql)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(_sql, cnn);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                    return (ds.Tables[0].Rows[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Instrucción SQL:  " + _sql + "\n\nEl DBMS ha devuelto el siguiente error:\n" + ex.Message,
                        "Error al conectar o recuperar los datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
            }
        }


        /// <summary>
        ///  Para Ejecutar consultas SELECT-FROM-WHERE que devuelvan 1 solo registro,  
        ///  <para>Ejemplo: DataRow dr = bd.Leer1Registro("SELECT * FROM Usuarios WHERE Usuario='" + textBox1.Text.Trim() + "';");</para>
        ///  <para>Si sucede algún error, este método te lo devuelve en la cadena 'error' para que puedas darle un Tratamiento personalizado.</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <param name="_error">string que contendrá el error en caso de que lo haya.</param>
        /// <returns>Retorna 1 registro en un DataRow, ó null si no se puede obtener el registro.</returns>
        public DataRow Leer1Registro(string _sql, ref string _error)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                _error = "";
                //MessageBox.Show(_cadconn);
                try
                {
                    cnn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(_sql, cnn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return (ds.Tables[0].Rows[0]);
                }
                catch (Exception ex)
                {
                    _error = _sql + "\n\n" + ex.Message;
                    return null;
                }
            }
        }


        /// <summary>
        /// para Ejecutar consultas SELECT-FROM-WHERE que devuelve un conjunto de Registros, 
        /// <para>Ejemplo: DataSet ds = bd.LeerRegistros("SELECT usuario, [nombre completo] FROM Usuarios ORDER BY usuario;");</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <returns>Retorna un conjunto de registros en un DataSet, ó Null en caso de error en la instrucción sql.</returns>
        public DataSet LeerRegistros(string _sql)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                cnn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(_sql, cnn);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                    return (ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Instrucción SQL:  " + _sql +
                        "\n\nEl DBMS ha devuelto el siguiente error:\n" + ex.Message,
                        "Error en la instrucción SQL", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return null;
                }
            }
        }


        /// <summary>
        /// para Ejecutar consultas SELECT-FROM-WHERE que devuelve un conjunto de Registros, 
        /// <para>Ejemplo: DataSet ds = bd.LeerRegistros("SELECT usuario, [nombre completo] FROM Usuarios ORDER BY usuario;");</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <param name="_error">string que contendrá el error en caso de que lo haya.</param>
        /// <returns>Retorna un conjunto de registros en un DataSet, ó Null en caso de error en la instrucción sql.</returns>
        public DataSet LeerRegistros(string _sql, ref string _error)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                cnn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(_sql, cnn);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                    return (ds);
                }
                catch (Exception ex)
                {
                    _error = _sql + "\n\n" + ex.Message;
                    return null;
                }
            }
        }


        /// <summary>
        /// Para ejecutar consultas que devuelvan un valor numérico.  <para>Ejemplo: SELECT MAX(num_emp) FROM Empleados;</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <returns>Devuelve el valor obtenido</returns>
        public double LeerNumerico(string _sql)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, cnn);
                try
                {
                    cnn.Open();
                    object num = cmd.ExecuteScalar();
                    cnn.Close();
                    cmd = null;
                    if (num is DBNull)
                        return 0.0;
                    else
                        return (Convert.ToDouble(num));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Instrucción SQL:  " + _sql + "\n\nEl DBMS ha devuelto el siguiente error:\n" + ex.Message,
                        "Error al conectar o recuperar los datos", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cmd = null;
                    return 0.0;
                }
            }
        }


        /// <summary>
        /// Para ejecutar consultas que devuelvan un valor numérico.  <para>Ejemplo: SELECT MAX(num_emp) FROM Empleados;</para>
        /// <para>Si sucede algún error, este método te lo devuelve en la cadena 'error' para que puedas darle un Tratamiento personalizado.</para>
        /// </summary>
        /// <param name="_sql">string con la consulta SQL</param>
        /// <param name="error">variable ref para retener el error, en caso de que lo haya</param>
        /// <returns>Devuelve el valor obtenido</returns>
        public double LeerNumerico(string _sql, ref string _error)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, cnn);
                try
                {
                    cnn.Open();
                    object num = cmd.ExecuteScalar();
                    cnn.Close();
                    cmd = null;
                    if (num is DBNull)
                        return 0.0;
                    else
                        return (Convert.ToDouble(num));
                }
                catch (Exception ex)
                {
                    _error = _sql + "\n\n" + ex.Message;
                    cmd = null;
                    return 0.0;
                }
            }
        }


        /// <summary>
        /// Método para ejecutar instrucciones SQL que no sean SELECT, como: INSERT, UPDATE, DELETE y Otras.
        /// </summary>
        /// <param name="_sql">string con la instrucción SQL</param>
        /// <returns>Retorna true si la consulta se ejecutó correctamente y false si no.</returns>
        public bool EjecutarSQL(string _sql)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, cnn);
                DataSet ds = new DataSet();
                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Instrucción SQL:  " + _sql + "\n\nEl DBMS ha devuelto el siguiente error:\n" + ex.Message,
                        "Error en la instrucción SQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cnn.Close();
                    cmd = null;
                    return false;
                }
            }
        }


        /// <summary>
        /// Método para ejecutar instrucciones SQL que no sean SELECT, como: INSERT, UPDATE, DELETE y Otras.
        /// </summary>
        /// <param name="_sql">string con la instrucción SQL</param>
        /// <param name="_error">string que contendrá el error en caso de que lo haya.</param>
        /// <returns>Retorna true si la consulta se ejecutó correctamente y false si no.</returns>
        public bool EjecutarSQL(string _sql, ref string _error)
        {
            using (MySqlConnection cnn = new MySqlConnection(_cadconn))
            {
                MySqlCommand cmd = new MySqlCommand(_sql, cnn);
                DataSet ds = new DataSet();
                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    _error = _sql + "\n\n" + ex.Message;
                    cnn.Close();
                    cmd = null;
                    return false;
                }
            }
        }


        /// <summary>
        /// Método para ejecutar INSERCIONES de registros individuales de manera más fácil.
        /// </summary>
        /// <param name="_tbl">string con la tabla que recibirá el registro</param>
        /// <param name="_datos">Un arreglo object[] formado por los contenidos de los controles que contienen la información a insertar.</param>
        /// <returns>Retorna true si la consulta se ejecutó correctamente y false si no.</returns>
        public bool InsertarRegistro(string _tbl, object[] _datos)
        /*********************************************************************
         Este método implementa de una manera más fácil las consultas INSERT cuando son muchos campos.
        
         Ejemplo. La instrucción siguiente:
        
         *  iSql = "INSERT INTO clientes VALUES (" + 
         *      txtNum_cliente.Text + ", '" + txtNIP.Text + "', '" + txtNombre.Text + "', ... ) ;";
          
         La puedes hacer más fácil, es decir, sin lidiar con comillas apóstrofes y signos de más, de esta manera:
         1) Primero creas un arreglo de objetos llamado datos:
        
         *  Object[] datos = { txtNum_cliente.Text.Trim(), txtNIP.Text.Trim(), txtNombre.Text.Trim(), 
         *      txtApellidos.Text.Trim(), txtCalleyNum.Text.Trim(), txtEntreCalles.Text.Trim(), 
         *      txtColonia.Text.Trim(), txtCP.Text.Trim(), txtCiudad.Text.Trim(), txtEstado.Text.Trim() };
         *  
        
         2) Y luego lo usas el método de esta manera:
       
         *  if (bd.InsertarRegistro("clientes", datos))
         *  {
         *       MessageBox.Show("Los datos han sido guardados.", "Aviso del Sistema");
         *       HuboCambio = true;
         *  }
         *********************************************************************/
        {
            string iSql = "INSERT INTO " + _tbl + " VALUES (";

            if (_datos[0] is string)
                iSql = iSql + "'" + _datos[0] + "'";
            else if (_datos[0] is DateTime)
                iSql = iSql + fechayhoraMySQL((DateTime)_datos[0]);
            else
                iSql = iSql + _datos[0];

            for (int x = 1; x < _datos.Length; x++)
            {
                if (_datos[x] is string)
                    iSql = iSql + ", '" + _datos[x] + "'";
                else if (_datos[x] is DateTime)
                    iSql = iSql + ", '" + fechayhoraMySQL((DateTime)_datos[x]) + "'";
                else
                    iSql = iSql + ", " + _datos[x];

            }
            iSql = iSql + ");";

            return (this.EjecutarSQL(iSql));
        }


        /// <summary>
        /// Método para ejecutar INSERCIONES de registros individuales de manera más fácil.
        /// </summary>
        /// <param name="_tbl">string con la tabla que recibirá el registro</param>
        /// <param name="_datos">Un arreglo object[] formado por los contenidos de los controles que contienen la información a insertar.</param>
        /// <param name="_error">string que contendrá el error en caso de que lo haya.</param>
        /// <returns>Retorna true si la consulta se ejecutó correctamente y false si no.</returns>
        public bool InsertarRegistro(string _tbl, object[] _datos, ref string _error)
        {
            string iSql = "INSERT INTO " + _tbl + " VALUES (";

            if (_datos[0] is string)
                iSql = iSql + "'" + _datos[0].ToString().ToUpper() + "'";
            else if (_datos[0] is DateTime)
                iSql = iSql + fechayhoraMySQL((DateTime)_datos[0]);
            else
                iSql = iSql + _datos[0];

            for (int x = 1; x < _datos.Length; x++)
            {
                if (_datos[x] is string)
                    iSql = iSql + ", '" + _datos[x].ToString().ToUpper() + "'";
                else if (_datos[x] is DateTime)
                    iSql = iSql + ", " + fechayhoraMySQL((DateTime)_datos[x]);
                else
                    iSql = iSql + ", " + _datos[x];

            }
            iSql = iSql + ");";
            return (this.EjecutarSQL(iSql, ref _error));
        }


        /// <summary>
        /// Devuelve una fecha en formato compatible con MySQL.
        /// </summary>
        /// <param name="_fecha">Un DateTime conteniendo la fecha.</param>
        /// <returns>Retorna un string conteniendo la fecha en formato MySQL.</returns>
        private string fechaMySQL(DateTime _fecha)
        {
            string fa = _fecha.Year + "-" + _fecha.Month + "-" + _fecha.Day;
            return fa;
        }


        /// <summary>
        /// Devuelve un DateTime en formato compatible con MySQL.
        /// </summary>
        /// <param name="_tiempo">Un DateTime conteniendo la fecha y hora.</param>
        /// <returns>Retorna un string conteniendo la fecha y la hora en formato MySQL.</returns>
        private string fechayhoraMySQL(DateTime _tiempo)
        {
            string fh = _tiempo.Year + "-" + _tiempo.Month + "-" + _tiempo.Day + " " +
                        _tiempo.Hour + ":" + _tiempo.Minute + ":" + _tiempo.Second;
            return fh;
        }


        /// <summary>
        /// Método para ejecutar actualizaciones (UPDATES) de registros individuales de manera más fácil.
        /// </summary>
        /// <param name="_tbl">string con la tabla que recibirá el registro</param>
        /// <param name="_campos">Un arreglo object[] formado por los nombres de los campos que se van a actualizar.</param>
        /// <param name="_datos">Un arreglo object[] formado por los contenidos de los controles que contienen la información a actualizar.</param>
        /// <param name="_key">string con el nombre del campo llave de la tabla.</param>
        /// <param name="_valorkey">string con el valor del campo llave de la tabla que se actualiza.</param>
        /// <returns>Retorna true si la consulta se ejecutó correctamente y false si no.</returns>
        public bool ModificarRegistro(string _tbl, string[] _campos, object[] _datos, string _key, object _valorkey)
        /*********************************************************************
         Este método implementa de una manera más fácil las consultas UPDATE cuando son muchos campos.
         *********************************************************************/
        {
            string iSql = "UPDATE " + _tbl + " SET ";

            if (_datos[0] is string)
                iSql = iSql + _campos[0] + " = '" + _datos[0].ToString() + "'";
            else if (_datos[0] is DateTime)
                iSql = iSql + _campos[0] + " = '" + fechayhoraMySQL((DateTime)_datos[0]) + "'";
            else
                iSql = iSql + _campos[0] + " = " + _datos[0];

            for (int x = 1; x < _datos.Length; x++)
            {
                if (_datos[x] is string)
                    iSql = iSql + ", " + _campos[x] + " = '" + _datos[x].ToString() + "'";
                else if (_datos[x] is DateTime)
                    iSql = iSql + ", " + _campos[x] + " = '" + fechayhoraMySQL((DateTime)_datos[x]) + "'";
                else
                    iSql = iSql + ", " + _campos[x] + " = " + _datos[x];
            }

            if (_valorkey is string)
                iSql = iSql + " WHERE " + _key + "='" + _valorkey.ToString() + "';";
            else if (_valorkey is DateTime)
                iSql = iSql + " WHERE " + _key + "='" + fechayhoraMySQL((DateTime)_valorkey) + "';";
            else
                iSql = iSql + " WHERE " + _key + "=" + _valorkey + ";";

            return (this.EjecutarSQL(iSql));
        }
    }
}
