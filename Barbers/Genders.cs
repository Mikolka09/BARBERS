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
    public partial class Genders : Form
    {
        //Объекты для работы в отсоединенном режиме
        SqlDataAdapter adapter; //посредник между БД и DataSet
        DataSet dataSet;  //локальное хранилище
        public Genders()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Genders_Load(object sender, EventArgs e)
        {
            SqlConnection con = (Owner  //Form1, который запустил данную форму
                as Form1).connection;
            //Адаптер - строится на основе SQL запроса
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText ="SELECT * FROM Gender";
            adapter = new SqlDataAdapter(command);

            //DataSet - отдельно - локальная копия таблицы (запроса)
            dataSet = new DataSet();

            //Заполняем DataSet через Adapter
            adapter.Fill(dataSet);

            //Подключаем DataSet к отображении dataGridView
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
