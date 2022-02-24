using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCS.Maps;

namespace VCS.Windows
{
    public partial class MapEditor : Form
    {
        private static readonly List<MapNode.Types> _specialNodeList = new List<MapNode.Types> { MapNode.Types.ChargingStation, MapNode.Types.WorkStation };
        private static readonly List<MapNode.Types> _specialCompanionNodeList = new List<MapNode.Types> { MapNode.Types.ChargingStationDock, MapNode.Types.WorkStationPickUp };
        public bool IsEditingType { get; private set; }
        public bool IsEditingEdge { get; private set; }
        public bool SmartEditingMode { get; set; }
        public MapEditor()
        {
            InitializeComponent();
            this.IsEditingType = false;
            this.IsEditingEdge = false;
            this.SmartEditingMode = true;
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));
            comboBox_types.SelectedIndex = 0;
            comboBox_passingRestrictions.DataSource = Enum.GetValues(typeof(MapEdge.PassingRestrictions));
            comboBox_types.SelectedIndex = 0;
            comboBox_companionNodeDirection.Items.Add("Up");
            comboBox_companionNodeDirection.Items.Add("Down");
            comboBox_companionNodeDirection.Items.Add("Left");
            comboBox_companionNodeDirection.Items.Add("Right");
            comboBox_companionNodeDirection.SelectedIndex = 0;
            comboBox_companionNodeDirection.Enabled = false;
        }

        private void MapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            IsEditingType = false;
            button_startEditingNode.Text = "Start Editing";
            button_startEditingEdge.Enabled = true;
            IsEditingEdge = false;
            button_startEditingEdge.Text = "Start Editing";
            button_startEditingNode.Enabled = true;
            (this.Tag as MainWindow).SelectedEdgeNode1 = null;
            (this.Tag as MainWindow).SelectedEdgeNode2 = null;
        }

        private void button_startEditingNode_Click(object sender, EventArgs e)
        {
            if (IsEditingType)
            {
                IsEditingType = false;
                button_startEditingNode.Text = "Start Editing";
                button_startEditingEdge.Enabled = true;
            }
            else
            {
                IsEditingType = true;
                button_startEditingNode.Text = "Stop";
                button_startEditingEdge.Enabled = false;
            }
        }

        private void comboBox_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedNodeType = (MapNode.Types)comboBox_types.SelectedItem;
            if (this.SmartEditingMode)
            {
                if (_specialCompanionNodeList.Contains(selectedNodeType))
                {
                    var substituteNodeType = _specialNodeList[_specialCompanionNodeList.IndexOf(selectedNodeType)];
                    MessageBox.Show($"You cannot place {{{selectedNodeType}}} node in Smart Editing Mode, place {{{substituteNodeType}}} node instead.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox_types.SelectedItem = substituteNodeType;
                    selectedNodeType = (MapNode.Types)comboBox_types.SelectedItem;
                }
            }
            comboBox_companionNodeDirection.Enabled = _specialNodeList.Contains(selectedNodeType);
            comboBox_types.BackColor = MapNode.NodeTypeColorDict[selectedNodeType];
        }

        private void comboBox_constraintLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            (this.Tag as MainWindow).comboBox_planningLayer.SelectedIndex = comboBox_constraintLayers.SelectedIndex;
        }

        private void button_addLayer_Click(object sender, EventArgs e)
        {
            var newLayer = new Map.ConstraintLayer((this.Tag as MainWindow).VCS.CurrentMap, $"Layer {(this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers.Count}");
            (this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers.Add(newLayer);
            comboBox_constraintLayers.Items.Clear();
            foreach (var layer in (this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
            }
            (this.Tag as MainWindow).comboBox_planningLayer.Items.Clear();
            foreach (var layer in (this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers)
            {
                (this.Tag as MainWindow).comboBox_planningLayer.Items.Add(layer.Name);
            }

            comboBox_constraintLayers.SelectedIndex = comboBox_constraintLayers.Items.Count - 1;
        }

        private void button_deleteLayer_Click(object sender, EventArgs e)
        {
            if (comboBox_constraintLayers.SelectedIndex == 0) return;
            (this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers.RemoveAt(comboBox_constraintLayers.SelectedIndex);
            comboBox_constraintLayers.Items.Clear();
            foreach (var layer in (this.Tag as MainWindow).VCS.CurrentMap.ConstraintLayers)
            {
                comboBox_constraintLayers.Items.Add(layer.Name);
            }
            comboBox_constraintLayers.SelectedIndex = 0;
        }

        private void button_startEditingEdge_Click(object sender, EventArgs e)
        {
            if (IsEditingEdge)
            {
                IsEditingEdge = false;
                button_startEditingEdge.Text = "Start Editing";
                button_startEditingNode.Enabled = true;
                (this.Tag as MainWindow).SelectedEdgeNode1 = null;
                (this.Tag as MainWindow).SelectedEdgeNode2 = null;
            }
            else
            {
                IsEditingEdge = true;
                button_startEditingEdge.Text = "Stop Editing";
                button_startEditingNode.Enabled = false;
            }
        }

        public void SetNodeTypeSmart(MapNode node, MapNode.Types type)
        {
            var currentMap = (this.Tag as MainWindow).VCS.CurrentMap;
            if (_specialCompanionNodeList.Contains(type)) return;
            if (!_specialNodeList.Contains(type))
            {
                node.Type = type;
                switch (type)
                {
                    case MapNode.Types.None:
                    case MapNode.Types.WorkStation:
                        for (int i = 0; i < currentMap.ConstraintLayers.Count; i++)
                        {
                            foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                            {
                                if (neighborNode.Type == MapNode.Types.Wall) continue;
                                var edge = currentMap.GetEdgeByNodes(i, node, neighborNode);
                                edge.PassingRestriction = MapEdge.PassingRestrictions.NoRestrictions;
                            }
                        }
                        break;
                    case MapNode.Types.Storage:
                        foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                        {
                            if (neighborNode.Type == MapNode.Types.Wall) continue;
                            var edge = currentMap.GetEdgeByNodes(0, node, neighborNode);
                            edge.PassingRestriction = MapEdge.PassingRestrictions.NoRestrictions;
                            if (neighborNode.Type != MapNode.Types.Storage) continue;
                            edge = currentMap.GetEdgeByNodes(1, node, neighborNode);
                            edge.PassingRestriction = MapEdge.PassingRestrictions.NoEnterOrLeaving;
                        }
                        break;
                    case MapNode.Types.Wall:
                        for (int i = 0; i < currentMap.ConstraintLayers.Count; i++)
                        {
                            foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                            {
                                var edge = currentMap.GetEdgeByNodes(i, node, neighborNode);
                                edge.PassingRestriction = MapEdge.PassingRestrictions.NoEnterOrLeaving;
                            }
                        }
                        break;
                    default:
                        break;
                }

                foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                {
                    var edge = currentMap.GetEdgeByNodes(0, node, neighborNode);
                    if (edge.IsBounded)
                    {
                        edge.IsBounded = false;
                        for (int i = 1; i < currentMap.ConstraintLayers.Count; i++)
                        {
                            edge = currentMap.GetEdgeByNodes(i, node, neighborNode);
                            edge.IsBounded = false;
                        }
                        SetNodeTypeSmart(neighborNode, type);                        
                    }                    
                }                
                return;
            }
            else
            {
                var companionNodeType = _specialCompanionNodeList[_specialNodeList.IndexOf(type)];
                MapNode companionNode;
                switch (comboBox_companionNodeDirection.SelectedItem.ToString())
                {
                    case "Up":
                        companionNode = currentMap.GetUpNode(node);
                        break;
                    case "Down":
                        companionNode = currentMap.GetDownNode(node);
                        break;
                    case "Left":
                        companionNode = currentMap.GetLeftNode(node);
                        break;
                    case "Right":
                        companionNode = currentMap.GetRightNode(node);
                        break;
                    default:
                        companionNode = null;
                        break;
                }
                if (companionNode == null)
                {
                    MessageBox.Show("Cannot place special node here!\r\nSelected direction is inavlid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                {
                    var edge = currentMap.GetEdgeByNodes(0, node, neighborNode);
                    if (edge.IsBounded)
                    {
                        MessageBox.Show("Cannot place special node here!\r\nSelected node is already bounded with another node!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                foreach (var neighborNode in currentMap.GetNeighborNodes(companionNode))
                {
                    var edge = currentMap.GetEdgeByNodes(0, companionNode, neighborNode);
                    if (edge.IsBounded)
                    {
                        MessageBox.Show("Cannot place special node here!\r\nCompanion node of selected direction is already bounded with another node!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                companionNode.Type = companionNodeType;
                node.Type = type;

                if (type == MapNode.Types.WorkStation)
                {
                    foreach (var neighborNode in currentMap.GetNeighborNodes(node))
                    {
                        if(neighborNode != companionNode && neighborNode.Type != MapNode.Types.WorkStation)
                        {
                            neighborNode.DisallowWaitingOnNode = true;
                        }
                    }
                }

                for (int i = 0; i < currentMap.ConstraintLayers.Count; i++)
                {
                    foreach (var neighborNode in currentMap.GetNeighborNodes(companionNode))
                    {
                        var edge = currentMap.GetEdgeByNodes(i, neighborNode, companionNode);
                        edge.PassingRestriction = MapEdge.PassingRestrictions.NoEnterOrLeaving;
                        if (neighborNode == node)
                        {
                            edge.IsBounded = true;
                        }
                    }
                }
            }
        }

        private void button_toggleSmartEditing_Click(object sender, EventArgs e)
        {
            if (this.SmartEditingMode)
            {
                var result = MessageBox.Show("Do you sure you want to disable Smart Editing Mode?\r\n You will loose node binding datas.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
                this.SmartEditingMode = false;
                button_toggleSmartEditing.Text = "Enable Smart Editing Mode";

            }
            else
            {
                this.SmartEditingMode = true;
                button_toggleSmartEditing.Text = "Disable Smart Editing Mode";
            }
        }
    }
}
