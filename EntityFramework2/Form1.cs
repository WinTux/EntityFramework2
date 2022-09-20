using EntityFramework2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ddbb = new GestionEmpresaXDB();
            var telefonos = ddbb.Telefonos.ToList();
            string listaT = "";
            foreach (var t in telefonos)
                listaT += $"ID: {t.idTelefono}, Nombre completo: {t.estudiante.nombre} {t.estudiante.apellido}; num: {t.numero}\n";
            label1.Text = listaT;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int edad = int.Parse(textBox1.Text);
            using (var ddbb = new GestionEmpresaXDB()) {
                var estudiantes = ddbb.Estudiantes.Where(est => DateTime.Now.Year - est.fecha_nac.Year >= edad).ToList();
                string listaT = "";
                foreach (var es in estudiantes)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}; Fecha nac: {es.fecha_nac}\n";
                label2.Text = listaT;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int edad = int.Parse(textBox2.Text);
            using (var ddbb = new GestionEmpresaXDB())
            {
                var estudiantes = ddbb.Estudiantes
                    .Where(est => DateTime.Now.Year - est.fecha_nac.Year > edad && est.direccion != null)
                    .ToList();
                string listaT = "";
                foreach (var es in estudiantes)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}; Fecha nac: {es.fecha_nac.ToString("dd-+-*-MM/yyyy")}\n";
                label4.Text = listaT;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Recuperamos los valores del formulario
            int ci = int.Parse(textBox3.Text);
            int num = int.Parse(textBox4.Text);
            //Armamos un objeto con esos valores
            Telefono telf = new Telefono { codigoEst = ci, numero = num };
            //Agregamos el objeto al conjunto de telefonos
            using (var ddbb = new GestionEmpresaXDB())
            {
                var telefonos = ddbb.Telefonos;
                telefonos.Add(telf);
                //guardamos
                ddbb.SaveChanges();
            }
            label8.Text = $"Numero {num} creado";
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //obtener el valor PK
            int ci = int.Parse(textBox5.Text);
            //armar un objeto con ese valor
            Estudiante est = new Estudiante { ci = ci};
            //eliminar del conjunto de estudiantes
            using (var ddbb = new GestionEmpresaXDB())
            {
                ddbb.Remove(est);
                //guardamos
                ddbb.SaveChanges();
            }
            label10.Text = $"Estudiante {ci} eliminado";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int edad = int.Parse(textBox6.Text);
            using (var ddbb = new GestionEmpresaXDB())
            {
                var estudiantes = ddbb.Estudiantes
                    .Where(est => DateTime.Now.Year - est.fecha_nac.Year > edad || est.direccion != null)
                    .ToList();
                string listaT = "";
                foreach (var es in estudiantes)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}; Fecha nac: {es.fecha_nac.ToString("dd/MM/yyyy")} Dir.: {es.direccion}\n";
                label11.Text = listaT;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Uso del LIKE
            string valor = textBox7.Text;
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .Where(est => est.apellido.StartsWith(valor))
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label14.Text = listaT;
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Uso del LIKE
            string valor = textBox7.Text;
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .Where(est => est.apellido.EndsWith(valor))
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label14.Text = listaT;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Uso del LIKE
            string valor = textBox7.Text;
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .Where(est => est.apellido.Contains(valor))
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label14.Text = listaT;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Uso de ORDER BY (ASC)
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .OrderBy(estu => estu.apellido)
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label16.Text = listaT;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Uso de ORDER BY (DESC)
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .OrderByDescending(estu => estu.apellido)
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label16.Text = listaT;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Uso de ORDER BY con WHERE
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .Where(estudiante => estudiante.email != null)
                    .OrderByDescending(estu => estu.apellido)
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}\n";
                label16.Text = listaT;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int cant = int.Parse(textBox8.Text);
            //Uso de LIKE
            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .OrderByDescending(estu => estu.fecha_nac)
                    .Skip(0).Take(cant)
                    .ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.nombre} {es.apellido}; Nac.: {es.fecha_nac}\n";
                label19.Text = listaT;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int ci = int.Parse(textBox9.Text);
            //Busqueda por llave primaria
            using (var ddbb = new GestionEmpresaXDB())
            {
                var est = ddbb.Estudiantes
                    .SingleOrDefault(es => es.ci == ci);
                string listaT = "";
                if (est != null) {
                    listaT += $"Nombre completo: {est.nombre} {est.apellido}; Nac.: {est.fecha_nac} ({est.ci})\n";
                
                }
                label21.Text = listaT;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {

            using (var ddbb = new GestionEmpresaXDB())
            {
                var lista = ddbb.Estudiantes
                    .Select(es => new
                    {
                        carnet = es.ci,
                        last_name = es.apellido,
                        name = es.nombre
                    }).ToList();
                string listaT = "";
                foreach (var es in lista)
                    listaT += $"Nombre completo: {es.name} {es.last_name}; Nac.: {es.carnet}\n";
                label23.Text = listaT;
            }
            

        }
    }
}
