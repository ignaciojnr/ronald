﻿using System;
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
        
        public static MySqlConnection miConexion = new MySqlConnection(strConexion);

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

        public static void mensajeError(string mensaje) {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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

        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }


    }


}
