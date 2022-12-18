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
using System.Threading;

namespace loginForm
{
    public partial class Form1 : Form
    {
        public static string user;
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        MySqlConnection con = new MySqlConnection(@"server=127.0.0.1;uid=root;pwd=;database=login");
        DataTable dt = new DataTable();
        Thread th;
        public Form1()
        {
            InitializeComponent();
            
        }

        public void openform()
        {
            Application.Run(new Form2());
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (cmbline.Text == "")
            {
                MessageBox.Show("Please Select Line!");
            }
            else if (txtpass.Text == "")
            {
                MessageBox.Show("Please Enter Password!");
            }
            else if (cmbmodel.Text == "")
            {
                MessageBox.Show("Please Select Model!");
            }
            else
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "Select username,password from admin_login WHERE username =@username AND password =@password";
                cmd.CommandText = "Select * from admin_login WHERE username='" + txtpass.Text + "'";
                //cmd.Parameters.AddWithValue("@username", txtusername.Text);
                //cmd.Parameters.AddWithValue("@password", txtpass.Text);
                cmd.ExecuteNonQuery();
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 1)
                {
                    //insertintoactivitylog();
                    //Admin_ID = txtusername.Text;
                    user = txtpass.Text;
                    this.Close();
                    th = new Thread(openform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }
        }
    }
}
