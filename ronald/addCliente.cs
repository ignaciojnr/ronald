﻿using System;
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
    public partial class addCliente : Form
    {
        public addCliente()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") {
                cosasGlobales.mensajeError("el rut y el nombre no pueden ser vacios");
                return;
            }
            if (cosasGlobales.arrojaResultados("select * from cliente where rut = '"+textBox1.Text+"';")) {
                cosasGlobales.mensajeError("el rut ingresado ya existe");
                return;
            }
            string nombre, rut, fono, mail, direccion;
            rut = textBox1.Text;
            nombre = textBox2.Text;
            fono = textBox3.Text;
            direccion = textBox4.Text;
            mail = textBox5.Text;
            cosasGlobales.insertarGeneral("INSERT INTO cliente (`rut`, `nombre`, `telefono`, `direccion`, `email`) VALUES ('"+rut+"', '"+nombre+"', '"+fono+"', '"+direccion+"', '"+mail+"');");




        }
    }
}
