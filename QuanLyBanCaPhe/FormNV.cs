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
    public partial class FormNV : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        private List<QuanLyNhanVien> dsNhanVien = new List<QuanLyNhanVien>();


        public FormNV()
        {
            InitializeComponent();
            cbChucVu.Items.Add("Quản lý");
            cbChucVu.Items.Add("Nhân viên vệ sinh");
            cbChucVu.Items.Add("Thu ngân");
            cbChucVu.Items.Add("Nhân viên pha chế");
            cbChucVu.Items.Add("Nhân viên phục vụ");
            cbChucVu.Items.Add("Bảo vệ");


        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            DanhSachNhanVien ds = new DanhSachNhanVien(connection);
            string iD = txtMaNV.Text;
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = dtNgaySinh.Value.Date;
            if (ds.tinhTuoi(ngaySinh) < 18)
            {
                MessageBox.Show("Không đủ tuổi .Vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sĐT = txtSĐT.Text;
            DateTime ngayVaoLam = dtNgayVaoLam.Value.Date;
            string viTriLamViec = cbChucVu.SelectedItem.ToString();
            string gioiTinh = null;
            if (radNam.Checked == true)
                gioiTinh = "Nam";
            else if (radNu.Checked == true)
                gioiTinh = "Nữ";
            else
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ds.kiemTraMa(iD) == true)
            {
                MessageBox.Show("ID đã tồn tại.Vui lòng nhập ID khác !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string them = "insert into nhanVien(maNV,hoTen,ngaySinh,sĐT,ngayVaoLam,viTriLamViec,gioiTinh) values (@maNV,@hoTen,@ngaySinh,@sĐT,@ngayVaoLam,@viTriLamViec,@gioiTinh)";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("maNv", iD);
            command.Parameters.AddWithValue("hoTen", hoTen);
            command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
            command.Parameters.AddWithValue("@sĐT", sĐT);
            command.Parameters.AddWithValue("@ngayVaoLam", ngayVaoLam);
            command.Parameters.AddWithValue("@viTriLamViec", viTriLamViec);
            command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
            loadnv();

        }
        private void FormNV_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadnv();
        }
        public void loadnv()
        {
            SqlCommand command = new SqlCommand("select * from nhanVien", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNhanVien.DataSource = dt;
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string iD = txtMaNV.Text;
            string xoa = "delete from nhanVien where maNV=@maNV";
            SqlCommand command = new SqlCommand(xoa, connection);
            command.Parameters.AddWithValue("maNv", iD);
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
            loadnv();
        }
        private void HienThiDanhSachNhanVien(DataGridView dgv, List<QuanLyNhanVien> ds)
        {
            dgv.DataSource = ds;

        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimMa.Text) && string.IsNullOrWhiteSpace(txtTimTen.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hoặc tên nhân viên để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radTimMa.Checked)
            {
                DanhSachNhanVien ds = new DanhSachNhanVien(connection);
                List<QuanLyNhanVien> ketqua = ds.TimTheoMa(txtTimMa.Text.Trim());
                if (ketqua.Count > 0)
                {
                    HienThiDanhSachNhanVien(dgvNhanVien, ketqua);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã này!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else if (radTimTen.Checked)
            {
                DanhSachNhanVien ds = new DanhSachNhanVien(connection);
                List<QuanLyNhanVien> ketqua = ds.TimTheoTen(txtTimTen.Text);
                if (ketqua.Count > 0)
                {
                    HienThiDanhSachNhanVien(dgvNhanVien, ketqua);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với tên này!", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string iD = txtMaNV.Text;
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = dtNgaySinh.Value;
            string sĐT = txtSĐT.Text;
            DateTime ngayVaoLam = dtNgayVaoLam.Value;
            string viTriLamViec = cbChucVu.SelectedItem.ToString();
            string gioiTinh = null;
            if (radNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";

            string sua = "update nhanVien set hoTen=@hoTen,ngaySinh=@ngaySinh,sĐt=@sĐT,ngayVaoLam=@ngayVaoLam,viTriLamViec=@viTriLAmVIec,gioiTinh=@gioiTinh where maNV=@maNV";
            SqlCommand command = new SqlCommand(sua, connection);
            command.Parameters.AddWithValue("maNv", iD);
            command.Parameters.AddWithValue("hoTen", hoTen);
            command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
            command.Parameters.AddWithValue("@sĐT", sĐT);
            command.Parameters.AddWithValue("@ngayVaoLam", ngayVaoLam);
            command.Parameters.AddWithValue("@viTriLamViec", viTriLamViec);
            command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
            command.ExecuteNonQuery();
            MessageBox.Show("Đã cập nhật thành công!", "Thông báo", MessageBoxButtons.OK);
            loadnv();
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult ketQua = MessageBox.Show("Bạn thật sự muốn thoát!", "Thông báo", MessageBoxButtons.OK);
            this.Close();
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvNhanVien.CurrentRow.Index;
            txtMaNV.Text = dgvNhanVien.Rows[i].Cells[0].Value.ToString();
            txtHoTen.Text = dgvNhanVien.Rows[i].Cells[1].Value.ToString();
            dtNgaySinh.Text = Convert.ToString(dgvNhanVien.Rows[i].Cells[2].Value);
            txtSĐT.Text = dgvNhanVien.Rows[i].Cells[3].Value.ToString();
            dtNgayVaoLam.Text = (Convert.ToDateTime(dgvNhanVien.Rows[i].Cells[4].Value)).ToString();
            cbChucVu.Text = dgvNhanVien.Rows[i].Cells[5].Value.ToString();
            string gioiTinh = dgvNhanVien.Rows[i].Cells[6].Value.ToString();
            gbGioiTinh.Text = gioiTinh;
            if (gioiTinh == "Nam")
            {
                radNam.Checked = true;
            }
            else
                radNu.Checked = true;
        }
    }
}
