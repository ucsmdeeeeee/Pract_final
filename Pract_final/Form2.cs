using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract_final
{
    public partial class Form2 : Form
    {
        private IConfigurationRoot _configuration;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 5)
            {
                if (textBox2.Text.Length >= 8)
                {
                    if (textBox3.Text.Length >= 8)
                    {
                        if (textBox3.Text == textBox2.Text)
                        {
                            button1.Enabled = true;
                        }
                    }
                }
            }
            if (textBox3.Text != textBox2.Text)
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _configuration = new ConfigurationBuilder()
            .AddJsonFile("Appconf.json")
                .Build();
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            string login = textBox1.Text;
            string password = textBox2.Text;
            comm.CommandText = $"INSERT INTO users(users_name,users_password)\r\nVALUES\r\n    ('{login}', '{password}')";
            NpgsqlDataReader dr = comm.ExecuteReader();
            Form1 f1 = new Form1();
            f1.Show();
            f1.button1.Enabled = false;
            f1.button2.Enabled = false;
            f1.button3.Enabled = true;
            f1.button4.Enabled = true;
            f1.button5.Enabled = true;
            f1.button6.Enabled = true;
            f1.button7.Enabled = true;
            f1.button8.Enabled = true;
            f1.button9.Enabled = true;
            f1.button10.Enabled = true;
            f1.dateTimePicker1.Enabled = true;
            f1.dateTimePicker2.Enabled = true;
            f1.dataGridView1.Show();
            this.Close();
            MessageBox.Show("Вы успешно зарегистрировались. Можете использовать эти данные для входа!");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
    }
}
