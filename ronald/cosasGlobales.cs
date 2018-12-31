using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ronald
{
    public static class cosasGlobales
    {
        public static string strConexion = "server=localhost; database= ronald;Uid=root;pwd=092947411;SSL Mode=None;";
        public static MySqlConnection miConexion = new MySqlConnection(strConexion);

        internal static void llenarCombobox(ComboBox comboBox1, string values, string display, string consulta)
        {
            miConexion.Open();

            
        }
    }
}
