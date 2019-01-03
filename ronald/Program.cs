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
            if (!cosasGlobales.testConexion()) {

                configurarConeccion configurar = new configurarConeccion();
                configurar.ShowDialog();
                if (!cosasGlobales.testConexion())
                    return;
            }
            Application.Run(new Form1());
        }
    }
}
