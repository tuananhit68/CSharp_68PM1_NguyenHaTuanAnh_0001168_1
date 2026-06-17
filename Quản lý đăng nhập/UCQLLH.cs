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
    public partial class UC_QLLH : UserControl
    {
        QLSVDataContext db = new QLSVDataContext();
        int trangHienTai = 1;
        int soDongTrenTrang = 5; 
        int tongTrang = 1;
        public UC_QLLH()
        {
            InitializeComponent();
        }
        private void UC_QLLH_Load(object sender, EventArgs e)
        {
            LoadData(); 
        }
        private void UC_QLLH_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            int tongSoLop = db.LopHocs.Count();
            tongTrang = (int)Math.Ceiling((double)tongSoLop / soDongTrenTrang);
            if (tongTrang == 0) tongTrang = 1;
            if (trangHienTai > tongTrang) trangHienTai = tongTrang;
            if (trangHienTai < 1) trangHienTai = 1;
            var dSLH = db.LopHocs
                         .OrderBy(lh => lh.MaID)
                         .Skip((trangHienTai - 1) * soDongTrenTrang)
                         .Take(soDongTrenTrang)
                         .Select(lh => new {
                             MaID = lh.MaID,
                             MaLop = lh.MaLop,
                             TenLop = lh.TenLop,
                             GhiChu = lh.GhiChu
                         }).ToList();

           dgvLopHoc.DataSource = dSLH;
            CapNhatTrangThaiNut();
        }
        private void CapNhatTrangThaiNut()
        {
            btn_Dau.Enabled = trangHienTai > 1;
            btn_Truoc.Enabled = trangHienTai > 1;
            btn_Sau.Enabled = trangHienTai < tongTrang;
            btn_Cuoi.Enabled = trangHienTai < tongTrang;
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void A(object sender, EventArgs e)
        {

        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            LopHoc lh = new LopHoc();
            lh.MaID = 0;
            lh.MaLop = txt_MaLop.Text;
            lh.TenLop = textBox3.Text;
            lh.GhiChu = txt_GhiChu.Text;

            try
            {
                db.LopHocs.InsertOnSubmit(lh);
                db.SubmitChanges();
                MessageBox.Show("Thêm lớp thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLopHoc.Rows[e.RowIndex];
                txt_MaID.Text = row.Cells["MaID"].Value?.ToString();
                txt_MaLop.Text = row.Cells["MaLop"].Value?.ToString();
                textBox3.Text = row.Cells["TenLop"].Value?.ToString();
                txt_GhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(txt_MaID.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp từ danh sách trước khi xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    
                    int idCanXoa = int.Parse(txt_MaID.Text.Trim());
                    LopHoc lh = db.LopHocs.FirstOrDefault(x => x.MaID == idCanXoa);

                    if (lh != null)
                    {
                        db.LopHocs.DeleteOnSubmit(lh);
                        db.SubmitChanges();
                        MessageBox.Show("Xóa lớp thành công!");
                        txt_MaID.Clear();
                        txt_MaLop.Clear();
                        textBox3.Clear();
                        txt_GhiChu.Clear();

                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp học này để xóa!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: Không thể xóa lớp này vì đang có sinh viên học!\nChi tiết: " + ex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
   
            if (string.IsNullOrEmpty(txt_MaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp học từ danh sách trước khi xem sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLop = txt_MaLop.Text.Trim();
            frm_DanhSachSinhVien frmModal = new frm_DanhSachSinhVien(maLop);
            frmModal.ShowDialog();
        }

        private void btn_Sua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_MaID.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp từ danh sách trước khi sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idCanSua = int.Parse(txt_MaID.Text.Trim());
                LopHoc lh = db.LopHocs.FirstOrDefault(x => x.MaID == idCanSua);

                if (lh != null)
                {
                    lh.MaLop = txt_MaLop.Text.Trim();
                    lh.TenLop = textBox3.Text.Trim();
                    lh.GhiChu = txt_GhiChu.Text.Trim();
                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật thông tin lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_MaID.Clear();
                    txt_MaLop.Clear();
                    textBox3.Clear();
                    txt_GhiChu.Clear();

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lớp học này để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (trangHienTai < tongTrang)
            {
                trangHienTai++;
                LoadData();
            }
        }

        private void btn_Cuoi_Click(object sender, EventArgs e)
        {
            trangHienTai = tongTrang;
            LoadData();
        }

        private void Xemds_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_MaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn một lớp học từ danh sách trước khi xem sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLop = txt_MaLop.Text.Trim();
            frm_DanhSachSinhVien frmModal = new frm_DanhSachSinhVien(maLop);
            frmModal.ShowDialog();
        }
    }
}
