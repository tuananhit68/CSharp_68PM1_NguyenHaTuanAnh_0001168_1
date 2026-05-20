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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            UC_QLSV uCQLSV = new UC_QLSV();
            uCQLSV.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(uCQLSV);
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UC_QLSV uCQLSV = new UC_QLSV();
            uCQLSV.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(uCQLSV);
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UC_QLLH uCQLLH = new UC_QLLH();
            uCQLLH.Dock = DockStyle.Fill; 
            panel1.Controls.Clear(); 
            panel1.Controls.Add(uCQLLH);
        }
    }
}
