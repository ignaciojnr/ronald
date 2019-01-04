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
            dateTimePicker1.MinDate = DateTime.Today;//Restringe que no se ingresen eventos en el pasado.
            refrescarComboBox();//Se cargan los clientes registrados en la base de datos.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// actualiza la información de los clientes en el comboBox co la información en la base de datos.
        /// </summary>
        private void refrescarComboBox() {
            string consulta = "SELECT rut,  CONCAT(nombre, ' (', rut,')') As nom FROM cliente order by nombre;";
            if (cosasGlobales.arrojaResultados(consulta))
                cosasGlobales.llenarCombobox(comboBox1, "rut", "nom", consulta);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            addCliente nuevoCliente = new addCliente();
            nuevoCliente.ShowDialog(); //Despliega la venta de ingreso de clientes.
            refrescarComboBox();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="") {
                cosasGlobales.mensajeError("el nombre del evento no puede ser vacio");
                return;
            }

            string consulta = "INSERT INTO evento (`nombre`, `fecha`, `clienterut`) VALUES ('" + textBox1.Text + "', '" + dateTimePicker1.Text + "', '" + comboBox1.SelectedValue.ToString() + "');";
            string evencode = "";
            // Se inserta un evento.
            if (cosasGlobales.insertarGeneral(consulta))
                //Si la inserción es exitosa se guarda la respectiva primary key
                evencode = cosasGlobales.getDatoUnico("SELECT LAST_INSERT_ID();");

            textBox1.ResetText();
            dateTimePicker1.ResetText();
            this.Hide();
            adminEvento formAdminEvento = new adminEvento(evencode);
            formAdminEvento.ShowDialog();//Carga la ventana de administración de eventos con el evento creado seleccionado.
            this.Show();


        }
    }
}
