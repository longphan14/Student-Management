using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab3_Group_BMCSDL
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public static SqlConnection conn;
        public static string user;
        string pass;
        String cmd;
        SqlDataAdapter da;
        DataTable dt;
        public static string nv, pk;

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=QLSV;Integrated Security=True; MultipleActiveResultSets=true";

            try
            {
                conn.Open();
                user = Username.Text;
                pass = Password.Text;
                cmd = "SELECT * FROM NHANVIENPUB WHERE MANV = \'";
                cmd += user + "\' AND MATKHAU = ";
                cmd += pass;

                da = new SqlDataAdapter(cmd, conn);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Connected");
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM NHANVIENPUB", conn);
                    SqlDataReader dr = sqlCommand.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            nv = dr.GetString(0);
                            pk = dr.GetString(3);
                        }
                    }
                    this.Hide();
                    FormShowClass fsc = new FormShowClass();
                    fsc.Show();
                }
                else
                {
                    MessageBox.Show("Username/Password Incorrect!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("## ERROR: " + ex.Message);
                return;
            }
            
            
        }

    }
}
