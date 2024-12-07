using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLyCTHĐ
    {
            private string maHD;
            private string maNuoc;
            private string tenNuoc;
            private int soLuong;
            private double gia;
            private double thanhTien; 

            public QuanLyCTHĐ()
            {
                this.maHD = null;
                this.maNuoc = null;
                this.tenNuoc = null;
                this.soLuong = 0;
                this.gia = 0;
                this.thanhTien = 0;
           

            }
            public QuanLyCTHĐ(string maHD, string maNuoc, string tenNuoc, int soLuong, double gia, double thanhTien)
            {
                this.maHD = maHD;
                this.maNuoc = maNuoc;
                this.tenNuoc = tenNuoc;
                this.soLuong = soLuong;
                this.gia = gia;
                this.thanhTien = thanhTien;
           
            }
            public string MaHD
            {
                get { return this.maHD; }
                set { this.maHD = value; }
            }
            public string MaNuoc
            {
                get { return this.maNuoc; }
                set { this.maNuoc = value; }
            }
            public string TenNuoc
            {
                get { return this.tenNuoc; }
                set { this.tenNuoc = value; }
            }
            public int SoLuong
            {
                get { return this.soLuong; }
                set { this.soLuong = value; }
            }
            public double Gia
            {
                get { return this.gia; }
                set { this.gia = value; }
            }
           public double ThanhTien
        {
            get { return this.thanhTien; }
            set { this.thanhTien = value; }
        }

        }
    }
