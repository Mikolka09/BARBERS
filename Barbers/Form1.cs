using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Barbers
{
    public partial class Form1 : Form
    {
        public SqlConnection connection;
        public List<Gender> Genders;

        public List<Gender> GetGendersList()
        {
            return Genders;
        }

        public Gender GetGenderById(int id)
        {
            foreach (var item in Genders)
            {
                if (item.Id == id) return item;
            }
            return null;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }

            // ORM - Gender List
            var cmd = new SqlCommand("SELECT G.id, G.name, G.description FROM Gender G", connection);
            Genders = new List<Gender>();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Genders.Add(
                        new Gender()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        }
                    );
                }
                reader.Close(); //С незакрытым reader блакируется connection
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void buttonGender_Click(object sender, EventArgs e)
        {
            Genders genders = new Genders();
            genders.ShowDialog(this);
        }

        private void buttonBarbers_Click(object sender, EventArgs e)
        {
            BarbersForm barbers = new BarbersForm();
            barbers.ShowDialog(this);
        }

        private void buttonClients_Click(object sender, EventArgs e)
        {
            ClientsForm clientsForm = new ClientsForm();
            clientsForm.ShowDialog(this);
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm();
            clientForm.ShowDialog(this);
        }

        private void buttonLINQ_Click(object sender, EventArgs e)
        {
            JournalForm journal = new JournalForm();
            journal.ShowDialog(this);
        }
    }

    public class Gender //ORM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}


//Задание. Создать таблицу Barbers (id, name, id_gender, dt_birthday, dt_work)