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

        }

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 15;
            textBox1.MaxLength = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT User_name, Password FROM Login WHERE User_name ='{loginUser}' and Password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                // MessageBox.Show("Авторизация завершена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Такого аккаунта не существует", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
    }
}

