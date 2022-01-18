using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Windows;
using WMS.Communication;

namespace WMS
{
    public partial class WMS : Form
    {
        private VCSClient _vcsClient;
        private MaterialsStockWindow _materialsStockWindow;
        private MaterialsManagementWindow _materialsManagementWindow;
        public WMS()
        {
            InitializeComponent();            
            _materialsStockWindow = new MaterialsStockWindow();
            _materialsManagementWindow = new MaterialsManagementWindow();
            _vcsClient = new VCSClient();
        }

        #region Menu Strip Buttons
        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox_MapViewer.Image = Properties.Resources.demo_map;
        }

        private void materialsStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _materialsStockWindow.Show();
        }
        private void materialsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _materialsManagementWindow.Show();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_vcsClient.Connect("127.0.0.1"))
            {
                MessageBox.Show("Connected!");
            }
        }

        #endregion


    }
}
