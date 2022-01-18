using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMS.Windows
{
    public partial class MaterialsManagementWindow : Form
    {
        public MaterialsManagementWindow()
        {
            InitializeComponent();
        }

        private void MaterialsManagementWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
