using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using WMS.Windows;
using WMS.Dialogs;
using WMS.Communication;
using WMS.Models;
using VCS.Maps;
using VCS.Models;

namespace WMS
{
    public partial class WMS : Form
    {
        private string _baseFilePath = AppDomain.CurrentDomain.BaseDirectory;
        private VCSClient _vcsClient;
        private Map _currentMap;
        private MapDrawerSlim _mapDrawer;
        private InventoryManagementWindow _inventoryManagementWindow;
        private MaterialsManagementWindow _materialsManagementWindow;
        public List<Rack> RackList { get; private set; }
        public List<RackInfo> RackInfoList { get; private set; }
        public List<Material> MaterialList { get; private set; }
        public List<Inventory> InventoryList { get; private set; }
        public WMS()
        {
            InitializeComponent();
            _vcsClient = new VCSClient();
            _inventoryManagementWindow = new InventoryManagementWindow(this);
            _materialsManagementWindow = new MaterialsManagementWindow(this);
        }

        #region Menu Strip Buttons
        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox_MapViewer.Image = Properties.Resources.demo_map;
        }

        private void inventoryManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _inventoryManagementWindow.Show();
            _inventoryManagementWindow.WindowState = FormWindowState.Normal;
        }
        private void materialsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _materialsManagementWindow.Show();
            _materialsManagementWindow.WindowState = FormWindowState.Normal;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new ConnectionDialog())
            {
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (_vcsClient.Connect(dialog.IPA, dialog.Port))
                    {
                        LogEvent("成功連線到VCS伺服器");
                        new Thread(() => { MessageBox.Show("Connected!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information); }).Start();
                        toolStripStatusLabel_ConnectionState.Text = "已連線";
                        toolStripStatusLabel_ConnectionState.ForeColor = Color.Green;
                        toolStripStatusLabel_ServerIP.Text = dialog.IPA + ":" + dialog.Port.ToString();
                        timer_RefreshMap.Interval = 1000 / 30;
                        timer_RefreshMap.Enabled = true;
                        _currentMap = _vcsClient.GetMapData();
                        if (_currentMap != null)
                        {
                            _mapDrawer = new MapDrawerSlim(_currentMap, pictureBox_MapViewer.Size);
                            LogEvent("成功載入地圖");
                        }
                        else
                        {
                            LogEvent("載入地圖失敗");
                        }
                    }
                    else
                    {
                        new Thread(() => { MessageBox.Show("Cannot connect to server!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error); }).Start();
                        toolStripStatusLabel_ConnectionState.Text = "連現失敗";
                        toolStripStatusLabel_ConnectionState.ForeColor = Color.Red;
                        LogEvent("無法連線到VCS伺服器");
                    }
                }
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _vcsClient.Disconnect();
        }
        #endregion

        #region UI Events
        private void WMS_Shown(object sender, EventArgs e)
        {
            if (!LoadRackInfosFromFile())
            {
                MessageBox.Show("無法載入料架資訊，檔案不存在\r\n程式即將關閉", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            else
            {
                LogEvent("料架資訊載入成功");
            }

            if (!LoadMaterialsFromFile())
            {
                MessageBox.Show("無法載入物料資料，檔案不存在\r\n程式即將關閉", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            else
            {
                LogEvent("物料資料載入成功");
            }

            if (!LoadInventoryFromFile())
            {
                MessageBox.Show("無法載庫存資料，檔案不存在\r\n程式即將關閉", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            else
            {
                LogEvent("庫存資料載入成功");
            }

        }
        private void timer_RefreshMap_Tick(object sender, EventArgs e)
        {
            this.RackList = _vcsClient.GetRackInfo();
            _mapDrawer.DrawNewMap();
            _mapDrawer.DrawRacks(this.RackList);
            var mapBMP = _mapDrawer.GetMapPicture();
            pictureBox_MapViewer.Image?.Dispose();
            pictureBox_MapViewer.Image = mapBMP;
        }

        private void pictureBox_MapViewer_SizeChanged(object sender, EventArgs e)
        {
            if (_mapDrawer == null) return;
            _mapDrawer.DrawSize = pictureBox_MapViewer.Size;
        }
        #endregion

        #region Functions
        private void LogEvent(string message)
        {
            textBox_Event.AppendText(DateTime.Now.ToString() + "\t" + message + "\r\n");
        }

        private bool LoadRackInfosFromFile()
        {
            string filePath = Path.Combine(_baseFilePath, "rackInfos.csv");
            if (File.Exists(filePath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Big5")))
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    this.RackInfoList = csvReader.GetRecords<RackInfo>().ToList();
                    _materialsManagementWindow.UpdateRackInfos();
                    return true;
                }
            }
            return false;
        }

        public bool LoadMaterialsFromFile()
        {
            string filePath = Path.Combine(_baseFilePath, "materials.csv");
            if (File.Exists(filePath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Big5")))
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    this.MaterialList = csvReader.GetRecords<Material>().ToList();
                    _materialsManagementWindow.UpdateMaterialList();
                    return true;
                }
            }
            return false;
        }
        public bool SaveMaterialsToFile()
        {
            string filePath = Path.Combine(_baseFilePath, "materials.csv");
            if (File.Exists(filePath))
            {
                try
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
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

        public bool LoadInventoryFromFile()
        {
            string filePath = Path.Combine(_baseFilePath, "invnetory.csv");
            if (File.Exists(filePath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Big5")))
                using (var csvReader = new CsvReader(streamReader, csvConfig))
                {
                    this.InventoryList = csvReader.GetRecords<Inventory>().ToList();
                    _inventoryManagementWindow.UpdateInventory();
                    return true;
                }
            }
            return false;
        }

        public bool SaveInventoryToFile()
        {
            string filePath = Path.Combine(_baseFilePath, "invnetory.csv");
            if (File.Exists(filePath))
            {
                try
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("Big5")))
                    using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
                    {
                        csvWriter.WriteRecords(this.InventoryList);
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

        #endregion
    }
}
