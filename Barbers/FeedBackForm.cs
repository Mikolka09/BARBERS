using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq; //LINQ to SQL
using System.Data.Linq.Mapping; //ORM
using System.Threading;

namespace Barbers
{
    public partial class FeedBackForm : Form
    {
        private BarberShopEx barberShopEx;
        public FeedBackForm()
        {
            InitializeComponent();
            barberShopEx = new BarberShopEx();
        }

        private void FeedBackForm_Load(object sender, EventArgs e)
        {
            foreach (var item in barberShopEx.Barbers)
            {
                comboBoxBarbers.Items.Add(item);
            }
        }

        private void comboBoxBarbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            BarbersL selectedbarber = comboBoxBarbers.SelectedItem as BarbersL;
            var feedback = from f in barberShopEx.FeedBacks
                           join j in barberShopEx.Journal on f.IdVisit equals j.Id
                           join c in barberShopEx.Clients on j.IdClient equals c.Id
                           where j.IdBarber == selectedbarber.Id
                           select new { f, j, c };
            listBoxFeedback.Items.Clear();
            foreach (var item in feedback)
            {
                listBoxFeedback.Items.Add("Клиент - " + " " + item.c.Name + ", " + "Оценка - " + " "
                                           + item.f.Rating + ", " + "Отзыв - " + " " + item.f.text);
            }
            double five = 0, four = 0, free = 0, two = 0, one = 0;
            double avg = 0;
            foreach (var item in feedback)
            {
                switch (item.f.Rating)
                {
                    case 5:
                        five++;
                        break;
                    case 4:
                        four++;
                        break;
                    case 3:
                        free++;
                        break;
                    case 2:
                        two++;
                        break;
                    case 1:
                        one++;
                        break;
                }
            }
            labelFive.Text = five.ToString();
            labelFour.Text = four.ToString();
            labelFree.Text = free.ToString();
            labelTwo.Text = two.ToString();
            labelOne.Text = one.ToString();
            avg = (((5 * five) + (4 * four) + (3 * free) + (2 * two) + (1 * one)) 
                                / (five + four + free + two + one));
            labelAVG.Text = avg.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    [Table(Name = "FeedBack")]
    public class FeedBack
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "text")]
        public string text { get; set; }
        [Column(Name = "idVisit")]
        public int IdVisit { get; set; }
        [Column(Name = "rating")]
        public int Rating { get; set; }
    }

    public class BarberShopEx : DataContext
    {
        public Table<ClientL> Clients; //ORM 2 - коллекция
        //GenderL
        public Table<GenderL> Genders;
        //BarbersL
        public Table<BarbersL> Barbers;
        public Table<Journal> Journal;
        public Table<FeedBack> FeedBacks;

        //Конструктор контекста принимает строку подключения
        public BarberShopEx() : base(System.Configuration.ConfigurationManager
                    .ConnectionStrings["DB"].ConnectionString)
        {
            Clients = GetTable<ClientL>();
            //GenderL
            Genders = GetTable<GenderL>();
            //BarbersL
            Barbers = GetTable<BarbersL>();
            Journal = GetTable<Journal>();
            FeedBacks = GetTable<FeedBack>();
        }
    }
}
