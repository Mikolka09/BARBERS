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

namespace Barbers
{
    public partial class ClientsForm : Form
    {
        SqlDataAdapter adapter;
        DataSet dataSet;
        SqlCommandBuilder builder;

        public ClientsForm()
        {
            InitializeComponent();
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(
                    new SqlCommand(
                        "SELECT * FROM Clients",
                        (Owner as Form1).connection
                        )
                    );
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            builder = new SqlCommandBuilder(adapter);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataSet);
                MessageBox.Show("Saved", "Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
