using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public bool kiemTraMa(string maThongKe)
            {
                string query = "SELECT COUNT(*) FROM thongKe WHERE maThongKe= @maThongKe";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maThongKe", maThongKe);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            public bool Them(QuanLyThongKe tk)
            {
                if (kiemTraMa(tk.MaThongKe))
                    return false;
                string them = "insert into thongKe(maThongKe,ngayTK,soLuongHĐ,tongTienTK,maNV) values(@maThongKe,@ngayTK,@soLuongHĐ,@tongTienTK,@maNV)";
                SqlCommand command = new SqlCommand(them, conn);
                command.Parameters.AddWithValue("@maThongKe", tk.MaThongKe);
                command.Parameters.AddWithValue("@soLuongHĐ", tk.SoLuongHĐ);
                command.Parameters.AddWithValue("@ngayTK", tk.NgayTK);
                command.Parameters.AddWithValue("@tongTienTK",tk.TongTienTK);
                command.Parameters.AddWithValue("@maNV", tk.MaNV);
                command.ExecuteNonQuery();
                this.dsTK.Add(tk);
                return true;

            }
            public bool Xoa(string maThongKe)
            {
            string query = "DELETE FROM thongKe WHERE maThongKe = @maThongKe  delete from nhanVien where maNV=@maNV";
            
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@maThongKe", maThongKe);
                command.ExecuteNonQuery();
                dsTK.RemoveAll(ln => ln.MaThongKe == maThongKe);
                return true;
            }
            public bool Sua(QuanLyThongKe tk)
            {
                string sua = "update thongKe set maThongKe=@maThongKe,ngayTK=@ngayTK,soLuongHĐ=@soLuongHĐ,tongTienTK=@tongTienTK,maNV=@maNV";
                SqlCommand command = new SqlCommand(sua, conn);
                command.Parameters.AddWithValue("@maThongKe", tk.MaThongKe);
            command.Parameters.AddWithValue("@soLuongHĐ", tk.SoLuongHĐ);
            command.Parameters.AddWithValue("@ngayTK", tk.NgayTK);
            command.Parameters.AddWithValue("@tongTienTK", tk.TongTienTK);
            command.Parameters.AddWithValue("@maNV", tk.MaNV);
                command.ExecuteNonQuery();
                int index = dsTK.FindIndex(n => n.MaThongKe == tk.MaThongKe);
                if (index != -1)
                {
                    dsTK[index] = tk;
                }
                return true;
            }
        }
    }
