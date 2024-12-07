using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCaPhe
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void quảnLýLoạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLN f = new FormLN();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSP f = new FormSP();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHĐ f = new FormHĐ();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        private void quảnLýThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTK f = new FormTK();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        private void quanLyNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNV f = new FormNV();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        
    }
}
