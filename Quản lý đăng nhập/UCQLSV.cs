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
        int trangHienTai = 1;
        int soDongTrenTrang = 5;
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
            var dSSV = db.SinhViens.Skip((trangHienTai - 1) * soDongTrenTrang).Take(soDongTrenTrang).ToList();
            dgvSinhVien.DataSource = dSSV;
        }
        public void LoadDSLH4CBX()
        {
            List<LopHoc> dSLH = db.LopHocs.ToList();
            cbo_MaLop.DataSource = dSLH;
            cbo_MaLop.DisplayMember = "TenLop";
            cbo_MaLop.ValueMember = "MaLop";
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            SinhVien sv = db.SinhViens.FirstOrDefault(s => s.MaSV == txt_MaSV.Text);

            if (sv != null)
            {
                sv.HoTen = txt_HoTen.Text;
                sv.GioiTinh = cbo_GioiTinh.Text;
                sv.NgaySinh = DateTime.Parse(dtp_NgaySinh.Text);
                sv.MaLop = cbo_MaLop.SelectedValue.ToString();

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Sửa thành công");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên để sửa!");
            }
        }
        
        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                txt_MaSV.Text = row.Cells["MaSV"].Value.ToString();
                txt_HoTen.Text = row.Cells["HoTen"].Value.ToString();
                cbo_GioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                dtp_NgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                cbo_MaLop.SelectedValue = row.Cells["MaLop"].Value.ToString();
            }
        }

        private void btn_xoa(object sender, EventArgs e)
        {
            SinhVien sv = db.SinhViens.FirstOrDefault(s => s.MaSV == txt_MaSV.Text);
            if (sv != null && MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    db.SinhViens.DeleteOnSubmit(sv);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa thành công");
                    LoadData();
                    txt_MaSV.Clear();
                    txt_HoTen.Clear();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (sv == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên để xóa!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btn_them_Click(object sender, EventArgs e)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Dau_Click(object sender, EventArgs e)
        {
            trangHienTai = 1;
            LoadData();
        }

        private void btn_Truoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                LoadData();
            }
        }
        private void btn_Sau_Click(object sender, EventArgs e)
        {
            int tongSoSinhVien = db.SinhViens.Count();
            int tongTrang = (int)Math.Ceiling((double)tongSoSinhVien / soDongTrenTrang);
            if (trangHienTai < tongTrang)
            {
                trangHienTai++;
                LoadData();
            }
        }
        private void btn_Cuoi_Click(object sender, EventArgs e)
        {
            int tongSoSinhVien = db.SinhViens.Count();
            trangHienTai = (int)Math.Ceiling((double)tongSoSinhVien / soDongTrenTrang);
            LoadData();
        }
    }
}
