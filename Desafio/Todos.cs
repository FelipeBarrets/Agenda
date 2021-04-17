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
    public partial class Todos : Form
    {
        public Todos()
        {
            InitializeComponent();
        }
        public Todos(string user)
        {
            InitializeComponent();
            label2.Text = user;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agenda Eventos = new Agenda(label2.Text);
            Eventos.Show();
        }

        

        private void Todos_Load(object sender, EventArgs e)
        {
            listagrid();
        }

        public void listagrid()
        {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idEventos,NomeEvento, descrição, Data, local, participantes, tipo from eventos", objcon);


            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            tabEventos.DataSource = dtlista;

            objcon.Close();


        }

    }
}
