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
    public partial class ClientEditForm : Form
    {
        Client client;
        public ClientEditForm()
        {
            InitializeComponent();
        }

        private void ClientEditForm_Load(object sender, EventArgs e)
        {
            client = (Owner as ClientForm).GetClientById();
            textBoxFIO.Text = client.Name;
            textBoxEmail.Text = client.Email;
            textBoxPhone.Text = client.Phone;

            List<Gender> genders = (Owner as ClientForm).genders;
            foreach (var item in genders)
            {
                comboBoxGender.Items.Add(item.Name);
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Client c = new Client();
            c.Id = client.Id;
            c.Name = textBoxFIO.Text;
            c.Email = textBoxEmail.Text;
            c.Phone = textBoxPhone.Text;
            c.GenderDescription = comboBoxGender.SelectedItem == null ?  null: comboBoxGender.SelectedItem.ToString();
            c.GenderId = comboBoxGender.SelectedIndex + 1;
            var con = (Owner as ClientForm).con;
            var cmd = new SqlCommand(@"UPDATE Clients 
                                       SET name = @Name, email = @Email, phone = @Phone, id_gender = @GenderId 
                                       WHERE id = @Id", con);
            cmd.Parameters.AddWithValue("@Name", c.Name);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cmd.Parameters.AddWithValue("@Phone", c.Phone);
            cmd.Parameters.AddWithValue("@GenderId", c.GenderId);
            cmd.Parameters.AddWithValue("@Id", c.Id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("SAVED CLIENT", "Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
