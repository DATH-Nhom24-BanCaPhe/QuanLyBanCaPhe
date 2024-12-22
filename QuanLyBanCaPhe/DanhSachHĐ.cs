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
        public bool Them(QuanLyHĐ hd)
        {
            string them = "insert into hoaDon(maHD,ngayLapHD,tongTien,maNV) values(@maHD,@ngayLapHD,@tongTien,@maNV)";
            SqlCommand command = new SqlCommand(them, conn);
            command.Parameters.AddWithValue("@maHD", hd.MaHD);
            command.Parameters.AddWithValue("@ngayLapHD", hd.NgayLapHoaDon);
            command.Parameters.AddWithValue("@tongTien", hd.TongTien);
            command.Parameters.AddWithValue("@maNV", hd.MaNV);
            command.ExecuteNonQuery();
            this.dsHD.Add(hd);
            return true;
        }
        //public bool Xoa(string maHD)
        //{
        //    string query = "delete from hoaDon where maHD=@maHD";
        //    SqlCommand command = new SqlCommand(query, conn);

        //    command.Parameters.AddWithValue("@maHD", maHD);
        //    command.ExecuteNonQuery();
        //    dsHD.RemoveAll(ln => ln.MaHD == maHD);
        //    return true;
        //}

        //public bool Sua(QuanLyHĐ hd)
        //{
        //    string query = "UPDATE hoaDon SET maNV=@maNV,maHD=@maHD,ngayLapHD=@ngayLapHD,tongTien=@tongTien where maHD=@maHD";
        //    SqlCommand command = new SqlCommand(query, conn);         
        //    command.Parameters.AddWithValue("@maHD", hd.MaHD);
        //    command.Parameters.AddWithValue("@ngayLapHD", hd.NgayLapHoaDon);
        //    command.Parameters.AddWithValue("@tongTien", hd.TongTien);
        //    command.Parameters.AddWithValue("@maNV", hd.MaNV);
        //    command.ExecuteNonQuery();

        //    int index = dsHD.FindIndex(n => n.MaHD == hd.MaHD);
        //    if (index != -1)
        //    {
        //        dsHD[index] = hd;
        //    }
        //    return true;
        //}
      
        public List<QuanLyHĐ> XemHĐ(DateTime ngayBD,DateTime ngayKT)
        {
            List<QuanLyHĐ> kq = new List<QuanLyHĐ>();
            SqlConnection conn = new SqlConnection("Data Source = TRUCLY; Initial Catalog = doan; Integrated Security = True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM hoaDon WHERE ngayLapHD between @ngayBD and @ngayKT ",conn);
            command.Parameters.AddWithValue("@ngayBD",ngayBD);
            command.Parameters.AddWithValue("@ngayKT",ngayKT);
            
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                QuanLyHĐ hd = new QuanLyHĐ
                {
                    MaHD = reader["maHD"].ToString(),
                    NgayLapHoaDon = Convert.ToDateTime(reader["ngayLapHD"]),
                    TongTien = Convert.ToDouble(reader["tongTien"]),
                    MaNV = reader["maNV"].ToString()
                };
                kq.Add(hd);
            }
            return kq;
        }
    }
    }
    

