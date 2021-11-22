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
using System.Reflection;
using System.IO;

namespace A_Star_Demo
{
    public partial class AStarDemo : Form
    {
        private Map _currentMap;
        private MapDrawer _mapDrawer;
        private bool _isEditingType = false;
        private bool _isEditingEdge = false;
        private MapNode _selectedNode;
        private MapNode _selectedEdgeNode1;
        private MapNode _selectedEdgeNode2;

        public AStarDemo()
        {
            InitializeComponent();
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));
            comboBox_types.SelectedIndex = 0;
            comboBox_edgeConstraints.DataSource = Enum.GetValues(typeof(Map.EdgeConstraints));
            comboBox_types.SelectedIndex = 0;
        }

        #region Menu
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewMapDialog dialog = new NewMapDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentMap = new Map(dialog.MapZoneID, dialog.MapWidth, dialog.MapHeight);
                    _mapDrawer = new MapDrawer(ref _currentMap, pictureBox_mapViewer.Width, pictureBox_mapViewer.Height);
                    saveMapToolStripMenuItem.Enabled = true;
                    UpdateMapInfo();
                }
            }
        }
        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                dialog.RestoreDirectory = true;
                dialog.Filter = "Map files (*.map)|*.map";
                dialog.Title = "Save map file";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentMap.SaveMap(dialog.FileName);
                }
            }
        }
        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                dialog.RestoreDirectory = true;
                dialog.Filter = "Map files (*.map)|*.map";
                dialog.Title = "Open map file";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentMap = new Map(dialog.FileName);
                    _mapDrawer = new MapDrawer(ref _currentMap, pictureBox_mapViewer.Width, pictureBox_mapViewer.Height);
                    saveMapToolStripMenuItem.Enabled = true;
                    UpdateMapInfo();
                }
            }
        }
        #endregion


        #region UI Control Events
        private void comboBox_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_types.BackColor = MapDrawer.NodeTypeColorDict[(MapNode.Types)comboBox_types.SelectedItem];
        }
        private void button_startEditingNode_Click(object sender, EventArgs e)
        {
            if (_isEditingType)
            {
                _isEditingType = false;
                button_startEditingNode.Text = "Start Editing";
                button_startEditingEdge.Enabled = true;
            }
            else
            {
                _isEditingType = true;
                button_startEditingNode.Text = "Stop";
                button_startEditingEdge.Enabled = false;
            }
        }
        private void button_startEditingEdge_Click(object sender, EventArgs e)
        {
            if (_isEditingEdge)
            {
                _isEditingEdge = false;
                button_startEditingEdge.Text = "Start Editing";
                button_startEditingNode.Enabled = true;
                _selectedEdgeNode1 = null;
                _selectedEdgeNode2 = null;
            }
            else
            {
                _isEditingEdge = true;
                button_startEditingEdge.Text = "Stop Editing";
                button_startEditingNode.Enabled = false;
            }
        }


        #endregion

        #region PictureBox events


        private void pictureBox_mapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            NodeSelect(e.Location, e.Button);
        }
        private void pictureBox_mapViewer_MouseMove(object sender, MouseEventArgs e)
        {
            NodeSelect(e.Location, e.Button);
        }
        #endregion

        private void UpdateMapInfo()
        {
            textBox_mapSN.Text = _currentMap.SerialNumber;
            textBox_mapZone.Text = _currentMap.ZoneID.ToString();
            textBox_mapDIM.Text = $"{_currentMap.Width} x {_currentMap.Height}";
        }

        private void NodeSelect(Point mousePosition, MouseButtons mouseButton)
        {
            if (mouseButton == MouseButtons.None) return;
            _selectedNode = _mapDrawer?.GetNodeByPosition(mousePosition.X, mousePosition.Y);
            if (_selectedNode != null)
            {
                switch (mouseButton)
                {
                    case MouseButtons.Left:
                        textBox_selectedNodeName.Text = _selectedNode.Name;
                        textBox_selectedNodeType.Text = _selectedNode.Type.ToString();
                        if (_isEditingType)
                        {
                            _selectedNode.Type = (MapNode.Types)comboBox_types.SelectedItem;
                        }
                        if (_isEditingEdge)
                        {
                            if (_selectedEdgeNode1 == null)
                            {
                                _selectedEdgeNode1 = _selectedNode;
                            }
                            else
                            {
                                if (_selectedNode.IsNeighbourNode(_selectedEdgeNode1))
                                {
                                    _selectedEdgeNode2 = _selectedNode;
                                }
                                else
                                {
                                    if (_selectedEdgeNode2 != null)
                                    {
                                        if (_selectedNode.IsNeighbourNode(_selectedEdgeNode2))
                                        {
                                            _selectedEdgeNode1 = _selectedEdgeNode2;
                                            _selectedEdgeNode2 = _selectedNode;
                                        }
                                        else
                                        {
                                            _selectedEdgeNode1 = _selectedNode;
                                            _selectedEdgeNode2 = null;
                                        }
                                    }
                                    else
                                    {
                                        _selectedEdgeNode1 = _selectedNode;
                                    }
                                }
                            }

                            if (_selectedEdgeNode1 != null && _selectedEdgeNode2 != null)
                            {
                                Debug.WriteLine($"{_selectedEdgeNode1.Name}-{_selectedEdgeNode2.Name}-{(byte)_currentMap.GetEdgeConstraintsByNodes(_selectedEdgeNode1, _selectedEdgeNode2)}");
                                _currentMap.SetEdgeConstraintsByNodes(_selectedEdgeNode1, _selectedEdgeNode2, (Map.EdgeConstraints)comboBox_edgeConstraints.SelectedItem);
                            }
                            textBox_edgeNode1.Text = _selectedEdgeNode1?.Name;
                            textBox_edgeNode2.Text = _selectedEdgeNode2?.Name;
                        }
                        break;
                    case MouseButtons.Right:
                        if (_isEditingType)
                        {
                            _selectedNode.Type = MapNode.Types.None;
                        }
                        break;
                    case MouseButtons.Middle:
                        break;
                    default:
                        break;
                }
            }
            else{
                textBox_selectedNodeName.Text = "";
                textBox_selectedNodeType.Text = "";
            }
        }


        private void timer_mapRefresh_Tick(object sender, EventArgs e)
        {
            if (_currentMap != null)
            {
                pictureBox_mapViewer.Image = _mapDrawer.GetMapPicture(_selectedNode, _selectedEdgeNode1, _selectedEdgeNode2, checkBox_showConstraints.Checked);
            }
        }

    }
}
