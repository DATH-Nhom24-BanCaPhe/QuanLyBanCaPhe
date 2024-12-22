using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
    internal class DanhSachThongKe
    {
        private List<QuanLyThongKe> dsTK;
        private SqlConnection conn;
        public DanhSachThongKe(SqlConnection conn)
        {
            this.dsTK = new List<QuanLyThongKe>();
            this.conn = conn;
        }
        public DanhSachThongKe(List<QuanLyThongKe> dsTK)
        {
            this.dsTK = dsTK;
        }
        public List<QuanLyThongKe> DSTK
        {
            get { return this.dsTK; }
            set { this.dsTK = value; }
        }
        public bool kiemtratrung(DateTime ngayTK)
        {
            string query = "select count (*) from thongKe where CONVERT(date, ngayTK) = CONVERT(date, @ngayTK) ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ngayTK", ngayTK);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
        public void Them(QuanLyThongKe tk)
        {
            if (kiemtratrung(tk.NgayTK))
            {
                MessageBox.Show("Thống kê ngày này đã có rồi!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            string them = "insert into thongKe(ngayTK,soLuongHĐ,tongTienTK,maNV) values(@ngayTK,@soLuongHĐ,@tongTienTK,@maNV)";
            SqlCommand command = new SqlCommand(them, conn);
            command.Parameters.AddWithValue("@ngayTK", tk.NgayTK);
            command.Parameters.AddWithValue("@soLuongHĐ", tk.SoLuongHĐ);
            command.Parameters.AddWithValue("@tongTienTK", tk.TongTienTK);
            command.Parameters.AddWithValue("@maNV", tk.MaNV);
            command.ExecuteNonQuery();
            this.dsTK.Add(tk);
            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);

        }
        public void Xoa(DateTime ngayTK)
        {
            string query = "DELETE FROM thongKe WHERE CONVERT(date, ngayTK) = CONVERT(date, @ngayTK)  ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@ngayTK", ngayTK);
            command.ExecuteNonQuery();
            dsTK.RemoveAll(ln => ln.NgayTK == ngayTK);
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
        }
        public bool Sua(QuanLyThongKe tk)
        {
            string sua = "update thongKe set maThongKe=@maThongKe,ngayTK=@ngayTK,soLuongHĐ=@soLuongHĐ,tongTien=@tongTien,maNV=@maNV";
            SqlCommand command = new SqlCommand(sua, conn);


            command.Parameters.AddWithValue("@ngayTK", tk.NgayTK);
            command.Parameters.AddWithValue("@soLuongHĐ", tk.SoLuongHĐ);
            command.Parameters.AddWithValue("@tongTienTK", tk.TongTienTK);
            command.Parameters.AddWithValue("@maNV", tk.MaNV);
            command.ExecuteNonQuery();
            int index = dsTK.FindIndex(n => n.NgayTK == tk.NgayTK);
            if (index != -1)
            {
                dsTK[index] = tk;
            }
            return true;
        }
        
    }
}
