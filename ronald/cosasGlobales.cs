using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ronald
{
    public static class cosasGlobales
    {
        //public static string strConexion = "server=localhost; database= ronald;Uid=root;pwd=123456;SSL Mode=None;";
        public static string strConexion = "server=localhost; database= ronald;Uid=root;pwd=092947411;SSL Mode=None;";
        public static MySqlConnection miConexion = new MySqlConnection(strConexion);

        internal static void llenarCombobox(ComboBox comboBox1, string values, string display, string consulta)
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

            comboBox1.ValueMember = values;
            comboBox1.DisplayMember = display;
            comboBox1.DataSource = dt;

            miconectar.Close();

        }
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

        public static string getDatoUnico(string consulta)
        {
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;


            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;
            micodigo.CommandText = (consulta);
            //MessageBox.Show(micodigo.CommandText);
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);

            miconectar.Close();
            return dt.Rows[0].ItemArray[0].ToString();



        }

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

        public static DataTable getDt(string consulta)
        {
            MySqlCommand micodigo = new MySqlCommand();
            MySqlConnection miconectar = miConexion;


            miconectar.Close();
            miconectar.Open();
            micodigo.Connection = miconectar;
            micodigo.CommandText = (consulta);
            //MessageBox.Show(micodigo.CommandText);
            MySqlDataAdapter da1 = new MySqlDataAdapter(micodigo);
            DataTable dt = new DataTable();
            da1.Fill(dt);

            miconectar.Close();
            return dt;



        }

    }


}
