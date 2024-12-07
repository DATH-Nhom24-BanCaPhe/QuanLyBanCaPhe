using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLyThongKe
    {
            private string maThongKe;
            private DateTime ngayTK;
            private int soLuongHĐ;
            private string maNV;
            private double tongTienTK;
            public QuanLyThongKe()
            {
                this.maThongKe = null;
                this.ngayTK = DateTime.Now;
                this.soLuongHĐ = 0;
                this.maNV = null;
                this.tongTienTK = 0;
            }
            public QuanLyThongKe(string maThongKe, DateTime ngayTK, int soLuongHĐ, string maNV, double tongTienTK)
            {
                this.maThongKe = maThongKe;
                this.ngayTK = ngayTK;
                this.soLuongHĐ = soLuongHĐ;
                this.maNV = maNV;
                this.tongTienTK = tongTienTK;
            }
            public string MaThongKe
            {
                get { return this.maThongKe; }
                set { this.maThongKe = value; }
            }
            public DateTime NgayTK
            {
                get { return this.ngayTK; }
                set { this.ngayTK = value; }
            }
            public int SoLuongHĐ
            {
                get { return this.soLuongHĐ; }
                set { this.soLuongHĐ = value; }
            }
            public string MaNV
            {
                get { return this.maNV; }
                set { this.maNV = value; }
            }
            public double TongTienTK
            {
                get { return this.tongTienTK; }
                set { this.tongTienTK = value; }
            }
        }
    }

