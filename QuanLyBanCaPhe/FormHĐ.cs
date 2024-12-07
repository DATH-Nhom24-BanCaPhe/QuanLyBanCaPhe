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
    public partial class FormHĐ : Form
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
            dgvHoaDon.DataSource = table;

        }

        void loaddataCTHĐ()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from CTHĐ";
            adapter.SelectCommand = command;
            dt.Clear();
            adapter.Fill(dt);
            dgvCTHĐ.DataSource = dt;

        }
        void loadCBMANUOC()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from doUong ";
            SqlDataAdapter comboBoxAdapter = new SqlDataAdapter(command);
            DataTable comboBoxTable = new DataTable();
            comboBoxAdapter.Fill(comboBoxTable);
            cbMaNuoc.DataSource = comboBoxTable;
            cbMaNuoc.DisplayMember = "maNuoc";
            cbMaNuoc.ValueMember = "maNuoc";
        }
        void LoadCBMaNV()
        {
            SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT maNV FROM nhanVien";
                SqlDataAdapter comboBoxAdapter = new SqlDataAdapter(command);
                DataTable comboBoxTable = new DataTable();
                comboBoxAdapter.Fill(comboBoxTable);
                cbMaNV.DataSource = comboBoxTable;
                cbMaNV.DisplayMember = "maNV";
                cbMaNV.ValueMember = "maNV";
        }
        public FormHĐ()
        {
            InitializeComponent();
        }
        private void HienThiDanhSachHoaDon(DataGridView dgv, List<QuanLyHĐ> dsHĐ)
        {
            dgv.DataSource = dsHĐ.ToList();
        }
        private void HienThiDanhSachCTHĐ(DataGridView dgv, List<QuanLyCTHĐ> dsCTHĐ)
        {
            dgv.DataSource = dsCTHĐ.ToList();
        }
        private double LayGiatheoMaNuoc(string maNuoc)
        {
            double gia = 0;
            string query = "select gia from doUong where maNuoc=@maNuoc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@maNuoc", maNuoc);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                gia = Convert.ToDouble(result);
            }
            return gia;

        }
        private string LayTenNuocTheomaNuoc(string maNuoc)
        {
            string tenNuoc = string.Empty;
            string query = "select tenNuoc from doUong where maNuoc=@maNuoc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@maNuoc", maNuoc);
            object result = command.ExecuteScalar();
            if (result != null)
            {
                tenNuoc = result.ToString();
            }
            return tenNuoc;
        }
        private void TinhTongTien()
        {
            double tongTien = 0;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();
                string query = @" SELECT SUM(thanhTien) FROM CTHĐ WHERE maHD = @maHD";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maHD", txtMaHD.Text);
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        tongTien = Convert.ToDouble(result);
                    }
                }
                string updateQuery = @"UPDATE hoaDon SET tongtien = ( SELECT SUM(thanhTien) FROM CTHĐ WHERE hoaDon.maHD = CTHĐ.maHD)"; 
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@tongTien", tongTien);
                    updateCommand.Parameters.AddWithValue("@maHD", txtMaHD.Text);
                    updateCommand.ExecuteNonQuery();
                }
            }
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells["MaHD"].Value != null &&
                    row.Cells["MaHD"].Value.ToString() == txtMaHD.Text)
                {
                    row.Cells["TongTienHD"].Value = tongTien; 
                    break; 
                }
            }
        }
        private void btn_ThemHD_Click(object sender, EventArgs e)
        {
            DanhSachHĐ ds = new DanhSachHĐ(connection);
            string maHD = txtMaHD.Text;
            DateTime ngayLapHD = dtNgayLapHD.Value;
            string maNV = cbMaNV.Text;
            double tongTien = 0;
            if (ds.kiemTraMa(maHD))
            {
                MessageBox.Show("Trùng mã!.Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string them = "insert into hoaDon(maHD,ngayLapHD,maNV,tongTien) values (@maHD,@ngayLapHD,@maNV,@tongTien)";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("@maHD", maHD);
            command.Parameters.AddWithValue("@ngayLapHD", ngayLapHD);
            command.Parameters.AddWithValue("@maNV", maNV);
            command.Parameters.AddWithValue("@tongTien",tongTien);
            
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadCBMaNV();
            loaddataHĐ();
        }
        private void btn_XoaHD_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text;

            string xoa = "delete from CTHĐ where maHD=@maHD delete from hoaDon where maHD=@maHD";
            SqlCommand command = new SqlCommand(xoa, connection);
            command.Parameters.AddWithValue("@maHD", maHD);
            
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadCBMaNV();
            loaddataHĐ();
        }
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dgvHoaDon.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtNgayLapHD.Text= dgvHoaDon.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbMaNV.Text = dgvHoaDon.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void FormHĐ_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadCBMaNV();
            loaddataHĐ();
            loaddataCTHĐ();
            loadCBMANUOC();
    }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text;
            string maNuoc = cbMaNuoc.Text;
            string tenNuoc = LayTenNuocTheomaNuoc(maNuoc);
            int soLuong = int.Parse(nbSoLuong.Text);
            double gia = LayGiatheoMaNuoc(maNuoc);
            double thanhTien = gia* soLuong;
            string them = "insert into CTHĐ(maHD,maNuoc,tenNuoc,soLuong,gia,thanhTien) values(@maHD,@maNuoc,@tenNuoc,@soLuong,@gia,@thanhTien)";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("@maHD", maHD);
            command.Parameters.AddWithValue("@maNuoc", maNuoc);
            command.Parameters.AddWithValue("@tenNuoc", tenNuoc);
            command.Parameters.AddWithValue("@soLuong", soLuong);
            command.Parameters.AddWithValue("@gia", gia);
            command.Parameters.AddWithValue("@thanhTien", thanhTien);
           
            command.ExecuteNonQuery();
            TinhTongTien();
            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            loadCBMANUOC();
            loaddataCTHĐ();
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text;

            string xoa = "delete from CTHĐ where maHD=@maHD ";
            SqlCommand command = new SqlCommand(xoa, connection);
            command.Parameters.AddWithValue("@maHD", maHD);
            command.ExecuteNonQuery();
            TinhTongTien();
            MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);

            loadCBMANUOC();
            loaddataCTHĐ();
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
           string maHD = txtMaHD.Text;
            string maNuoc = cbMaNuoc.Text;
            string tenNuoc = LayTenNuocTheomaNuoc(maNuoc);
            int soLuong = int.Parse(nbSoLuong.Text);
            double gia = LayGiatheoMaNuoc(maNuoc);
            double thanhTien = gia * soLuong;
            string sua = "update CTHĐ set maNuoc=@maNuoc,@tenNuoc=tenNuoc,soLuong=@soLuong, where maHD=@maHD ";
            SqlCommand command = new SqlCommand(sua, connection);
           command.Parameters.AddWithValue("@maHD", maHD);
            command.Parameters.AddWithValue("@maNuoc", maNuoc);
            command.Parameters.AddWithValue("@tenNuoc", tenNuoc);
            command.Parameters.AddWithValue("@soLuong", soLuong);
            command.Parameters.AddWithValue("@thanhTien", thanhTien);
            command.ExecuteNonQuery();
            MessageBox.Show("Thông tin cập nhật thành công!", "Thông báo", MessageBoxButtons.OK);
            
            loadCBMANUOC();
            loaddataCTHĐ();
        }
        private void dgvCTHĐ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            txtMaHD.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbMaNuoc.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[1].Value.ToString();
            nbSoLuong.Text = dgvCTHĐ.Rows[e.RowIndex].Cells[2].Value.ToString();

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
            DanhSachHĐ ds=new DanhSachHĐ(connection);
            List<QuanLyCTHĐ>ketqua=ds.XemCTHĐ(txtMaHD.Text);
            if(ketqua.Count > 0 ) 
            {
              HienThiDanhSachCTHĐ(dgvCTHĐ,ketqua);
            }
            else
            {
                MessageBox.Show("Không tìm thấy!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
    }
