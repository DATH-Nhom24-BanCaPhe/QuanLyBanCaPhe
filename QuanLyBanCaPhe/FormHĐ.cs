using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
    public partial class FormHD : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable dt = new DataTable();
        private List<QuanLyHĐ> qlhd = new List<QuanLyHĐ>();
        private List<QuanLyCTHĐ>qlct=new List<QuanLyCTHĐ>();
        void loaddataHĐ()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from hoaDon";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvHD.DataSource = table;
        }
        //void loaddataCTHĐ()
        //{
        //    command = connection.CreateCommand();
        //    command.CommandText = "select * from CTHĐ";
        //    adapter.SelectCommand = command;
        //    dt.Clear();
        //    adapter.Fill(dt);
        //    dgvCTHĐ.DataSource = dt;
        //}
        public FormHD()
        {
            InitializeComponent();
        }
        //private void HienThiDanhSachHoaDon(DataGridView dgv, List<QuanLyHĐ> dsHĐ)
        //{
        //    dgv.DataSource = dsHĐ.ToList();
        //}
        private void HienThiDanhSachCTHĐ(DataGridView dgv, List<QuanLyCTHĐ> dsCTHĐ)
        {
            dgv.DataSource = dsCTHĐ.ToList();
        }
        private void dgvCTHĐ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMaNuoc.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTenNuoc.Text = dgvCTHĐ.Rows[e.RowIndex ].Cells[2].Value.ToString();
            nbSoLuong.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult ketqua = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (ketqua == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            DanhSachCTHĐ ds=new DanhSachCTHĐ(connection);
            string maHD=txtMaHD.Text;
            List<QuanLyCTHĐ>ketqua=ds.XemCTHĐ(maHD);
            if(ketqua.Count > 0 ) 
            {   
                HienThiDanhSachCTHĐ(dgvCTHĐ,ketqua);             
            }
            else
            {
                MessageBox.Show("Không tìm thấy!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dgvHD.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtNgayLapHD.Text = dgvHD.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTongTien.Text = dgvHD.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMaNV.Text = dgvHD.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void FormDSHD_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddataHĐ();
          //  loaddataCTHĐ();
            txtMaHD.Enabled = false;
            dtNgayLapHD.Enabled = false;
            txtMaNV.Enabled = false;
            txtTongTien.Enabled = false;
            txtMaNuoc.Enabled = false;
            txtTenNuoc.Enabled = false; 
            nbSoLuong.Enabled = false;
        }

      
    }
    }
