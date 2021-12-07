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
using A_Star_Demo.AGVs;

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
        private List<MapNode> _path;
        private Point _prevMouseLocation;
        private AGVHandler _agvHandler;

        public AStarDemo()
        {
            InitializeComponent();
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));
            comboBox_types.SelectedIndex = 0;
            comboBox_passingRestrictions.DataSource = Enum.GetValues(typeof(MapEdge.PassingRestrictions));
            comboBox_types.SelectedIndex = 0;
            _agvHandler = new AGVHandler();
        }

        #region Menu
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewMapDialog dialog = new NewMapDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadNewMap(new Map(dialog.MapZoneID, dialog.MapWidth, dialog.MapHeight));
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
                    LoadNewMap(new Map(dialog.FileName));   
                }
            }
        }
        private void addAGVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedNode == null)
            {
                MessageBox.Show("Please select a node first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_selectedNode.Type == MapNode.Types.Wall)
            {
                MessageBox.Show("You cannot place an AGV over here!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(_agvHandler.AGVList.FindAll(agv => agv.Node == _selectedNode).Count > 0)
            {
                MessageBox.Show("You cannot place an AGV over here!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var selectedAGV = _agvHandler.AddSimulatedAGV(_selectedNode);
            textBox_selectedAGVName.Text = selectedAGV.Name;
            textBox_selectedAGVNode.Text = selectedAGV.Node.Name;
            textBox_selectedAGVStatus.Text = selectedAGV.Status.ToString();
            textBox_selectedAGVHeading.Text = selectedAGV.Heading.ToString();
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
            var newLayer = new Map.ConstraintLayer(_currentMap, $"Layer {_currentMap.ConstraintLayers.Count}");
            _currentMap.ConstraintLayers.Add(newLayer);
            comboBox_constraintLayers.Items.Clear();
            foreach (var layer in _currentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
            }
            comboBox_planningLayer.Items.Clear();
            foreach (var layer in _currentMap.ConstraintLayers)
            {
                comboBox_planningLayer.Items.Add(layer.Name);
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

        private void comboBox_constraintLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_planningLayer.SelectedIndex = comboBox_constraintLayers.SelectedIndex;
        }

        private void comboBox_planningLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_constraintLayers.SelectedIndex = comboBox_planningLayer.SelectedIndex;
        }

        private void button_ClearPath_Click(object sender, EventArgs e)
        {
            textBox_planningPath.Clear();
            _path?.Clear();
            _path = null;
        }
        #endregion

        #region PictureBox events


        private void pictureBox_mapViewer_MouseDown(object sender, MouseEventArgs e)
        {
            NodeSelect(e.Location, e.Button);
            if (e.Button == MouseButtons.Middle)
            {
                _prevMouseLocation = e.Location;
                Cursor.Current = Cursors.Hand;
            }
        }
        private void pictureBox_mapViewer_MouseMove(object sender, MouseEventArgs e)
        {
            NodeSelect(e.Location, e.Button);
            if (e.Button == MouseButtons.Middle && _mapDrawer != null)
            {
                int xDiff = e.Location.X - _prevMouseLocation.X;
                int yDiff = e.Location.Y - _prevMouseLocation.Y;

                if (xDiff > _mapDrawer.CellSize)
                {
                    _mapDrawer.OffsetX += xDiff / _mapDrawer.CellSize;
                    _prevMouseLocation = e.Location;
                }
                else if (-xDiff > _mapDrawer.CellSize)
                {
                    _mapDrawer.OffsetX += xDiff / _mapDrawer.CellSize;
                    _prevMouseLocation = e.Location;
                }
                else if (yDiff > _mapDrawer.CellSize)
                {
                    _mapDrawer.OffsetY += yDiff / _mapDrawer.CellSize;
                    _prevMouseLocation = e.Location;
                }
                else if (-yDiff > _mapDrawer.CellSize)
                {
                    _mapDrawer.OffsetY += yDiff / _mapDrawer.CellSize;
                    _prevMouseLocation = e.Location;
                }
            }
        }
        private void pictureBox_mapViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _prevMouseLocation = e.Location;
                Cursor.Current = Cursors.Default;
            }
        }
        private void pictureBox_mapViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_mapDrawer != null)
            {
                _mapDrawer.Scale += e.Delta / 1200.0f;
                Debug.WriteLine(_mapDrawer.Scale);
            }
        }

        private void pictureBox_mapViewer_SizeChanged(object sender, EventArgs e)
        {
            if (_mapDrawer != null) _mapDrawer.DrawSize = pictureBox_mapViewer.Size;
        }
        #endregion

        private void LoadNewMap(Map newMap)
        {
            _currentMap = newMap;
            _mapDrawer = new MapDrawer(ref _currentMap, pictureBox_mapViewer.Size);
            _pathPlanner = new AStarPlanner(_currentMap);
            saveMapToolStripMenuItem.Enabled = true;
            groupBox_nodeTypeEditor.Enabled = true;
            groupBox_edgeConstraintsEditor.Enabled = true;
            _selectedNode = null;
            _selectedEdgeNode1 = null;
            _selectedEdgeNode2 = null;
            textBox_selectedNodeName.Clear();
            textBox_selectedNodeType.Clear();
            textBox_edgeNode1.Clear();
            textBox_edgeNode2.Clear();
            
            textBox_mapSN.Text = _currentMap.SerialNumber;
            textBox_mapZone.Text = _currentMap.ZoneID.ToString();
            textBox_mapDIM.Text = $"{_currentMap.Width} x {_currentMap.Height}";
            comboBox_constraintLayers.Items.Clear();
            comboBox_planningLayer.Items.Clear();
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
            if (mouseButton != MouseButtons.Left && mouseButton != MouseButtons.Right) return;
            _selectedNode = _mapDrawer?.GetNodeByPosition(mousePosition.X, mousePosition.Y);
            if (_selectedNode != null)
            {
                switch (mouseButton)
                {
                    case MouseButtons.Left:
                        textBox_selectedNodeName.Text = _selectedNode.Name;
                        textBox_selectedNodeType.Text = _selectedNode.Type.ToString();
                        textBox_selectedNodeType.BackColor = MapDrawer.NodeTypeColorDict[_selectedNode.Type];
                        var selectedAGV = _agvHandler.AGVList.Find(agv => agv.Node == _selectedNode);
                        if (selectedAGV != null)
                        {
                            textBox_selectedAGVName.Text = selectedAGV.Name;
                            textBox_selectedAGVNode.Text = selectedAGV.Node.Name;
                            textBox_selectedAGVStatus.Text = selectedAGV.Status.ToString();
                            textBox_selectedAGVHeading.Text = selectedAGV.Heading.ToString();
                        }          
                        if (_isEditingType)
                        {
                            _selectedNode.Type = (MapNode.Types)comboBox_types.SelectedItem;
                            _selectedNode.DisallowTurningOnNode = checkBox_disallowTurning.Checked;
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
                            if (_selectedNode != _startNode)
                            {
                                _goalNode = _selectedNode;
                                button_startPlanning.Text = "Start Planning";
                                textBox_goalNode.Text = _goalNode.Name;
                                _planningFlag = 0;

                                _path = _pathPlanner.FindPath(_startNode, _goalNode, comboBox_planningLayer.SelectedIndex);
                                textBox_planningPath.Clear();
                                textBox_planningPath.AppendText($"Length = {(_path is null ? -1 : _path.Count)}\r\n");
                                if (_path != null)
                                    foreach (var node in _path)
                                    {
                                        textBox_planningPath.AppendText(node.Name + Environment.NewLine);
                                    }
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
                _mapDrawer.DrawNewMap();
                _mapDrawer.DrawSelectedNode(_selectedNode);
                _mapDrawer.DrawSelectedEdge(_selectedEdgeNode1, _selectedEdgeNode2);
                if (checkBox_showConstraints.Checked)
                {
                    _mapDrawer.DrawEdgeConstraints(comboBox_constraintLayers.SelectedIndex);
                }
                _mapDrawer.DrawSinglePath(_path);
                _mapDrawer.DrawAGVs(_agvHandler.AGVList);
                pictureBox_mapViewer.Image = _mapDrawer.GetMapPicture();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {            
        }

       
    }
}
