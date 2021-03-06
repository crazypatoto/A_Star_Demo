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
using VCS.Dialogs;
using VCS.Windows;
using VCS.Maps;
using VCS.Models;
using VCS.PathPlanning;
using VCS.AGVs;
using VCS.Tasks;

namespace VCS
{
    public partial class MainWindow : Form
    {
        private MapDrawer _mapDrawer;
        private int _planningFlag = 0;
        private MapNode _startNode;
        private MapNode _goalNode;
        private List<MapNode> _path;
        private Point _prevMouseLocation;
        private AGV _selectedAGV;
        private Rack _selectedRack;
        private MapEditor _mapEditorForm;
        private MapNode _prevSelectedNode = null;
        public VCS VCS { get; private set; }
        public MapNode SelectedNode { get; private set; }
        public MapNode SelectedEdgeNode1 { get; set; }
        public MapNode SelectedEdgeNode2 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _mapEditorForm = new MapEditor();
            _mapEditorForm.Tag = this;
            comboBox_rackHeading.DataSource = Enum.GetValues(typeof(Rack.RackHeading));
            comboBox_rackHeading.SelectedIndex = 0;
        }

        #region Menu
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewMapDialog dialog = new NewMapDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    VCS?.Dispose();
                    VCS = new VCS(new Map(dialog.MapZoneID, dialog.MapWidth, dialog.MapHeight));
                    UpdateNewMapInfo();
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
                    VCS.CurrentMap.SaveToFile(dialog.FileName);
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
                    VCS?.Dispose();
                    VCS = new VCS(new Map(dialog.FileName));
                    UpdateNewMapInfo();
                }
            }
        }
        private void addAGVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null)
            {
                MessageBox.Show("Please select a node first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SelectedNode.Type == MapNode.Types.Wall)
            {
                MessageBox.Show("You cannot place an AGV here!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (VCS.AGVHandler.AGVList.FindAll(agv => agv.CurrentNode == SelectedNode).Count > 0)
            {
                MessageBox.Show("There is already an AGV here!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //_selectedAGV = VCSServer.AGVHandler.AddSimulatedAGV(SelectedNode);           
            VCS.AddNewSimulationAGVTemp(SelectedNode);
            _selectedAGV = VCS.AGVHandler.AGVList.Last();
        }

        private void deleteAGVToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void editMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapEditorForm.Show();
        }
        private void addRackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null)
            {
                MessageBox.Show("Please select a node first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SelectedNode.Type != MapNode.Types.Storage)
            {
                MessageBox.Show("Rack is only allowed to be placed on a stroge node", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (VCS.RackList.FindAll(rack => rack.CurrentNode == SelectedNode).Count > 0)
            {
                MessageBox.Show("There is already an Rack here!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            VCS.AddNewRackTemp(SelectedNode);
            _selectedRack = VCS.RackList.Last();
            //var newRackID = VCSServer.RackList.LastOrDefault()?.ID + 1 ?? 0;
            //var newRack = new Rack(newRackID, SelectedNode, Rack.RackHeading.Up);
            //VCSServer.RackList.Add(newRack);
            //_selectedRack = newRack;
        }

        private void deleteRackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRack == null) return;
            VCS.RackList.RemoveAll(rack => rack.CurrentNode == SelectedNode);
            _selectedRack = null;
        }
        private void addRacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 3; y <= 14; y++)
            {
                for (int x = 3; x <= 4; x++)
                {
                    VCS.AddNewRackTemp(VCS.CurrentMap.AllNodes[y, x]);
                }
            }
        }

        private void addSimulatedAGVsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = 16; x <= 26; x++)
            {
                VCS.AddNewSimulationAGVTemp(VCS.CurrentMap.AllNodes[16, x]);
            }
        }
        private void testGoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = VCS.AGVHandler.AGVList.Count - 1; i >= 0; i--)
            {
                var agv = VCS.AGVHandler.AGVList[i];
                var rack = VCS.RackList[i];
                agv.TaskHandler.NewAGVMoveTask(rack.HomeNode);
                agv.TaskHandler.NewRackPickUpTask(rack);
                agv.TaskHandler.NewAGVMoveTask(VCS.CurrentMap.AllNodes[11, 19]);
                agv.TaskHandler.NewRackRotateTask(rack.Heading + 90);
                agv.TaskHandler.NewRackDropOffTask();

                agv.TaskHandler.NewAGVMoveTask(VCS.CurrentMap.AllNodes[16, 16 + i]);

                agv.TaskHandler.NewAGVMoveTask(VCS.CurrentMap.AllNodes[11, 19]);
                agv.TaskHandler.NewRackPickUpTask(rack);
                agv.TaskHandler.NewAGVMoveTask(rack.HomeNode);
                agv.TaskHandler.NewRackDropOffTask();

                agv.TaskHandler.NewAGVMoveTask(VCS.CurrentMap.AllNodes[16, 16 + i]);
            }

        }

        private void deadlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<LinkedList<AGV>> waitList = new List<LinkedList<AGV>>();
            lock (AGVTask._agvTaskLock)
            {
                foreach (var agv in VCS.AGVHandler.AGVList)
                {
                    if (agv.TaskHandler.CurrentTask is AGVMoveTask)
                    {
                        if (agv.State == AGV.AGVStates.WaitingToMove)
                        {
                            var nextNode = (agv.TaskHandler.CurrentTask as AGVMoveTask).RemainingPath.FirstOrDefault();
                            if (nextNode != null)
                            {
                                var nextAGVNodeQueue = VCS.AGVNodeQueue[nextNode.Location.Y, nextNode.Location.X];
                                var nextAGV = nextAGVNodeQueue.First.Value;
                                if (agv == nextAGV)
                                {
                                    var realNextAGV = nextAGVNodeQueue.FirstOrDefault((targetAGV) =>
                                    {
                                        if (!(targetAGV.TaskHandler.TaskQueue.FirstOrDefault() is RackPickUpTask)) return false;
                                        if ((targetAGV.TaskHandler.TaskQueue.FirstOrDefault() as RackPickUpTask).TargetRack.CurrentNode != nextNode) return false;
                                        return true;
                                    });
                                    nextAGV = realNextAGV;
                                }
                                if (nextAGV != null)
                                {
                                    var newLinkedList = new LinkedList<AGV>();
                                    newLinkedList.AddLast(agv);
                                    newLinkedList.AddLast(nextAGV);
                                    waitList.Add(newLinkedList);
                                }
                            }
                        }
                    }
                }
                foreach (var agvQueue in waitList)
                {
                    if (agvQueue.Count > 1)
                    {
                        foreach (var agv in agvQueue)
                        {
                            Debug.Write($"{agv.Name} ->");
                        }
                        Debug.WriteLine("");
                    }
                }
            }
            List<AGVTreeNode> graph = new List<AGVTreeNode>();
            foreach (var list in waitList)
            {
                var currentNode = graph.Find(node => node.Value == list.First.Value);
                if (currentNode == null)
                {
                    currentNode = new AGVTreeNode(list.First.Value);
                    graph.Add(currentNode);
                }
                var targetNode = graph.Find(node => node.Value == list.Last.Value);
                if (targetNode == null)
                {
                    targetNode = new AGVTreeNode(list.Last.Value);
                    graph.Add(targetNode);
                }
                currentNode.Next = targetNode;
            }

            DFS(graph);
            Debug.WriteLine($"DeadLock detection took {sw.ElapsedMilliseconds}ms");
        }

        void DFS(List<AGVTreeNode> graph)
        {
            int time = 0;
            foreach (var node in graph)
            {
                node.Color = 0;
            }

            foreach (var node in graph)
            {
                if (node.Color == 0)
                {
                    DFSVisit(node, ref time);
                }
            }
        }

        void DFSVisit(AGVTreeNode node, ref int time)
        {
            time++;
            node.Color = 1;
            node.StartTime = time;
            var childNode = node.Next;
            if (childNode != null)
            {
                if (childNode.Color == 0)
                {
                    DFSVisit(childNode, ref time);
                }
                else if (childNode.Color == 1)
                {
                    Debug.WriteLine($"Back Edge Found! {node.Value.ID} -> {childNode.Value.ID}");
                }
            }
            node.Color = 2;
            node.FinishTime = time;
        }


        class AGVTreeNode
        {
            public AGV Value { get; }
            public int Color { get; set; }  // white = 0, gray = 1, black = 2
            public int StartTime { get; set; }
            public int FinishTime { get; set; }
            public AGVTreeNode Next { get; set; }

            public AGVTreeNode(AGV value)
            {
                this.Value = value;
                this.Color = 0;
                this.StartTime = 0;
                this.FinishTime = 0;
            }
        }

        private void aGVTaskViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AGVTaskViewer taskViewer = new AGVTaskViewer(VCS);
            taskViewer.Show();
        }
        #endregion

        #region UI Control Events      

        private void button_startPlanning_Click(object sender, EventArgs e)
        {
            if (_planningFlag == 0)
            {
                button_startPlanning.Enabled = false;
                button_startPlanning.Text = "Select start node";
                _planningFlag++;
            }
        }

        private void comboBox_planningLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mapEditorForm.comboBox_constraintLayers.SelectedIndex = comboBox_planningLayer.SelectedIndex;
        }

        private void button_ClearPath_Click(object sender, EventArgs e)
        {
            textBox_planningPath.Clear();
            _path?.Clear();
            _path = null;
        }

        private void button_Go_Click(object sender, EventArgs e)
        {
            if (_selectedAGV != null && SelectedNode != null)
            {
                _selectedAGV.TaskHandler.NewAGVMoveTask(SelectedNode);
            }
        }
        private void button_pickUpRack_Click(object sender, EventArgs e)
        {
            if (_selectedAGV == null) return;
            //_selectedAGV.PickUpRack(_selectedRack);
            _selectedAGV.TaskHandler.NewRackPickUpTask(_selectedRack);
        }

        private void button_dropOffRack_Click(object sender, EventArgs e)
        {
            if (_selectedAGV == null) return;
            _selectedAGV.TaskHandler.NewRackDropOffTask();
            //_selectedAGV.DropOffRack();
        }

        private void button_rotateRackTemp_Click(object sender, EventArgs e)
        {
            if (_selectedAGV == null) return;
            //_selectedAGV.RotateRack((Rack.RackHeading)comboBox_rackHeading.SelectedItem);
            _selectedAGV.TaskHandler.NewRackRotateTask((Rack.RackHeading)comboBox_rackHeading.SelectedItem);
        }

        private void button_sendRackTo_Click(object sender, EventArgs e)
        {
            if (_selectedRack == null) return;
            if (this.SelectedNode == null) return;
            VCS.SendRackTo(this.SelectedNode, _selectedRack, (Rack.RackHeading)comboBox_rackHeading.SelectedItem);
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
            NodeSelect(e.Location, e.Button, true);
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
            }
        }

        private void pictureBox_mapViewer_SizeChanged(object sender, EventArgs e)
        {
            if (_mapDrawer != null) _mapDrawer.DrawSize = pictureBox_mapViewer.Size;
        }

        private void comboBox_selectedNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedNode = comboBox_selectedNode.SelectedItem as MapNode;
        }
        #endregion

        private void UpdateNewMapInfo()
        {
            _mapDrawer = new MapDrawer(VCS, pictureBox_mapViewer.Size);
            timer_mapRefresh.Enabled = true;
            saveMapToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            simulationToolStripMenuItem.Enabled = true;
            taskToolStripMenuItem.Enabled = true;
            SelectedNode = null;
            SelectedEdgeNode1 = null;
            SelectedEdgeNode2 = null;
            _selectedAGV = null;
            _selectedRack = null;
            comboBox_selectedNode.Text = "";
            textBox_selectedNodeType.Clear();
            _mapEditorForm.textBox_edgeNode1.Clear();
            _mapEditorForm.textBox_edgeNode2.Clear();

            textBox_mapSN.Text = VCS.CurrentMap.SerialNumber;
            textBox_mapZone.Text = VCS.CurrentMap.ZoneID.ToString();
            textBox_mapDIM.Text = $"{VCS.CurrentMap.Width} x {VCS.CurrentMap.Height}";
            _mapEditorForm.comboBox_constraintLayers.Items.Clear();
            comboBox_planningLayer.Items.Clear();
            foreach (var layer in VCS.CurrentMap.ConstraintLayers)
            {
                _mapEditorForm.comboBox_constraintLayers.Items.Add(layer.Name);
                comboBox_planningLayer.Items.Add(layer.Name);
            }
            _mapEditorForm.comboBox_constraintLayers.SelectedIndex = 0;
            comboBox_planningLayer.SelectedIndex = 0;

            foreach (var node in VCS.CurrentMap.AllNodes)
            {
                comboBox_selectedNode.Items.Add(node);
            }
           
        }

        private void NodeSelect(Point mousePosition, MouseButtons mouseButton, bool sequenceMode = false)
        {
            if (mouseButton != MouseButtons.Left && mouseButton != MouseButtons.Right) return;
            SelectedNode = _mapDrawer?.GetNodeByPosition(mousePosition.X, mousePosition.Y);
            if (SelectedNode == null)
            {
                comboBox_selectedNode.Text = "";
                textBox_selectedNodeType.Text = "";
                return;
            }
            if (SelectedNode == _prevSelectedNode && sequenceMode) return;
            _prevSelectedNode = SelectedNode;
            if (VCS.AGVNodeQueue[SelectedNode.Location.Y, SelectedNode.Location.X].Count > 0)
            {
                Debug.WriteLine($"AGV Queue = ");
                foreach (var AGV in VCS.AGVNodeQueue[SelectedNode.Location.Y, SelectedNode.Location.X])
                {
                    Debug.WriteLine($"\t{AGV.Name}");
                }
            }
            else
            {
                Debug.WriteLine("Empty!");
            }
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    comboBox_selectedNode.SelectedItem = SelectedNode;
                    textBox_selectedNodeType.Text = SelectedNode.Type.ToString();
                    textBox_selectedNodeType.BackColor = MapNode.NodeTypeColorDict[SelectedNode.Type];
                    _selectedAGV = VCS.AGVHandler.AGVList.Find(agv => agv.CurrentNode == SelectedNode) ?? _selectedAGV;
                    _selectedRack = VCS.RackList.Find(rack => rack.CurrentNode == SelectedNode) ?? _selectedRack;
                    if (_mapEditorForm.IsEditingType)
                    {
                        if (_mapEditorForm.SmartEditingMode)
                        {
                            _mapEditorForm.SetNodeTypeSmart(SelectedNode, (MapNode.Types)_mapEditorForm.comboBox_types.SelectedItem);
                        }
                        else
                        {
                            SelectedNode.Type = (MapNode.Types)_mapEditorForm.comboBox_types.SelectedItem;
                        }                                                
                        SelectedNode.DisallowWaitingOnNode = _mapEditorForm.checkBox_disallowWaitingOnNode.Checked;
                    }
                    if (_mapEditorForm.IsEditingEdge)
                    {
                        if (SelectedEdgeNode1 == null)
                        {
                            SelectedEdgeNode1 = SelectedNode;
                        }
                        else
                        {
                            if (SelectedNode.IsNeighbourNode(SelectedEdgeNode1))
                            {
                                SelectedEdgeNode2 = SelectedNode;
                            }
                            else
                            {
                                if (SelectedEdgeNode2 != null)
                                {
                                    if (SelectedNode.IsNeighbourNode(SelectedEdgeNode2))
                                    {
                                        SelectedEdgeNode1 = SelectedEdgeNode2;
                                        SelectedEdgeNode2 = SelectedNode;
                                    }
                                    else
                                    {
                                        SelectedEdgeNode1 = SelectedNode;
                                        SelectedEdgeNode2 = null;
                                    }
                                }
                                else
                                {
                                    SelectedEdgeNode1 = SelectedNode;
                                }
                            }
                        }

                        if (SelectedEdgeNode1 != null && SelectedEdgeNode2 != null)
                        {
                            var selectedEdge = VCS.CurrentMap.GetEdgeByNodes(_mapEditorForm.comboBox_constraintLayers.SelectedIndex, SelectedEdgeNode1, SelectedEdgeNode2);
                            selectedEdge.PassingRestriction = (MapEdge.PassingRestrictions)_mapEditorForm.comboBox_passingRestrictions.SelectedItem;
                            if (SelectedEdgeNode1.Location.X + SelectedEdgeNode1.Location.Y > SelectedEdgeNode2.Location.X + SelectedEdgeNode2.Location.Y)
                            {
                                selectedEdge.InvertPassingRestrictionDirection();
                            }
                        }
                        _mapEditorForm.textBox_edgeNode1.Text = SelectedEdgeNode1?.Name;
                        _mapEditorForm.textBox_edgeNode2.Text = SelectedEdgeNode2?.Name;
                    }
                    if (_planningFlag == 1)
                    {
                        button_startPlanning.Text = "Select goal node";
                        _startNode = SelectedNode;
                        textBox_startNode.Text = _startNode.Name;
                        _planningFlag++;
                    }
                    else if (_planningFlag == 2)
                    {
                        if (SelectedNode != _startNode)
                        {
                            _goalNode = SelectedNode;
                            button_startPlanning.Text = "Start Planning";
                            textBox_goalNode.Text = _goalNode.Name;
                            _planningFlag = 0;
                            _path = VCS.PathPlanner.FindPath(_startNode, _goalNode, comboBox_planningLayer.SelectedIndex == 1, comboBox_planningLayer.SelectedIndex);
                            textBox_planningPath.Clear();
                            textBox_planningPath.AppendText($"Length = {(_path is null ? -1 : _path.Count)}\r\n");

                            var targetAGV = VCS.AGVHandler.AGVList.Find(agv => agv.CurrentNode == _startNode);
                            if (targetAGV != null)
                            {
                                targetAGV.StartNewPath(_path);
                                targetAGV.EndPath();
                            }
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
                    if (_mapEditorForm.IsEditingType)
                    {
                        if (_mapEditorForm.SmartEditingMode)
                        {
                            _mapEditorForm.SetNodeTypeSmart(SelectedNode, MapNode.Types.None);
                        }
                        else
                        {
                            SelectedNode.Type = MapNode.Types.None;
                        }                                                
                        SelectedNode.DisallowWaitingOnNode = false;
                    }
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    break;
            }

        }


        private void timer_mapRefresh_Tick(object sender, EventArgs e)
        {
            if (VCS.CurrentMap != null)
            {
                _mapDrawer.DrawNewMap();
                _mapDrawer.DrawSelectedNode(SelectedNode);
                _mapDrawer.DrawSelectedEdge(SelectedEdgeNode1, SelectedEdgeNode2);
                if (_mapEditorForm.checkBox_showConstraints.Checked)
                {
                    _mapDrawer.DrawEdgeConstraints(_mapEditorForm.comboBox_constraintLayers.SelectedIndex);
                }
                //_mapDrawer.DrawSinglePath(_path);
                _mapDrawer.DrawAllAGVPath();
                _mapDrawer.DrawAGVs();
                _mapDrawer.DrawRacks();
                //_mapDrawer.DrawOccupancy();
                pictureBox_mapViewer.Image = _mapDrawer.GetMapPicture();
            }
            if (_selectedAGV != null)
            {
                textBox_agvName.Text = _selectedAGV.Name;
                textBox_agvNode.Text = _selectedAGV.CurrentNode.Name;
                textBox_agvRack.Text = _selectedAGV.BoundRack?.Name;
                textBox_agvStatus.Text = _selectedAGV.State.ToString();
                textBox_agvHeading.Text = _selectedAGV.Heading.ToString();
            }
            else
            {
                textBox_agvName.Clear();
                textBox_agvNode.Clear();
                textBox_agvRack.Clear();
                textBox_agvStatus.Clear();
                textBox_agvHeading.Clear();
            }
            if (_selectedRack != null)
            {
                textBox_rackName.Text = _selectedRack.Name;
                textBox_rackNode.Text = _selectedRack.CurrentNode.Name;
                textBox_rackHome.Text = _selectedRack.HomeNode.Name;
                textBox_rackHeading.Text = _selectedRack.Heading.ToString();
            }
            else
            {
                textBox_rackName.Clear();
                textBox_rackNode.Clear();
                textBox_rackHome.Clear();
                textBox_rackHeading.Clear();
            }
        }
       
    }
}
