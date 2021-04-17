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
    public partial class Filtro : Form
    {
        public Filtro()
        {
            InitializeComponent();
        }
        public Filtro(string user)
        {
            InitializeComponent();
            label3.Text = user;
        }
        private void Filtro_Load(object sender, EventArgs e)
        {
            
        }

       
        private void btn1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value != null && txtNome.Text != "")
            {
                filtrarDataeNome();
            }
            else
            {
                filtrarData();
            }
        }

        public void filtrarData()
        {
           
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idEventos,NomeEvento, descrição, Data, local, participantes, tipo from eventos where Data = ? ", objcon);
            objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = dateTimePicker1.Value;

            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            tabFiltro.DataSource = dtlista;            
            objcon.Close();


        }
        public void filtrarDataeNome()
        {

            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idEventos,NomeEvento, descrição, Data, local, participantes, tipo from eventos where Data = ? and NomeEvento= ? ", objcon);
            objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = dateTimePicker1.Value;
            objcomando.Parameters.Add("@Nome", MySqlDbType.VarChar, 45).Value = txtNome.Text;

            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            tabFiltro.DataSource = dtlista;
            objcon.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agenda Eventos = new Agenda(label3.Text);
            Eventos.Show();
        }
    }
}
