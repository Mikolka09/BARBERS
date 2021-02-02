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
    public partial class ClientForm : Form
    {
        private List<Client> Clients; //ORM part 2 - Коллекция
        private int showClientIndex;
        public List<Gender> genders;
        public SqlConnection con;

        public Client GetClientById()
        {
            foreach (var item in Clients)
            {
                if (item.Id == showClientIndex + 1) return item;
            }
            return null;
        }

        public ClientForm()
        {
            InitializeComponent();

            Clients = new List<Client>();
        }

        private void LoadClientFromDB()
        {
            genders = (Owner as Form1).Genders;
            //ORM part 3 - заполнение коллекции
            con = (Owner as Form1).connection;
            var cmd = new SqlCommand("SELECT id, name, phone, email, id_gender FROM Clients", con);
            //var cmd = new SqlCommand("SELECT C.id, C.name, C.phone, C.email, C.id_gender, G.description FROM Clients C LEFT JOIN Gender G ON C.id_gender = G.id", con);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                Clients.Clear();
                while (reader.Read())
                {
                    Clients.Add(
                        new Client()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.GetString(3),
                            GenderId = reader.GetValue(4) as int?,
                            //GenderDescription = reader.GetValue(5) as string
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
            //Отображаем на форме данные о размере коллекции
            int n = Clients.Count;
            labelClientsCnt.Text = n.ToString();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            LoadClientFromDB();

            if (Clients.Count > 0) //если коллекция не пуста - отображаем первого клиента
            {
                showClientIndex = 0;
                ShowClient();
                buttonLast.Enabled = false;
            }

        }

        private void ShowClient()
        {
            Client c = Clients[showClientIndex];
            labelFIO.Text = c.Name;
            labelPhone.Text = c.Phone;
            labelEmail.Text = c.Email;
            //labelGender.Text = c.GenderId.ToString();
            labelID.Text = c.Id.ToString();
            //toolTip1.SetToolTip(labelGender, c.GenderDescription);
            if (c.GenderId != null)
            {
                Gender g = (Owner as Form1).GetGenderById(c.GenderId.Value);
                if (g != null)
                {
                    labelGender.Text = g.Name;
                    toolTip1.SetToolTip(labelGender, g.Description);
                }
            }
            else
            {
                labelGender.Text = "-";
                toolTip1.SetToolTip(labelGender, "");
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (showClientIndex + 1 < Clients.Count)
            {
                showClientIndex += 1;
                ShowClient();
                buttonLast.Enabled = true;
            }
            else
            {
                buttonNext.Enabled = false;
                buttonLast.Enabled = true;
                //MessageBox.Show("Достигнут конец данных");
            }
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            if (showClientIndex - 1 >= 0)
            {
                showClientIndex -= 1;
                ShowClient();
                buttonNext.Enabled = true;
            }
            else
            {
                buttonLast.Enabled = false;
                buttonNext.Enabled = true;
                //MessageBox.Show("Достигнуто начало данных");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ClientEditForm clientEditForm = new ClientEditForm();
            clientEditForm.Action = DBActions.Edit;
            clientEditForm.client = Clients[showClientIndex];
            clientEditForm.ShowDialog(this);
            if (clientEditForm.isDataChanged)
                Clients[showClientIndex] = clientEditForm.client;


        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClientEditForm clientEditForm = new ClientEditForm();
            clientEditForm.Action = DBActions.Add;
            clientEditForm.ShowDialog(this);
            if (clientEditForm.isDataChanged)
                LoadClientFromDB();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Client c = Clients[showClientIndex];
            var cmd = new SqlCommand (@"DELETE Clients WHERE id = @id", (Owner as Form1).connection);
            cmd.Parameters.AddWithValue("@id", c.Id);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("CLIENT DELETED", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadClientFromDB();
        }
    }

    public class Client //класс системы ORM - отображение (mapping) таблицы Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }
        //public string GenderDescription { get; set; }

    }

}

//ORM - Object Relation Mapping (отображение данных на объекты)
//создание ORM - описание классов с полями как у таблиц базы данных
//             - создание коллекции объектов с данными из БД


//Проблема совместимости с NULL:
//Например, при получении полей типа int
//Решение:
// 1. COALESCE  в запрос SQL
// 2. Проверка reader.GetValue -> 0 / значение       filed = (reader.GetValue(4) == DBNull.Value)? 0 : reader.GetInt32(4)
// 3. Использование специальных типов   Nullable<int> (int?)   filed = reader.GetValue(4) as int?
