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
    public partial class adminEvento : Form
    {
        private string evento = "";
        public adminEvento(string codEvento)
        {
            evento = codEvento;
            InitializeComponent();
        }

        private void adminEvento_Load(object sender, EventArgs e)
        {
            string consulta = "";
            if (!cosasGlobales.arrojaResultados("SELECT * FROM evento;")){ }else{


                if (evento == "" || evento == null)
                {
                    consulta = "select cod_even, concat(cod_even , ' ',evento.nombre,' (',fecha,' ',cliente.nombre,' ',cliente.rut ,')' ) as nom from evento, cliente where evento.clienterut = cliente.rut order by evento.nombre;";
                }
                else
                {
                    consulta = "select cod_even, concat(cod_even , ' ',evento.nombre,' (',fecha,' ',cliente.nombre,' ',cliente.rut ,')' ) as nom from evento, cliente where evento.clienterut = cliente.rut and evento.cod_even = '" + evento + "' order by evento.nombre;"
                }
                cosasGlobales.llenarCombobox(comboBox1, "cod_even", "nom", consulta);
                string minDate = cosasGlobales.getDatoUnico("SELECT min(fecha) FROM evento;");
                string maxDate = cosasGlobales.getDatoUnico("SELECT max(fecha) FROM evento;");
                DateTime fecha1 = DateTime.ParseExact(minDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime fecha2 = DateTime.ParseExact(maxDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                dateTimePicker1.MinDate = fecha1;
                dateTimePicker1.MaxDate = fecha2;
                dateTimePicker2.MinDate = fecha1;
                dateTimePicker2.MaxDate = fecha2;
                consulta = "SELECT cod_prod, concat(nombre_prod, ' $(',precio_venta,'/',precio_compra,')') as nom FROM producto order by nombre_prod; ";
                if (cosasGlobales.arrojaResultados(consulta))
                    cosasGlobales.llenarCombobox(comboBox2, "cod_prod", "nom", consulta);
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refrescarListBox();
            

        }

        private void refrescarListBox()
        {
            string consulta = "SELECT producto.cod_prod, concat(producto.nombre_prod, ' ', ingreso_producto.cant_ingr) as nom FROM ingreso_producto, producto where producto.cod_prod = ingreso_producto.producto_cod and ingreso_producto.evento_cod = '" + comboBox1.SelectedValue.ToString() + "' order by producto.nombre_prod;";
            if (cosasGlobales.arrojaResultados(consulta))
                cosasGlobales.llenarListBox(listBox1, "cod_prod", "nom", consulta);
            consulta = "SELECT producto.cod_prod, concat(producto.nombre_prod, ' ', egresos_producto.cantidad_egr) as nom FROM egresos_producto, producto where producto.cod_prod = egresos_producto.producto_cod and egresos_producto.evento_cod = '" + comboBox1.SelectedValue.ToString() + "' order by producto.nombre_prod;";
            if (cosasGlobales.arrojaResultados(consulta))
                cosasGlobales.llenarListBox(listBox2, "cod_prod", "nom", consulta);
        }
    }
}
