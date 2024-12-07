using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLyLoaiNuoc
    {
            private string maLoai;
            private string tenLoai;

            public QuanLyLoaiNuoc()
            {
                this.maLoai = null;
                this.tenLoai = null;
            }
            public QuanLyLoaiNuoc(string maLoai, string tenLoai)
            {
                this.maLoai = maLoai;
                this.tenLoai = tenLoai;
            }
            public string MaLoai
            {
                get { return this.maLoai; }
                set { this.maLoai = value; }
            }
            public string TenLoai
            {
                get { return this.tenLoai; }
                set { this.tenLoai = value; }
            }

        }
    }
