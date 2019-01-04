using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ronald
{
    public partial class reportes : Form
    {
        private DataTable mitabla1;
        private DataTable mitabla2;
        private DataTable mitabla3;
        public reportes()
        {
            InitializeComponent();
        }

        private void reportes_Load(object sender, EventArgs e)
        {
            string consulta = "";
            if (!cosasGlobales.arrojaResultados("SELECT * FROM evento;")) { }
            else
            {
                // se restringe el rango de años seleccionables al rango de eventos registrados.
                consulta = "select distinct year(fecha) as nom from evento  order by fecha;";
                cosasGlobales.llenarCombobox(comboBox1, "nom", "nom", consulta);
                cosasGlobales.llenarCombobox(comboBox2, "nom", "nom", consulta);
                string minDate = cosasGlobales.getDatoUnico("SELECT min(fecha) FROM evento;");
                string maxDate = cosasGlobales.getDatoUnico("SELECT max(fecha) FROM evento;");
                //// se restringe el rango de fechas al rango de eventos registrados.
                DateTime fecha1 = DateTime.ParseExact(minDate, "dd-MM-yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime fecha2 = DateTime.ParseExact(maxDate, "dd-MM-yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                dateTimePicker1.MinDate = fecha1;
                dateTimePicker1.MaxDate = fecha2;
                dateTimePicker2.MinDate = fecha1;
                dateTimePicker2.MaxDate = fecha2;
          
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                cosasGlobales.mensajeError("las fechas no son validas");
                return;
            }
            string consulta = "", inicio = dateTimePicker1.Text, fin = dateTimePicker2.Text;
            consulta = "select tab_ingr.nom_ingr as nom, (tab_ingr.ingreso - tab_egr.egreso) as utilidad  from (select evento.nombre as nom_ingr, sum(producto.precio_venta*ingreso_producto.cant_ingr) as ingreso from evento,producto,ingreso_producto where ingreso_producto.evento_cod= evento.cod_even and  ingreso_producto.producto_cod = producto.cod_prod and evento.fecha >= '" + inicio + "' and evento.fecha <= '" + fin + "' group by evento.nombre order by evento.fecha) as tab_ingr, (select evento.nombre as nom_egr, sum(producto.precio_compra*egresos_producto.cantidad_egr) as egreso from evento,producto,egresos_producto where egresos_producto.evento_cod= evento.cod_even and  egresos_producto.producto_cod = producto.cod_prod and evento.fecha >= '" + inicio + "' and evento.fecha <= '" + fin + "' group by evento.nombre order by evento.fecha) as tab_egr where tab_ingr.nom_ingr =  tab_egr.nom_egr;";
            if (!cosasGlobales.arrojaResultados(consulta))
            {
                MessageBox.Show("no se encontraron resultados");
                return;
            }
            
            mitabla1 = cosasGlobales.getDt(consulta);
            string[] vectnom = new string[mitabla1.Rows.Count];
            int[] vectUtilidad = new int[vectnom.Length];
            for (int i =0; i < vectnom.Length;i++) {
                vectnom[i] = mitabla1.Rows[i].ItemArray[0].ToString();
                vectUtilidad[i] = Int32.Parse(mitabla1.Rows[i].ItemArray[1].ToString());
            }
            // Carga el grafico de utilidad por evento.
            chart1.Titles.Add("Utilidad por Evento");
            chart1.Series["Series1"].LegendText = "Evento";
            chart1.Series["Series1"].XValueMember = "nom";
            chart1.Series["Series1"].YValueMembers = "utilidad";
            chart1.DataSource = mitabla1;
            chart1.Visible = true;
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpia el grafico.
            chart2.Series.Clear();
            chart2.Titles.Clear();
            
            if (comboBox1.Text == "") { return; }
            string anio = comboBox1.Text;
            string consulta = "select tab_ingr.num_mes, sum(floor(((tab_ingr.ingreso - tab_egr.egreso)*0.25))) as impuesto  from (select month(evento.fecha) as num_mes , monthname(evento.fecha) as mes, evento.nombre as nom_ingr, sum(producto.precio_venta*ingreso_producto.cant_ingr) as ingreso from evento,producto,ingreso_producto where ingreso_producto.evento_cod= evento.cod_even and  ingreso_producto.producto_cod = producto.cod_prod and year(evento.fecha) = '"+anio+"'  group by evento.nombre order by evento.fecha) as tab_ingr, (select evento.nombre as nom_egr, sum(producto.precio_compra*egresos_producto.cantidad_egr) as egreso from evento,producto,egresos_producto where egresos_producto.evento_cod= evento.cod_even and  egresos_producto.producto_cod = producto.cod_prod and year( evento.fecha) = '"+anio+"' group by evento.nombre order by evento.fecha) as tab_egr where tab_ingr.nom_ingr =  tab_egr.nom_egr group by num_mes;";
            if (!cosasGlobales.arrojaResultados(consulta)) {
                MessageBox.Show("no se encontraron resultados");
                return;
            }
            //Se obtienen los datos a graficar desde la base de datos y se guardan en arreglos.
            mitabla2 = cosasGlobales.getDt(consulta);
            string[] vectnom = new string[mitabla2.Rows.Count];
            int[] vectImpuestos = new int[vectnom.Length];
            for (int i = 0; i < vectnom.Length; i++)
            {
                vectnom[i] = cosasGlobales.MonthName( Int32.Parse(mitabla2.Rows[i].ItemArray[0].ToString())).ToUpper();
                vectImpuestos[i] = Int32.Parse(mitabla2.Rows[i].ItemArray[1].ToString());
            }
            for (int i = 1; i<vectImpuestos.Length;i++) {
                vectImpuestos[i] += vectImpuestos[i - 1];
            }

            chart2.Palette = ChartColorPalette.Pastel;
            chart2.Titles.Add("Impuesto Acumulado");

            //Carga el grafico de impuestos (acumulado mensual).
            for (int i =0;i<vectnom.Length;i++) {
                Series series = chart2.Series.Add(vectnom[i]);
                series.Label = vectnom[i]+"\n"+ vectImpuestos[i].ToString();
                series.Points.Add(vectImpuestos[i]);
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Limpia el grafico.
            chart3.Series.Clear();
            chart3.Titles.Clear();

            if (comboBox2.Text == "") { return; }
            string anio = comboBox2.Text;
            string consulta = "select tab_ingr.num_mes, sum((tab_ingr.ingreso - tab_egr.egreso)) as utilidad  from (select month(evento.fecha) as num_mes , monthname(evento.fecha) as mes, evento.nombre as nom_ingr, sum(producto.precio_venta*ingreso_producto.cant_ingr) as ingreso from evento,producto,ingreso_producto where ingreso_producto.evento_cod= evento.cod_even and  ingreso_producto.producto_cod = producto.cod_prod and year(evento.fecha) = '" + anio + "'  group by evento.nombre order by evento.fecha) as tab_ingr, (select evento.nombre as nom_egr, sum(producto.precio_compra*egresos_producto.cantidad_egr) as egreso from evento,producto,egresos_producto where egresos_producto.evento_cod= evento.cod_even and  egresos_producto.producto_cod = producto.cod_prod and year( evento.fecha) = '" + anio + "' group by evento.nombre order by evento.fecha) as tab_egr where tab_ingr.nom_ingr =  tab_egr.nom_egr group by num_mes;";
            if (!cosasGlobales.arrojaResultados(consulta))
            {
                MessageBox.Show("no se encontraron resultados");
                return;
            }
            //Se obtienen los datos a graficar desde la base de datos y se guardan en arreglos.
            mitabla3 = cosasGlobales.getDt(consulta);
            string[] vectnom = new string[mitabla3.Rows.Count];
            int[] vectUtilidad = new int[vectnom.Length];
            for (int i = 0; i < vectnom.Length; i++)
            {
                vectnom[i] = cosasGlobales.MonthName(Int32.Parse(mitabla3.Rows[i].ItemArray[0].ToString())).ToUpper();
                vectUtilidad[i] = Int32.Parse(mitabla3.Rows[i].ItemArray[1].ToString());
            }
           

            
            chart3.Titles.Add("Utilidad por Mes");

            //Carga el grafico de utilidad por mes cambiando el color de la serie de acuerdo al valor del KPI seleccionado en el comboBox. 
            int kpi = Int32.Parse(numericUpDown1.Value.ToString());
            for (int i = 0; i < vectnom.Length; i++)
            {
                Series series = chart3.Series.Add(vectnom[i]);
                series.Label = vectnom[i]+ "\n" + vectUtilidad[i].ToString();
                series.Points.Add(vectUtilidad[i]);
                if (vectUtilidad[i] < kpi) {
                    series.Color = Color.Red;
                } else if (vectUtilidad[i] == kpi) {
                    series.Color = Color.Purple;
                } else {
                    series.Color = Color.LightSkyBlue;
                }

            }
            chart3.Visible = true;


        }
    }
}
