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
using static System.ComponentModel.Design.ObjectSelectorEditor;

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

            /*
             Insertando varios registros
             */

            //Primera opción
            using (var ddbb = new GestionEmpresaXDB())
            {
                List<Estudiante> estudiantes = new List<Estudiante> { 
                    new Estudiante{ ci= 789, nombre = "Pedro", apellido="Perales"},
                    new Estudiante{ ci= 780, nombre = "Rocio", apellido="Mora"},
                    new Estudiante{ ci= 679, nombre = "Rebeca", apellido="Peralta"}
                };
                ddbb.AddRange(estudiantes);
                //guardamos
                ddbb.SaveChanges();
            }

            //Segunda opción
            using (var ddbb = new GestionEmpresaXDB())
            {

                Estudiante es1 = new Estudiante { ci = 789, nombre = "Pedro", apellido = "Perales" };
                Estudiante es2 = new Estudiante { ci = 780, nombre = "Rocio", apellido = "Mora" };
                Estudiante es3 = new Estudiante { ci = 679, nombre = "Rebeca", apellido = "Peralta" };
                Telefono telf1 = new Telefono { codigoEst = 123, numero = 620123 };
                
                ddbb.AddRange(es1, es2, telf1 , es3);
                //guardamos
                ddbb.SaveChanges();
            }

            //Inserción multiple (varios registros de varias tablas relacionadas)
            using (var ddbb = new GestionEmpresaXDB())
            {
                Estudiante est = new Estudiante {
                    ci = 789, 
                    nombre = "Pedro", 
                    apellido = "Perales",
                    email = "pedro@yahoo.com",
                    telefonos = new List<Telefono>{
                        new Telefono { numero = 620123 },
                        new Telefono { numero = 620123 }
                    }
                };
                
                ddbb.Add<Estudiante>(est);//Se puede omitir la especificación del tipo <Estudiante>
                //guardamos
                ddbb.SaveChanges();
            }

            //Ejemplo INCORRECTO
            using (var ddbb = new GestionEmpresaXDB())
            {
                Estudiante est = new Estudiante
                {
                    ci = 789,
                    nombre = "Pedro",
                    apellido = "Perales",
                    email = "pedro@yahoo.com",
                    
                };
                Telefono tlf1 = new Telefono {estudiante = est, numero = 620123 };
                Telefono tlf2 = new Telefono {estudiante = est, numero = 620123 };
                    
                ddbb.Add<Estudiante>(est);//Se puede omitir la especificación del tipo <Estudiante>
                //guardamos
                ddbb.SaveChanges();
            }


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

                /* 
                //Otra forma de realizarlo
                var estudiante = ddbb.Estudiantes.SingleOrDefault(es => es.ci == 777);
                ddbb.Estudiantes.Remove(estudiante);
                ddbb.SaveChanges();
                */
            }
            label10.Text = $"Estudiante {ci} eliminado";


        }

        //Método no utilizado pero funcional.
        private void modificar_estudiante(object sender, EventArgs e) {
            using (var ddbb = new GestionEmpresaXDB())
            {
                
                //Otra forma de realizarlo
                var estudiante = ddbb.Estudiantes.SingleOrDefault(es => es.ci == 777);
                estudiante.nombre = "Ana";
                estudiante.apellido = "Sosa";
                ddbb.SaveChanges();
                
            }
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
                /*
                //Ordenamiento con multiples campos
                //En SQL es: SELECt * FROM estudiante ORDER BY apellido, nombre, fecha_nac;
                lista = ddbb.Estudiantes
                    .OrderBy(estu => estu.apellido)
                    .ThenBy(estu => estu.nombre)
                    .ThenBy(estu => estu.fecha_nac)
                    .ToList();
                */
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
            //Uso de LIMIT
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

        private void button16_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                var suma = ddbb.MateriasCursadas.Sum(mc => mc.calificacion);
                //suma = ddbb.MateriasCursadas.Where(mm => mm.idEst == 123).Sum(mc => mc.calificacion);
                label24.Text = "Sumatoria: " + suma;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                //var cant = ddbb.MateriasCursadas.Count();
                var cant = ddbb.MateriasCursadas.Count(mc => mc.calificacion > 50);
                label24.Text = "Cantidad: " + cant;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                var min = ddbb.MateriasCursadas.Min(mc => mc.calificacion);
                label24.Text = "Nota mínima: " + min;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                var max = ddbb.MateriasCursadas.Max(mc => mc.calificacion);
                label24.Text = "Nota máxima: " + max;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                var avg = ddbb.MateriasCursadas.Average(mc => mc.calificacion);
                label24.Text = "Nota promedio: " + avg;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            using (var ddbb = new GestionEmpresaXDB())
            {
                var estudiantes = ddbb.Estudiantes
                    .Where(es => es.fecha_nac.Year > 1999 && es.fecha_nac.Month == 12 && es.fecha_nac.Day > 2)
                    .ToList();
                string listaT = "";
                foreach (var es in estudiantes)
                    listaT += $"Nac.: {es.fecha_nac.ToString("dd-MM-yyyy")}\n";
                label24.Text = "Fechas que cumplen:\n " + listaT;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //Uso de JOIN
            using (var ddbb = new GestionEmpresaXDB())
            {
                var listaNueva = ddbb.Estudiantes.Join(
                        ddbb.Telefonos,
                        est => est.ci,//PK
                        tel => tel.codigoEst,//FK
                        (est, tel) => new { 
                            carnet = est.ci,
                            nom = est.nombre,
                            ap = est.apellido,
                            num = tel.numero
                        }
                ).ToList();
                var res = listaNueva.Where(x => x.num > 100).ToList();
                string listaT = "";
                foreach (var es in listaNueva)
                    listaT += $"Nom: {es.nom} {es.ap} ({es.carnet}) - Telf.: {es.num}\n";
                label24.Text = "Fechas que cumplen:\n " + listaT;

                /*
                //Con condiciones
                listaNueva = ddbb.Estudiantes.Join(
                        ddbb.Telefonos,
                        est => est.ci,//PK
                        tel => tel.codigoEst,//FK
                        (est, tel) => new {
                            carnet = est.ci,
                            nom = est.nombre,
                            ap = est.apellido,
                            num = tel.numero
                        }
                ).Where(est => est.carnet > 200).ToList();
                */
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //Vamos a representar la consulta SQL: 
            //SELECT idEst, COUNT(*), MIN(calificacion), MAX(calificacion), AVG(calificacion) FROM desarrollo.materia_cursada GROUP BY idEst;
            using (var ddbb = new GestionEmpresaXDB())
            {
                var grupos = ddbb.MateriasCursadas
                    .GroupBy(mc => mc.idEst)
                    .Select(gr => new
                    {
                        Est = gr.Key,
                        Cantidad = gr.Count(),
                        Menor = gr.Min(mmcc => mmcc.calificacion),
                        Mayor = gr.Max(mmcc => mmcc.calificacion),
                        Promedio = gr.Average(mmcc => mmcc.calificacion)
                    }).ToList();
                string listaT = "";
                foreach (var grupo in grupos)
                    listaT += $"Nom.: {grupo.Est}, Cant {grupo.Cantidad}" +
                        $", Min:{grupo.Menor}, Max:{grupo.Mayor}->{grupo.Promedio}\n";
                label24.Text = listaT;

                /*
                //SELECT 
                //    idEst,COUNT(*),  MIN(calificacion), MAX(calificacion), AVG(calificacion)
                //FROM
                //    desarrollo.materia_cursada
                //GROUP BY idEst HAVING AVG(calificacion) > 85;
                grupos = ddbb.MateriasCursadas
                    .GroupBy(mc => mc.idEst)
                    .Select(gr => new
                    {
                        Est = gr.Key,
                        Cantidad = gr.Count(),
                        Menor = gr.Min(mmcc => mmcc.calificacion),
                        Mayor = gr.Max(mmcc => mmcc.calificacion),
                        Promedio = gr.Average(mmcc => mmcc.calificacion)
                    }).Where(gr => gr.Promedio > 85).ToList();
                */
            }
        }
    }
}
