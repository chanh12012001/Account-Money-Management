using BUS;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace QuanLySoTietKiem
{
    
    public partial class FormBaoCaoThang : Form
    {
        public FormBaoCaoThang()
        {
            InitializeComponent();
            LoadLoaiTietKiem();
            LoadBaoCaoThang();
        }

        List<BaoCaoThang_DTO> danhSach;
        private void LoadBaoCaoThang()
        {
            danhSach = BaoCaoThang_BUS.LoadBaoCaoThang();

            if (danhSach == null)
                //return;
                danhSach = new List<BaoCaoThang_DTO>();

            dgvBaoCaoThang.DataSource = danhSach;

            cbThang.Text = DateTime.Today.Month.ToString();
            cbNam.Text = DateTime.Today.Year.ToString();

            dgvBaoCaoThang.Columns["MaChiTietThang"].Visible = false;
            dgvBaoCaoThang.Columns["MaLoaiTietKiem"].Visible = false;
            dgvBaoCaoThang.Columns["MaSoTietKiem"].Visible = false;

            dgvBaoCaoThang.Columns["NgayMoSo"].HeaderText = "Ngày";
            dgvBaoCaoThang.Columns["SoMo"].HeaderText = "Sổ Mở";
            dgvBaoCaoThang.Columns["SoDong"].HeaderText = "Sổ Đóng";
            dgvBaoCaoThang.Columns["ChenhLech"].HeaderText = "Chênh lệch";

        }


        private List<LoaiTietKiem_DTO> danhSachLoaiTietKiem;
        private void LoadLoaiTietKiem()
        {
            cbLoaiTietKiem.Items.Clear();
            danhSachLoaiTietKiem = LoaiTietKiem_DAO.LoadLoaiTietKiem();
            if (danhSachLoaiTietKiem != null)
            {
                for (int i = 0; i < danhSachLoaiTietKiem.Count; i++)
                {
                    string str = danhSachLoaiTietKiem[i].TenLoaiTietKiem;
                    cbLoaiTietKiem.Items.Add(str);
                }
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (cbThang.Text != "" && cbNam.Text != "" && cbLoaiTietKiem.Text != "")
            {
                string maLoaiTietKiem = SoTietKiem_DAO.LayMaLoaiTietKiem(cbLoaiTietKiem.Text);
                List<BaoCaoThang_DTO> ketQua = BaoCaoThang_BUS.TraCuuThang(cbThang.Text, cbNam.Text, maLoaiTietKiem);
                if (ketQua == null)
                {
                    /*return;*/
                    ketQua = new List<BaoCaoThang_DTO>();
                }
                dgvBaoCaoThang.DataSource = ketQua;

                int somo = 0;
                int sodong = 0;
                int chenhlech;
                for (int i = 0; i < dgvBaoCaoThang.Rows.Count; i++)
                {
                    somo += int.Parse(dgvBaoCaoThang.Rows[i].Cells["SoMo"].Value.ToString());
                    sodong += int.Parse(dgvBaoCaoThang.Rows[i].Cells["SoDong"].Value.ToString());
                }
                chenhlech = Math.Abs(somo - sodong);
                lbSoMo.Text = somo.ToString();
                lbSoDong.Text = sodong.ToString();
                lbChenhLech.Text = chenhlech.ToString();
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin");
            }
           
        }       


        int count = 0;
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            int temp = count++;
            try
            {
                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 5, 5, 2, 2);
                string timesTTF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");
                BaseFont arial = BaseFont.CreateFont(timesTTF, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f_24_Bold = new iTextSharp.text.Font(arial, 24, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font f_20_normal = new iTextSharp.text.Font(arial, 20, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font f_20_Bold = new iTextSharp.text.Font(arial, 20, iTextSharp.text.Font.BOLD);
                FileStream os = new FileStream("BAO CAO DOANH SO THANG " + cbThang.Text +" TRONG NAM "+cbNam.Text+" ("+temp+")"+".pdf", FileMode.Create);

                using (os)
                {
                    //Khi báo lỗi tick vào ô itextsharp, lỗi này đã được itextsharp exception xử lý rồi nên sẽ không sao
                    //Sau khi skip exception của lỗi này thì chương trình vẫn chạy bình thường
                    //Lỗi này không xuất hiện khi chạy ứng dụng cài bằng file setup
                    PdfWriter wri = PdfWriter.GetInstance(doc, os);
                    doc.Open();

                    PdfPTable table1 = new PdfPTable(1);
                    float[] width = new float[] { 20f, 40f };

                    PdfPCell cell1 = new PdfPCell(new Phrase("\nBÁO CÁO DOANH SỐ THÁNG", f_24_Bold));
                    PdfPCell cell2 = new PdfPCell(new Phrase("\nTHÁNG: " + cbThang.Text.ToString()+"/"+cbNam.Text.ToString(), f_20_normal));
                    PdfPCell cell3 = new PdfPCell(new Phrase("-----------------------------------------", f_20_normal));
                    PdfPCell cell4 = new PdfPCell(new Phrase("Tổng sổ mở: " + lbSoMo.Text.ToString() + "                 Tổng sổ đóng: " + lbSoDong.Text.ToString(), f_20_normal));
                    PdfPCell cell5 = new PdfPCell(new Phrase("Chênh lệch: " + lbChenhLech.Text.ToString(), f_20_normal));

                    cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    cell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED_ALL;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;

                    cell1.VerticalAlignment = Element.ALIGN_TOP;
                    cell2.VerticalAlignment = Element.ALIGN_CENTER;


                    table1.WidthPercentage = 80;
                    table1.HorizontalAlignment = Element.ALIGN_CENTER;

                    table1.AddCell(cell1);
                    table1.AddCell(cell2);
                    table1.AddCell(cell3);
                    table1.AddCell(cell4);
                    table1.AddCell(cell5);
                    table1.SpacingAfter = 5;
                    table1.SpacingBefore = 5;

                    doc.Add(table1);

                    PdfPTable pdfTable = new PdfPTable(4);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 80;
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.BorderWidth = 1;
                    pdfTable.SpacingAfter = 5;
                    pdfTable.SpacingBefore = 5;
                    foreach (DataGridViewColumn column in dgvBaoCaoThang.Columns)
                    {
                        if(column.HeaderText == "Ngày" || column.HeaderText == "Sổ Mở" || column.HeaderText == "Sổ Đóng"|| column.HeaderText == "Chênh lệch")
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, f_20_Bold));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell);
                        }
                        
                    }
                    for (int i = 0; i < dgvBaoCaoThang.Rows.Count; i++)
                    {

                        for (int j = 0; j < dgvBaoCaoThang.Columns.Count; j++)
                        {
                            if (dgvBaoCaoThang.Rows[i].Cells[j].Value == null)
                            {
                                dgvBaoCaoThang.Rows[i].Cells[j].Value = " ";
                            }
                            if (j==1||j==4||j==5||j==6)
                            {
                                PdfPCell c = new PdfPCell(new Phrase(dgvBaoCaoThang.Rows[i].Cells[j].Value.ToString(), f_20_normal));
                                c.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfTable.AddCell(c);
                            }
                           
                        }
                    }
                    doc.Add(pdfTable);

                    doc.Close();
                    System.Diagnostics.Process.Start("BAO CAO DOANH SO THANG " + cbThang.Text + " TRONG NAM " + cbNam.Text + " (" + temp + ")" + ".pdf");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
