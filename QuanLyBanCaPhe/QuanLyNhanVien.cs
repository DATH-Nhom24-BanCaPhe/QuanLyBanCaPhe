using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCaPhe
{
    internal class QuanLyNhanVien
    {
        private string iD;
        private string hoTen;
        private DateTime ngaySinh;
        private string sĐT;
        private DateTime ngayVaoLam;
        private string viTriLamViec;
        private string gioiTinh;

        public QuanLyNhanVien()
        {
            this.iD = null;
            this.hoTen = null;
            this.ngaySinh = System.DateTime.Now;
            this.sĐT = null;
            this.ngayVaoLam = System.DateTime.Now;
            this.viTriLamViec = null;
            this.gioiTinh = null;

        }
        public QuanLyNhanVien(string iD, string hoTen, DateTime ngaySinh, string sĐT, DateTime ngayVaoLam, string viTriLamViec, string gioiTinh)
        {
            this.iD = iD;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.ngayVaoLam = ngayVaoLam;
            this.sĐT = sĐT;
            this.viTriLamViec = viTriLamViec;
            this.gioiTinh = gioiTinh;
        }
        public string ID
        {
            get { return this.iD; }
            set { this.iD = value; }
        }
        public string HoTen
        {
            get { return this.hoTen; }
            set { this.hoTen = value; }
        }
        public DateTime NgaySinh
        {
            get { return this.ngaySinh; }
            set { this.ngaySinh = value; }
        }
        public string SĐT
        {
            get { return this.sĐT; }
            set { this.sĐT = value; }
        }
        public DateTime NgayVaoLam
        {
            get { return this.ngayVaoLam; }
            set { this.ngayVaoLam = value; }
        }
        public string ViTriLamViec
        {
            get { return this.viTriLamViec; }
            set { this.viTriLamViec = value; }
        }
        public string GioiTinh
        {
            get { return this.gioiTinh; }
            set { this.gioiTinh = value; }
        }
    }
}
