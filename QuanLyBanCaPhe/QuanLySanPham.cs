using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLySanPham
    {
        private string maLoai;
        private string maNuoc;
        private string tenNuoc;
        private double gia;
        public QuanLySanPham()
            {
                this.maLoai = null;
                this.maNuoc = null;
                this.tenNuoc = null;
                this.gia = 0;

            }
            public QuanLySanPham(string maLoai, string maNuoc, string tenNuoc, double gia)
            {
                this.maLoai = maLoai;
                this.maNuoc = maNuoc;
                this.tenNuoc = tenNuoc;
                this.gia = gia;

            }
            public string MaLoai
            {
                get { return this.maLoai; }
                set { this.maLoai = value; }

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

            public double Gia
            {
                get { return this.gia; }
                set { this.gia = value; }
            }

        }
    }


