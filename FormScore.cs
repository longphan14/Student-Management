using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Group_BMCSDL
{
    public partial class FormScore : Form
    {
        string lop;
        string username = FormLogin.user;
        SqlConnection conn = FormLogin.conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sql;

        string masv, hp, diem;
        public FormScore(string lop)
        {
            InitializeComponent();
            this.lop = lop;
        }
        

        private void FormDiem_Load(object sender, EventArgs e)
        {
            sql = "SELECT MASV FROM SINHVIEN WHERE MALOP ='" + lop + "'";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = dt.Columns[0].ColumnName;
            comboBox1.ValueMember = dt.Columns[0].ColumnName;

            sql = "SELECT MAHP FROM HOCPHAN";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = dt.Columns[0].ColumnName;
            comboBox2.ValueMember = dt.Columns[0].ColumnName;
        }

        private void FormScore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormEdit fe = new FormEdit(lop);
            fe.ShowDialog();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            masv = this.comboBox1.Text;
            hp = this.comboBox2.Text;
            diem = this.Diem.Text;

            cmd = new SqlCommand("SP_INS_BangDiem", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MASV", SqlDbType.VarChar).Value = masv;
            cmd.Parameters.Add("@MAHP", SqlDbType.VarChar).Value = hp;
            cmd.Parameters.Add("@DIEM", SqlDbType.VarChar).Value = diem;
            cmd.Parameters.Add("@PUBKEY", SqlDbType.VarChar).Value = username;

            cmd.Parameters["@MASV"].Direction = ParameterDirection.Input;
            cmd.Parameters["@MAHP"].Direction = ParameterDirection.Input;
            cmd.Parameters["@DIEM"].Direction = ParameterDirection.Input;
            cmd.Parameters["@PUBKEY"].Direction = ParameterDirection.Input;

            da = new SqlDataAdapter(cmd);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Successful!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("## ERROR: " + ex.Message);
                return;
            }

        }
    }
}
