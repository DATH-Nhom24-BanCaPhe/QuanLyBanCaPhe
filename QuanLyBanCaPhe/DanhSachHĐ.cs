using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class DanhSachHĐ
    {
        private List<QuanLyCTHĐ> dsCTHĐ;
        private List<QuanLyHĐ> dsHD;
        private SqlConnection conn;
        public DanhSachHĐ(SqlConnection conn)
        {
            this.dsCTHĐ = new List<QuanLyCTHĐ>();
            this.dsHD = new List<QuanLyHĐ>();
            this.conn = conn;
        }
        public DanhSachHĐ(List<QuanLyHĐ> dsHD)
        {
            this.dsHD = dsHD;
        }
        public DanhSachHĐ(List<QuanLyCTHĐ> dsCTHĐ)
        {
            this.dsCTHĐ = dsCTHĐ;
        }
        public List<QuanLyHĐ> DSHD
        {
            get { return this.dsHD; }
            set { this.dsHD = value; }
        }
        public bool kiemTraMa(string maHD)
        {

            string query = "SELECT COUNT(*) FROM hoaDon WHERE maHD= @maHD";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@maHD", maHD);

            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
        public bool Them(QuanLyHĐ hd)
        {
            if (kiemTraMa(hd.MaHD))
                return false;

            string them = "insert into hoaDon(maNV,maHD,ngayLapHD,tongTien) values(@maNV,@maHD,@ngayLapHD,@tongTien)";
            SqlCommand command = new SqlCommand(them, conn);
            command.Parameters.AddWithValue("@maNV", hd.MaNV);
            command.Parameters.AddWithValue("@maHD", hd.MaHD);
            command.Parameters.AddWithValue("@ngayLapHD", hd.NgayLapHoaDon);
            command.Parameters.AddWithValue("@tongTien", hd.TongTien);
       
            command.ExecuteNonQuery();
            this.dsHD.Add(hd);
            return true;
        }
        public bool Xoa(string maHD)
        {
            string query = "BEGIN TRANSACTION;DELETE FROM CTHĐ WHERE maHD =@maHD;DELETE FROM hoaDon WHERE maHD =@maHD;COMMIT TRANSACTION";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@maHD", maHD);
            command.ExecuteNonQuery();
            dsHD.RemoveAll(ln => ln.MaHD == maHD);
            return true;
        }

        public bool Sua(QuanLyHĐ hd)
        {
            string query = "UPDATE hoaDon SET maNV=@maNV,maHD=@maHD,ngayLapHD=@ngayLapHD,tongTien=@tongTien";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@maNV", hd.MaNV);
            command.Parameters.AddWithValue("@maHD", hd.MaHD);
            command.Parameters.AddWithValue("@ngayLapHD", hd.NgayLapHoaDon);
            command.Parameters.AddWithValue("@tongTien", hd.TongTien);
            command.ExecuteNonQuery();

            int index = dsHD.FindIndex(n => n.MaHD == hd.MaHD);
            if (index != -1)
            {
                dsHD[index] = hd;
            }
            return true;
        }
        public List<QuanLyCTHĐ>XemCTHĐ(string maHD)
        {
            List<QuanLyCTHĐ> ketQua = new List<QuanLyCTHĐ>();
            SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = doan; Integrated Security = True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from CTHĐ where maHD=@maHD", conn);
            cmd.Parameters.AddWithValue("@maHD",maHD);
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
                };
                ketQua.Add(ct);
            }
            return ketQua;
        }
    }
    }
    

