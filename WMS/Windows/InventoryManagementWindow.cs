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
            var deleteList = new List<Inventory>();
            foreach (var inventory in _wms.InventoryList)
            {
                var targetMaterial = _wms.MaterialList.FirstOrDefault(material => material.Name == inventory.Name);
                if (targetMaterial != null)
                {
                    var listViewItem = new ListViewItem();
                    listViewItem.Text = inventory.Name;
                    listViewItem.SubItems.Add(inventory.Quantity.ToString());
                    listViewItem.SubItems.Add($"{targetMaterial.Length.ToString().PadLeft(2)} x {targetMaterial.Width.ToString().PadLeft(2)}");
                    listViewItem.SubItems.Add(inventory.RackName);
                    listViewItem.SubItems.Add(inventory.Layer.ToString());
                    listViewItem.SubItems.Add(inventory.Box.ToString());
                    if(inventory.RackName != "null")
                        listViewItem.SubItems.Add(WorkOrder.Mission.RackFaceDescription[_wms.GetAvailablePickUpFaces(targetMaterial)]);
                    listView.Items.Add(listViewItem);
                }
                else
                {
                    var result = MessageBox.Show($"偵測到未知物料({inventory.Name})的庫存資訊，是否刪除?", "操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes) {
                        deleteList.Add(inventory);
                    }
                }               
            }
            foreach (var inv in deleteList)
            {
                _wms.InventoryList.Remove(inv);
            }
            _wms.SaveInventoryToFile();
            //listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            comboBox_MaterialName.DataSource = _wms.InventoryList;
        }
        public void UpdateRackInfos()
        {
            comboBox_RackBox.Items.Clear();
            foreach (var rackInfo in _wms.RackInfoList)
            {
                comboBox_RackName.Items.Add(rackInfo);
            }
        }
        #endregion

        private void button_ChangeRackInfo_Click(object sender, EventArgs e)
        {
            if (this.SelectedInventory == null)
            {
                MessageBox.Show("請先選擇欲變更庫存資訊", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox_RackName.Text == "" || comboBox_RackLayer.Text == "" || comboBox_RackBox.Text == "")
            {
                MessageBox.Show("料架資訊不得為空", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var targetRackInfo = comboBox_RackName.SelectedItem as RackInfo;
            if (_wms.InventoryList.FirstOrDefault(inventory =>
                 inventory.RackName == targetRackInfo.RackName && inventory.Layer == int.Parse(comboBox_RackLayer.Text) && inventory.Box == int.Parse(comboBox_RackBox.Text)
                ) != null)
            {
                MessageBox.Show("該物料箱已有物料", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.SelectedInventory.RackName = (comboBox_RackName.SelectedItem as RackInfo).RackName;
            this.SelectedInventory.Layer = int.Parse(this.comboBox_RackLayer.Text);
            this.SelectedInventory.Box = int.Parse(this.comboBox_RackBox.Text);
            if (!_wms.SaveInventoryToFile())
            {
                MessageBox.Show("無法存取庫存檔案", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _wms.LoadInventoryFromFile();
        }

        private void comboBox_RackName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_RackLayer.Items.Clear();
            comboBox_RackBox.Items.Clear();
            comboBox_RackLayer.Text = "";
            comboBox_RackBox.Text = "";
            for (int i = 0; i < (comboBox_RackName.SelectedItem as RackInfo).LayerCount; i++)
            {
                comboBox_RackLayer.Items.Add(i + 1);
            }
            for (int i = 0; i < (comboBox_RackName.SelectedItem as RackInfo).BoxCountPerLayer; i++)
            {
                comboBox_RackBox.Items.Add(i + 1);
            }
        }

        
    }
}
