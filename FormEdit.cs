using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Group_BMCSDL
{
    public partial class FormEdit : Form
    {
        string lop;
        string username = FormLogin.user;
        SqlConnection conn = FormLogin.conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sql;
        string masv, hoten, ngaysinh, diachi, tendn, matkhau;

       

        public FormEdit(string lop)
        {
            InitializeComponent();
            this.lop = lop;
            sql = "SELECT MASV, HOTEN, NGAYSINH, DIACHI, TENDN FROM SINHVIEN WHERE MALOP ='" + lop + "'";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            FormShowClass formShowClass= new FormShowClass();
            this.Hide();
            formShowClass.ShowDialog();
        }

        private void buttonScore_Click(object sender, EventArgs e)
        {
            FormScore formScore = new FormScore(lop);
            this.Hide();
            formScore.ShowDialog();
        }

        private void FormEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count - 1)
            {
                masv = dataGridView1.Rows[index].Cells["MASV"].Value.ToString();
                sql = "SELECT MASV, HOTEN, NGAYSINH, DIACHI, TENDN FROM SINHVIEN WHERE MALOP ='" + lop + "' AND MASV = '" + masv + "'";
                da = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                dataGridView1.DataSource = dt;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SP_DELETE_SINHVIEN", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MASV", SqlDbType.VarChar).Value = masv;
            cmd.Parameters["@MASV"].Direction = ParameterDirection.Input;

            da = new SqlDataAdapter(cmd);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successful!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("## ERROR: " + ex.Message);
                return;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SP_UPDATE_SINHVIEN", conn);
            hoten = dataGridView1.Rows[0].Cells["HOTEN"].Value.ToString();
            ngaysinh = dataGridView1.Rows[0].Cells["NGAYSINH"].Value.ToString();
            diachi = dataGridView1.Rows[0].Cells["DIACHI"].Value.ToString();
            tendn = dataGridView1.Rows[0].Cells["TENDN"].Value.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MASV", SqlDbType.VarChar).Value = masv;
            cmd.Parameters.Add("@HOTEN", SqlDbType.VarChar).Value = hoten;
            cmd.Parameters.Add("@NGAYSINH", SqlDbType.VarChar).Value = ngaysinh;
            cmd.Parameters.Add("@DIACHI", SqlDbType.VarChar).Value = diachi;
            cmd.Parameters.Add("@TENDN", SqlDbType.VarChar).Value = tendn;

            cmd.Parameters["@MASV"].Direction = ParameterDirection.Input;
            cmd.Parameters["@HOTEN"].Direction = ParameterDirection.Input;
            cmd.Parameters["@NGAYSINH"].Direction = ParameterDirection.Input;
            cmd.Parameters["@DIACHI"].Direction = ParameterDirection.Input;
            cmd.Parameters["@TENDN"].Direction = ParameterDirection.Input;

            da = new SqlDataAdapter(cmd);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("## ERROR: " + ex.Message);
                return;
            }
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SP_INS_PUBLIC_SV", conn);
            masv = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["MASV"].Value.ToString();
            hoten = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["HOTEN"].Value.ToString();
            ngaysinh = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["NGAYSINH"].Value.ToString();
            diachi = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["DIACHI"].Value.ToString();
            tendn = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["TENDN"].Value.ToString();
            matkhau = Password.Text;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MASV", SqlDbType.VarChar).Value = masv;
            cmd.Parameters.Add("@HOTEN", SqlDbType.VarChar).Value = hoten;
            cmd.Parameters.Add("@NGAYSINH", SqlDbType.VarChar).Value = ngaysinh;
            cmd.Parameters.Add("@DIACHI", SqlDbType.VarChar).Value = diachi;
            cmd.Parameters.Add("@MALOP", SqlDbType.VarChar).Value = lop;
            cmd.Parameters.Add("@TENDN", SqlDbType.VarChar).Value = tendn;
            cmd.Parameters.Add("@MK", SqlDbType.VarChar).Value = matkhau;


            cmd.Parameters["@MASV"].Direction = ParameterDirection.Input;
            cmd.Parameters["@HOTEN"].Direction = ParameterDirection.Input;
            cmd.Parameters["@NGAYSINH"].Direction = ParameterDirection.Input;
            cmd.Parameters["@DIACHI"].Direction = ParameterDirection.Input;
            cmd.Parameters["@MALOP"].Direction = ParameterDirection.Input;
            cmd.Parameters["@TENDN"].Direction = ParameterDirection.Input;
            cmd.Parameters["@MK"].Direction = ParameterDirection.Input;

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
