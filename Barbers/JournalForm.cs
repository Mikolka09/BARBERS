using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;  //LINQ to Generic (List/Arrays)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Linq; //LINQ to SQL
using System.Data.Linq.Mapping; //ORM

namespace Barbers
{
    public partial class JournalForm : Form
    {
        private BarberShop barberShop;

        public JournalForm()
        {
            InitializeComponent();
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {
            //ORM 3 - instance of context (создаем)
            barberShop = new BarberShop(
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["DB"].ConnectionString
                );

            var query4 = from c in barberShop.Clients
                         join g in barberShop.Genders on c.GenderId equals g.Id
                         select new {Name = c.Name, Phone = c.Phone, Email = c.Email, Gender = g.Name};

            var query = from client in barberShop.Clients
                        select client;
            var query2 = from gender in barberShop.Genders
                         select gender;
            var query3 = from barber in barberShop.Barbers
                         select barber;
            string str = "Genders - " + query2.Count() + "\n" +
                         "Clients - " + query.Count() + "\n" +
                         "Barbers - " + query3.Count() + "\n";
            string str2 = str + "\n";
            foreach (var item in query4)
            {
                str2 += item.Name + " " + item.Phone +  " " + item.Email + " " + item.Gender + "\n";
            }
            //MessageBox.Show(str, "Count", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(str2, "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    [Table(Name ="Clients")]
    public class ClientL //класс системы ORM - отображение (mapping) таблицы Clients
    {
        [Column(IsPrimaryKey =true, IsDbGenerated =true)]
        public int Id { get; set; }
        [Column(Name ="name")]
        public string Name { get; set; }
        [Column(Name = "phone")]
        public string Phone { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "id_gender")]
        public int? GenderId { get; set; }
   
    }

    [Table(Name = "Barbers")]
    public class BarbersL
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "id_gender")]
        public int? GenderId { get; set; }
        [Column(Name = "dt_birthday")]
        public string Birthday { get; set; }
        [Column(Name = "dt_work")]
        public string Work { get; set; }
        
    }

    //GenderL
    [Table(Name ="Gender")]
    public class GenderL //ORM
    {
        [Column(IsPrimaryKey =true, IsDbGenerated =true)]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class BarberShop: DataContext
    {
        public Table<ClientL> Clients; //ORM 2 - коллекция
        //GenderL
        public Table<GenderL> Genders;
        //BarbersL
        public Table<BarbersL> Barbers;

        //Конструктор контекста принимает строку подключения
        public BarberShop(string conStr): base (conStr)
        {
            Clients = GetTable<ClientL>();
            //GenderL
            Genders = GetTable<GenderL>();
            //BarbersL
            Barbers = GetTable<BarbersL>();
        }
    }

}

//LINQ - Language Integrated Queris (запросы, интегрированные в язык)

// 1. ORM(Mapping) -> описываем классы для отображения данных из БД
//      используем атрибуты [Table], [Column]
// 2. Контекст (DataContext) - объект взаимодействия (~ Adapter)
//      вместо коллекции List<> приминяется коллекция Table<>
// 3. Реализация (инстанциация) 