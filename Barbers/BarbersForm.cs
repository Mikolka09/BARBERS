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
    public partial class BarbersForm : Form
    {
        SqlDataAdapter adapter;
        DataSet dataSet;
        SqlCommandBuilder builder;
        public BarbersForm()
        {
            InitializeComponent();
        }

        private void BarbersForm_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(
                    new SqlCommand(
                        "SELECT * FROM Barbers",
                        (Owner as Form1).connection
                        )
                );
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.Columns["id"].ReadOnly = true;
            builder = new SqlCommandBuilder(adapter);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataSet);
                MessageBox.Show("Barber SAVED", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }
    }
}
