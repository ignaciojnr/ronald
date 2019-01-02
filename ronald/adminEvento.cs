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
                    consulta = "select cod_even, concat(cod_even , ' ',evento.nombre,' (',fecha,' ',cliente.nombre,' ',cliente.rut ,')' ) as nom from evento, cliente where evento.clienterut = cliente.rut and evento.cod_even = '" + evento + "' order by evento.nombre;";
                }
                cosasGlobales.llenarCombobox(comboBox1, "cod_even", "nom", consulta);
                string minDate = cosasGlobales.getDatoUnico("SELECT min(fecha) FROM evento;");
                string maxDate = cosasGlobales.getDatoUnico("SELECT max(fecha) FROM evento;");
               // MessageBox.Show(minDate+"\n"+maxDate);
                DateTime fecha1 = DateTime.ParseExact(minDate, "dd-MM-yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime fecha2 = DateTime.ParseExact(maxDate, "dd-MM-yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text== "" || comboBox2.Text =="" ) {
                cosasGlobales.mensajeError("Error no existe informacion suficiente para realizar un ingreso");
                return;
            }
            int resultado = 0;
            string evenCode = comboBox1.SelectedValue.ToString(), producCode = comboBox2.SelectedValue.ToString();
            string consulta = "select * from ingreso_producto where producto_cod= '"+producCode+"' and evento_cod = '"+evenCode+"' ;";
            if (cosasGlobales.arrojaResultados(consulta))
            {
                consulta = "UPDATE ingreso_producto SET cant_ingr = '" + numericUpDown1.Value.ToString() + "' WHERE  producto_cod = '" + producCode + "' and evento_cod = '" + evenCode + "';";
                resultado = cosasGlobales.ejecutarConsulta(consulta);
                if (resultado > 0)
                    MessageBox.Show("modificacion relizada");
            }
            else {
                consulta = "INSERT INTO ingreso_producto (`producto_cod`, `evento_cod`, `fecha`, `cant_ingr`) VALUES ('"+producCode+"', '"+evenCode+"', '"+dateTimePicker3.Text+"', '"+numericUpDown1.Value.ToString()+"');";
                cosasGlobales.insertarGeneral(consulta);
            }
            refrescarListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                cosasGlobales.mensajeError("Error no existe informacion suficiente para realizar un ingreso");
                return;
            }
            int resultado = 0;
            string evenCode = comboBox1.SelectedValue.ToString(), producCode = comboBox2.SelectedValue.ToString();
            string consulta = "SELECT * FROM egresos_producto where producto_cod = '"+producCode+"' and evento_cod = '"+evenCode+"';";
            if (cosasGlobales.arrojaResultados(consulta))
            {
                consulta = "UPDATE egresos_producto SET cantidad_egr = '" + numericUpDown1.Value.ToString() +"' WHERE `producto_cod`='"+producCode+"' and`evento_cod`='"+evenCode+"';";
                resultado = cosasGlobales.ejecutarConsulta(consulta);
                if (resultado > 0)
                    MessageBox.Show("modificacion relizada");
            }
            else
            {
                consulta = "INSERT INTO egresos_producto (`producto_cod`, `evento_cod`, `fecha`, `tipo`, `cantidad_egr`) VALUES ('"+producCode+"', '"+evenCode+"', '"+dateTimePicker4.Text+"', '"+comboBox3.Text+"', '"+numericUpDown1.Value.ToString()+"');";
                cosasGlobales.insertarGeneral(consulta);
            }
            refrescarListBox();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string buscar = listBox1.SelectedValue.ToString();
            cambiarProducto(buscar);
        }

        private void cambiarProducto(string buscar)
        {
            
           /*    
            for (int i = 0; i < comboBox2.Items.Count - 1;i++) {
                comboBox2.fir
                if(comboBox2.SelectedValue. ToString() == buscar)
                   break;
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value) {
                cosasGlobales.mensajeError("las fechas no son validas");
                return;
            }
            string consulta = "", inicio= dateTimePicker1.Text, fin =dateTimePicker2.Text;
            consulta = "SELECT * FROM evento where fecha >= '"+inicio+"' and fecha <= '"+fin+"' order by fecha;";
            if (cosasGlobales.arrojaResultados(consulta)) {
                consulta= "select cod_even, concat(cod_even , ' ',evento.nombre,' (',fecha,' ',cliente.nombre,' ',cliente.rut ,')' ) as nom from evento, cliente where evento.clienterut = cliente.rut and evento.fecha >= '" + inicio + "' and evento.fecha <= '" + fin + "' order by evento.fecha;";
                cosasGlobales.llenarCombobox(comboBox1, "cod_even","nom",consulta);
            } else {
                MessageBox.Show("no se encontraron resultados");
            }
        }
    }
}
