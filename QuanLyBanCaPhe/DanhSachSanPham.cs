using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class DanhSachSanPham
    {
            private List<QuanLySanPham> dsDU;
            private SqlConnection conn;
            public DanhSachSanPham(SqlConnection conn)
            {
                this.dsDU = new List<QuanLySanPham>();
                this.conn = conn;
            }
            public DanhSachSanPham(List<QuanLySanPham> dsDU)
            {
                this.dsDU = dsDU;
            }
            public List<QuanLySanPham> DSDU
            {
                get { return this.dsDU; }
                set { this.dsDU = value; }
            }
            public bool kiemTraMa(string maNuoc)
            {

                string query = "SELECT COUNT(*) FROM doUong WHERE maNuoc = @maNuoc";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maNuoc", maNuoc);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            public bool Them(QuanLySanPham du)
            {
                if (kiemTraMa(du.MaNuoc))
                    return false;

                string them = "insert into doUong(maNuoc,tenNuoc,gia) values(@maNuoc,@tenNuoc,@gia)";
                SqlCommand command = new SqlCommand(them, conn);

                command.Parameters.AddWithValue("@maNUoc", du.MaNuoc);
                command.Parameters.AddWithValue("@tenNuoc", du.TenNuoc);
                command.Parameters.AddWithValue("@gia", du.Gia);

                command.ExecuteNonQuery();
                this.dsDU.Add(du);
                return true;

            }
            public bool Xoa(string maNuoc)
            {
                string query = "DELETE FROM doUong WHERE maNuoc = @maNuoc";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@maNuoc", maNuoc);
                command.ExecuteNonQuery();
                dsDU.RemoveAll(du => du.MaNuoc == maNuoc);
                return true;
            }
            public bool Sua(QuanLySanPham du)
            {
                string query = "UPDATE doUong SET maNuoc=@maNuoc,tenNuoc=@tenNuoc,gia=@gia";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maNUoc", du.MaNuoc);
                command.Parameters.AddWithValue("@tenNuoc", du.TenNuoc);
                command.Parameters.AddWithValue("@gia", du.Gia);

                command.ExecuteNonQuery();

                int index = dsDU.FindIndex(n => n.MaNuoc == du.MaNuoc);
                if (index != -1)
                {
                    dsDU[index] = du;
                }
                return true;

            }
            public List<QuanLySanPham> TimTheoTen(string ten)
            {
                List<QuanLySanPham> ketQua = new List<QuanLySanPham>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = doan; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from doUong where tenNuoc like '%'+@tenNuoc+'%'", conn);
                cmd.Parameters.AddWithValue("@tenNuoc", ten);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLySanPham du = new QuanLySanPham
                    {
                        MaLoai = reader["maLoai"].ToString(),
                        MaNuoc = reader["maNuoc"].ToString(),
                        TenNuoc = reader["tenNuoc"].ToString(),
                        Gia = Convert.ToDouble(reader["gia"])

                    };
                    ketQua.Add(du);
                }
                return ketQua;
            }
            public List<QuanLySanPham> TimTheoMa(string ma)
            {
                List<QuanLySanPham> ketQua = new List<QuanLySanPham>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = doan; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from doUong where maNuoc=@maNuoc", conn);
                cmd.Parameters.AddWithValue("@maNuoc", ma);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLySanPham du = new QuanLySanPham
                    {
                        MaLoai = reader["maLoai"].ToString(),
                        MaNuoc = reader["maNuoc"].ToString(),
                        TenNuoc = reader["tenNuoc"].ToString(),
                        Gia = Convert.ToDouble(reader["gia"])

                    };
                    ketQua.Add(du);
                }
                return ketQua;
            }
        }
    }



