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
                MessageBox.Show(ex.Message);
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
    }
}


//Задание. Создать таблицу Barbers (id, name, id_gender, dt_birthday, dt_work)