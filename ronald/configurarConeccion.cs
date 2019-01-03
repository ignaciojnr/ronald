using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ronald
{
    public partial class configurarConeccion : Form
    {
        public configurarConeccion()
        {
            InitializeComponent();
        }

        private void configurarConeccion_Load(object sender, EventArgs e)
        {
            textBox1.Text = cosasGlobales.servidor;
            textBox2.Text = cosasGlobales.baseDeDatos;
            textBox3.Text = cosasGlobales.usuario;
            textBox5.Text = cosasGlobales.SSLmode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cosasGlobales.reConectar(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text))
                cosasGlobales.mensajeError("No se pudo establecer la conexión con la base de datos.");
            else
                this.Close();
        }
    }
}
