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
using System.Diagnostics;
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
        private WorkOrder _currentWorkOrder;
        private WorkOrder _selectedWorkOrder;
        public List<Rack> RackList { get; private set; }
        public List<RackInfo> RackInfoList { get; private set; }
        public List<Material> MaterialList { get; private set; }
        public List<Inventory> InventoryList { get; private set; }
        public List<MapNode> AvailableDestinations { get; private set; }
        public List<WorkOrder> WorkOrderTemplates { get; private set; }
        public LinkedList<WorkOrder> WorkOrderQueue { get; private set; }
        public WMS()
        {
            InitializeComponent();
            _vcsClient = new VCSClient();
            _inventoryManagementWindow = new InventoryManagementWindow(this);
            _materialsManagementWindow = new MaterialsManagementWindow(this);
            WorkOrderQueue = new LinkedList<WorkOrder>();
        }

        #region Menu Strip Buttons  

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
                        new Thread(() => { MessageBox.Show("成功連線到VCS伺服器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }).Start();
                        toolStripStatusLabel_ConnectionState.Text = "已連線";
                        toolStripStatusLabel_ConnectionState.ForeColor = Color.Green;
                        toolStripStatusLabel_ServerIP.Text = dialog.IPA + ":" + dialog.Port.ToString();
                        _currentMap = _vcsClient.GetMapData();
                        if (_currentMap != null)
                        {
                            LogEvent("成功載入地圖");                            
                            _mapDrawer = new MapDrawerSlim(_currentMap, pictureBox_MapViewer.Size);                            
                            this.AvailableDestinations = new List<MapNode>();
                            foreach (var node in _currentMap.AllNodes)
                            {
                                if (node.Type == MapNode.Types.WorkStation)
                                {
                                    this.AvailableDestinations.Add(node);
                                }
                            }
                            comboBox_Destination.DataSource = this.AvailableDestinations;
                            timer_MapRefresh.Interval = 1000 / 30;
                            timer_MapRefresh.Enabled = true;
                            groupBox_WorkOrderEdit.Enabled = true;
                            groupBox_WorkOrderQueue.Enabled = true;
                            timer_WorkOrderHandling.Interval = 100;
                            timer_WorkOrderHandling.Enabled = true;
                        }
                        else
                        {
                            LogEvent("載入地圖失敗");
                        }
                    }
                    else
                    {
                        new Thread(() => { MessageBox.Show("無法連線至伺服器", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }).Start();
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
                MessageBox.Show("無法載入庫存資料，檔案不存在\r\n程式即將關閉", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            else
            {
                LogEvent("庫存資料載入成功");
            }

            if (!LoadWorkOrderTemplates())
            {
                MessageBox.Show("無法載入工單模板，資料夾不存在\r\n", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LogEvent("工單模板載入成功");
            }
        }
        private void WMS_FormClosed(object sender, FormClosedEventArgs e)
        {
            _vcsClient.Disconnect();
        }
        private void timer_MapRefresh_Tick(object sender, EventArgs e)
        {
            this.RackList = _vcsClient.GetRackInfo();
            _mapDrawer.DrawNewMap();
            _mapDrawer.DrawRacks(this.RackList);
            _mapDrawer.DrawSelectedNode(comboBox_Destination.SelectedItem as MapNode);            
            pictureBox_MapViewer.Image = _mapDrawer.GetMapPicture();
        }

        private void pictureBox_MapViewer_SizeChanged(object sender, EventArgs e)
        {
            if (_mapDrawer == null) return;
            _mapDrawer.DrawSize = pictureBox_MapViewer.Size;
        }


        private void button_LoadWorkOderTemplate_Click(object sender, EventArgs e)
        {
            var selectedWorkOrderTemplate = comboBox_WorkOrderTemplates.SelectedItem as WorkOrder;
            _currentWorkOrder = new WorkOrder(selectedWorkOrderTemplate.Name);
            _currentWorkOrder.MissionList = selectedWorkOrderTemplate.MissionList.ToList();
            foreach (var mission in _currentWorkOrder.MissionList)
            {
                var targetMaterial = this.MaterialList.First(material => material.Name == mission.MaterialName);
                mission.AvailableFaces = GetAvailablePickUpFaces(targetMaterial);
            }
            SortMissionList();
            UpdateMissionListView();
        }

        private void button_AddToWorkOrder_Click(object sender, EventArgs e)
        {
            if (_currentWorkOrder == null) return;
            if (comboBox_MaterialList.Text == "" || comboBox_Destination.Text == "") return;
            var existingMission = _currentWorkOrder.MissionList.FirstOrDefault(mission => mission.MaterialName == comboBox_MaterialList.SelectedItem.ToString());

            if (existingMission != null)
            {
                var inventory = InventoryList.First(inv => inv.Name == existingMission.MaterialName);
                var newQuantity = existingMission.Quantity + (int)numericUpDown_Quantity.Value;
                if (newQuantity > inventory.Quantity)
                {
                    MessageBox.Show("庫存不足", "無法新增任務", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    existingMission.Quantity = newQuantity;
                }
            }
            else
            {
                var inventory = InventoryList.First(inv => inv.Name == comboBox_MaterialList.SelectedItem.ToString());
                if ((int)numericUpDown_Quantity.Value > inventory.Quantity)
                {
                    MessageBox.Show("庫存不足", "無法新增任務", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var targetMaterial = this.MaterialList.FirstOrDefault(material => material.Name == comboBox_MaterialList.SelectedItem.ToString());
                    var newMission = new WorkOrder.Mission()
                    {
                        MaterialName = comboBox_MaterialList.SelectedItem.ToString(),
                        RackName = inventory.RackName,
                        Destination = comboBox_Destination.SelectedItem.ToString(),
                        Quantity = (int)numericUpDown_Quantity.Value,
                        AvailableFaces = GetAvailablePickUpFaces(targetMaterial)
                    };
                    _currentWorkOrder.MissionList.Add(newMission);
                }
            }
            SortMissionList();
            UpdateMissionListView();
        }

        private void comboBox_MaterialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InventoryList == null) return;
            var targetInventory = this.InventoryList.FirstOrDefault(inventory => inventory.Name == comboBox_MaterialList.SelectedItem.ToString());
            numericUpDown_Quantity.Maximum = targetInventory.Quantity;
        }

        private void button_NewWorkOrder_Click(object sender, EventArgs e)
        {
            _currentWorkOrder = null;
            _currentWorkOrder = new WorkOrder();
            UpdateMissionListView();
        }

        private void button_DeleteSelectedMission_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_MissionList.SelectedItems)
            {
                var targetMission = _currentWorkOrder.MissionList.First(mission => mission.MaterialName == item.Text);
                _currentWorkOrder.MissionList.Remove(targetMission);
            }
            SortMissionList();
            UpdateMissionListView();
        }

        private void button_AddToQueue_Click(object sender, EventArgs e)
        {
            if (_currentWorkOrder == null) return;
            _currentWorkOrder.EnqueuedTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            _currentWorkOrder.State = WorkOrder.WorkOrderState.Enqueued;
            WorkOrderQueue.AddLast(_currentWorkOrder);
            _currentWorkOrder = null;
            UpdateMissionListView();
            UpdateWorkOrderQueueListView();
        }
        private void listView_WorkOrderQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_WorkOrderQueue.SelectedIndices.Count == 0)
            {
                _selectedWorkOrder = null;
            }
            else
            {
                _selectedWorkOrder = WorkOrderQueue.First(workOrder => workOrder.UUID == listView_WorkOrderQueue.SelectedItems[0].Text);
            }
        }

        private void button_RemoveWorkOrderFromQueue_Click(object sender, EventArgs e)
        {
            if (_selectedWorkOrder == null) return;
            if (_selectedWorkOrder.State != WorkOrder.WorkOrderState.Enqueued) return;
            WorkOrderQueue.Remove(_selectedWorkOrder);
            _selectedWorkOrder = null;
            UpdateWorkOrderQueueListView();
        }

        private void button_EditWorkOrder_Click(object sender, EventArgs e)
        {
            if (_selectedWorkOrder == null) return;
            WorkOrderQueue.Remove(_selectedWorkOrder);
            _currentWorkOrder = _selectedWorkOrder;
            UpdateWorkOrderQueueListView();
            UpdateMissionListView();
        }
        private void button_MoveWorkOrderUp_Click(object sender, EventArgs e)
        {
            if (_selectedWorkOrder == null) return;
            var selectedNode = WorkOrderQueue.Find(_selectedWorkOrder);
            var prevNode = selectedNode.Previous;
            if (prevNode != null)
            {
                WorkOrderQueue.Remove(selectedNode);
                WorkOrderQueue.AddBefore(prevNode, _selectedWorkOrder);
                UpdateWorkOrderQueueListView();
            }
        }

        private void button_MoveWorkOrderDown_Click(object sender, EventArgs e)
        {
            if (_selectedWorkOrder == null) return;
            var selectedNode = WorkOrderQueue.Find(_selectedWorkOrder);
            var nextNode = selectedNode.Next;
            if (nextNode != null)
            {
                WorkOrderQueue.Remove(selectedNode);
                WorkOrderQueue.AddAfter(nextNode, _selectedWorkOrder);
                UpdateWorkOrderQueueListView();
            }
        }

        private void timer_WorkOrderHandling_Tick(object sender, EventArgs e)
        {
            if (WorkOrderQueue.Count == 0) return;
            var firstWorkOrder = WorkOrderQueue.First.Value;
            switch (firstWorkOrder.State)
            {
                case WorkOrder.WorkOrderState.Enqueued:
                    var result = _vcsClient.AssignNewWorkOrder(firstWorkOrder);
                    if (result)
                    {
                        firstWorkOrder.State = WorkOrder.WorkOrderState.Executing;
                    }
                    break;
                case WorkOrder.WorkOrderState.Executing:
                    firstWorkOrder.State = _vcsClient.GetWorkOrderState(firstWorkOrder);
                    break;
                case WorkOrder.WorkOrderState.Finished:
                    WorkOrderQueue.RemoveFirst();
                    break;
                case WorkOrder.WorkOrderState.Error:
                    break;
            }
            UpdateWorkOrderQueueListView();
        }
        #endregion

        #region Public Methods
        public void LogEvent(string message)
        {
            textBox_Event.AppendText(DateTime.Now.ToString() + "\t" + message + "\r\n");
        }

        public bool LoadRackInfosFromFile()
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
                    _inventoryManagementWindow.UpdateRackInfos();
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
                    comboBox_MaterialList.DataSource = this.MaterialList;
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

        public bool LoadWorkOrderTemplates()
        {
            string folderPath = Path.Combine(_baseFilePath, "workorders");
            if (Directory.Exists(folderPath))
            {
                this.WorkOrderTemplates = new List<WorkOrder>();
                string[] files = Directory.GetFiles(folderPath, "WO-*.csv");
                foreach (var file in files)
                {
                    var newWorkOrder = new WorkOrder(file);
                    if (newWorkOrder.IsValid)
                    {
                        foreach (var mission in newWorkOrder.MissionList)
                        {                            
                            var targetInventoryInfo = InventoryList.FirstOrDefault(inventoryInfo => inventoryInfo.Name == mission.MaterialName);
                            if (targetInventoryInfo != null)
                            {
                                mission.RackName = targetInventoryInfo.RackName;
                            }
                        }
                        this.WorkOrderTemplates.Add(newWorkOrder);
                    }
                }

                comboBox_WorkOrderTemplates.DataSource = this.WorkOrderTemplates;
                return true;
            }
            return false;
        }

        public WorkOrder.Mission.RackFace GetAvailablePickUpFaces(Material material)
        {
            WorkOrder.Mission.RackFace availableFaces = WorkOrder.Mission.RackFace.None;
            var targetInventoryInfo = InventoryList.First(inventoryInfo => inventoryInfo.Name == material.Name);
            var targetRackInfo = RackInfoList.First(rackInfo => rackInfo.RackName == targetInventoryInfo.RackName);            
            var wn = 1;
            var ne = wn + ((int)(RackInfo.RackWidth / targetRackInfo.BoxWidth) - 1);
            var es = ne + ((int)(RackInfo.RackLength / targetRackInfo.BoxLength) - 1);
            var sw = es + ((int)(RackInfo.RackWidth / targetRackInfo.BoxWidth) - 1);
            if (targetInventoryInfo.Box == wn)
            {
                availableFaces = WorkOrder.Mission.RackFace.West | WorkOrder.Mission.RackFace.North;
            }
            else if (targetInventoryInfo.Box == ne)
            {
                availableFaces = WorkOrder.Mission.RackFace.North | WorkOrder.Mission.RackFace.East;
            }
            else if (targetInventoryInfo.Box == es)
            {
                availableFaces = WorkOrder.Mission.RackFace.East | WorkOrder.Mission.RackFace.South;
            }
            else if (targetInventoryInfo.Box == sw)
            {
                availableFaces = WorkOrder.Mission.RackFace.South | WorkOrder.Mission.RackFace.West;
            }
            else
            {
                if (targetInventoryInfo.Box < ne)
                {
                    availableFaces = WorkOrder.Mission.RackFace.North;
                }
                else if (targetInventoryInfo.Box < es)
                {
                    availableFaces = WorkOrder.Mission.RackFace.East;
                }
                else if (targetInventoryInfo.Box < sw)
                {
                    availableFaces = WorkOrder.Mission.RackFace.South;
                }
                else
                {
                    availableFaces = WorkOrder.Mission.RackFace.West;
                }
            }
            return availableFaces;
        }
        #endregion

        #region Private Methods
        private void UpdateMissionListView()
        {
            listView_MissionList.Items.Clear();
            textBox_WorkOrderUUID.Clear();
            if (_currentWorkOrder == null) return;
            textBox_WorkOrderUUID.Text = _currentWorkOrder.UUID;
            if (_currentWorkOrder.MissionList.Count == 0) return;
            foreach (var mission in _currentWorkOrder.MissionList)
            {
                var newListViewItem = new ListViewItem();
                newListViewItem.Text = mission.MaterialName;
                newListViewItem.SubItems.Add(mission.RackName);
                newListViewItem.SubItems.Add(mission.Destination);
                newListViewItem.SubItems.Add(mission.Quantity.ToString());
                newListViewItem.SubItems.Add(WorkOrder.Mission.RackFaceDescription[mission.AvailableFaces]);
                newListViewItem.SubItems.Add(WorkOrder.Mission.RackFaceDescription[mission.PickUpFace]);
                listView_MissionList.Items.Add(newListViewItem);
            }
        }

        private void SortMissionList()
        {
            var sortedList = _currentWorkOrder.MissionList.OrderBy(mission => mission.RackName);
            //.ThenBy(mission => mission.Quantity).ToList();

            List<List<WorkOrder.Mission>> rackMissionLists = new List<List<WorkOrder.Mission>>();
            string currentRack = "";
            foreach (var mission in sortedList)
            {
                if (currentRack == null)
                {
                    currentRack = mission.RackName;
                    rackMissionLists.Add(new List<WorkOrder.Mission>());
                }
                else if (mission.RackName != currentRack)
                {
                    currentRack = mission.RackName;
                    rackMissionLists.Add(new List<WorkOrder.Mission>());
                }
                rackMissionLists.Last().Add(mission);
            }

            var finalList = new List<WorkOrder.Mission>();
            for (int i = 0; i < rackMissionLists.Count; i++)
            {
                var rackMissionList = rackMissionLists[i];
                var sortedRackMissionList = new List<WorkOrder.Mission>();
                while (rackMissionList.Count > 0)
                {
                    int[] newsCount = new int[4];
                    int maxIndex = -1;
                    WorkOrder.Mission.RackFace maxFace = WorkOrder.Mission.RackFace.None;
                    foreach (var mission in rackMissionList)
                    {
                        newsCount[0] += (mission.AvailableFaces & WorkOrder.Mission.RackFace.North) > 0 ? 1 : 0;
                        newsCount[1] += (mission.AvailableFaces & WorkOrder.Mission.RackFace.East) > 0 ? 1 : 0;
                        newsCount[2] += (mission.AvailableFaces & WorkOrder.Mission.RackFace.West) > 0 ? 1 : 0;
                        newsCount[3] += (mission.AvailableFaces & WorkOrder.Mission.RackFace.South) > 0 ? 1 : 0;
                        var maxCount = newsCount.Max();
                        maxIndex = Array.IndexOf(newsCount, maxCount);
                    }
                    switch (maxIndex)
                    {
                        case 0:
                            maxFace = WorkOrder.Mission.RackFace.North;
                            break;
                        case 1:
                            maxFace = WorkOrder.Mission.RackFace.East;
                            break;
                        case 2:
                            maxFace = WorkOrder.Mission.RackFace.West;
                            break;
                        case 3:
                            maxFace = WorkOrder.Mission.RackFace.South;
                            break;
                    }
                    rackMissionList = rackMissionList.OrderByDescending(mission => (mission.AvailableFaces & maxFace) > 0).ThenByDescending(mission => mission.Quantity).ToList();
                    int removeIndex = 0;
                    foreach (var mission in rackMissionList)
                    {
                        if ((mission.AvailableFaces & maxFace) > 0)
                        {
                            mission.PickUpFace = maxFace;
                            sortedRackMissionList.Add(mission);
                            removeIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    rackMissionList.RemoveRange(0, removeIndex);
                }
                finalList.AddRange(sortedRackMissionList);
            }
            _currentWorkOrder.MissionList = finalList;
        }

        private void UpdateWorkOrderQueueListView()
        {
            listView_WorkOrderQueue.Items.Clear();
            foreach (var workOrder in WorkOrderQueue)
            {
                var newListViewItem = new ListViewItem();
                newListViewItem.Text = workOrder.UUID;
                newListViewItem.SubItems.Add(workOrder.MissionList.Count.ToString()); ;
                newListViewItem.SubItems.Add(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(workOrder.EnqueuedTimestamp).ToLocalTime().ToString());
                newListViewItem.SubItems.Add(WorkOrder.WorkOrderStateDescription[workOrder.State]);
                listView_WorkOrderQueue.Items.Add(newListViewItem);
            }
            if (_selectedWorkOrder != null)
            {
                var targetItem = listView_WorkOrderQueue.FindItemWithText(_selectedWorkOrder.UUID);
                if (targetItem != null)
                {
                    for (int i = 0; i < listView_WorkOrderQueue.Items.Count; i++)
                    {
                        listView_WorkOrderQueue.Items[i].Selected = false;
                    }
                    targetItem.Focused = true;
                    targetItem.Selected = true;
                    targetItem.EnsureVisible();
                    listView_WorkOrderQueue.Select();
                }
            }
        }
        #endregion

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WorkOrderQueue.Count == 0) return;
            var firstWorkOrder = WorkOrderQueue.First.Value;
            switch (firstWorkOrder.State)
            {
                case WorkOrder.WorkOrderState.Enqueued:
                    var result = _vcsClient.AssignNewWorkOrder(firstWorkOrder);
                    firstWorkOrder.State = _vcsClient.GetWorkOrderState(firstWorkOrder);                   
                    break;
                case WorkOrder.WorkOrderState.Executing:
                    firstWorkOrder.State = _vcsClient.GetWorkOrderState(firstWorkOrder);
                    break;
                case WorkOrder.WorkOrderState.Finished:
                    break;
                case WorkOrder.WorkOrderState.Error:
                    break;
            }
            UpdateWorkOrderQueueListView();
        }
    }
}
