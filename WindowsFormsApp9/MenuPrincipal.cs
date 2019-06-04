using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            this.Hide();
            Login objMenu = new Login();
            objMenu.Show();
        }

        private void administracionDeUsauriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formUsuarios objLogin = new formUsuarios();
            objLogin.ShowDialog();
        }
    }
}
