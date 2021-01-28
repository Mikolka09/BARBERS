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
        public bool isDataChanged { get; set; }
        public DBActions Action { get; set; }

        public Client client { get; set; }
        public ClientEditForm()
        {
            InitializeComponent();
            isDataChanged = false;
        }

        private void ClientEditForm_Load(object sender, EventArgs e)
        {
           
            //client = (Owner as ClientForm).GetClientById();
            //if (client != null)
            //{
            //    textBoxFIO.Text = client.Name;
            //    textBoxEmail.Text = client.Email;
            //    textBoxPhone.Text = client.Phone;
            //}
            //else
            //{
            //    textBoxFIO.Text = "-";
            //    textBoxEmail.Text = "-";
            //    textBoxPhone.Text = "-";
            //}

            //var genders = (Owner as Form1).GetGendersList();
            List<Gender> genders = (Owner as ClientForm).genders;
            foreach (var item in genders)
            {
                comboBoxGender.Items.Add(item);
            }
            if (Action == DBActions.Edit)
            {
                textBoxFIO.Text = client.Name;
                textBoxEmail.Text = client.Email;
                textBoxPhone.Text = client.Phone;

                foreach (Gender item in comboBoxGender.Items)
                {
                    if(item.Id == client.GenderId)
                    {
                        comboBoxGender.SelectedItem = item;
                    }
                }
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddClient()
        {
            //Валидация данных - проверка корректности
            string Name = textBoxFIO.Text;
            if (Name.Equals(string.Empty))
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            string Email = textBoxEmail.Text;
            if (Email.Equals(string.Empty))
            {
                MessageBox.Show("Email не может быть пустым");
                return;
            }
            string Phone = textBoxPhone.Text;
            if (Phone.Equals(string.Empty))
            {
                MessageBox.Show("Телефон не может быть пустым");
                return;
            }
            String GenderId;
            if (comboBoxGender.SelectedIndex >= 0)
            {
                GenderId = ((Gender)comboBoxGender.Items[comboBoxGender.SelectedIndex]).Id.ToString();
            }
            else
            {
                GenderId = "null";
            }

            //Собираем данные в запрос
            string query = string.Format("INSERT INTO Clients ([name], [email], [phone], [id_gender]) VALUES (N'{0}', N'{1}', N'{2}', {3} )",
                    Name, Email, Phone, GenderId
                    );
            //MessageBox.Show(query);
            try
            {
                new SqlCommand(query, (Owner as ClientForm).con).ExecuteNonQuery();
                MessageBox.Show("INSERT CLIENT", "Message");

                isDataChanged = true;

                textBoxFIO.Text = "";
                textBoxEmail.Text = "";
                textBoxPhone.Text = "";
                comboBoxGender.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditClient()
        {
            //Client c = new Client();
            //c.Id = client.Id;
            //c.Name = textBoxFIO.Text;
            //c.Email = textBoxEmail.Text;
            //c.Phone = textBoxPhone.Text;
            //c.GenderDescription = comboBoxGender.SelectedItem == null ? null : comboBoxGender.SelectedItem.ToString();
            //c.GenderId = comboBoxGender.SelectedIndex + 1;
            //var con = (Owner as ClientForm).con;
            //var cmd = new SqlCommand(@"UPDATE Clients 
            //                           SET name = @Name, email = @Email, phone = @Phone, id_gender = @GenderId 
            //                           WHERE id = @Id", con);
            //cmd.Parameters.AddWithValue("@Name", c.Name);
            //cmd.Parameters.AddWithValue("@Email", c.Email);
            //cmd.Parameters.AddWithValue("@Phone", c.Phone);
            //cmd.Parameters.AddWithValue("@GenderId", c.GenderId);
            //cmd.Parameters.AddWithValue("@Id", c.Id);
            //try
            //{
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("SAVED CLIENT", "Message");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            switch (Action)
            {
                case DBActions.Add:
                    AddClient();
                    break;
                case DBActions.Edit:
                    EditClient();
                    break;
                default:
                    break;
            }
                     

        }
    }

    public enum DBActions
    {
        Add,
        Edit
    }
}
