using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Linq; //LINQ to SQL
using System.Data.Linq.Mapping; //ORM

namespace Barbers
{
    public partial class ReviewsForm : Form
    {
        public ReviewShop reviewShop;
        public ReviewsForm()
        {
            InitializeComponent();
        }

        private void CreateRating()
        {
            comboBoxRating.Items.Add("Очень плохо");
            comboBoxRating.Items.Add("Плохо");
            comboBoxRating.Items.Add("Нормально");
            comboBoxRating.Items.Add("Хорошо");
            comboBoxRating.Items.Add("Великолепно");
        }

        private void ReviewsForm_Load(object sender, EventArgs e)
        {
            reviewShop = new ReviewShop(
                    ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            var client = from C in reviewShop.clientR
                         select C;
            var barber = from B in reviewShop.barbersR
                         select B;
            foreach (var item in client)
            {
                comboBoxClient.Items.Add(item);
            }
            foreach (var item in barber)
            {
                comboBoxBarber.Items.Add(item);
            }
            CreateRating();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int clientId;
            if (comboBoxClient.SelectedIndex > -1)
            {
                clientId = (comboBoxClient.SelectedItem as ClientR).Id;
            }
            else
            {
                MessageBox.Show("Выберите Клиента!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int barberId;
            if (comboBoxBarber.SelectedIndex > -1)
            {
                barberId = (comboBoxBarber.SelectedItem as BarbersR).Id;
            }
            else
            {
                MessageBox.Show("Выберите Барбера!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string rating = comboBoxRating.Text;
            string review = textBoxReview.Text;
            reviewShop.reviewsR.InsertOnSubmit(
                   new ReviewsR
                   {
                       ClientId = clientId,
                       BarberId = barberId,
                       Rating = rating,
                       Review = review
                   }
                );;
            reviewShop.SubmitChanges();
            MessageBox.Show("Запись ДОБАВЛЕНА!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBoxBarber.SelectedIndex = -1;
            comboBoxClient.SelectedIndex = -1;
            comboBoxRating.SelectedIndex = -1;
            textBoxReview.Text = "";
        }
    }

    [Table(Name = "Clients")]
    public class ClientR //класс системы ORM - отображение (mapping) таблицы Clients
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "phone")]
        public string Phone { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "id_gender")]
        public int? GenderId { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    [Table(Name = "Barbers")]
    public class BarbersR
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "id_gender")]
        public int? GenderId { get; set; }
        [Column(Name = "dt_birthday")]
        public DateTime BirthDay { get; set; }
        [Column(Name = "dt_work")]
        public DateTime WorkDay { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [Table(Name = "Reviews")]
    public class ReviewsR
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "id_client")]
        public int ClientId { get; set; }
        [Column(Name = "id_barber")]
        public int BarberId { get; set; }
        [Column(Name = "rating")]
        public string Rating { get; set; }
        [Column(Name = "review")]
        public string Review { get; set; }
    }

    public class ReviewShop : DataContext
    {
        public Table<ClientR> clientR;
        public Table<BarbersR> barbersR;
        public Table<ReviewsR> reviewsR;

        public ReviewShop(string conStr) : base(conStr)
        {
            clientR = GetTable<ClientR>();
            barbersR = GetTable<BarbersR>();
            reviewsR = GetTable<ReviewsR>();
        }
    }

}
