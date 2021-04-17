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
    public partial class Agenda : Form
    {

        public Agenda()
        {
            InitializeComponent();            
        }
        public Agenda(string user)
        {
            InitializeComponent();
            label2.Text = user;           
            
        }

        private void Agenda_Load(object sender, EventArgs e)
        {
            
            listagrid();           
            
        }

        public void listagrid()
        {
            MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
            objcon.Open();
            MySqlCommand objcomando = new MySqlCommand("Select idEventos,NomeEvento, descrição, Data, local, participantes, tipo from eventos where criadorEvento='"+ label2.Text+ "'", objcon);


            MySqlDataAdapter lista = new MySqlDataAdapter(objcomando);
            DataTable dtlista = new DataTable();
            lista.Fill(dtlista);
            tabEventos.DataSource = dtlista;

            objcon.Close();                       


        }

        public void deletarEve() {
            try
            {
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
                objcon.Open();
                MySqlCommand objcomando = new MySqlCommand("delete from eventos where idEventos= ?", objcon);
                objcomando.Parameters.Add("@idEventos", MySqlDbType.Int32).Value = del.Text;
                objcomando.ExecuteNonQuery();

                MessageBox.Show("Produto deletado");
                del.Value = 0;
                listagrid();

            }
            catch
            {
                MessageBox.Show("Id não existe");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            LOGIN home = new LOGIN();
            home.Show();
        }       

      
        
        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cadastro criar = new Cadastro(label2.Text);
            criar.Show();
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja mesmo deletar o evento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                try
                {
                    MySqlConnection objcon = new MySqlConnection("server=localhost;port=3306;username=root;database=agenda");
                    objcon.Open();
                    MySqlCommand objcomando = new MySqlCommand("SELECT count(*) from eventos where idEventos= ? and criadorEvento = ? ", objcon);
                    objcomando.Parameters.Add("@idEventos", MySqlDbType.Int32).Value = del.Value;
                    objcomando.Parameters.Add("@criadorEvento", MySqlDbType.VarChar, 45).Value = label2.Text;

                    MySqlDataAdapter comparacao = new MySqlDataAdapter(objcomando);
                    DataTable dtlista = new DataTable();
                    comparacao.Fill(dtlista);
                    objcon.Close();




                    foreach (DataRow list in dtlista.Rows)
                    {

                        if (Convert.ToInt32(list.ItemArray[0]) > 0)
                        {
                            deletarEve();

                        }
                        else
                        {

                            MessageBox.Show("Evento foi criado por outro usuario ou não existe", " Erro ao deletar evento " + del.Value, MessageBoxButtons.OK, MessageBoxIcon.Error);


                        }
                    }
                }
                catch
                {


                }
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            editar edicao = new editar(label2.Text);
            edicao.Show();
        }

        private void filtrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Filtro busca  = new Filtro(label2.Text);
            busca.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void meusEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Todos TODOS = new Todos(label2.Text);
            TODOS.Show();
        }
    }
}
