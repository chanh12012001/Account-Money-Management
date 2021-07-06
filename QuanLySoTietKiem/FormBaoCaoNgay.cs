using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySoTietKiem
{
    public partial class FormBaoCaoNgay : Form
    {
        public FormBaoCaoNgay()
        {
            InitializeComponent();
            LoadBaoCaoNgay();
        }

       List<BaoCaoNgay_DTO> danhSach;
        private void LoadBaoCaoNgay()
        {
            danhSach = BaoCaoNgay_BUS.LoadBaoCaoNgay();
            dgvBaoCaoNgay.DataSource = danhSach;

            dgvBaoCaoNgay.Columns["MaLoaiTietKiem"].HeaderText = "Loại Tiết Kiệm";
            dgvBaoCaoNgay.Columns["TongThu"].HeaderText = "Tổng Thu";
            dgvBaoCaoNgay.Columns["TongChi"].HeaderText = "Tổng Chi";
            dgvBaoCaoNgay.Columns["ChenhLech"].HeaderText = "Chênh lệch";

            dgvBaoCaoNgay.Columns["MaChiTietNgay"].Visible = false;
            dgvBaoCaoNgay.Columns["NgayLap"].Visible = false;

        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            List<BaoCaoNgay_DTO> ketQua = BaoCaoNgay_BUS.TraCuuNgay(DateTime.Parse(dtpBaoCaoNgay.Text));
            if (ketQua == null)
                return;
            dgvBaoCaoNgay.DataSource = ketQua;

            int tongthu = 0;
            int tongchi = 0;
            int chenhlech;
            for (int i = 0; i < dgvBaoCaoNgay.Rows.Count; i++)
            {
                tongthu = tongthu + int.Parse(dgvBaoCaoNgay.Rows[i].Cells["TongThu"].Value.ToString());
                tongchi = tongchi + int.Parse(dgvBaoCaoNgay.Rows[i].Cells["TongChi"].Value.ToString());
            }
            chenhlech = Math.Abs(tongthu - tongchi);
            lbTongThu.Text = tongthu.ToString();
            lbTongChi.Text = tongchi.ToString();
            lbChenhLech.Text = chenhlech.ToString();
        }
    }
}
