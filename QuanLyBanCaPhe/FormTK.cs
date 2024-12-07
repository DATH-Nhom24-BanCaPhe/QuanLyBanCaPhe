using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
    public partial class FormTK : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        private List<QuanLyThongKe> qltk = new List<QuanLyThongKe>();
        private List<QuanLyHĐ> qlhd = new List<QuanLyHĐ>();
        void LoadCbMaNV()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT maNV FROM nhanVien";
                SqlDataAdapter comboBoxAdapter = new SqlDataAdapter(command);
                DataTable comboBoxTable = new DataTable();
                comboBoxAdapter.Fill(comboBoxTable);

                cbMaNV.DataSource = comboBoxTable;
                cbMaNV.DisplayMember = "maNV";
                cbMaNV.ValueMember = "maNV";
            }
        }
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from thongKe";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvThongKe.DataSource = table;
        }
        public FormTK()
        {
            InitializeComponent();
        }
        private int DemSLHĐ(DateTime ngayTK)
        {
            int soLuongHĐ = 0;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();
                string query = @"SELECT  COUNT(hd.maHD) AS soLuongHoaDon FROM thongKe tk LEFT JOIN hoaDon hd ON tk.ngayTK = hd.ngayLapHD GROUP BY tk.maThongKe, tk.ngayTK;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@ngayTK", ngayTK.Date));
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        soLuongHĐ = Convert.ToInt32(result);
                    }
                }
                string update = @"UPDATE thongKe SET soLuongHĐ = (SELECT COUNT(hd.maHD) FROM hoaDon hd WHERE thongKe.ngayTK = hd.ngayLapHD);";
                using (SqlCommand updateCommand = new SqlCommand(update, connection))
                {
                    updateCommand.Parameters.AddWithValue("@soLuongHĐ", soLuongHĐ);
                    updateCommand.Parameters.AddWithValue("@maThongKe", txtmaThongKe.Text);
                    updateCommand.ExecuteNonQuery();
                }
            }
            return soLuongHĐ;
        }
        private void TinhTongTienTK()
        {
            double tongTienTK = 0; ;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();

                string query = @"SELECT  SUM(hd.tongTien) FROM thongKe tk
                                 LEFT JOIN hoaDon hd ON tk.ngayTK = hd.ngayLapHD
                                  GROUP BY tk.maThongKe, tk.ngayTK;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@ngayTK", dtNgayTK.Value));
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        tongTienTK = Convert.ToDouble(result);
                    }
                }
                string update = @"UPDATE thongKe
                                  SET tongTienTK =( SELECT SUM(hd.tongTien)
                                  FROM hoaDon hd
                               WHERE thongKe.ngayTK = hd.ngayLapHD);";
                using (SqlCommand updateCommand = new SqlCommand(update, connection))
                {
                    updateCommand.Parameters.AddWithValue("@tongTienTK", tongTienTK);
                    updateCommand.Parameters.AddWithValue("@maThongKe", txtmaThongKe.Text);
                    updateCommand.ExecuteNonQuery();
                }
            }
            foreach (DataGridViewRow row in dgvThongKe.Rows)
            {
                if (row.Cells["MaTK"].Value != null &&
                    row.Cells["MaTK"].Value.ToString() == txtmaThongKe.Text)
                {
                    row.Cells["TongTienTK"].Value = tongTienTK;
                    break;
                }
            }
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            DanhSachThongKe ds = new DanhSachThongKe(connection);
            string maThongKe = txtmaThongKe.Text;
            DateTime ngayTK = dtNgayTK.Value;
            int soLuongHĐ =DemSLHĐ(dtNgayTK.Value);
            string maNV = cbMaNV.Text;
            double tongTienTk = 0;
            if (ds.kiemTraMa(maThongKe))
            {
                MessageBox.Show("Trùng mã!.Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string them = "insert into thongKe(maThongKe,ngayTK,soLuongHĐ,maNV) values(@maThongKe,@ngayTK,@soLuongHĐ,@maNV)";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("@maThongKe", maThongKe);
            command.Parameters.AddWithValue("@ngayTK", ngayTK);
            command.Parameters.AddWithValue("@soLuongHĐ", soLuongHĐ);
            command.Parameters.AddWithValue("@maNV", maNV);
            command.Parameters.AddWithValue("@tongTienTK", tongTienTk);
            TinhTongTienTK();
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadCbMaNV();
            loaddata();
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maThongKe = txtmaThongKe.Text;
            string xoa = "delete from thongKe where maThongKe=@maThongKe";
            SqlCommand command = new SqlCommand(xoa, connection);
            command.Parameters.AddWithValue("@maThongKe", maThongKe);
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadCbMaNV();
            loaddata();
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string maThongKe = txtmaThongKe.Text;
            DateTime ngayTK = dtNgayTK.Value;
            int soLuongHĐ = DemSLHĐ(dtNgayTK.Value);
            string maNV = cbMaNV.Text;
            double tongTienTk = 0;
            string sua = "update thongKe set maThongKe=@maThongKe,ngayTK=@ngayTK,soLuongHĐ=@soLuongHĐ,maNV=@maNV,tongTienTK=@tongTienTK";
            SqlCommand command = new SqlCommand(sua, connection);
            command.Parameters.AddWithValue("@maThongKe", maThongKe);
            command.Parameters.AddWithValue("@ngayTK", ngayTK);
            command.Parameters.AddWithValue("@soLuongHĐ", soLuongHĐ);
            command.Parameters.AddWithValue("@maNV", maNV);
            command.Parameters.AddWithValue("@tongTienTK", tongTienTk);
            TinhTongTienTK();
            command.ExecuteNonQuery();
            
            MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadCbMaNV();
            loaddata();
        }
        private void HienThiDanhSachThongKe(DataGridView dgv, List<QuanLyThongKe> ds)
        {
            dgv.DataSource = ds.ToList();
        }
        private void dgvThongKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaThongKe.Text = dgvThongKe.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtNgayTK.Text = dgvThongKe.Rows[e.RowIndex].Cells[1].Value?.ToString();
            cbMaNV.Text = dgvThongKe.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void FormTK_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadCbMaNV();
           loaddata() ;
        }
    }
}
