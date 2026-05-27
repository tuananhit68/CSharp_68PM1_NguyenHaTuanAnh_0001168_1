using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_đăng_nhập
{
    public partial class UC_QLSV : UserControl
    {
        QLSVDataContext db = new QLSVDataContext();
        public UC_QLSV()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UC_QLSV_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDSLH4CBX();
        }

        public void LoadData()
        {
            List<SinhVien> dSSV = db.SinhViens.ToList();
            dgvSinhVien.DataSource = dSSV;
        }
        public void LoadDSLH4CBX()
        {
            List<LopHoc> dSLH = db.LopHocs.ToList();
            cbo_MaLop.DataSource = dSLH;
            cbo_MaLop.DisplayMember = "TenLop";
            cbo_MaLop.ValueMember = "MaLop";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = new SinhVien();
            sinhVien.MaSV = txt_MaSV.Text;
            sinhVien.HoTen = txt_HoTen.Text;
            sinhVien.GioiTinh = cbo_GioiTinh.Text;
            sinhVien.NgaySinh = DateTime.Parse(dtp_NgaySinh.Text);
            sinhVien.MaLop = cbo_MaLop.SelectedValue.ToString();
            try
            {
                db.SinhViens.InsertOnSubmit(sinhVien);
                db.SubmitChanges();
                MessageBox.Show("Thêm sinh viên thành công");
                LoadData();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
