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
using CsvHelper;
using CsvHelper.Configuration;

namespace WMS.Windows
{
    public partial class MaterialsManagementWindow : Form
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "materials.csv");
        public List<Material> MaterialList;
        public Material SelectedMaterial { get; private set; }
        public MaterialsManagementWindow()
        {
            InitializeComponent();
            this.MaterialList = new List<Material>();
        }

        private void MaterialsManagementWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public bool LoadMaterialsFromFile()
        {
            if (File.Exists(_filePath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Big5")))
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    this.MaterialList = csvReader.GetRecords<Material>().ToList();
                    listView.Items.Clear();
                    foreach (var material in this.MaterialList)
                    {
                        var listViewItem = new ListViewItem();
                        listViewItem.Text = material.Name;
                        listViewItem.SubItems.Add($"{material.Length.ToString().PadLeft(2)} x {material.Width.ToString().PadLeft(2)}");
                        listViewItem.SubItems.Add(material.RackName);
                        listViewItem.SubItems.Add(material.Layer.ToString());
                        listViewItem.SubItems.Add(material.Box.ToString());                        
                        listView.Items.Add(listViewItem);
                    }
                    listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    return true;
                }
            }
            //else
            //{
            //    Debug.WriteLine("File not exist");
            //    File.Create(filePath);
            //}
            return false;
        }

        public bool SaveMaterialsToFile()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                    using (var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("Big5")))
                    using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
                    {
                        csvWriter.WriteRecords(this.MaterialList);
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                this.SelectedMaterial = null;
                return;
            }
            this.SelectedMaterial = this.MaterialList.First(material => material.Name == listView.SelectedItems[0].Text);
            textBox_Name.Text = this.SelectedMaterial.Name;
            textBox_Dimension.Text = $"{this.SelectedMaterial.Length}x{this.SelectedMaterial.Width}";
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (textBox_Name.Text == "")
            {
                MessageBox.Show("物料名稱不能為空", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.MaterialList.FirstOrDefault(material => material.Name == this.textBox_Name.Text) != null)
            {
                MessageBox.Show("該物料已經存在", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectedMaterial = MaterialList.First(material => material.Name == this.textBox_Name.Text);
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
                    Width = width
                };
                MaterialList.Add(newMaterial);
                if (!SaveMaterialsToFile())
                {
                    MessageBox.Show("無法存取物料檔案", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("物料材積格式錯誤\r\n範例： 3x8", "無法新增物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadMaterialsFromFile();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (this.SelectedMaterial != null)
            {
                this.MaterialList.Remove(this.SelectedMaterial);
                Debug.WriteLine(this.MaterialList.Count);
                foreach (var item in this.MaterialList)
                {
                    Debug.WriteLine(item.Name);
                }

                if (!SaveMaterialsToFile())
                {
                    MessageBox.Show("無法存取物料檔案", "無法刪除物料", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadMaterialsFromFile();
            }
        }        
    }
}
