using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Project
{
    public partial class Form3 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=pro;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Form3()
        {
            InitializeComponent();
        }

        

        private void bot1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            string sql = "INSERT INTO user (name,user,pass,Phonnumber) VALUES('" + name.Text + "','" + userl.Text + "','" + passl.Text + "','" + tell.Text + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("สมัครสมาชิกเรียบร้อยค่ะ");
                name.Clear();
                userl.Clear();
                passl.Clear();
                tell.Clear();
               
            }


        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
