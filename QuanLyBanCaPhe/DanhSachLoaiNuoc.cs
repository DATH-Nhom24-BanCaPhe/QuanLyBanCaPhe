using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
        internal class DanhSachLoaiNuoc
        {
            private List<QuanLyLoaiNuoc> dsLN;
            private SqlConnection conn;

            public DanhSachLoaiNuoc(SqlConnection conn)
            {
                this.dsLN = new List<QuanLyLoaiNuoc>();
                this.conn = conn;
            }
            public DanhSachLoaiNuoc(List<QuanLyLoaiNuoc> dsLN)
            {
                this.dsLN = dsLN;
            }
            public List<QuanLyLoaiNuoc> DSLN
            {
                get { return dsLN; }
                set { dsLN = value; }
            }
            public bool kiemTraMa(string maLoai)
            {

                string query = "SELECT COUNT(*) FROM LoaiNuoc WHERE maLoai= @maLoai";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maLoai", maLoai);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            public void Them(QuanLyLoaiNuoc ln)
            {
                if (kiemTraMa(ln.MaLoai))
            {
                MessageBox.Show("Trùng mã!.Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK);
                return;
            }      
                string them = "insert into LoaiNuoc(maLoai,tenLoai) values(@maLoai,@tenLoai)";
                SqlCommand command = new SqlCommand(them, conn);
                command.Parameters.AddWithValue("@maLoai", ln.MaLoai);
                command.Parameters.AddWithValue("@tenLoai", ln.TenLoai);
                command.ExecuteNonQuery();
                this.dsLN.Add(ln);
            MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            }
            public void Xoa(string maLoai)
            {
                string query = "DELETE FROM LoaiNuoc WHERE maLoai=@maLoai";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maLoai", maLoai);
                command.ExecuteNonQuery();
                dsLN.RemoveAll(ln => ln.MaLoai == maLoai);
                MessageBox.Show("Xóa thành công !","Thông báo",MessageBoxButtons.OK);
            }
            public void Sua(QuanLyLoaiNuoc ln)
            {
                string query = "Update LoaiNuoc SET tenLoai=@tenLoai where maLoai=@maLoai";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@maLoai", ln.MaLoai);
                command.Parameters.AddWithValue("@tenLoai", ln.TenLoai);
                command.ExecuteNonQuery();
                int index = dsLN.FindIndex(n => n.MaLoai == ln.MaLoai);
                if (index != -1)
                {
                    dsLN[index] = ln;
                }
            MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            }
            public List<QuanLyLoaiNuoc> TimTheoMa(string ma)
            {
                List<QuanLyLoaiNuoc> ketQua = new List<QuanLyLoaiNuoc>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from LoaiNuoc where maLoai=@maLoai", conn);
                cmd.Parameters.AddWithValue("@maLoai", ma);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLyLoaiNuoc ln = new QuanLyLoaiNuoc
                    {
                        MaLoai = reader["maloai"].ToString(),
                        TenLoai = reader["tenLoai"].ToString()
                    };
                    ketQua.Add(ln);

                }
                return ketQua;

            }
            public List<QuanLyLoaiNuoc> TimTheoTen(string ten)
            {
                List<QuanLyLoaiNuoc> ketQua = new List<QuanLyLoaiNuoc>();
                SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = csdl; Integrated Security = True;");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from LoaiNuoc where tenLoai like '%'+@tenLoai+'%'", conn);
                cmd.Parameters.AddWithValue("@tenLoai", ten);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuanLyLoaiNuoc nv = new QuanLyLoaiNuoc
                    {
                        MaLoai = reader["maLoai"].ToString(),
                        TenLoai = reader["tenLoai"].ToString()
                    };
                    ketQua.Add(nv);

                }
                return ketQua;
            }
        }
    }


