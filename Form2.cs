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
using System.IO;

namespace Project
{
    public partial class Form2 : Form
    {
        public int sum = 0;
        private MySqlConnection databaseConnection()
        {
            string connectionString = " datasource=127.0.0.1;port=3306;username=root;password=;database=pro;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        
        private void summ()
        {
            if (databasket.Rows.Count > 0)
            {
                int selected = databasket.CurrentCell.RowIndex;
                int sum = 0;
                for (int i = 0; i < databasket.Rows.Count; i++)
                {
                    if (databasket.Rows[i].Cells[3].Value != null && databasket.Rows[selected].Cells[3].Value.ToString() != "")
                    {
                        sum = sum + Convert.ToInt32(databasket.Rows[i].Cells[3].Value.ToString());

                    }

                }
                textBox1.Text = Convert.ToString(sum);
                

            }

        }
        private void showproduct(string sqlsent)
        {
            MySqlConnection conn = databaseConnection();

            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM product";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataproduct.DataSource = ds.Tables[0].DefaultView;
        }
        
       
        private void showbasket(string sqlsent)
        {

            MySqlConnection conn = databaseConnection();

            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM basket  ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            databasket.DataSource = ds.Tables[0].DefaultView;
        }

        public Form2()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            showproduct("SELECT * FROM product ");
        }

        private void addbasket_Click(object sender, EventArgs e)
        {
            int selectedRow = dataproduct.CurrentCell.RowIndex;
            string addname = Convert.ToString(dataproduct.Rows[selectedRow].Cells["name"].Value);
            string addcost = Convert.ToString(dataproduct.Rows[selectedRow].Cells["ราคา"].Value);

            MySqlConnection conn = databaseConnection();

            string sql = "INSERT INTO basket ( ชื่อสินค้า,จำนวน,ราคา,รวม) VALUES ( '" + addname + "','" + numproduct.Text + "', '" + addcost + "',('" + numproduct.Text + "'* '" + addcost + "'))";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            int rows = cmd.ExecuteNonQuery();

            conn.Close(); 
            

            if (rows > 0)
            {
                MessageBox.Show("เพิ่มลงตะกร้าสำเร็จ");
                showbasket("SELECT * FROM basket ");
                summ();
                numproduct.Clear();
            }
           
        }

        private void edit_Click(object sender, EventArgs e)
        {

            int selected = databasket.CurrentCell.RowIndex;

            string edit = Convert.ToString(databasket.Rows[selected].Cells["ชื่อสินค้า"].Value);
            string addcost = Convert.ToString(databasket.Rows[selected].Cells["ราคา"].Value);

            MySqlConnection conn = databaseConnection();

            string sql = "UPDATE basket SET จำนวน = '" + numproduct.Text + "' ,รวม =  ('" + numproduct.Text + "' * '" + addcost + "') WHERE ชื่อสินค้า = '" + edit + "'";


            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            int rows = cmd.ExecuteNonQuery();

            conn.Close();

            if (rows > 0)
            {
                MessageBox.Show("แก้ไขจำนวนสินค้าสำเร็จ");
                showbasket("SELECT * FROM basket ");
                summ();
                numproduct.Clear();
            }

        }
        

        private void Delete_Click(object sender, EventArgs e)
        {
            int selectedRow = databasket.CurrentCell.RowIndex;
            string delete = Convert.ToString(databasket.Rows[selectedRow].Cells["ชื่อสินค้า"].Value);

            MySqlConnection conn = databaseConnection();

            string sql = "DELETE FROM basket WHERE ชื่อสินค้า = '" + delete + "'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            int rows = cmd.ExecuteNonQuery();

            conn.Close();

            if (rows > 0)
            {
                MessageBox.Show("ลบรายการสินค้าสำเร็จ");
                showbasket("SELECT * FROM basket");
                summ();
                
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            if (databasket.Rows.Count >0 ) 
            {
                int selected = databasket.CurrentCell.RowIndex;
                int sum = 0;
                for (int i = 0; i < databasket.Rows.Count; i++ )
                {
                    if (databasket.Rows[i].Cells[4].Value!= null && databasket.Rows[selected].Cells[4].Value.ToString()!= "")
                    {
                        sum = sum + Convert.ToInt32(databasket.Rows[i].Cells[4].Value.ToString());

                    }
                
                }
                textBox1.Text = Convert.ToString(sum);
                

            }
           
 
            
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            
            MySqlConnection conn = databaseConnection();

            string sql = "DELETE FROM basket";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            int rows = cmd.ExecuteNonQuery();

            conn.Close();

            if (rows > 0)
            {
                MessageBox.Show("ออกจากโปรแกรมเรียบร้อย");
                showbasket("SELECT * FROM basket");
                textBox1.Text = "0";
                Application.Exit();
                MessageBox.Show("ออกจากโปรแกรมเรียบร้อย");


            }
        }




        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



        private void dataproduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataproduct.CurrentRow.Selected = true;
            int selectedRow = dataproduct.CurrentCell.RowIndex;
            string addname = Convert.ToString(dataproduct.Rows[selectedRow].Cells["name"].Value);
            



            MySqlConnection con = databaseConnection(); 
            MySqlCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT picture FROM product WHERE name = '"+addname+ "'  ";
           
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if(ds.Tables[0].Rows.Count > 0 )
            {
                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["picture"]);
                pictureBox1.Image = new Bitmap(ms);
            }
        }


    }
}
