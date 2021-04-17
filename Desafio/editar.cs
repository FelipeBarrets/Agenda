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
    public partial class editar : Form
    {
        public editar()
        {
            InitializeComponent();
        }
        public editar(string user)
        {
            InitializeComponent();
            label8.Text = user;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agenda Eventos = new Agenda(label8.Text);
            Eventos.Show();
        }

        private void CriarEvento_Click(object sender, EventArgs e)
        {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("SELECT count(*) from eventos where idEventos= ? and criadorEvento = ? ", objcon);
            objcomando.Parameters.Add("@idEventos", MySqlDbType.Int32).Value = numEd.Value;
            objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label8.Text;

            MySqlDataAdapter comparacao = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            comparacao.Fill(dtlista);
            objcon.Close();




            foreach (DataRow list in dtlista.Rows)
            {

                if (Convert.ToInt32(list.ItemArray[0]) > 0)
                {
                    condicao();

                }
                else
                {

                    MessageBox.Show("Evento foi criado por outro usuario ou não existe", " Erro ao editar evento " + numEd.Value, MessageBoxButtons.OK, MessageBoxIcon.Error);


                }

            }


        }


        public void condicao() {

            string tipoEvento = tipo.Text;
            DateTime comp = dia.Value;

            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("SELECT count(Tipo) from eventos where Data = ? and Tipo = 'Exclusivo' and criadorEvento = ? ", objcon);
            objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = comp;
            objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label8.Text;

            MySqlDataAdapter comparacao = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            comparacao.Fill(dtlista);
            objcon.Close();

            if (tipo.Text == "Compartilhado")
            {
                edit();
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
                        Agenda Eventos = new Agenda(label8.Text);
                        Eventos.Show();

                    }
                    else
                    {

                        edit();
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

        public void edit()
        {
            int id = Convert.ToInt32(numEd.Value);

            try
            {
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
                objcon.Open();
                MySqlCommand objcomando = new MySqlCommand("UPDATE eventos SET `NomeEvento` = ?, `descrição` = ?, `Data` = ?, `local` = ?, `participantes` = ?, `Tipo` = ?, `criadorEvento` = ? WHERE (`idEventos` = '"+ id+ "');", objcon);
                objcomando.Parameters.Add("@NomeEvento", MySqlDbType.VarChar, 45).Value = nome.Text;
                objcomando.Parameters.Add("@descrição", MySqlDbType.VarChar, 150).Value = desc.Text;
                objcomando.Parameters.Add("@Data", MySqlDbType.Date).Value = dia.Value;
                objcomando.Parameters.Add("@local", MySqlDbType.VarChar, 45).Value = local.Text;
                objcomando.Parameters.Add("@participantes", MySqlDbType.Int32).Value = participantes.Text;
                objcomando.Parameters.Add("@tipo", MySqlDbType.VarChar, 45).Value = tipo.SelectedItem;
                objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label8.Text;
                objcomando.Parameters.Add("@idEventos", MySqlDbType.Int32).Value = numEd.Value;

                objcomando.ExecuteNonQuery();
                objcon.Close();
            }
            catch
            {
                MessageBox.Show("Erro ao Editar produto");
            }


        }

        private void editar_Load(object sender, EventArgs e)
        {

        }
    }
}
