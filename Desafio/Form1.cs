using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Desafio
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string user, pwd;
                user = usuario.Text;
                pwd = senha.Text;
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");

                MySqlCommand objcomando = new MySqlCommand("select count(idLogin) from login where NomeLogin ='" + usuario.Text + "' and SenhaLogin = '" + senha.Text + "'", objcon);
                objcon.Open();

                MySqlDataAdapter log = new MySqlDataAdapter(objcomando);
                DataTable dataTable = new DataTable();                
                log.Fill(dataTable);

                foreach (DataRow list in dataTable.Rows)
                {
                    if (Convert.ToInt32(list.ItemArray[0]) > 0)
                    {
                        MessageBox.Show("Bem Vindo: " + usuario.Text, "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objcon.Close();
                        this.Hide();
                        Agenda Eventos = new Agenda(user);
                        Eventos.Show();

                    }
                    else
                    {
                        objcon.Close();
                        MessageBox.Show("Usuario inválido", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }




            }
            catch
            {

                MessageBox.Show("Erro de conexão", "Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int intIndex = Application.OpenForms.Count - 1; intIndex >= 0; intIndex--)
            {
                if (Application.OpenForms[intIndex] != this)
                    Application.OpenForms[intIndex].Close();
            }
            this.Close();
        }

        private void usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                button1_Click(null,null);
            }
            }

        private void senha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                button1_Click(null, null);

            }
        }
    }
}
