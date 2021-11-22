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
using System.Reflection;
using System.IO;
using A_Star_Demo.Dialogs;
using A_Star_Demo.Maps;
using A_Star_Demo.PathPlanning;

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
        private int _planningFlag = 0;
        private MapNode _startNode;
        private MapNode _goalNode;
        private AStarPlanner _pathPlanner;

        public AStarDemo()
        {
            InitializeComponent();
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));
            comboBox_types.SelectedIndex = 0;
            comboBox_passingRestrictions.DataSource = Enum.GetValues(typeof(MapEdge.PassingRestrictions));
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
                    _pathPlanner = new AStarPlanner(_currentMap);
                    saveMapToolStripMenuItem.Enabled = true;
                    groupBox_nodeTypeEditor.Enabled = true;
                    groupBox_edgeConstraintsEditor.Enabled = true;
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
                    _currentMap.SaveToFile(dialog.FileName);
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
                    _pathPlanner = new AStarPlanner(_currentMap);
                    saveMapToolStripMenuItem.Enabled = true;
                    groupBox_nodeTypeEditor.Enabled = true;
                    groupBox_edgeConstraintsEditor.Enabled = true;
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
        private void button_addLayer_Click(object sender, EventArgs e)
        {
            _currentMap.ConstraintLayers.Add(new Map.ConstraintLayer(_currentMap, $"Layer {_currentMap.ConstraintLayers.Count}"));
            comboBox_constraintLayers.Items.Clear();
            foreach (var layer in _currentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
            }
            comboBox_constraintLayers.SelectedIndex = comboBox_constraintLayers.Items.Count - 1;
        }

        private void button_deleteLayer_Click(object sender, EventArgs e)
        {
            if (comboBox_constraintLayers.SelectedIndex == 0) return;
            _currentMap.ConstraintLayers.RemoveAt(comboBox_constraintLayers.SelectedIndex);
            comboBox_constraintLayers.Items.Clear();
            foreach (var layer in _currentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
            }
            comboBox_constraintLayers.SelectedIndex = 0;
        }

        private void button_startPlanning_Click(object sender, EventArgs e)
        {
            if (_planningFlag == 0)
            {
                button_startPlanning.Enabled = false;
                button_startPlanning.Text = "Select start node";
                _planningFlag++;
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
            foreach (var layer in _currentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
                comboBox_planningLayer.Items.Add(layer.Name);
            }
            comboBox_constraintLayers.SelectedIndex = 0;
            comboBox_planningLayer.SelectedIndex = 0;
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
                                var selectedEdge = _currentMap.GetEdgeByNodes(comboBox_constraintLayers.SelectedIndex, _selectedEdgeNode1, _selectedEdgeNode2);
                                selectedEdge.PassingRestriction = (MapEdge.PassingRestrictions)comboBox_passingRestrictions.SelectedItem;
                                if (_selectedEdgeNode1.Location.X + _selectedEdgeNode1.Location.Y > _selectedEdgeNode2.Location.X + _selectedEdgeNode2.Location.Y)
                                {
                                    selectedEdge.InvertPassingRestrictionDirection();
                                }                                
                            }
                            textBox_edgeNode1.Text = _selectedEdgeNode1?.Name;
                            textBox_edgeNode2.Text = _selectedEdgeNode2?.Name;
                        }
                        if (_planningFlag == 1)
                        {
                            button_startPlanning.Text = "Select goal node";
                            _startNode = _selectedNode;
                            textBox_startNode.Text = _startNode.Name;
                            _planningFlag++;
                        }
                        else if (_planningFlag == 2)
                        {
                            if(_selectedNode != _startNode)
                            {
                                _goalNode = _selectedNode;
                                button_startPlanning.Text = "Start Planning";
                                textBox_goalNode.Text = _goalNode.Name;
                                _planningFlag = 0;
                                MessageBox.Show(_pathPlanner.FindPath(_startNode, _goalNode, comboBox_planningLayer.SelectedIndex).ToString());
                                button_startPlanning.Enabled = true;
                            }                            
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
            else
            {
                textBox_selectedNodeName.Text = "";
                textBox_selectedNodeType.Text = "";
            }
        }


        private void timer_mapRefresh_Tick(object sender, EventArgs e)
        {
            if (_currentMap != null)
            {
                pictureBox_mapViewer.Image = _mapDrawer.GetMapPicture(_selectedNode, _selectedEdgeNode1, _selectedEdgeNode2, checkBox_showConstraints.Checked, comboBox_constraintLayers.SelectedIndex);
            }
        }


    }
}
