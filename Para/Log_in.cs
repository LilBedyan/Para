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
using System.Timers;
using System.Threading;

namespace Para
{
    public partial class log_in : Form
    {

        DataBase dataBase = new DataBase();
        public log_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            button2.Click += new EventHandler(button2_firstClick);
            button1.Click += new EventHandler(button1_1Click);
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private string text = String.Empty;

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 15;
            textBox1.MaxLength = 50;
        }

        private void button1_1Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT User_name, Password FROM Login WHERE User_name ='{loginUser}' and Password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1 && textBox3.Text == this.text)
            {
                // MessageBox.Show("Авторизация завершена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует или неверно введена CAPCHA", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            button1.Click -= new EventHandler(button1_1Click);
            button1.Click += new EventHandler(button1_2Click);
        }
        private void button1_2Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT User_name, Password FROM Login WHERE User_name ='{loginUser}' and Password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1 && textBox3.Text == this.text)
            {
                // MessageBox.Show("Авторизация завершена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует или неверно введена CAPCHA", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            button1.Click -= new EventHandler(button1_2Click);
            button1.Click += new EventHandler(button1_3Click);
        }
        private void button1_3Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT User_name, Password FROM Login WHERE User_name ='{loginUser}' and Password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1 && textBox3.Text == this.text)
            {
                // MessageBox.Show("Авторизация завершена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует или неверно введена CAPCHA", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timer1.Interval = 60000; //Миллисекунды
                timer1.Tick += timer_Tick;
                timer1.Start();
                button1.Enabled = false;
            }

            button1.Click -= new EventHandler(button1_3Click);
            //button1.Click += new EventHandler(button1_1Click);
        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            button1.Enabled = true;
            timer1.Stop();
        }

        private void button2_firstClick(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            button2.Text = "Скрыть пароль";
            button2.Click -= new EventHandler(button2_firstClick);
            button2.Click += new EventHandler(button2_secondClick);
        }

        private void button2_secondClick(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            button2.Text = "Показать пароль";
            button2.Click += new EventHandler(button2_firstClick);
            button2.Click -= new EventHandler(button2_secondClick);
        }

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 100);
            int Ypos = rnd.Next(5, Height - 20);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }
    }
}

