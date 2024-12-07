using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLyHĐ
    {
           
            private string maHD;
            private DateTime ngayLapHD;
            private string maNV;
            private double tongTien;

            public QuanLyHĐ()
            {             
                this.maHD = null;
                this.ngayLapHD = DateTime.Now;
                this.maNV = null;
                this.tongTien = 0;
            }
            public QuanLyHĐ( string maHD, DateTime ngayLapHD, string maNV, double tongTien)
            {
                this.maHD = maHD;
                this.ngayLapHD = ngayLapHD;
                this.maNV = maNV;
                this.tongTien = tongTien;
            }
         
            public string MaHD
            {
                get { return this.maHD; }
                set { this.maHD = value; }
            }
            public DateTime NgayLapHoaDon
            {
                get { return this.ngayLapHD; }
                set { this.ngayLapHD = value; }
            }
            public string MaNV
            {
            get { return this.maNV; }
            set { this.maNV = value; }
            }
            public double TongTien
            {
                get { return this.tongTien; }
                set { this.tongTien = value; }
            }
        }
    }

