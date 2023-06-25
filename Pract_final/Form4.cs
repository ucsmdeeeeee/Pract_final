using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Pract_final
{
    public partial class Form4 : Form
    {
        private IConfigurationRoot _configuration;
        public Form4()
        {
            InitializeComponent();
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
            comm.CommandText = "SELECT * from logs";
            string filePath = @"all_table.json";
            File.WriteAllText("all_table", "");
            using (FileStream fs = File.Create(filePath))
            {

            }
            using (var reader = comm.ExecuteReader())
            {
                int a = 0;
                while (reader.Read())
                {
                    var jsonObject = new
                    {
                        id_logs = reader.GetInt32(0),
                        ip = reader.GetString(1),
                        date_time_tz = reader.GetDateTime(2),
                        requests = reader.GetString(3),
                        kod_status = reader.GetInt32(4)
                    };
                    string json = JsonConvert.SerializeObject(jsonObject);
                    File.AppendAllText("all_table.json", json + Environment.NewLine);

                    a++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            string filePath = @"grup_ip.json";
            File.WriteAllText("grup_ip.json", "");
            using (FileStream fs = File.Create(filePath))
            {

            }
            using (var reader = comm.ExecuteReader())
            {
                int a = 0;
                while (reader.Read())
                {
                    var jsonObject = new
                    {
                        ip = reader.GetString(0),
                        count = reader.GetInt32(1)
                    };
                    string json = JsonConvert.SerializeObject(jsonObject);
                    File.AppendAllText("grup_ip.json", json + Environment.NewLine);

                    a++;
                }
            }
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
            comm.CommandText = "SELECT date_time_tz, COUNT(date_time_tz) \r\nFROM logs\r\nGROUP BY date_time_tz;";
            string filePath = @"grup_date.json";
            File.WriteAllText("grup_date.json", "");
            using (FileStream fs = File.Create(filePath))
            {

            }
            using (var reader = comm.ExecuteReader())
            {
                int a = 0;
                while (reader.Read())
                {
                    var jsonObject = new
                    {
                        date_time_tz = reader.GetDateTime(0),
                        count = reader.GetInt32(1)
                    };
                    string json = JsonConvert.SerializeObject(jsonObject);
                    File.AppendAllText("grup_date.json", json + Environment.NewLine);

                    a++;
                }
            }
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
            comm.CommandText = $"SELECT * FROM logs\r\nWHERE date_time_tz >= '{dateTimePicker1.Text}' AND date_time_tz <= '{dateTimePicker2.Text}';";
            string filePath = @"selection_date.json";
            File.WriteAllText("selection_date.json", "");
            using (FileStream fs = File.Create(filePath))
            {

            }
            using (var reader = comm.ExecuteReader())
            {
                int a = 0;
                while (reader.Read())
                {
                    var jsonObject = new
                    {
                        id_logs = reader.GetInt32(0),
                        ip = reader.GetString(1),
                        date_time_tz = reader.GetDateTime(2),
                        requests = reader.GetString(3),
                        kod_status = reader.GetInt32(4)
                    };
                    string json = JsonConvert.SerializeObject(jsonObject);
                    File.AppendAllText("selection_date.json", json + Environment.NewLine);

                    a++;
                }
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

        private void button5_Click(object sender, EventArgs e)
        {
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
        }
    }
}
