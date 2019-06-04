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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] datosA = { textBox5.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(),  textBox6.Text.Trim() };

            if (obj.agregarUsuarioCtl(datosA))
            {
                MessageBox.Show("Los datos han sido guardados.");
                this.listarUsuarios();

                this.textBox3.Clear();
                this.textBox4.Clear();
                this.textBox5.Clear();
                this.textBox6.Clear();

            }


        }
    }
}
