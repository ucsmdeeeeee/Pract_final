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
using System.IO;
using System.Text.RegularExpressions;

namespace Pract_final
{
    public partial class Form3 : Form
    {
        private IConfigurationRoot _configuration;
        public Form3()
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
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT users_name from users";

            using (var reader = comm.ExecuteReader())
            {
                int a = 1;
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);

                    File.AppendAllText("Logconf.log", "users_name" + a + ": " + columnName + Environment.NewLine);

                    a++;
                }
            }
            comm.CommandText = "SELECT users_password from users";

            using (var reader = comm.ExecuteReader())
            {
                int a = 1;
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);

                    File.AppendAllText("Pasconf.log", "users_password" + a + ": " + columnName + Environment.NewLine);

                    a++;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filePath = "Logconf.log";
            string fileContents = File.ReadAllText(filePath);

            string[] lines = fileContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string login = ExtractName(line);
                if (login == textBox1.Text)
                {
                    textBox2.Enabled = true;
                }
                else
                {
                    textBox2.Enabled = false;
                }
            }

            string ExtractName(string line)
            {
                Match match = Regex.Match(line, @"users_name\d+: (.+)");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
                return null;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string filePath1 = "Pasconf.log";
            string fileContents1 = File.ReadAllText(filePath1);

            string[] lines1 = fileContents1.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line1 in lines1)
            {
                string pass = ExtractPass(line1);
                if (pass == textBox2.Text)
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }
            }

            string ExtractPass(string line)
            {
                Match match = Regex.Match(line, @"users_password\d+: (.+)");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            File.WriteAllText("Logconf.log", "");
            File.WriteAllText("Pasconf.log", "");
            MessageBox.Show("Вы вошли в аккаунт!");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox2.Enabled = false;
        }
    }
}
