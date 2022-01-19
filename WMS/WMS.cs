using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WMS.Windows;
using WMS.Dialogs;
using WMS.Communication;
using VCS.Maps;
using VCS.Models;

namespace WMS
{
    public partial class WMS : Form
    {
        private VCSClient _vcsClient;
        private Map _currentMap;
        private MapDrawerSlim _mapDrawer;
        private List<Rack> _rackList;
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
            using (var dialog = new ConnectionDialog())
            {
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (_vcsClient.Connect(dialog.IPA, dialog.Port))
                    {
                        new Thread(() => { MessageBox.Show("Connected!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information); }).Start();
                        _currentMap = _vcsClient.GetMapData();
                        _mapDrawer = new MapDrawerSlim(_currentMap, pictureBox_MapViewer.Size);
                        timer_mapRefresh.Interval = 1000 / 30;  // 30 FPS
                        timer_mapRefresh.Enabled = true;
                        toolStripStatusLabel_ConnectionState.Text = "已連線";
                        toolStripStatusLabel_ConnectionState.ForeColor = Color.Green;
                        toolStripStatusLabel_ServerIP.Text = dialog.IPA + ":" + dialog.Port.ToString();
                    }
                    else
                    {
                        new Thread(() => { MessageBox.Show("Cannot connect to server!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error); }).Start();
                        toolStripStatusLabel_ConnectionState.Text = "連現失敗";
                        toolStripStatusLabel_ConnectionState.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _vcsClient.Disconnect();
        }
        #endregion

        private void timer_mapRefresh_Tick(object sender, EventArgs e)
        {
            _rackList = _vcsClient.GetRackInfo();
            _mapDrawer.DrawNewMap();
            _mapDrawer.DrawRacks(_rackList);
            var mapBMP = _mapDrawer.GetMapPicture();
            pictureBox_MapViewer.Image = mapBMP;
        }

        private void pictureBox_MapViewer_ClientSizeChanged(object sender, EventArgs e)
        {
            _mapDrawer.DrawSize = pictureBox_MapViewer.Size;
        }
    }
}
