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
    public partial class addProducto : Form
    {
        public addProducto()
        {
            InitializeComponent();
        }

        private void addProducto_Load(object sender, EventArgs e)
        {
            
            refrescarProvedor();
        }

        private void refrescarProvedor()
        {
            string consulta = "SELECT rut_empresa, nombre FROM proveedor order by nombre;";
            if (cosasGlobales.arrojaResultados(consulta))
                cosasGlobales.llenarCombobox(comboBox1, "rut_empresa", "nombre",consulta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "") {
                cosasGlobales.mensajeError("el nombre y el productor no pueden ser vacios");
                return;
            }
            string consulta = "INSERT INTO producto (`nombre_prod`, `precio_venta`, `precio_compra`, `proveedor_rut`) VALUES ('"+textBox1.Text+"', '"+numericUpDown1.Value.ToString()+"', '"+numericUpDown2.Value.ToString()+"', '"+comboBox1.SelectedValue.ToString()+"');";
            cosasGlobales.insertarGeneral(consulta);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            addProveedor proveedor = new addProveedor();
            proveedor.ShowDialog();
            refrescarProvedor();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
