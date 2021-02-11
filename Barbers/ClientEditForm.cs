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
using System.Text.RegularExpressions;

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

            //var genders = (Owner as Form1).GetGendersList();
            List<Gender> genders = (Owner as ClientForm).genders;
            //List<Gender> genders = (Owner.Owner as Form1).Genders;
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
                    if (item.Id == client.GenderId)
                    {
                        comboBoxGender.SelectedItem = item;
                    }
                }
            }
            comboBoxGender.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxGender.DrawItem += comboBoxGender_DrawItem;
            comboBoxGender.DropDownClosed += comboBoxGender_DropDownClosed;
        }

        //Закрытие ToolTip при сворачивании comboBoxGender
        private void comboBoxGender_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBoxGender);
        }

        //Перересовка ToolTip при наведении курсора на Gender в comboBoxGender
        private void comboBoxGender_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            string hint = (comboBoxGender.Items[e.Index] as Gender).Description;
            String text = (comboBoxGender.Items[e.Index] as Gender).Name;

            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(hint, comboBoxGender, e.Bounds.Right, e.Bounds.Bottom);
            }
            e.DrawFocusRectangle();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddClient()
        {

            //Валидация данных - проверка корректности
            string regexName = @"([А-ЩЁЭ-Я][а-яё]+[\-\s]?){3,}";
            string Name = textBoxFIO.Text;
            if (Name.Equals(string.Empty))
            {
                MessageBox.Show("Имя не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Name, regexName))
            {
                MessageBox.Show("Имя введено не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string regexPhone = @"^[0][1-9]{1}[0-9]{1}[0-9]{7}";
            string Phone = textBoxPhone.Text;
            if (Phone.Equals(string.Empty))
            {
                MessageBox.Show("Номер телефона не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Phone, regexPhone))
            {
                MessageBox.Show("Номер телефона введен не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string regexEmail = @"[a-zA-Z0-9\-\._]+@[a-z0-9]+(.[a-z0-9]+){1,}";
            string Email = textBoxEmail.Text;
            if (Email.Equals(string.Empty))
            {
                MessageBox.Show("Email не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Email, regexEmail))
            {
                MessageBox.Show("Email введено не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("INSERT CLIENT", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                isDataChanged = true;

                textBoxFIO.Text = "";
                textBoxEmail.Text = "";
                textBoxPhone.Text = "";
                comboBoxGender.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditClient()
        {
            //Валидация данных - проверка корректности
            string regexName = @"([А-ЩЁЭ-Я][а-яё]+[\-\s]?){3,}";
            string Name = textBoxFIO.Text;
            if (Name.Equals(string.Empty))
            {
                MessageBox.Show("Имя не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Name, regexName))
            {
                MessageBox.Show("Имя введено не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string regexPhone = @"^[0][1-9]{1}[0-9]{1}[0-9]{7}";
            string Phone = textBoxPhone.Text;
            if (Phone.Equals(string.Empty))
            {
                MessageBox.Show("Номер телефона не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Phone, regexPhone))
            {
                MessageBox.Show("Номер телефона введен не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string regexEmail = @"[a-zA-Z0-9\-\._]+@[a-z0-9]+(.[a-z0-9]+){1,}";
            string Email = textBoxEmail.Text;
            if (Email.Equals(string.Empty))
            {
                MessageBox.Show("Email не может быть пустым", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(Email, regexEmail))
            {
                MessageBox.Show("Email введено не правильно", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var con = (Owner as ClientForm).con;
            var cmd = new SqlCommand(@"UPDATE Clients 
                                       SET name = @Name, email = @Email, phone = @Phone, id_gender = @GenderId
                                       WHERE id = @Id", con);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@GenderId", GenderId);
            cmd.Parameters.AddWithValue("@Id", client.Id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("EDIT CLIENT", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                isDataChanged = true;

                client.Name = Name;
                client.Email = Email;
                client.Phone = Phone;
                client.GenderId = (GenderId == "null") ? 0 : Convert.ToInt32(GenderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
