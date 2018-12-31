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
            string consulta = "SELECT rut , nombre FROM cliente;";
            if(cosasGlobales.arrojaResultados(consulta))
            cosasGlobales.llenarCombobox(comboBox1,"rut","nombre",consulta);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
