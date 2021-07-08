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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
            //danhSach = BaoCaoNgay_BUS.LoadBaoCaoNgay();
            //if(danhSach == null)
            //{
            //    //return;
            //    danhSach = new List<BaoCaoNgay_DTO>();
            //}
            //dgvBaoCaoNgay.DataSource = danhSach;
            loadBaoCao();

            dgvBaoCaoNgay.Columns["MaLoaiTietKiem"].HeaderText = "Loại Tiết Kiệm";
            dgvBaoCaoNgay.Columns["TongThu"].HeaderText = "Tổng Thu";
            dgvBaoCaoNgay.Columns["TongChi"].HeaderText = "Tổng Chi";
            dgvBaoCaoNgay.Columns["ChenhLech"].HeaderText = "Chênh lệch";

            dgvBaoCaoNgay.Columns["MaChiTietNgay"].Visible = false;
            dgvBaoCaoNgay.Columns["NgayLap"].Visible = false;
            }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            loadBaoCao();
        }

        private void loadBaoCao()
        {
            List<BaoCaoNgay_DTO> ketQua = BaoCaoNgay_BUS.TraCuuNgay(DateTime.Parse(dtpBaoCaoNgay.Value.ToString()));
            if (ketQua == null)
                //return;
                ketQua = new List<BaoCaoNgay_DTO>();
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

        int count = 0;
        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            int temp = count++;
            string ngay = dtpBaoCaoNgay.Text.ToString();
            var s = ngay.Replace("/", ".");
            try
            {
                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 5, 5, 2, 2);
                string timesTTF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");
                BaseFont arial = BaseFont.CreateFont(timesTTF, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font f_24_Bold = new iTextSharp.text.Font(arial, 24, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font f_20_normal = new iTextSharp.text.Font(arial, 20, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font f_20_Bold = new iTextSharp.text.Font(arial, 20, iTextSharp.text.Font.BOLD);
                FileStream os = new FileStream("BAO CAO DOANH SO NGAY "+s + " (" + temp + ")" + ".pdf" , FileMode.Create);

                using (os)
                {
                    //Khi báo lỗi tick vào ô itextsharp, lỗi này đã được itextsharp exception xử lý rồi nên sẽ không sao
                    //Sau khi skip exception của lỗi này thì chương trình vẫn chạy bình thường
                    //Lỗi này không xuất hiện khi chạy ứng dụng cài bằng file setup
                    PdfWriter wri = PdfWriter.GetInstance(doc, os);
                    doc.Open();

                    PdfPTable table1 = new PdfPTable(1);
                    float[] width = new float[] { 20f, 40f };

                    PdfPCell cell1 = new PdfPCell(new Phrase("\nBÁO CÁO DOANH SỐ NGÀY", f_24_Bold));
                    PdfPCell cell2 = new PdfPCell(new Phrase("\nNGÀY: " + dtpBaoCaoNgay.Text.ToString(), f_20_normal));
                    PdfPCell cell3 = new PdfPCell(new Phrase("-----------------------------------------", f_20_normal));
                    PdfPCell cell4 = new PdfPCell(new Phrase("Tổng thu ngày: " + lbTongThu.Text.ToString()+ "                 Tổng chi ngày: " + lbTongChi.Text.ToString(), f_20_normal));
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

                    PdfPTable pdfTable = new PdfPTable(dgvBaoCaoNgay.Columns.Count-2);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 80;
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.BorderWidth = 1;
                    pdfTable.SpacingAfter = 5;
                    pdfTable.SpacingBefore = 5;
                    for(int i =1; i< dgvBaoCaoNgay.Columns.Count - 1 ; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dgvBaoCaoNgay.Columns[i].HeaderText, f_20_Bold));
                       
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfTable.AddCell(cell);
                    }
                    for (int i = 0; i < dgvBaoCaoNgay.Rows.Count; i++)
                    {

                        for (int j = 1; j < dgvBaoCaoNgay.Columns.Count -1 ; j++)
                        {
                            if (dgvBaoCaoNgay.Rows[i].Cells[j].Value == null)
                            {
                                dgvBaoCaoNgay.Rows[i].Cells[j].Value = " ";
                            }
                            PdfPCell c = new PdfPCell(new Phrase(dgvBaoCaoNgay.Rows[i].Cells[j].Value.ToString(), f_20_normal));
                           
                            c.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(c);
                        }
                    }
                    doc.Add(pdfTable);

                    doc.Close();
                    System.Diagnostics.Process.Start("BAO CAO DOANH SO NGAY " + s + " (" + temp + ")" + ".pdf");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }    
        }
    }
}
