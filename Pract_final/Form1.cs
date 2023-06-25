using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Pract_final
{
    public partial class Form1 : Form
    {
        private IConfigurationRoot _configuration;
        public Form1()
        {
            InitializeComponent();
            _configuration = new ConfigurationBuilder()
                           .AddJsonFile("Appconf.json")
                           .Build();
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
        }

        private void button8_Click(object sender, EventArgs e)
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
            comm.CommandText = "TRUNCATE TABLE logs";
            comm.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
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

            comm.CommandText = "SELECT * from logs";
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                comm.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Таблица не содержит данных!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Owner = this;
            f3.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            MessageBox.Show("Вы вышли из аккаунта!");
            dataGridView1.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
            MessageBox.Show("До свидания!");
        }

        private void button4_Click(object sender, EventArgs e)
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
            comm.CommandText = "SELECT ip, COUNT(ip) \r\nFROM logs\r\nGROUP BY ip;";
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                comm.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Таблица не содержит данных!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
            comm.CommandText = "SELECT date_time_tz, COUNT(date_time_tz) \r\nFROM logs\r\nGROUP BY date_time_tz;";
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                comm.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Таблица не содержит данных!");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "yyyy/MM/dd";
        }

        private void button6_Click(object sender, EventArgs e)
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
            comm.CommandText = $"SELECT * FROM logs\r\nWHERE date_time_tz >= '{dateTimePicker1.Text}' AND date_time_tz <= '{dateTimePicker2.Text}';";
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                comm.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Таблица не содержит данных!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filePath = "pgslog.log";
            _configuration = new ConfigurationBuilder()
                           .AddJsonFile("Appconf.json")
                           .Build();
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            string[] lines = File.ReadAllLines(filePath);
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                string ipAddress = parts[0];
                string timestamp = parts[1] + " " + parts[2];
                string request = parts[3] + " " + parts[4] + " " + parts[5];
                string statusCode = parts[6];
                NpgsqlCommand comm = new NpgsqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = $"INSERT INTO logs (ip, date_time_tz, requests, kod_status) VALUES ('{ipAddress}', '{timestamp}', '{request}', '{statusCode}');";
                comm.ExecuteNonQuery();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dataGridView1.Hide();
        }
    }
}
