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
        public bool IsEditingType { get; private set; }
        public bool IsEditingEdge { get; private set; }
        public MapEditor()
        {
            InitializeComponent();      
            comboBox_types.DataSource = Enum.GetValues(typeof(MapNode.Types));            
            comboBox_types.SelectedIndex = 0;
            comboBox_passingRestrictions.DataSource = Enum.GetValues(typeof(MapEdge.PassingRestrictions));
            comboBox_types.SelectedIndex = 0;
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
            comboBox_types.BackColor = MapNode.NodeTypeColorDict[(MapNode.Types)comboBox_types.SelectedItem];
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
    }
}
