using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ronald
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Si la configuración predeterminada para conectar 
            //a la base de datos no es valida abre una ventana para editar los campos.
            if (!cosasGlobales.testConexion()) {

                configurarConeccion configurar = new configurarConeccion();
                configurar.ShowDialog();
                if (!cosasGlobales.testConexion())//En caso de salir de la ventana de configuración sin corregir el problema cierra el programa.
                    return;
            }
            Application.Run(new Form1());
        }
    }
}
