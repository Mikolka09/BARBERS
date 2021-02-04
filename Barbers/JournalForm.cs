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

        private void showJournal()
        {
            //очистка - поиск по типу label, с не нулевым Tag
            List<Control> toRemove = new List<Control>();
            foreach (Control c in this.Controls)
            {
                if(c as Label != null)
                if((c as Label).Tag != null)
                {
                    //Controls.Remove(c) - нельзя, коллекция изменена в цикле по коллекции
                    toRemove.Add(c);
                }
            }
            foreach (Control c in toRemove)
            {
                this.Controls.Remove(c);
            }

            //Создание запросов (запрос - query)
            var jor = from J in barberShop.Journal
                      join B in barberShop.Barbers on J.IdBarber equals B.Id
                      join C in barberShop.Clients on J.IdClient equals C.Id
                      select new { J, B, C };

            int x = 10, y = 25;
            foreach (var j in jor)
            {
                //создаем label
                var lable = new Label()
                {
                    Text = j.C.Name + " - " + j.B.Name,
                    Location = new Point(x, y),
                    AutoSize = true,

                    Tag = j.J.Moment //заначка (спрятали в label)
                };

                //обработчик событий
                lable.Click += new EventHandler(labelClick);
                //смещение координат следуюшей label
                y += 20;
                //добавляем label на форму
                this.Controls.Add(lable);
            }

        }

        private void InitRecord() //Загрузка Combobox - на запись
        {
            foreach (BarbersL b in barberShop.Barbers)
            {
                comboBoxBarber.Items.Add(b);
            }
            foreach (ClientL c in barberShop.Clients)
            {
                comboBoxClient.Items.Add(c);
            }
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {
            //Создание объекта контекста
            barberShop = new BarberShop(
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["DB"].ConnectionString
                );

            showJournal();
            InitRecord();
        }

        //событие клик на label
        private void labelClick(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Label).Tag.ToString(), "MOMENT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void JournalForm_Load2(object sender, EventArgs e) //Набор упражнений - не активно
        {
            //ORM 3 - instance of context (создаем)
            barberShop = new BarberShop(
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["DB"].ConnectionString
                );

            //var query4 = from c in barberShop.Clients
            //             join g in barberShop.Genders on c.GenderId equals g.Id
            //             select new {Name = c.Name, Phone = c.Phone, Email = c.Email, Gender = g.Name};

            //var query = from client in barberShop.Clients
            //            select client;
            //var query2 = from gender in barberShop.Genders
            //             select gender;
            //var query3 = from barber in barberShop.Barbers
            //             select barber;
            //string str = "Genders - " + query2.Count() + "\n" +
            //             "Clients - " + query.Count() + "\n" +
            //             "Barbers - " + query3.Count() + "\n";
            //string str2 = str + "\n";
            //foreach (var item in query4)
            //{
            //    str2 += item.Name + " " + item.Phone +  " " + item.Email + " " + item.Gender + "\n";
            //}
            //MessageBox.Show(str, "Count", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show(str2, "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Получение результата запроса ввиде коллекции (массива/словаря)
            //var query4 = from c in barberShop.Clients
            //             where c.Id < 10
            //             select c;
            //var list = query4.ToList();
            //MessageBox.Show(list.Count.ToString());

            var query5 = from c in barberShop.Clients
                         join g in barberShop.Genders on c.GenderId equals g.Id
                         where c.Id < 10 && g.Id < 100
                         orderby c.Name descending
                         //select new { Name = c.Name, Gender = g.Name }; //Анонимные классы
                         select new Mixed() { Name = c.Name, Gender = g.Name }; //Именнованные классы
            StringBuilder sb = new StringBuilder();
            foreach (var obj in query5)
            {
                sb.Append(obj.Name);
                sb.Append(' ');
                sb.Append(obj.Gender);
                sb.Append('\n');
            }
            //MessageBox.Show(sb.ToString());

            //Текучий (Fluent) интерфейс / Method-based linq
            //-подход организации ООП при котором методы объектов возвращают 
            //указатель на "себя" - на измененный объект
            //Подход противопоставляется возврату новых объектов в которые внесены изменения:
            //str = str.Replace(a,b) <-> str.Replace(a,b)
            //Наиболее популярное применение - для объктов с множественнными настройками
            //cell = new Cell.setValue(10).setColor(blue).setBorder(..)

            var query6 = barberShop.Clients.Where(c => c.Id < 10).Where(r => r.Name.Length < 25);
            var list = query6.ToList();
            //MessageBox.Show(list.Count.ToString());

            sb.Clear();
            foreach (var item in barberShop.Clients)
            {
                sb.Append(item.Name + " ");
                try
                {
                    sb.Append(barberShop.Genders.Where(g => g.Id == item.GenderId).First().Name);
                }
                catch
                {
                    sb.Append('-');

                }

                //sb.Append(barberShop.Genders.Where(g => g.Id == item.GenderId).FirstOrDefault()?.Name ?? "-"); //без исключений

                sb.Append('\n');
            }
            //MessageBox.Show(sb.ToString());

            //Найти клиентов без указанного гендера
            var q = from c in barberShop.Clients
                    join g in barberShop.Genders on c.GenderId equals g.Id into temp
                    from t in temp.DefaultIfEmpty()
                    where t == null
                    select new Mixed() { Name = c.Name, Gender = t.Name };
            sb.Clear();
            foreach (var item in q)
            {
                sb.Append(item.Name + " ");
                sb.Append("--" + "\n");
            }
            //MessageBox.Show(sb.ToString());

            //Найти клиентов с гендером и ФИО, длиннее 25 символов
            sb.Clear();

            var res = barberShop.Clients
                .Where(c => c.Name.Length > 25)
                .Where(c => c.GenderId != null)
                .Select(c => c.Name).ToList();

            foreach (var item in res)
            {
                sb.Append(item);
                sb.Append("\n");
            }

            //MessageBox.Show(sb.ToString());

            //Написать Method-based запрос на объединение таблиц Клиент и Гендер
            sb.Clear();

            var result = barberShop.Clients.GroupJoin(barberShop.Genders,
                    c => c.GenderId,
                    g => g.Id,
                    (x, y) => new { x, y }
                    ).SelectMany
                    (
                        z => z.y.DefaultIfEmpty(),
                        (z, g) => new { Name = z.x.Name, Gender = g.Name }
                    );

            foreach (var item in result)
            {
                //sb.Append(item);
                sb.Append(item.Name + " ");
                sb.Append(item.Gender);
                sb.Append("\n");
            }
            //MessageBox.Show(sb.ToString());
        }

        private void buttonAddRecord_Click(object sender, EventArgs e)
        {
            int barberId;
            if (comboBoxBarber.SelectedIndex > -1)
            {
                barberId = (comboBoxBarber.SelectedItem as BarbersL).Id;
            }
            else
            {
                MessageBox.Show("Выберите Барбера!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int clientId;
            if (comboBoxClient.SelectedIndex > -1)
            {
                clientId = (comboBoxClient.SelectedItem as ClientL).Id;
            }
            else
            {
                MessageBox.Show("Выберите Клиента!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime moment = dateTimePicker1.Value;
            barberShop.Journal.InsertOnSubmit(
                  new Journal()
                  {
                      IdBarber = barberId,
                      IdClient = clientId,
                      Moment = moment
                  }
                );
            barberShop.SubmitChanges();
            MessageBox.Show(barberId + " - " + clientId + "  " + moment);
            showJournal();
        }
    }

    class Mixed // Класс для смешенной выборки при соединении коллекции
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    [Table(Name = "Clients")]
    public class ClientL //класс системы ORM - отображение (mapping) таблицы Clients
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
    public class BarbersL
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

    //GenderL
    [Table(Name = "Gender")]
    public class GenderL //ORM
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
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

    class BarberShop : DataContext
    {
        public Table<ClientL> Clients; //ORM 2 - коллекция
        //GenderL
        public Table<GenderL> Genders;
        //BarbersL
        public Table<BarbersL> Barbers;
        public Table<Journal> Journal;

        //Конструктор контекста принимает строку подключения
        public BarberShop(string conStr) : base(conStr)
        {
            Clients = GetTable<ClientL>();
            //GenderL
            Genders = GetTable<GenderL>();
            //BarbersL
            Barbers = GetTable<BarbersL>();
            Journal = GetTable<Journal>();
        }
    }

    [Table(Name = "Journal")]
    public class Journal
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "id_client")]
        public int IdClient { get; set; }
        [Column(Name = "id_barber")]
        public int IdBarber { get; set; }
        [Column(Name = "moment")]
        public DateTime Moment { get; set; }
    }
}

//LINQ - Language Integrated Queris (запросы, интегрированные в язык)

// 1. ORM(Mapping) -> описываем классы для отображения данных из БД
//      используем атрибуты [Table], [Column]
// 2. Контекст (DataContext) - объект взаимодействия (~ Adapter)
//      вместо коллекции List<> приминяется коллекция Table<>
// 3. Реализация (инстанциация) 

//Создание журнала
//1. [Data First] - создаем таблицу в БД, вносим несколько запесей
//2. ORM 
//2.1 Класс class Journal
//2.2 Создаем атрибуты
//2.3 Добавляем в коллукцию Контекст
//3. Реализация - создание объекта контекста, запросы и их обработка