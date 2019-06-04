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
    public partial class formUsuarios : Form
    {
        public formUsuarios()
        {
            InitializeComponent();
            listarUsuarios();
        }

        private DataSet ds;
        private controladorUsuarios obj = new controladorUsuarios(Login.config);

        private string cuenta;

        private void formUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void listarUsuarios()
        {
            if (textBox1.Text.Trim() == "")
                ds = obj.listarUsuariosCtl();
            else
                ds = obj.listarUsuarios2Ctl(textBox1.Text);

            dataGridView1.DataSource = ds.Tables[0];
            Font negrita = new Font(this.Font, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = negrita;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listarUsuarios();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox5.Text.Trim() == "" || textBox3.Text.Trim() == "" ||textBox4.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("Complete los campos!");
                return;
            }

            if (Validaciones.soloLetras(textBox3.Text) == false)
            {
                MessageBox.Show("Solo se permiten letras en el campo nombres.");
                textBox3.Focus();
                return;
            }

            if (Validaciones.soloLetras(textBox4.Text) == false)
            {
                MessageBox.Show("Solo se permiten letras en el campo apellidos.");
                textBox4.Focus();
                return;
            }


            object[] datosA = { textBox5.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(),  textBox6.Text.Trim() };

            string error = "";
            if (obj.agregarUsuarioCtl(datosA, ref error))
            {
                MessageBox.Show("Usuario guardado con exito!");

                this.listarUsuarios();

                this.textBox3.Clear();
                this.textBox4.Clear();
                this.textBox5.Clear();
                this.textBox6.Clear();

            }
            else
            {
                if (error.Contains("Duplicate entry"))
                {
                    MessageBox.Show("Error: La cuenta ya esta asignada.");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error inesperado intente de nuevo.");
                }

                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.cuenta = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            MessageBox.Show(cuenta);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
