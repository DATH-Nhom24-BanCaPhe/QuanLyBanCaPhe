using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class DanhSachCTHĐ
    {
       
            private List<QuanLyCTHĐ> dsCTHĐ;
            private SqlConnection conn;

            public DanhSachCTHĐ(SqlConnection conn)
            {
                this.dsCTHĐ = new List<QuanLyCTHĐ>();
                this.conn = conn;
            }
            public DanhSachCTHĐ(List<QuanLyCTHĐ> dsCTHĐ)
            {
                this.dsCTHĐ = dsCTHĐ;
            }
            public List<QuanLyCTHĐ> DsCTHĐ
            {
                get { return this.dsCTHĐ; }
                set { this.dsCTHĐ = value; }
            }
            public bool Them(QuanLyCTHĐ ct)
            {
                string them = "insert into CTHĐ(maHD,maNuoc,tenNuoc,soLuong,gia,@thanhTien) values(@maHD,@maNuoc,@tenNuoc,@soLuong,@thanhTien)";
                SqlCommand command = new SqlCommand(them, conn);
                command.Parameters.AddWithValue("@maHD", ct.MaHD);
                command.Parameters.AddWithValue("@maNuoc", ct.MaNuoc);
                command.Parameters.AddWithValue("@tenNuoc", ct.TenNuoc);
                command.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                command.Parameters.AddWithValue("@gia", ct.Gia);
                command.Parameters.AddWithValue("@thanhTien",ct.ThanhTien);
                command.ExecuteNonQuery();
                this.dsCTHĐ.Add(ct);
                return true;
            }
            public bool Xoa(string maHD)
            {

                string query = "DELETE FROM hoaDon WHERE maHD = @maHD";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@maHD", maHD);
                command.ExecuteNonQuery();
                dsCTHĐ.RemoveAll(ct => ct.MaHD == maHD);
                return true;
            }
            public bool Sua(QuanLyCTHĐ ct)
            {
                string query = "UPDATE hoaDon SET maNuoc=@maNuoc,tenNuoc=@tenNuoc,soLuong=@soLuong,gia=@gia,thanhTien=@thanhTien where maHD=@maHD";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maHD", ct.MaHD);
                command.Parameters.AddWithValue("@maNuoc", ct.MaNuoc);
                command.Parameters.AddWithValue("@tenNuoc", ct.TenNuoc);
                command.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                command.Parameters.AddWithValue("@gia", ct.Gia);
            command.Parameters.AddWithValue("@thanhTien", ct.ThanhTien);
            command.ExecuteNonQuery();

                int index = dsCTHĐ.FindIndex(n => n.MaHD == ct.MaHD);
                if (index != -1)
                {
                    dsCTHĐ[index] = ct;
                }
                return true;

            }

        }
    }

