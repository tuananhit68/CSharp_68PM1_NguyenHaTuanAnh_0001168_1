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
    public partial class frm_DanhSachSinhVien : Form
    {
        private string maLopDuocChon;
        public frm_DanhSachSinhVien(string MaLop)
        {
            InitializeComponent();
            this.maLopDuocChon = MaLop;

            this.Text = "Danh sách sinh viên theo lớp";
        }
        private void frm_DanhSachSinhVien_Load(object sender, EventArgs e)
        {
            // Hiển thị mã lớp lên tiêu đề Form cho người dùng biết
            this.Text = "Danh sách sinh viên - Lớp: " + maLopDuocChon;

            // Tiến hành tải dữ liệu sinh viên thuộc lớp này
            LoadSinhVienTheoLop();
        }
        private void LoadSinhVienTheoLop()
        {
            try
            {
                using (QLSVDataContext db = new QLSVDataContext())
                {
                    var dsSinhVien = db.SinhViens
                                       .Where(sv => sv.MaLop == maLopDuocChon)
                                       .ToList();

                    dgvSinhVien.DataSource = dsSinhVien;

                    if (dsSinhVien.Count == 0)
                    {
                        MessageBox.Show("Lớp này hiện tại chưa có sinh viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message);
            }
        }

        private void frm_DanhSachSinhVien_Load_1(object sender, EventArgs e)
        {
            this.Text = "Danh sách sinh viên - Lớp: " + maLopDuocChon;
            LoadSinhVienTheoLop();
        }
    }
}
