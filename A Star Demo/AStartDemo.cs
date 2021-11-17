using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using A_Star_Demo.Dialogs;


namespace A_Star_Demo
{
    public partial class AStartDemo : Form
    {
        private Map _currentMap;
        private MapDrawer _mapDrawer;
        private bool _isEditingType = false;

        public AStartDemo()
        {
            InitializeComponent();
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));
            comboBox_types.SelectedIndex = 0;
        }

        #region Menu
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewMapDialog dialog = new NewMapDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Debug.WriteLine($"{dialog.MapZoneID}-{dialog.MapWidth}-{dialog.MapHeight}");
                    _currentMap = new Map(dialog.MapZoneID, dialog.MapWidth, dialog.MapHeight);
                    _mapDrawer = new MapDrawer(ref _currentMap, pictureBox_mapViewer.Width, pictureBox_mapViewer.Height);
                    saveMapToolStripMenuItem.Enabled = true;
                    UpdateMapInfo();
                }
            }
        }
        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void loadExistingMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void pictureBox_mapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            //var selectedNode = _mapDrawer?.GetNodeByPosition(e.X, e.Y);
            //if (selectedNode != null)
            //{
            //    textBox_selectedNodeName.Text = selectedNode.Name;
            //    textBox_selectedNodeType.Text = selectedNode.Type.ToString();
            //}
        }

        private void UpdateMapInfo()
        {
            textBox_mapSN.Text = _currentMap.SerialNumber;
            textBox_mapZone.Text = _currentMap.ZoneID.ToString();
            textBox_mapDIM.Text = $"{_currentMap.Width} x {_currentMap.Height}";
        }

        private void button_editType_Click(object sender, EventArgs e)
        {
            if (_isEditingType)
            {
                _isEditingType = false;
                button_editType.Text = "Start Editing";
            }
            else
            {
                _isEditingType = true;
                button_editType.Text = "Stop";
            }
        }

        private void comboBox_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_types.BackColor = MapDrawer.NodeTypeColorDict[(MapNode.Types)comboBox_types.SelectedItem];
        }

        private void timer_mapRefresh_Tick(object sender, EventArgs e)
        {
            if (_currentMap != null)
            {
                pictureBox_mapViewer.Image = _mapDrawer.GetMapPicture();
            }
        }

        private void pictureBox_mapViewer_MouseMove(object sender, MouseEventArgs e)
        {
            var selectedNode = _mapDrawer?.GetNodeByPosition(e.X, e.Y);
            if (selectedNode != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        textBox_selectedNodeName.Text = selectedNode.Name;
                        textBox_selectedNodeType.Text = selectedNode.Type.ToString();
                        if (_isEditingType)
                        {
                            selectedNode.Type = (MapNode.Types)comboBox_types.SelectedItem;
                        }
                        break;
                    case MouseButtons.Right:
                        if (_isEditingType)
                        {
                            selectedNode.Type = MapNode.Types.None;
                        }
                        break;
                    case MouseButtons.Middle:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
