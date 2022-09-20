using EntityFramework2.Models;
using System;
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
    }
}
