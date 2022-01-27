using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCS;
using VCS.Maps;
using VCS.AGVs;
using VCS.Tasks;
using VCS.Models;


namespace VCS.Windows
{
    public partial class AGVTaskViewer : Form
    {
        private VCS _vcs;
        public AGVTaskViewer(VCS vcs)
        {
            InitializeComponent();
            _vcs = vcs;
            comboBox_AGV.DataSource = _vcs.AGVHandler.AGVList;
            timer.Interval = 100;
            timer.Enabled = true;            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            listView.Items.Clear();
            var selectedAGV = (comboBox_AGV.SelectedItem as AGV);
            if (selectedAGV == null) return;

            if (selectedAGV.TaskHandler.CurrentTask != null)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = selectedAGV.TaskHandler.CurrentTask.GetType().Name;
                listViewItem.SubItems.Add(selectedAGV.TaskHandler.CurrentTask.Finished.ToString());
                listView.Items.Add(listViewItem);
            }                        
            foreach (var task in selectedAGV.TaskHandler.TaskQueue)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = task.GetType().Name;
                listViewItem.SubItems.Add(task.Finished.ToString());
                listView.Items.Add(listViewItem);
            }            
        }

        private void comboBox_AGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
