using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ronald
{
    public static class cosasGlobales
    {
        public static string servidor = "localhost";
        public static string baseDeDatos = "ronald";
        public static string usuario = "root";
        public static string password = "123456";
        public static string SSLmode = "None";

        public static string strConexion = "server="+servidor+"; database= "+baseDeDatos+";Uid="+usuario+";pwd="+password+";SSL Mode="+SSLmode+";";
        // Conexión a una base de datos MySQL server
        public static MySqlConnection miConexion = new MySqlConnection(strConexion);
        /// <summary>
        /// retorna true si es posible establecer la coneccion 
        /// a la base de datos, caso contrario retorna false
        /// </summary>
        public static bool testConexion()
        {
            try {
                miConexion.Open();
                miConexion.Close();
                return true;

            } catch (Exception e) {
                miConexion = null;
                return false;
            }
        }
        /// <summary>
        /// Modifica los parámetros de la conexión a la base de datos, retorna true 
        /// si puede establecer una conexión con los nuevos parametros, caso contrario retorna false
        /// </summary>
        public static bool reConectar(string nuevoServidor,string nuevaBaseDeDatos , string nuevoUsuario, string nuevaPassword, string nuevoSSLMode ) {
            servidor = nuevoServidor;
            baseDeDatos = nuevaBaseDeDatos;
            usuario = nuevoUsuario;
            password = nuevaPassword;
            SSLmode = nuevoSSLMode;
            strConexion = "server=" + servidor + "; database= " + baseDeDatos + ";Uid=" + usuario + ";pwd=" + password + ";SSL Mode=" + SSLmode + ";";

            miConexion = new MySqlConnection(strConexion);
            return testConexion();

    }
        /// <summary>
        /// carga un comboBox a partir de una consulta SQL que se recibe por parámetro.  
        /// </summary>
        internal static void llenarCombobox(ComboBox comboBox1, string values, string display, string consulta)
        {
            
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;
            miconectar.Close();
            miconectar.Open();// inicia una conexion
            micodigo.Connection = miconectar;

            micodigo.CommandText = consulta;
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);// carga un objeto DataTable con el resultado de la consulta

            comboBox1.ValueMember = values;
            comboBox1.DisplayMember = display;
            comboBox1.DataSource = dt;// carga el ComboBox con los datos de la DataTable

            miconectar.Close();

        }
        /// <summary>
        /// ejecuta una consulta SQL y retorna el número de filas afectadas 
        /// </summary>
        public static int ejecutarConsulta(string consulta)
        {
            int i = 0;
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;
            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;

            micodigo.CommandText = consulta;
            i = micodigo.ExecuteNonQuery();

            miconectar.Close();
            return i;
        }
        /// <summary>
        /// retorna como un tipo string el primer elemento
        /// de la tabla resultante de una consulta SQL recibida por parámetro.
        /// </summary>
        public static string getDatoUnico(string consulta)
        {
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;


            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;
            micodigo.CommandText = (consulta);
            
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);

            miconectar.Close();
            return dt.Rows[0].ItemArray[0].ToString();



        }
        /// <summary>
        /// método que carga un ListBox a partir de una consulta SQL 
        /// que se recibe por parámetro. 
        /// </summary>
        internal static void llenarListBox(ListBox listBox1, string values, string display, string consulta)
        {
            miConexion.Open();

            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;
            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;

            micodigo.CommandText = consulta;
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);

            listBox1.ValueMember = values;
            listBox1.DisplayMember = display;
            listBox1.DataSource = dt;

            miconectar.Close();

        }
        /// <summary>
        /// retorna verdadero o falso dependiendo si una consulta SQL recibida por parámetro tiene resultados.
        /// </summary>
        internal static bool arrojaResultados(string consulat)
        {

            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar =miConexion;
            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;

            micodigo.CommandText = (consulat);
            MySqlDataReader leer = micodigo.ExecuteReader();
            bool i = (leer.Read());
            miconectar.Close();
            return i;
        }
        /// <summary>
        /// retorna un objeto DataTable cargado con la tabla resultante de una consulta SQL recibida por parámetro.
        /// </summary>
        public static DataTable getDt(string consulta)
        {
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;


            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;
            micodigo.CommandText = (consulta);
           
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);

            miconectar.Close();
            return dt;



        }
        /// <summary>
        /// despliega una ventana emergente con un icono de error y un mensaje recibido por parámetro.
        /// </summary>
        public static void mensajeError(string mensaje) {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// ejecuta una consulta SQL de tipo INSERT y despliega una notificación para informar
        /// el éxito de la operación. El método retorna TRUE 
        /// si la inserción se completo satisfactoriamente, caso contrario retorna FALSE.
        /// </summary>
        public static bool insertarGeneral(string consulta) {
            int resultado = ejecutarConsulta(consulta);

            if (resultado > 0)
            {
                MessageBox.Show("Ingreso correcto");


            }
            else
            {
                mensajeError("No se pudo guardar los datos");

            }

            return resultado > 0;
        }
        /// <summary>
        /// retorna el nombre del mes correspondiente al numero recibido por parametro como un tipo string.
        /// </summary>
        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }


    }


}
