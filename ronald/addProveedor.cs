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
    public partial class addProveedor : Form
    {
        public addProveedor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text== "") {
                cosasGlobales.mensajeError("el rut y el nombre no pueden estar vacios");
                return;
            }
            if (cosasGlobales.arrojaResultados("select * from proveedor where rut_empresa = '"+textBox1.Text+"';")) {
                cosasGlobales.mensajeError("error, el rut ngresado ya existe");
                return;
            }
            string rut, nombre, fono, direccion, mail;
            rut = textBox1.Text;
            nombre = textBox2.Text;
            fono = textBox3.Text;
            direccion = textBox4.Text;
            mail = textBox5.Text;
            string consulta = "INSERT INTO proveedor (`rut_empresa`, `nombre`, `telefono`, `direccion`, `email`) VALUES ('"+rut+"', '"+nombre+"', '"+fono+"', '"+direccion+"', '"+mail+"');";
            cosasGlobales.insertarGeneral(consulta);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
