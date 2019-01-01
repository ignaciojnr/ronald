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
    public partial class addEvento : Form
    {
        public addEvento()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void addEvento_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Today;
            refrescarComboBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void refrescarComboBox() {
            string consulta = "SELECT rut,  CONCAT(nombre, ' (', rut,')') As nom FROM cliente order by nombre;";
            if (cosasGlobales.arrojaResultados(consulta))
                cosasGlobales.llenarCombobox(comboBox1, "rut", "nom", consulta);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            addCliente nuevoCliente = new addCliente();
            nuevoCliente.ShowDialog();
            refrescarComboBox();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="") {
                cosasGlobales.mensajeError("el nombre del evento no puede ser vacio");
                return;
            }
            string consulta = "INSERT INTO evento (`fecha`, `clienterut`) VALUES ('" + dateTimePicker1.Text + "', '" + comboBox1.SelectedValue.ToString() +"');";
            cosasGlobales.insertarGeneral(consulta);
            textBox1.ResetText();
            dateTimePicker1.ResetText();
            this.Hide();

        }
    }
}
