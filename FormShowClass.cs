using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab3_Group_BMCSDL
{
    public partial class FormShowClass : Form
    {
        public FormShowClass()
        {
            InitializeComponent();
            this.buttonEdit.Hide();
        }
        string type;
        string username = FormLogin.user;        
        SqlConnection conn = FormLogin.conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sql;
        string lop;

        private void FormShowClass_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = this.comboBox1.Text;
            if(type == "SINH VIEN LOP 1" && username == "NV01"){
                this.buttonEdit.Show();
            }
            else if (type == "SINH VIEN LOP 2" && username == "NV02")
            {
                this.buttonEdit.Show();
            }
            else
            {
                this.buttonEdit.Hide();
            }
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            this.dataGridView1.Columns.Clear();
            type = this.comboBox1.Text;
            sql = "";

            cmd = conn.CreateCommand();
            if (type == "SINH VIEN LOP 1")
            {
                lop = "20CLC";
                sql = "SELECT MASV, HOTEN, NGAYSINH, DIACHI, MALOP FROM SINHVIEN WHERE MALOP = '20CLC'";
            }
            else if (type == "SINH VIEN LOP 2")
            {
                lop = "20VP";
                sql = "SELECT MASV, HOTEN, NGAYSINH, DIACHI, MALOP FROM SINHVIEN WHERE MALOP = '20VP'";
            }
            else if (type == "LOP")
            {
                sql = "SELECT * FROM LOP";
            }
            else if (type == "HOC PHAN")
            {
                sql = "SELECT * FROM HOCPHAN";                
            }
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormEdit fe = new FormEdit(lop);   
            fe.ShowDialog();
        }

        
    }
}
