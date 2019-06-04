using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp9.Controlador;

namespace WindowsFormsApp9
{
    public partial class Login : Form
    {
        public static ControlConfig config = new ControlConfig("MySQL", "Server=Localhost; Database=consultorio; Uid=root; Pwd=");
        private controladorUsuarios obj = new controladorUsuarios(config);
        private int intentos = 0;

        public bool entrar;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                Console.Beep();
                textBox1.Focus();
                return;
            }
            else intentos++;
            entrar =  obj.validarUsuario(textBox1.Text.Trim(), textBox2.Text.Trim());
            if (entrar)
            {
                this.Hide();
                MenuPrincipal objMenu = new MenuPrincipal();
                objMenu.Show();
            }

            if (intentos == 3)
                this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

