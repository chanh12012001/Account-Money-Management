using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySoTietKiem
{
    public partial class FormLapSoTietKiem : Form
    {
        public FormLapSoTietKiem()
        {
            InitializeComponent();
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.btnThongTinKhachHang_Click(sender,e);
            formMain.Show();
        }
    }
}
