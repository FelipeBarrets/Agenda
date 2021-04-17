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
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }
        public Cadastro(string user)
        {
            InitializeComponent();
            label7.Text = user;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void CriarEvento_Click(object sender, EventArgs e)
        {
            string tipoEvento = tipo.Text;
            DateTime comp = dia.Value;

            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("SELECT count(Tipo) from eventos where Data = ? and Tipo = 'Exclusivo' and criadorEvento = ? ", objcon);
            objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = comp;
            objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label7.Text;

            MySqlDataAdapter comparacao = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            comparacao.Fill(dtlista);            
            objcon.Close();

            if (tipo.Text == "Compartilhado")
            {
                inserir();
                nome.Text = "";
                desc.Text = "";
                dia.Text = "";
                participantes.Value = 0;
                local.Text = "";
                tipo.Text = "";
            }
            else
            {


                foreach (DataRow list in dtlista.Rows)
                {

                    if (Convert.ToInt32(list.ItemArray[0]) > 0)
                    {
                        MessageBox.Show("Erro ao criar nesssa data " + comp, " já contem um evento exclusivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        objcon.Close();
                        this.Hide();
                        Agenda Eventos = new Agenda(label7.Text);
                        Eventos.Show();

                    }
                    else
                    {

                        inserir();
                        nome.Text = "";
                        desc.Text = "";
                        dia.Text = "";
                        participantes.Value = 0;
                        local.Text = "";
                        tipo.Text = "";

                    }



                }

            }

        }
        public void inserir()
        {

            try
            {
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
                objcon.Open();
                MySqlCommand objcomando = new MySqlCommand("insert into eventos (NomeEvento, descrição, Data, local, participantes, tipo, criadorEvento) values (?,?,?,?,?,?,?)", objcon);
                objcomando.Parameters.Add("@NomeEvento", MySqlDbType.VarChar, 45).Value = nome.Text;
                objcomando.Parameters.Add("@descrição", MySqlDbType.VarChar, 150).Value = desc.Text;
                objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = dia.Value;
                objcomando.Parameters.Add("@local", MySqlDbType.VarChar, 45).Value = local.Text;
                objcomando.Parameters.Add("@participantes", MySqlDbType.Int32).Value = participantes.Text;
                objcomando.Parameters.Add("@tipo", MySqlDbType.VarChar, 45).Value = tipo.SelectedItem;
                objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label7.Text;

                objcomando.ExecuteNonQuery();
                objcon.Close();
            }
            catch
            {
                MessageBox.Show("Erro ao inserir produto");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agenda Eventos = new Agenda(label7.Text);
            Eventos.Show();
        }
    }
}
