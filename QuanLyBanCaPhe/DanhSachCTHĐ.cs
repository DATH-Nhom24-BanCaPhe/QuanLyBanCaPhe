using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                string them = "insert into CTHĐ(maHD,maNuoc,soLuong,gia,@thanhTien,tenNuoc) values(@maHD,@maNuoc,@soLuong,@thanhTien,@tenNuoc)";
                SqlCommand command = new SqlCommand(them, conn);
                command.Parameters.AddWithValue("@maHD", ct.MaHD);
                command.Parameters.AddWithValue("@maNuoc", ct.MaNuoc);
                command.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                command.Parameters.AddWithValue("@gia", ct.Gia);
                command.Parameters.AddWithValue("@thanhTien",ct.ThanhTien);
                command.Parameters.AddWithValue("@tenNuoc", ct.TenNuoc);
                command.ExecuteNonQuery();
                this.dsCTHĐ.Add(ct);
                return true;
            }
            public void Xoa(string maHD)
            {
                string query = "delete from CTHĐ where maHD=@maHD delete from hoaDon where maHD=@maHD";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maHD", maHD);
                command.ExecuteNonQuery();
                dsCTHĐ.RemoveAll(ct => ct.MaHD == maHD);
                MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OK);
            }
            public void Sua(QuanLyCTHĐ ct)
            {
                string query = "UPDATE hoaDon SET maNuoc=@maNuoc,soLuong=@soLuong,gia=@gia,thanhTien=@thanhTien,tenNuoc=@tenNuoc where maHD=@maHD";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maHD", ct.MaHD);
                command.Parameters.AddWithValue("@maNuoc", ct.MaNuoc); 
                command.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                command.Parameters.AddWithValue("@gia", ct.Gia);
                command.Parameters.AddWithValue("@thanhTien", ct.ThanhTien);
                command.Parameters.AddWithValue("@tenNuoc", ct.TenNuoc);
                command.ExecuteNonQuery();

                int index = dsCTHĐ.FindIndex(n => n.MaHD == ct.MaHD);
                if (index != -1)
                {
                    dsCTHĐ[index] = ct;
                }
            MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);

            }
        public List<QuanLyCTHĐ> XemCTHĐ(string maHD)
        {
            List<QuanLyCTHĐ> ketQua = new List<QuanLyCTHĐ>();
            SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from CTHĐ where maHD=@maHD", conn);
            cmd.Parameters.AddWithValue("@maHD", maHD);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                QuanLyCTHĐ ct = new QuanLyCTHĐ
                {
                    MaHD = reader["maHD"].ToString(),
                    MaNuoc = reader["maNuoc"].ToString(),
                    TenNuoc = reader["tenNuoc"].ToString(),
                    SoLuong = Convert.ToInt32(reader["soLuong"]),
                    Gia = Convert.ToDouble(reader["gia"]),
                    ThanhTien = Convert.ToDouble(reader["thanhTien"])
                };
                ketQua.Add(ct);
            }
            return ketQua;
        }

    }
    }

