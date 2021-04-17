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
    public partial class MeusEventos : Form
    {
        public MeusEventos()
        {
            InitializeComponent();
        }

        private void MeusEventos_Load(object sender, EventArgs e)
        {

            listagrid();
        }

        public void listagrid()
        {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idEventos,NomeEvento, descrição, Data, local, participantes, tipo from eventos where ", objcon);


            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            tabEventos.DataSource = dtlista;

            objcon.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
