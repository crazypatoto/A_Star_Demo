using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS.Models;

namespace WMS.Windows
{
    public partial class InventoryManagementWindow : Form
    {
        private WMS _wms;
        public Inventory SelectedInventory { get; private set; }
        public InventoryManagementWindow(WMS wms)
        {
            InitializeComponent();
            _wms = wms;
        }

        #region UI Events
        private void InventoryManagementWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void comboBox_MaterialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_MaterialName.SelectedIndex < 0) return;
            var targetMaterial = _wms.MaterialList.FirstOrDefault(material => material.Name == comboBox_MaterialName.Text);
            if (targetMaterial != null)
            {
                textBox_MaterialDimension.Text = $"{targetMaterial.Length}x{targetMaterial.Width}";
                textBox_Quantity.Text = (comboBox_MaterialName.SelectedItem as Inventory).Quantity.ToString();
                var targetItem = listView.FindItemWithText(targetMaterial.Name);
                if (targetItem != null)
                {
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        listView.Items[i].Selected = false;
                    }
                    targetItem.Focused = true;
                    targetItem.Selected = true;
                    targetItem.EnsureVisible();
                    listView.Select();
                }

            }
            else
            {
                textBox_MaterialDimension.Text = "";
                textBox_Quantity.Text = "";
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count == 1)
            {
                comboBox_MaterialName.SelectedIndex = comboBox_MaterialName.FindStringExact(listView.SelectedItems[0].Text);
                this.SelectedInventory = _wms.InventoryList.First(inventory => inventory.Name == listView.SelectedItems[0].Text);
            }
            else
            {
                this.SelectedInventory = null;
            }
        }

        private void button_Plus_Click(object sender, EventArgs e)
        {
            if (this.SelectedInventory == null) return;
            this.SelectedInventory.Quantity += (int)numericUpDown_DeltaQuantity.Value;
            if (!_wms.SaveInventoryToFile())
            {
                MessageBox.Show("無法存取物料檔案", "無法變更庫存資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _wms.LoadInventoryFromFile();
        }

        private void button_Minus_Click(object sender, EventArgs e)
        {
            if (this.SelectedInventory == null) return;
            this.SelectedInventory.Quantity -= (int)numericUpDown_DeltaQuantity.Value;
            if (!_wms.SaveInventoryToFile())
            {
                MessageBox.Show("無法存取物料檔案", "無法變更庫存資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _wms.LoadInventoryFromFile();
        }
        #endregion

        #region Functions
        public void UpdateInventory()
        {
            listView.Items.Clear();
            foreach (var inventory in _wms.InventoryList)
            {
                var targetMaterial = _wms.MaterialList.FirstOrDefault(material => material.Name == inventory.Name);
                if (targetMaterial != null)
                {
                    var listViewItem = new ListViewItem();
                    listViewItem.Text = targetMaterial.Name;
                    listViewItem.SubItems.Add(inventory.Quantity.ToString());
                    listViewItem.SubItems.Add($"{targetMaterial.Length.ToString().PadLeft(2)} x {targetMaterial.Width.ToString().PadLeft(2)}");
                    listViewItem.SubItems.Add(targetMaterial.RackName);
                    listViewItem.SubItems.Add(targetMaterial.Layer.ToString());
                    listViewItem.SubItems.Add(targetMaterial.Box.ToString());
                    listView.Items.Add(listViewItem);
                }
            }
            //listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            comboBox_MaterialName.DataSource = _wms.InventoryList;
        }


        #endregion

    }
}
