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
    }
}
