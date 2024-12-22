using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
    internal class DanhSachNhanVien
    {
            private List<QuanLyNhanVien> dsNV;
            private SqlConnection conn;
            public DanhSachNhanVien(SqlConnection conn)
            {
                this.dsNV = new List<QuanLyNhanVien>();
                this.conn = conn;
            }
            public DanhSachNhanVien(List<QuanLyNhanVien> dsNV)
            {
                this.dsNV = dsNV;
            }
            public List<QuanLyNhanVien> DsNV
            {
                get { return dsNV; }
                set { dsNV = value; }
            }
            public void Them(QuanLyNhanVien nv)
            {                  
                string query = "INSERT INTO nhanVien(maNV, hoTen, ngaySinh, sĐT, ngayVaoLam, viTriLamViec, gioiTinh) VALUES (@maNV, @hoTen, @ngaySinh, @sĐT, @ngayVaoLam, @viTriLamViec, @gioiTinh)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maNV", nv.ID);
                command.Parameters.AddWithValue("@hoTen", nv.HoTen);
                command.Parameters.AddWithValue("@ngaySinh", nv.NgaySinh);
                command.Parameters.AddWithValue("@sĐT", nv.SĐT);
                command.Parameters.AddWithValue("@ngayVaoLam", nv.NgayVaoLam);
                command.Parameters.AddWithValue("@viTriLamViec", nv.ViTriLamViec);
                command.Parameters.AddWithValue("@gioiTinh", nv.GioiTinh);
                command.ExecuteNonQuery();
                this.dsNV.Add(nv);       
            MessageBox.Show("Thêm thông tin thành công !","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
            public void Xoa(string maNV)
            {
                string query = "DELETE FROM nhanVien WHERE maNV = @maNV";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@maNV", maNV);
                command.ExecuteNonQuery();
                dsNV.RemoveAll(nv => nv.ID == maNV);
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);

        }
            public void Sua(QuanLyNhanVien nv)
            {
                string query = "UPDATE nhanVien SET hoTen = @hoTen, ngaySinh = @ngaySinh, sĐT = @sĐT, ngayVaoLam = @ngayVaoLam, viTriLamViec = @viTriLamViec, gioiTinh = @gioiTinh WHERE maNV = @maNV";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@maNV", nv.ID);
                command.Parameters.AddWithValue("@hoTen", nv.HoTen);
                command.Parameters.AddWithValue("@ngaySinh", nv.NgaySinh);
                command.Parameters.AddWithValue("@sĐT", nv.SĐT);
                command.Parameters.AddWithValue("@ngayVaoLam", nv.NgayVaoLam);
                command.Parameters.AddWithValue("@viTriLamViec", nv.ViTriLamViec);
                command.Parameters.AddWithValue("@gioiTinh", nv.GioiTinh);

                command.ExecuteNonQuery();

                int index = dsNV.FindIndex(n => n.ID == nv.ID);
                if (index != -1)
                {
                    dsNV[index] = nv;
                }
            MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);

        }
            public List<QuanLyNhanVien> TimTheoMa(string ma)
            {
                List<QuanLyNhanVien> ketQua = new List<QuanLyNhanVien>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from nhanVien where maNV=@maNV", conn);
                cmd.Parameters.AddWithValue("@maNV", ma);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLyNhanVien nv = new QuanLyNhanVien
                    {
                        ID = reader["maNV"].ToString(),
                        HoTen = reader["hoTen"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["ngaySinh"]),
                        SĐT = reader["sĐT"].ToString(),
                        NgayVaoLam = Convert.ToDateTime(reader["ngayVaoLam"]),
                        ViTriLamViec = reader["viTriLamViec"].ToString(),
                        GioiTinh = reader["gioiTinh"].ToString()
                    };
                    ketQua.Add(nv);
                }
                return ketQua;
            }
            public List<QuanLyNhanVien> TimTheoTen(string ten)
            {
                List<QuanLyNhanVien> ketQua = new List<QuanLyNhanVien>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from nhanVien where hoTen like '%'+@hoTen+'%'", conn);
                cmd.Parameters.AddWithValue("@hoTen", ten);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLyNhanVien nv = new QuanLyNhanVien
                    {
                        ID = reader["maNV"].ToString(),
                        HoTen = reader["hoTen"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["ngaySinh"]),
                        SĐT = reader["sĐT"].ToString(),
                        NgayVaoLam = Convert.ToDateTime(reader["ngayVaoLam"]),
                        ViTriLamViec = reader["viTriLamViec"].ToString(),
                        GioiTinh = reader["gioiTinh"].ToString()
                    };
                    ketQua.Add(nv);
                }
                return ketQua;
            }
            public int tinhTuoi(DateTime ngaySinh)
            {
                DateTime ngayHienTai = DateTime.Now;
                int tuoi = ngayHienTai.Year - ngaySinh.Year;
                if (ngayHienTai < ngaySinh)
                {
                    return -1;
                }
                if (ngayHienTai.Month < ngaySinh.Month || (ngayHienTai.Month == ngaySinh.Month && ngayHienTai.Day < ngaySinh.Day))
                {
                    tuoi--;
                }
                return tuoi;
            }

        }
    }
    
