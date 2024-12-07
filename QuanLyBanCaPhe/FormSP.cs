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
    public partial class FormSP : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        private List<QuanLySanPham> qldu = new List<QuanLySanPham>();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from doUong";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDoUong.DataSource = table;

        }
        void LoadComboBox()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT maLoai FROM LoaiNuoc";
                SqlDataAdapter comboBoxAdapter = new SqlDataAdapter(command);
                DataTable comboBoxTable = new DataTable();
                comboBoxAdapter.Fill(comboBoxTable);

                cbMaLoai.DataSource = comboBoxTable;
                cbMaLoai.DisplayMember = "maLoai";
                cbMaLoai.ValueMember = "maLoai";
            }
        }
        public FormSP()
        {
            InitializeComponent();
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            DanhSachSanPham ds = new DanhSachSanPham(connection);
            string maLoai = cbMaLoai.Text;
            string maNuoc = txtMaNuoc.Text;
            if (ds.kiemTraMa(maNuoc))
            {
                MessageBox.Show("Trùng mã!.Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tenNuoc = txtTenNuoc.Text;
            double gia = double.Parse(txtGia.Text);
            string them = "insert into doUong(maLoai,maNuoc,tenNuoc,gia) values(@maLoai,@maNuoc,@tenNuoc,@gia)";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("maLoai", maLoai);
            command.Parameters.AddWithValue("maNUoc", maNuoc);
            command.Parameters.AddWithValue("@tenNuoc", tenNuoc);
            command.Parameters.AddWithValue("@gia", gia);
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadComboBox();
            loaddata();
        }
        private void btn_oa_Click(object sender, EventArgs e)
        {
            string maLoai = cbMaLoai.Text;
            string maNuoc = txtMaNuoc.Text;
            string tenNuoc = txtTenNuoc.Text;
            double gia = double.Parse(txtGia.Text);
            string xoa = "delete from doUong tenNuoc=@tenNuoc,gia=@gia where  maNuoc=@maNUoc";
            SqlCommand command = new SqlCommand(xoa, connection);
            command.Parameters.AddWithValue("maLoai", maLoai);
            command.Parameters.AddWithValue("maNUoc", maNuoc);
            command.Parameters.AddWithValue("@tenNuoc", tenNuoc);
            command.Parameters.AddWithValue("@gia", gia);
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            LoadComboBox();
            loaddata();
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string maLoai = cbMaLoai.Text;
            string maNuoc = txtMaNuoc.Text;
            string tenNuoc = txtTenNuoc.Text;
            double gia = double.Parse(txtGia.Text);
            string them = "update doUong(maLoai,maNuoc,tenNuoc,gia) where maNuoc=@maNUoc";
            SqlCommand command = new SqlCommand(them, connection);
            command.Parameters.AddWithValue("maLoai", maLoai);
            command.Parameters.AddWithValue("maNUoc", maNuoc);
            command.Parameters.AddWithValue("@tenNuoc", tenNuoc);
            command.Parameters.AddWithValue("@gia", gia);
            command.ExecuteNonQuery();


            MessageBox.Show("Thông tin đã được cập nhật!", "Thông báo", MessageBoxButtons.OK);
            LoadComboBox();
            loaddata();
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (radTimMa.Checked)
            {
                DanhSachSanPham ds = new DanhSachSanPham(connection);
                List<QuanLySanPham> ketqua = ds.TimTheoMa(txtTimMa.Text);

                if (ketqua.Count > 0)
                {
                    HienThiDanhSachSanPham(dgvDoUong, ketqua);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            if (radTimTen.Checked)
            {
                DanhSachSanPham ds = new DanhSachSanPham(connection);
                List<QuanLySanPham> ketqua = ds.TimTheoTen(txtTimTen.Text);

                if (ketqua.Count > 0)
                {
                    HienThiDanhSachSanPham(dgvDoUong, ketqua);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy ", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
        private void HienThiDanhSachSanPham(DataGridView dgv, List<QuanLySanPham> ds)
        {
            dgv.DataSource = ds.ToList();
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult ketqua = MessageBox.Show("Bạn muons thoát?", "Thông báo", MessageBoxButtons.OK);
            this.Close();
        }
        private void dgvDoUong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvDoUong.CurrentCell.RowIndex;
            cbMaLoai.Text = dgvDoUong.Rows[i].Cells[0].Value.ToString();
            txtMaNuoc.Text = dgvDoUong.Rows[i].Cells[1].Value.ToString();
            txtTenNuoc.Text = dgvDoUong.Rows[i].Cells[2].Value.ToString();
            txtGia.Text = dgvDoUong.Rows[i].Cells[3].Value.ToString();
        }
        private void FormSP_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadComboBox();
            loaddata();
        }
    }
}
