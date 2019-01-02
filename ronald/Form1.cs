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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminEvento formadminEvento = new adminEvento("");
            formadminEvento.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            addEvento forumEvento = new addEvento();
            forumEvento.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            reportes reporte = new reportes();
            reporte.ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            addProducto producto = new addProducto();
            producto.ShowDialog();
            this.Show();

        }
    }
}
