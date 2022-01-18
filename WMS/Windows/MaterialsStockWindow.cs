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
    public partial class MaterialsStockWindow : Form
    {
        public MaterialsStockWindow()
        {
            InitializeComponent();
        }

        private void MaterialsStockWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        
    }
}
