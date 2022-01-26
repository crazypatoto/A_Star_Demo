
namespace WMS
{
    partial class WMS
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMS));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialsManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_Map = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_Events = new System.Windows.Forms.GroupBox();
            this.textBox_Event = new System.Windows.Forms.TextBox();
            this.groupBox_MapViewer = new System.Windows.Forms.GroupBox();
            this.pictureBox_MapViewer = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel_WorkOrder = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_WorkOrder = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_MaterialList = new System.Windows.Forms.ComboBox();
            this.comboBox_Destination = new System.Windows.Forms.ComboBox();
            this.numericUpDown_Quantity = new System.Windows.Forms.NumericUpDown();
            this.button_AddToWorkOrder = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_WorkOrderUUID = new System.Windows.Forms.TextBox();
            this.button_LoadWorkOderTemplate = new System.Windows.Forms.Button();
            this.comboBox_WorkOrderTemplates = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_NewWorkOrder = new System.Windows.Forms.Button();
            this.listView_MissionList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_AddToQueue = new System.Windows.Forms.Button();
            this.button_DeleteSelectedMission = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.listView_WorkOrderQueue = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_RemoveWorkOrderFromQueue = new System.Windows.Forms.Button();
            this.button_EditWorkOrder = new System.Windows.Forms.Button();
            this.button_MoveWorkOrderUp = new System.Windows.Forms.Button();
            this.button_MoveWorkOrderDown = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ConnectionState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ServerIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_RefreshMap = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.tableLayoutPanel_Map.SuspendLayout();
            this.groupBox_Events.SuspendLayout();
            this.groupBox_MapViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MapViewer)).BeginInit();
            this.tableLayoutPanel_WorkOrder.SuspendLayout();
            this.groupBox_WorkOrder.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quantity)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.inventoryManagementToolStripMenuItem,
            this.materialsManagementToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1328, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.connectionToolStripMenuItem.Text = "VCS連線";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.connectToolStripMenuItem.Text = "連接";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.disconnectToolStripMenuItem.Text = "斷開連接";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // inventoryManagementToolStripMenuItem
            // 
            this.inventoryManagementToolStripMenuItem.Name = "inventoryManagementToolStripMenuItem";
            this.inventoryManagementToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.inventoryManagementToolStripMenuItem.Text = "庫存管理";
            this.inventoryManagementToolStripMenuItem.Click += new System.EventHandler(this.inventoryManagementToolStripMenuItem_Click);
            // 
            // materialsManagementToolStripMenuItem
            // 
            this.materialsManagementToolStripMenuItem.Name = "materialsManagementToolStripMenuItem";
            this.materialsManagementToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.materialsManagementToolStripMenuItem.Text = "物料管理";
            this.materialsManagementToolStripMenuItem.Click += new System.EventHandler(this.materialsManagementToolStripMenuItem_Click);
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Controls.Add(this.tableLayoutPanel_Map, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.tableLayoutPanel_WorkOrder, 1, 0);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 1;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1328, 671);
            this.tableLayoutPanel_Main.TabIndex = 1;
            // 
            // tableLayoutPanel_Map
            // 
            this.tableLayoutPanel_Map.ColumnCount = 1;
            this.tableLayoutPanel_Map.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Map.Controls.Add(this.groupBox_Events, 0, 1);
            this.tableLayoutPanel_Map.Controls.Add(this.groupBox_MapViewer, 0, 0);
            this.tableLayoutPanel_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Map.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_Map.Name = "tableLayoutPanel_Map";
            this.tableLayoutPanel_Map.RowCount = 2;
            this.tableLayoutPanel_Map.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Map.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel_Map.Size = new System.Drawing.Size(658, 665);
            this.tableLayoutPanel_Map.TabIndex = 0;
            // 
            // groupBox_Events
            // 
            this.groupBox_Events.Controls.Add(this.textBox_Event);
            this.groupBox_Events.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Events.Location = new System.Drawing.Point(3, 568);
            this.groupBox_Events.Name = "groupBox_Events";
            this.groupBox_Events.Size = new System.Drawing.Size(652, 94);
            this.groupBox_Events.TabIndex = 1;
            this.groupBox_Events.TabStop = false;
            this.groupBox_Events.Text = "事件";
            // 
            // textBox_Event
            // 
            this.textBox_Event.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Event.Location = new System.Drawing.Point(3, 19);
            this.textBox_Event.Multiline = true;
            this.textBox_Event.Name = "textBox_Event";
            this.textBox_Event.ReadOnly = true;
            this.textBox_Event.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Event.Size = new System.Drawing.Size(646, 72);
            this.textBox_Event.TabIndex = 0;
            // 
            // groupBox_MapViewer
            // 
            this.groupBox_MapViewer.Controls.Add(this.pictureBox_MapViewer);
            this.groupBox_MapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_MapViewer.Location = new System.Drawing.Point(3, 3);
            this.groupBox_MapViewer.Name = "groupBox_MapViewer";
            this.groupBox_MapViewer.Size = new System.Drawing.Size(652, 559);
            this.groupBox_MapViewer.TabIndex = 2;
            this.groupBox_MapViewer.TabStop = false;
            this.groupBox_MapViewer.Text = "地圖總覽";
            // 
            // pictureBox_MapViewer
            // 
            this.pictureBox_MapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_MapViewer.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_MapViewer.Image")));
            this.pictureBox_MapViewer.Location = new System.Drawing.Point(3, 19);
            this.pictureBox_MapViewer.Name = "pictureBox_MapViewer";
            this.pictureBox_MapViewer.Size = new System.Drawing.Size(646, 537);
            this.pictureBox_MapViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_MapViewer.TabIndex = 0;
            this.pictureBox_MapViewer.TabStop = false;
            this.pictureBox_MapViewer.SizeChanged += new System.EventHandler(this.pictureBox_MapViewer_SizeChanged);
            // 
            // tableLayoutPanel_WorkOrder
            // 
            this.tableLayoutPanel_WorkOrder.ColumnCount = 2;
            this.tableLayoutPanel_WorkOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel_WorkOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel_WorkOrder.Controls.Add(this.groupBox_WorkOrder, 0, 0);
            this.tableLayoutPanel_WorkOrder.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel_WorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_WorkOrder.Location = new System.Drawing.Point(667, 3);
            this.tableLayoutPanel_WorkOrder.Name = "tableLayoutPanel_WorkOrder";
            this.tableLayoutPanel_WorkOrder.RowCount = 2;
            this.tableLayoutPanel_WorkOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_WorkOrder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_WorkOrder.Size = new System.Drawing.Size(658, 665);
            this.tableLayoutPanel_WorkOrder.TabIndex = 1;
            // 
            // groupBox_WorkOrder
            // 
            this.tableLayoutPanel_WorkOrder.SetColumnSpan(this.groupBox_WorkOrder, 2);
            this.groupBox_WorkOrder.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_WorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_WorkOrder.Location = new System.Drawing.Point(3, 3);
            this.groupBox_WorkOrder.Name = "groupBox_WorkOrder";
            this.groupBox_WorkOrder.Size = new System.Drawing.Size(652, 326);
            this.groupBox_WorkOrder.TabIndex = 0;
            this.groupBox_WorkOrder.TabStop = false;
            this.groupBox_WorkOrder.Text = "工單任務編輯";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView_MissionList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_AddToQueue, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_DeleteSelectedMission, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 304);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_MaterialList, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_Destination, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown_Quantity, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_AddToWorkOrder, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 40);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(640, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "物料名稱：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的地：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "數量：";
            // 
            // comboBox_MaterialList
            // 
            this.comboBox_MaterialList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_MaterialList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_MaterialList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_MaterialList.FormattingEnabled = true;
            this.comboBox_MaterialList.Location = new System.Drawing.Point(77, 3);
            this.comboBox_MaterialList.Name = "comboBox_MaterialList";
            this.comboBox_MaterialList.Size = new System.Drawing.Size(130, 24);
            this.comboBox_MaterialList.TabIndex = 4;
            this.comboBox_MaterialList.SelectedIndexChanged += new System.EventHandler(this.comboBox_MaterialList_SelectedIndexChanged);
            // 
            // comboBox_Destination
            // 
            this.comboBox_Destination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox_Destination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Destination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Destination.FormattingEnabled = true;
            this.comboBox_Destination.Location = new System.Drawing.Point(275, 3);
            this.comboBox_Destination.Name = "comboBox_Destination";
            this.comboBox_Destination.Size = new System.Drawing.Size(130, 24);
            this.comboBox_Destination.TabIndex = 5;
            // 
            // numericUpDown_Quantity
            // 
            this.numericUpDown_Quantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown_Quantity.Location = new System.Drawing.Point(461, 3);
            this.numericUpDown_Quantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Quantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Quantity.Name = "numericUpDown_Quantity";
            this.numericUpDown_Quantity.Size = new System.Drawing.Size(84, 23);
            this.numericUpDown_Quantity.TabIndex = 6;
            this.numericUpDown_Quantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_AddToWorkOrder
            // 
            this.button_AddToWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_AddToWorkOrder.Location = new System.Drawing.Point(551, 3);
            this.button_AddToWorkOrder.Name = "button_AddToWorkOrder";
            this.button_AddToWorkOrder.Size = new System.Drawing.Size(86, 23);
            this.button_AddToWorkOrder.TabIndex = 7;
            this.button_AddToWorkOrder.Text = "加入工單";
            this.button_AddToWorkOrder.UseVisualStyleBackColor = true;
            this.button_AddToWorkOrder.Click += new System.EventHandler(this.button_AddToWorkOrder_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox_WorkOrderUUID, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button_LoadWorkOderTemplate, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBox_WorkOrderTemplates, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.button_NewWorkOrder, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(640, 31);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "工單編號：";
            // 
            // textBox_WorkOrderUUID
            // 
            this.textBox_WorkOrderUUID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_WorkOrderUUID.Location = new System.Drawing.Point(77, 3);
            this.textBox_WorkOrderUUID.Name = "textBox_WorkOrderUUID";
            this.textBox_WorkOrderUUID.ReadOnly = true;
            this.textBox_WorkOrderUUID.Size = new System.Drawing.Size(149, 23);
            this.textBox_WorkOrderUUID.TabIndex = 4;
            // 
            // button_LoadWorkOderTemplate
            // 
            this.button_LoadWorkOderTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LoadWorkOderTemplate.Location = new System.Drawing.Point(575, 3);
            this.button_LoadWorkOderTemplate.Name = "button_LoadWorkOderTemplate";
            this.button_LoadWorkOderTemplate.Size = new System.Drawing.Size(62, 25);
            this.button_LoadWorkOderTemplate.TabIndex = 2;
            this.button_LoadWorkOderTemplate.Text = "載入";
            this.button_LoadWorkOderTemplate.UseVisualStyleBackColor = true;
            this.button_LoadWorkOderTemplate.Click += new System.EventHandler(this.button_LoadWorkOderTemplate_Click);
            // 
            // comboBox_WorkOrderTemplates
            // 
            this.comboBox_WorkOrderTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_WorkOrderTemplates.FormattingEnabled = true;
            this.comboBox_WorkOrderTemplates.Location = new System.Drawing.Point(420, 3);
            this.comboBox_WorkOrderTemplates.Name = "comboBox_WorkOrderTemplates";
            this.comboBox_WorkOrderTemplates.Size = new System.Drawing.Size(149, 24);
            this.comboBox_WorkOrderTemplates.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "選擇現有工單模板：";
            // 
            // button_NewWorkOrder
            // 
            this.button_NewWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NewWorkOrder.Location = new System.Drawing.Point(232, 3);
            this.button_NewWorkOrder.Name = "button_NewWorkOrder";
            this.button_NewWorkOrder.Size = new System.Drawing.Size(60, 25);
            this.button_NewWorkOrder.TabIndex = 5;
            this.button_NewWorkOrder.Text = "新工單";
            this.button_NewWorkOrder.UseVisualStyleBackColor = true;
            this.button_NewWorkOrder.Click += new System.EventHandler(this.button_NewWorkOrder_Click);
            // 
            // listView_MissionList
            // 
            this.listView_MissionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader13,
            this.columnHeader14});
            this.listView_MissionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_MissionList.FullRowSelect = true;
            this.listView_MissionList.GridLines = true;
            this.listView_MissionList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_MissionList.HideSelection = false;
            this.listView_MissionList.Location = new System.Drawing.Point(3, 75);
            this.listView_MissionList.Name = "listView_MissionList";
            this.listView_MissionList.Size = new System.Drawing.Size(640, 168);
            this.listView_MissionList.TabIndex = 2;
            this.listView_MissionList.UseCompatibleStateImageBehavior = false;
            this.listView_MissionList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "物料名稱";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "所屬料架";
            this.columnHeader4.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "目的地";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "數量";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "可供取料方向";
            this.columnHeader13.Width = 95;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "目前取料方向";
            this.columnHeader14.Width = 95;
            // 
            // button_AddToQueue
            // 
            this.button_AddToQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_AddToQueue.Location = new System.Drawing.Point(3, 278);
            this.button_AddToQueue.Name = "button_AddToQueue";
            this.button_AddToQueue.Size = new System.Drawing.Size(640, 23);
            this.button_AddToQueue.TabIndex = 3;
            this.button_AddToQueue.Text = "新增目前工單到佇列";
            this.button_AddToQueue.UseVisualStyleBackColor = true;
            this.button_AddToQueue.Click += new System.EventHandler(this.button_AddToQueue_Click);
            // 
            // button_DeleteSelectedMission
            // 
            this.button_DeleteSelectedMission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DeleteSelectedMission.Location = new System.Drawing.Point(3, 249);
            this.button_DeleteSelectedMission.Name = "button_DeleteSelectedMission";
            this.button_DeleteSelectedMission.Size = new System.Drawing.Size(640, 23);
            this.button_DeleteSelectedMission.TabIndex = 4;
            this.button_DeleteSelectedMission.Text = "移除選取任務";
            this.button_DeleteSelectedMission.UseVisualStyleBackColor = true;
            this.button_DeleteSelectedMission.Click += new System.EventHandler(this.button_DeleteSelectedMission_Click);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel_WorkOrder.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 335);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 327);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工單佇列";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Controls.Add(this.listView_WorkOrderQueue, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button_RemoveWorkOrderFromQueue, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.button_EditWorkOrder, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.button_MoveWorkOrderUp, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.button_MoveWorkOrderDown, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(646, 305);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // listView_WorkOrderQueue
            // 
            this.listView_WorkOrderQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader9});
            this.listView_WorkOrderQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_WorkOrderQueue.FullRowSelect = true;
            this.listView_WorkOrderQueue.HideSelection = false;
            this.listView_WorkOrderQueue.Location = new System.Drawing.Point(3, 3);
            this.listView_WorkOrderQueue.MultiSelect = false;
            this.listView_WorkOrderQueue.Name = "listView_WorkOrderQueue";
            this.tableLayoutPanel4.SetRowSpan(this.listView_WorkOrderQueue, 2);
            this.listView_WorkOrderQueue.Size = new System.Drawing.Size(610, 236);
            this.listView_WorkOrderQueue.TabIndex = 0;
            this.listView_WorkOrderQueue.UseCompatibleStateImageBehavior = false;
            this.listView_WorkOrderQueue.View = System.Windows.Forms.View.Details;
            this.listView_WorkOrderQueue.SelectedIndexChanged += new System.EventHandler(this.listView_WorkOrderQueue_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "工單編號";
            this.columnHeader5.Width = 283;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "任務數量";
            this.columnHeader6.Width = 67;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "佇列時間";
            this.columnHeader7.Width = 159;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "狀態";
            this.columnHeader9.Width = 77;
            // 
            // button_RemoveWorkOrderFromQueue
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.button_RemoveWorkOrderFromQueue, 2);
            this.button_RemoveWorkOrderFromQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RemoveWorkOrderFromQueue.Location = new System.Drawing.Point(3, 274);
            this.button_RemoveWorkOrderFromQueue.Name = "button_RemoveWorkOrderFromQueue";
            this.button_RemoveWorkOrderFromQueue.Size = new System.Drawing.Size(640, 28);
            this.button_RemoveWorkOrderFromQueue.TabIndex = 2;
            this.button_RemoveWorkOrderFromQueue.Text = "從佇列中移除";
            this.button_RemoveWorkOrderFromQueue.UseVisualStyleBackColor = true;
            this.button_RemoveWorkOrderFromQueue.Click += new System.EventHandler(this.button_RemoveWorkOrderFromQueue_Click);
            // 
            // button_EditWorkOrder
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.button_EditWorkOrder, 2);
            this.button_EditWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_EditWorkOrder.Location = new System.Drawing.Point(3, 245);
            this.button_EditWorkOrder.Name = "button_EditWorkOrder";
            this.button_EditWorkOrder.Size = new System.Drawing.Size(640, 23);
            this.button_EditWorkOrder.TabIndex = 1;
            this.button_EditWorkOrder.Text = "移到工單任務編輯區";
            this.button_EditWorkOrder.UseVisualStyleBackColor = true;
            this.button_EditWorkOrder.Click += new System.EventHandler(this.button_EditWorkOrder_Click);
            // 
            // button_MoveWorkOrderUp
            // 
            this.button_MoveWorkOrderUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveWorkOrderUp.Location = new System.Drawing.Point(619, 3);
            this.button_MoveWorkOrderUp.Name = "button_MoveWorkOrderUp";
            this.button_MoveWorkOrderUp.Size = new System.Drawing.Size(24, 115);
            this.button_MoveWorkOrderUp.TabIndex = 3;
            this.button_MoveWorkOrderUp.Text = "上移";
            this.button_MoveWorkOrderUp.UseVisualStyleBackColor = true;
            this.button_MoveWorkOrderUp.Click += new System.EventHandler(this.button_MoveWorkOrderUp_Click);
            // 
            // button_MoveWorkOrderDown
            // 
            this.button_MoveWorkOrderDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveWorkOrderDown.Location = new System.Drawing.Point(619, 124);
            this.button_MoveWorkOrderDown.Name = "button_MoveWorkOrderDown";
            this.button_MoveWorkOrderDown.Size = new System.Drawing.Size(24, 115);
            this.button_MoveWorkOrderDown.TabIndex = 4;
            this.button_MoveWorkOrderDown.Text = "下移";
            this.button_MoveWorkOrderDown.UseVisualStyleBackColor = true;
            this.button_MoveWorkOrderDown.Click += new System.EventHandler(this.button_MoveWorkOrderDown_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_ConnectionState,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel_ServerIP});
            this.statusStrip.Location = new System.Drawing.Point(0, 695);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1328, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "連線狀態：";
            // 
            // toolStripStatusLabel_ConnectionState
            // 
            this.toolStripStatusLabel_ConnectionState.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel_ConnectionState.Name = "toolStripStatusLabel_ConnectionState";
            this.toolStripStatusLabel_ConnectionState.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel_ConnectionState.Text = "未連線";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel3.Text = "    ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel4.Text = "VCS伺服器位置:";
            // 
            // toolStripStatusLabel_ServerIP
            // 
            this.toolStripStatusLabel_ServerIP.Name = "toolStripStatusLabel_ServerIP";
            this.toolStripStatusLabel_ServerIP.Size = new System.Drawing.Size(110, 17);
            this.toolStripStatusLabel_ServerIP.Text = "192.168.0.102:987";
            // 
            // timer_RefreshMap
            // 
            this.timer_RefreshMap.Interval = 33;
            this.timer_RefreshMap.Tick += new System.EventHandler(this.timer_RefreshMap_Tick);
            // 
            // WMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1328, 717);
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "WMS";
            this.Text = "WMS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WMS_FormClosed);
            this.Shown += new System.EventHandler(this.WMS_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.tableLayoutPanel_Map.ResumeLayout(false);
            this.groupBox_Events.ResumeLayout(false);
            this.groupBox_Events.PerformLayout();
            this.groupBox_MapViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MapViewer)).EndInit();
            this.tableLayoutPanel_WorkOrder.ResumeLayout(false);
            this.groupBox_WorkOrder.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quantity)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Map;
        private System.Windows.Forms.PictureBox pictureBox_MapViewer;
        private System.Windows.Forms.GroupBox groupBox_Events;
        private System.Windows.Forms.TextBox textBox_Event;
        private System.Windows.Forms.GroupBox groupBox_MapViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_WorkOrder;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem inventoryManagementToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox_WorkOrder;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ConnectionState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ServerIP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_MaterialList;
        private System.Windows.Forms.ComboBox comboBox_Destination;
        private System.Windows.Forms.NumericUpDown numericUpDown_Quantity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_WorkOrderTemplates;
        private System.Windows.Forms.Button button_LoadWorkOderTemplate;
        private System.Windows.Forms.ListView listView_MissionList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_WorkOrderUUID;
        private System.Windows.Forms.Button button_NewWorkOrder;
        private System.Windows.Forms.Button button_AddToQueue;
        private System.Windows.Forms.Button button_DeleteSelectedMission;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ListView listView_WorkOrderQueue;
        private System.Windows.Forms.Button button_RemoveWorkOrderFromQueue;
        private System.Windows.Forms.Button button_EditWorkOrder;
        private System.Windows.Forms.Button button_MoveWorkOrderUp;
        private System.Windows.Forms.Button button_MoveWorkOrderDown;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ToolStripMenuItem materialsManagementToolStripMenuItem;
        private System.Windows.Forms.Timer timer_RefreshMap;
        private System.Windows.Forms.Button button_AddToWorkOrder;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
    }
}

