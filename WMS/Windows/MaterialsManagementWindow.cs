using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using WMS.Models;

namespace WMS.Windows
{
    public partial class MaterialsManagementWindow : Form
    {
        private WMS _wms;
        public Material SelectedMaterial { get; private set; }
        public MaterialsManagementWindow(WMS wms)
        {
            InitializeComponent();
            _wms = wms;
        }

        private void MaterialsManagementWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        #region UI_Events
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                this.SelectedMaterial = null;
                return;
            }
            this.SelectedMaterial = _wms.MaterialList.First(material => material.Name == listView.SelectedItems[0].Text);
            textBox_Name.Text = this.SelectedMaterial.Name;
            textBox_Dimension.Text = $"{this.SelectedMaterial.Length}x{this.SelectedMaterial.Width}";
            comboBox_RackName.Text = this.SelectedMaterial.RackName;
            comboBox_RackLayer.Text = this.SelectedMaterial.Layer.ToString();
            comboBox_RackBox.Text = this.SelectedMaterial.Box.ToString();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (textBox_Name.Text == "")
            {
                MessageBox.Show("物料名稱不能為空", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_wms.MaterialList.FirstOrDefault(material => material.Name == this.textBox_Name.Text) != null)
            {
                MessageBox.Show("該物料已經存在", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectedMaterial = _wms.MaterialList.First(material => material.Name == this.textBox_Name.Text);
                return;
            }
            if (comboBox_RackName.Text == "" || comboBox_RackLayer.Text == "" || comboBox_RackBox.Text == "")
            {
                MessageBox.Show("料架資訊不得為空", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var targetRackInfo = comboBox_RackName.SelectedItem as RackInfo;
            if (_wms.MaterialList.FirstOrDefault(material =>
                 material.RackName == targetRackInfo.RackName && material.Layer == int.Parse(comboBox_RackLayer.Text) && material.Box == int.Parse(comboBox_RackBox.Text)
                ) != null)
            {
                MessageBox.Show("該物料箱已有物料", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var length = int.Parse(this.textBox_Dimension.Text.Split('x')[0]);
                var width = int.Parse(this.textBox_Dimension.Text.Split('x')[1]);
                var newMaterial = new Material()
                {
                    Name = this.textBox_Name.Text,
                    Length = length,
                    Width = width,
                    RackName = (this.comboBox_RackName.SelectedItem as RackInfo).RackName,
                    Layer = int.Parse(this.comboBox_RackLayer.Text),
                    Box = int.Parse(this.comboBox_RackBox.Text)
                };
                _wms.MaterialList.Add(newMaterial);
                if (!_wms.SaveMaterialsToFile())
                {
                    MessageBox.Show("無法存取物料檔案", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (_wms.InventoryList.FirstOrDefault(inventory => inventory.Name == newMaterial.Name) == null)
                {
                    var result = MessageBox.Show("庫存尚無此物料的資訊，是否一併新增?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        _wms.InventoryList.Add(new Inventory()
                        {
                            Name = newMaterial.Name,
                            Quantity = 0
                        });
                        if (!_wms.SaveInventoryToFile())
                        {
                            MessageBox.Show("無法存取庫存檔案", "無法新增庫存", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("物料材積格式錯誤\r\n範例： 3x8", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _wms.LoadMaterialsFromFile();
                _wms.LoadInventoryFromFile();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (this.SelectedMaterial != null)
            {
                _wms.MaterialList.Remove(this.SelectedMaterial);
                if (!_wms.SaveMaterialsToFile())
                {
                    MessageBox.Show("無法存取物料檔案", "無法刪除物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _wms.LoadMaterialsFromFile();
            }
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
        private void button_ChangeRackInfo_Click(object sender, EventArgs e)
        {
            if (this.SelectedMaterial == null)
            {
                MessageBox.Show("請先選擇欲變更物料", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox_RackName.Text == "" || comboBox_RackLayer.Text == "" || comboBox_RackBox.Text == "")
            {
                MessageBox.Show("料架資訊不得為空", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var targetRackInfo = comboBox_RackName.SelectedItem as RackInfo;
            if (_wms.MaterialList.FirstOrDefault(material =>
                 material.RackName == targetRackInfo.RackName && material.Layer == int.Parse(comboBox_RackLayer.Text) && material.Box == int.Parse(comboBox_RackBox.Text)
                ) != null)
            {
                MessageBox.Show("該物料箱已有物料", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.SelectedMaterial.RackName = (comboBox_RackName.SelectedItem as RackInfo).RackName;
            this.SelectedMaterial.Layer = int.Parse(this.comboBox_RackLayer.Text);
            this.SelectedMaterial.Box = int.Parse(this.comboBox_RackBox.Text);
            if (!_wms.SaveMaterialsToFile())
            {
                MessageBox.Show("無法存取物料檔案", "無法變更料架資訊", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _wms.LoadMaterialsFromFile();
        }
        #endregion

        #region Functions
        public void UpdateMaterialList()
        {
            listView.Items.Clear();
            foreach (var material in _wms.MaterialList)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = material.Name;
                listViewItem.SubItems.Add($"{material.Length.ToString().PadLeft(2)} x {material.Width.ToString().PadLeft(2)}");
                listViewItem.SubItems.Add(material.RackName);
                listViewItem.SubItems.Add(material.Layer.ToString());
                listViewItem.SubItems.Add(material.Box.ToString());
                listView.Items.Add(listViewItem);
            }
            //listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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

    }
}
