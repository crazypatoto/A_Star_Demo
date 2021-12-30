
namespace A_Star_Demo
{
    partial class AStarDemo
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAGVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testGoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_nodeInfo = new System.Windows.Forms.GroupBox();
            this.textBox_selectedNodeType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_selectedNodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_mapInfo = new System.Windows.Forms.GroupBox();
            this.textBox_mapDIM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_mapSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mapZone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer_mapRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBox_pathPlanning = new System.Windows.Forms.GroupBox();
            this.button_ClearPath = new System.Windows.Forms.Button();
            this.comboBox_planningLayer = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_planningPath = new System.Windows.Forms.TextBox();
            this.button_startPlanning = new System.Windows.Forms.Button();
            this.textBox_goalNode = new System.Windows.Forms.TextBox();
            this.textBox_startNode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_mapViewer = new System.Windows.Forms.PictureBox();
            this.panel_infos = new System.Windows.Forms.Panel();
            this.button_Go = new System.Windows.Forms.Button();
            this.comboBox_rackHeading = new System.Windows.Forms.ComboBox();
            this.button_rotateRackTemp = new System.Windows.Forms.Button();
            this.button_dropOffRack = new System.Windows.Forms.Button();
            this.button_pickUpRack = new System.Windows.Forms.Button();
            this.groupBox_rackInfo = new System.Windows.Forms.GroupBox();
            this.textBox_rackHeading = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_rackHome = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_rackNode = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_rackName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox_agvInfo = new System.Windows.Forms.GroupBox();
            this.textBox_agvRack = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_agvHeading = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_agvStatus = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_agvNode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_agvName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.deadlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.groupBox_nodeInfo.SuspendLayout();
            this.groupBox_mapInfo.SuspendLayout();
            this.groupBox_pathPlanning.SuspendLayout();
            this.tableLayoutPanel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).BeginInit();
            this.panel_infos.SuspendLayout();
            this.groupBox_rackInfo.SuspendLayout();
            this.groupBox_agvInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.testToolStripMenuItem,
            this.testGoToolStripMenuItem,
            this.deadlockToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1450, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.saveMapToolStripMenuItem,
            this.loadMapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Enabled = false;
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMapEditorToolStripMenuItem,
            this.editRacksToolStripMenuItem});
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // openMapEditorToolStripMenuItem
            // 
            this.openMapEditorToolStripMenuItem.Name = "openMapEditorToolStripMenuItem";
            this.openMapEditorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openMapEditorToolStripMenuItem.Text = "Open Map Editor";
            this.openMapEditorToolStripMenuItem.Click += new System.EventHandler(this.editMapToolStripMenuItem_Click);
            // 
            // editRacksToolStripMenuItem
            // 
            this.editRacksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRackToolStripMenuItem,
            this.deleteRackToolStripMenuItem});
            this.editRacksToolStripMenuItem.Name = "editRacksToolStripMenuItem";
            this.editRacksToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editRacksToolStripMenuItem.Text = "Edit Racks";
            // 
            // addRackToolStripMenuItem
            // 
            this.addRackToolStripMenuItem.Name = "addRackToolStripMenuItem";
            this.addRackToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addRackToolStripMenuItem.Text = "Add Rack";
            this.addRackToolStripMenuItem.Click += new System.EventHandler(this.addRackToolStripMenuItem_Click);
            // 
            // deleteRackToolStripMenuItem
            // 
            this.deleteRackToolStripMenuItem.Name = "deleteRackToolStripMenuItem";
            this.deleteRackToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deleteRackToolStripMenuItem.Text = "Delete Rack";
            this.deleteRackToolStripMenuItem.Click += new System.EventHandler(this.deleteRackToolStripMenuItem_Click);
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAGVToolStripMenuItem});
            this.simulationToolStripMenuItem.Enabled = false;
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // addAGVToolStripMenuItem
            // 
            this.addAGVToolStripMenuItem.Name = "addAGVToolStripMenuItem";
            this.addAGVToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.addAGVToolStripMenuItem.Text = "Add AGV";
            this.addAGVToolStripMenuItem.Click += new System.EventHandler(this.addAGVToolStripMenuItem_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.Enabled = false;
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.taskToolStripMenuItem.Text = "Task";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // testGoToolStripMenuItem
            // 
            this.testGoToolStripMenuItem.Name = "testGoToolStripMenuItem";
            this.testGoToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.testGoToolStripMenuItem.Text = "Test Go";
            this.testGoToolStripMenuItem.Click += new System.EventHandler(this.testGoToolStripMenuItem_Click);
            // 
            // groupBox_nodeInfo
            // 
            this.groupBox_nodeInfo.Controls.Add(this.textBox_selectedNodeType);
            this.groupBox_nodeInfo.Controls.Add(this.label2);
            this.groupBox_nodeInfo.Controls.Add(this.textBox_selectedNodeName);
            this.groupBox_nodeInfo.Controls.Add(this.label1);
            this.groupBox_nodeInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_nodeInfo.Location = new System.Drawing.Point(3, 122);
            this.groupBox_nodeInfo.Name = "groupBox_nodeInfo";
            this.groupBox_nodeInfo.Size = new System.Drawing.Size(181, 89);
            this.groupBox_nodeInfo.TabIndex = 3;
            this.groupBox_nodeInfo.TabStop = false;
            this.groupBox_nodeInfo.Text = "Node Information";
            // 
            // textBox_selectedNodeType
            // 
            this.textBox_selectedNodeType.Location = new System.Drawing.Point(69, 51);
            this.textBox_selectedNodeType.Name = "textBox_selectedNodeType";
            this.textBox_selectedNodeType.ReadOnly = true;
            this.textBox_selectedNodeType.Size = new System.Drawing.Size(100, 23);
            this.textBox_selectedNodeType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // textBox_selectedNodeName
            // 
            this.textBox_selectedNodeName.Location = new System.Drawing.Point(69, 22);
            this.textBox_selectedNodeName.Name = "textBox_selectedNodeName";
            this.textBox_selectedNodeName.ReadOnly = true;
            this.textBox_selectedNodeName.Size = new System.Drawing.Size(100, 23);
            this.textBox_selectedNodeName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // groupBox_mapInfo
            // 
            this.groupBox_mapInfo.Controls.Add(this.textBox_mapDIM);
            this.groupBox_mapInfo.Controls.Add(this.label6);
            this.groupBox_mapInfo.Controls.Add(this.textBox_mapSN);
            this.groupBox_mapInfo.Controls.Add(this.label3);
            this.groupBox_mapInfo.Controls.Add(this.textBox_mapZone);
            this.groupBox_mapInfo.Controls.Add(this.label5);
            this.groupBox_mapInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_mapInfo.Location = new System.Drawing.Point(3, 3);
            this.groupBox_mapInfo.Name = "groupBox_mapInfo";
            this.groupBox_mapInfo.Size = new System.Drawing.Size(181, 113);
            this.groupBox_mapInfo.TabIndex = 5;
            this.groupBox_mapInfo.TabStop = false;
            this.groupBox_mapInfo.Text = "Map Information";
            // 
            // textBox_mapDIM
            // 
            this.textBox_mapDIM.Location = new System.Drawing.Point(69, 80);
            this.textBox_mapDIM.Name = "textBox_mapDIM";
            this.textBox_mapDIM.ReadOnly = true;
            this.textBox_mapDIM.Size = new System.Drawing.Size(100, 23);
            this.textBox_mapDIM.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "DIM:";
            // 
            // textBox_mapSN
            // 
            this.textBox_mapSN.Location = new System.Drawing.Point(69, 22);
            this.textBox_mapSN.Name = "textBox_mapSN";
            this.textBox_mapSN.ReadOnly = true;
            this.textBox_mapSN.Size = new System.Drawing.Size(100, 23);
            this.textBox_mapSN.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "S/N:";
            // 
            // textBox_mapZone
            // 
            this.textBox_mapZone.Location = new System.Drawing.Point(69, 51);
            this.textBox_mapZone.Name = "textBox_mapZone";
            this.textBox_mapZone.ReadOnly = true;
            this.textBox_mapZone.Size = new System.Drawing.Size(100, 23);
            this.textBox_mapZone.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Zone:";
            // 
            // timer_mapRefresh
            // 
            this.timer_mapRefresh.Interval = 41;
            this.timer_mapRefresh.Tick += new System.EventHandler(this.timer_mapRefresh_Tick);
            // 
            // groupBox_pathPlanning
            // 
            this.groupBox_pathPlanning.Controls.Add(this.button_ClearPath);
            this.groupBox_pathPlanning.Controls.Add(this.comboBox_planningLayer);
            this.groupBox_pathPlanning.Controls.Add(this.label12);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_planningPath);
            this.groupBox_pathPlanning.Controls.Add(this.button_startPlanning);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_goalNode);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_startNode);
            this.groupBox_pathPlanning.Controls.Add(this.label10);
            this.groupBox_pathPlanning.Controls.Add(this.label11);
            this.groupBox_pathPlanning.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_pathPlanning.Location = new System.Drawing.Point(190, 3);
            this.groupBox_pathPlanning.Name = "groupBox_pathPlanning";
            this.groupBox_pathPlanning.Size = new System.Drawing.Size(174, 718);
            this.groupBox_pathPlanning.TabIndex = 7;
            this.groupBox_pathPlanning.TabStop = false;
            this.groupBox_pathPlanning.Text = "Path Planning";
            // 
            // button_ClearPath
            // 
            this.button_ClearPath.Location = new System.Drawing.Point(6, 149);
            this.button_ClearPath.Name = "button_ClearPath";
            this.button_ClearPath.Size = new System.Drawing.Size(160, 33);
            this.button_ClearPath.TabIndex = 25;
            this.button_ClearPath.Text = "Clear Path";
            this.button_ClearPath.UseVisualStyleBackColor = true;
            this.button_ClearPath.Click += new System.EventHandler(this.button_ClearPath_Click);
            // 
            // comboBox_planningLayer
            // 
            this.comboBox_planningLayer.FormattingEnabled = true;
            this.comboBox_planningLayer.Location = new System.Drawing.Point(68, 80);
            this.comboBox_planningLayer.Name = "comboBox_planningLayer";
            this.comboBox_planningLayer.Size = new System.Drawing.Size(100, 24);
            this.comboBox_planningLayer.TabIndex = 20;
            this.comboBox_planningLayer.SelectedIndexChanged += new System.EventHandler(this.comboBox_planningLayer_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 19;
            this.label12.Text = "Layer:";
            // 
            // textBox_planningPath
            // 
            this.textBox_planningPath.Location = new System.Drawing.Point(6, 188);
            this.textBox_planningPath.Multiline = true;
            this.textBox_planningPath.Name = "textBox_planningPath";
            this.textBox_planningPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_planningPath.Size = new System.Drawing.Size(160, 514);
            this.textBox_planningPath.TabIndex = 24;
            // 
            // button_startPlanning
            // 
            this.button_startPlanning.Location = new System.Drawing.Point(6, 110);
            this.button_startPlanning.Name = "button_startPlanning";
            this.button_startPlanning.Size = new System.Drawing.Size(160, 33);
            this.button_startPlanning.TabIndex = 23;
            this.button_startPlanning.Text = "Start Planning";
            this.button_startPlanning.UseVisualStyleBackColor = true;
            this.button_startPlanning.Click += new System.EventHandler(this.button_startPlanning_Click);
            // 
            // textBox_goalNode
            // 
            this.textBox_goalNode.Location = new System.Drawing.Point(50, 51);
            this.textBox_goalNode.Name = "textBox_goalNode";
            this.textBox_goalNode.ReadOnly = true;
            this.textBox_goalNode.Size = new System.Drawing.Size(116, 23);
            this.textBox_goalNode.TabIndex = 22;
            // 
            // textBox_startNode
            // 
            this.textBox_startNode.Location = new System.Drawing.Point(50, 19);
            this.textBox_startNode.Name = "textBox_startNode";
            this.textBox_startNode.ReadOnly = true;
            this.textBox_startNode.Size = new System.Drawing.Size(116, 23);
            this.textBox_startNode.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Goal:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "Start:";
            // 
            // tableLayoutPanel_main
            // 
            this.tableLayoutPanel_main.ColumnCount = 2;
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_main.Controls.Add(this.pictureBox_mapViewer, 0, 0);
            this.tableLayoutPanel_main.Controls.Add(this.panel_infos, 1, 0);
            this.tableLayoutPanel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_main.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel_main.Name = "tableLayoutPanel_main";
            this.tableLayoutPanel_main.RowCount = 1;
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 733F));
            this.tableLayoutPanel_main.Size = new System.Drawing.Size(1450, 733);
            this.tableLayoutPanel_main.TabIndex = 8;
            // 
            // pictureBox_mapViewer
            // 
            this.pictureBox_mapViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_mapViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_mapViewer.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_mapViewer.Name = "pictureBox_mapViewer";
            this.pictureBox_mapViewer.Size = new System.Drawing.Size(1068, 727);
            this.pictureBox_mapViewer.TabIndex = 2;
            this.pictureBox_mapViewer.TabStop = false;
            this.pictureBox_mapViewer.SizeChanged += new System.EventHandler(this.pictureBox_mapViewer_SizeChanged);
            this.pictureBox_mapViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseDown);
            this.pictureBox_mapViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseMove);
            this.pictureBox_mapViewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseUp);
            this.pictureBox_mapViewer.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseWheel);
            // 
            // panel_infos
            // 
            this.panel_infos.Controls.Add(this.button_Go);
            this.panel_infos.Controls.Add(this.comboBox_rackHeading);
            this.panel_infos.Controls.Add(this.button_rotateRackTemp);
            this.panel_infos.Controls.Add(this.button_dropOffRack);
            this.panel_infos.Controls.Add(this.button_pickUpRack);
            this.panel_infos.Controls.Add(this.groupBox_rackInfo);
            this.panel_infos.Controls.Add(this.groupBox_agvInfo);
            this.panel_infos.Controls.Add(this.groupBox_mapInfo);
            this.panel_infos.Controls.Add(this.groupBox_nodeInfo);
            this.panel_infos.Controls.Add(this.groupBox_pathPlanning);
            this.panel_infos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_infos.Location = new System.Drawing.Point(1077, 3);
            this.panel_infos.Name = "panel_infos";
            this.panel_infos.Size = new System.Drawing.Size(370, 727);
            this.panel_infos.TabIndex = 3;
            // 
            // button_Go
            // 
            this.button_Go.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Go.Location = new System.Drawing.Point(12, 541);
            this.button_Go.Name = "button_Go";
            this.button_Go.Size = new System.Drawing.Size(160, 33);
            this.button_Go.TabIndex = 22;
            this.button_Go.Text = "Go";
            this.button_Go.UseVisualStyleBackColor = true;
            this.button_Go.Click += new System.EventHandler(this.button_Go_Click);
            // 
            // comboBox_rackHeading
            // 
            this.comboBox_rackHeading.FormattingEnabled = true;
            this.comboBox_rackHeading.Location = new System.Drawing.Point(12, 658);
            this.comboBox_rackHeading.Name = "comboBox_rackHeading";
            this.comboBox_rackHeading.Size = new System.Drawing.Size(160, 20);
            this.comboBox_rackHeading.TabIndex = 21;
            // 
            // button_rotateRackTemp
            // 
            this.button_rotateRackTemp.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_rotateRackTemp.Location = new System.Drawing.Point(12, 684);
            this.button_rotateRackTemp.Name = "button_rotateRackTemp";
            this.button_rotateRackTemp.Size = new System.Drawing.Size(160, 33);
            this.button_rotateRackTemp.TabIndex = 12;
            this.button_rotateRackTemp.Text = "Rotate Rack";
            this.button_rotateRackTemp.UseVisualStyleBackColor = true;
            this.button_rotateRackTemp.Click += new System.EventHandler(this.button_rotateRackTemp_Click);
            // 
            // button_dropOffRack
            // 
            this.button_dropOffRack.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_dropOffRack.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_dropOffRack.Location = new System.Drawing.Point(12, 619);
            this.button_dropOffRack.Name = "button_dropOffRack";
            this.button_dropOffRack.Size = new System.Drawing.Size(160, 33);
            this.button_dropOffRack.TabIndex = 11;
            this.button_dropOffRack.Text = "Drop Off Rack";
            this.button_dropOffRack.UseVisualStyleBackColor = true;
            this.button_dropOffRack.Click += new System.EventHandler(this.button_dropOffRack_Click);
            // 
            // button_pickUpRack
            // 
            this.button_pickUpRack.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_pickUpRack.Location = new System.Drawing.Point(12, 580);
            this.button_pickUpRack.Name = "button_pickUpRack";
            this.button_pickUpRack.Size = new System.Drawing.Size(160, 33);
            this.button_pickUpRack.TabIndex = 10;
            this.button_pickUpRack.Text = "Pick Up Rack";
            this.button_pickUpRack.UseVisualStyleBackColor = true;
            this.button_pickUpRack.Click += new System.EventHandler(this.button_pickUpRack_Click);
            // 
            // groupBox_rackInfo
            // 
            this.groupBox_rackInfo.Controls.Add(this.textBox_rackHeading);
            this.groupBox_rackInfo.Controls.Add(this.label17);
            this.groupBox_rackInfo.Controls.Add(this.textBox_rackHome);
            this.groupBox_rackInfo.Controls.Add(this.label18);
            this.groupBox_rackInfo.Controls.Add(this.textBox_rackNode);
            this.groupBox_rackInfo.Controls.Add(this.label19);
            this.groupBox_rackInfo.Controls.Add(this.textBox_rackName);
            this.groupBox_rackInfo.Controls.Add(this.label20);
            this.groupBox_rackInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_rackInfo.Location = new System.Drawing.Point(3, 393);
            this.groupBox_rackInfo.Name = "groupBox_rackInfo";
            this.groupBox_rackInfo.Size = new System.Drawing.Size(181, 142);
            this.groupBox_rackInfo.TabIndex = 9;
            this.groupBox_rackInfo.TabStop = false;
            this.groupBox_rackInfo.Text = "Rack Information";
            // 
            // textBox_rackHeading
            // 
            this.textBox_rackHeading.Location = new System.Drawing.Point(69, 109);
            this.textBox_rackHeading.Name = "textBox_rackHeading";
            this.textBox_rackHeading.ReadOnly = true;
            this.textBox_rackHeading.Size = new System.Drawing.Size(100, 23);
            this.textBox_rackHeading.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 112);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 16);
            this.label17.TabIndex = 10;
            this.label17.Text = "Heading: ";
            // 
            // textBox_rackHome
            // 
            this.textBox_rackHome.Location = new System.Drawing.Point(69, 80);
            this.textBox_rackHome.Name = "textBox_rackHome";
            this.textBox_rackHome.ReadOnly = true;
            this.textBox_rackHome.Size = new System.Drawing.Size(100, 23);
            this.textBox_rackHome.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 83);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 8;
            this.label18.Text = "Home: ";
            // 
            // textBox_rackNode
            // 
            this.textBox_rackNode.Location = new System.Drawing.Point(69, 51);
            this.textBox_rackNode.Name = "textBox_rackNode";
            this.textBox_rackNode.ReadOnly = true;
            this.textBox_rackNode.Size = new System.Drawing.Size(100, 23);
            this.textBox_rackNode.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 16);
            this.label19.TabIndex = 6;
            this.label19.Text = "Node: ";
            // 
            // textBox_rackName
            // 
            this.textBox_rackName.Location = new System.Drawing.Point(69, 22);
            this.textBox_rackName.Name = "textBox_rackName";
            this.textBox_rackName.ReadOnly = true;
            this.textBox_rackName.Size = new System.Drawing.Size(100, 23);
            this.textBox_rackName.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(14, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 4;
            this.label20.Text = "Name: ";
            // 
            // groupBox_agvInfo
            // 
            this.groupBox_agvInfo.Controls.Add(this.textBox_agvRack);
            this.groupBox_agvInfo.Controls.Add(this.label21);
            this.groupBox_agvInfo.Controls.Add(this.textBox_agvHeading);
            this.groupBox_agvInfo.Controls.Add(this.label16);
            this.groupBox_agvInfo.Controls.Add(this.textBox_agvStatus);
            this.groupBox_agvInfo.Controls.Add(this.label15);
            this.groupBox_agvInfo.Controls.Add(this.textBox_agvNode);
            this.groupBox_agvInfo.Controls.Add(this.label14);
            this.groupBox_agvInfo.Controls.Add(this.textBox_agvName);
            this.groupBox_agvInfo.Controls.Add(this.label13);
            this.groupBox_agvInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_agvInfo.Location = new System.Drawing.Point(3, 217);
            this.groupBox_agvInfo.Name = "groupBox_agvInfo";
            this.groupBox_agvInfo.Size = new System.Drawing.Size(181, 170);
            this.groupBox_agvInfo.TabIndex = 8;
            this.groupBox_agvInfo.TabStop = false;
            this.groupBox_agvInfo.Text = "AGV Information";
            // 
            // textBox_agvRack
            // 
            this.textBox_agvRack.Location = new System.Drawing.Point(69, 138);
            this.textBox_agvRack.Name = "textBox_agvRack";
            this.textBox_agvRack.ReadOnly = true;
            this.textBox_agvRack.Size = new System.Drawing.Size(100, 23);
            this.textBox_agvRack.TabIndex = 13;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(23, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 16);
            this.label21.TabIndex = 12;
            this.label21.Text = "Rack: ";
            // 
            // textBox_agvHeading
            // 
            this.textBox_agvHeading.Location = new System.Drawing.Point(69, 109);
            this.textBox_agvHeading.Name = "textBox_agvHeading";
            this.textBox_agvHeading.ReadOnly = true;
            this.textBox_agvHeading.Size = new System.Drawing.Size(100, 23);
            this.textBox_agvHeading.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1, 113);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 16);
            this.label16.TabIndex = 10;
            this.label16.Text = "Heading: ";
            // 
            // textBox_agvStatus
            // 
            this.textBox_agvStatus.Location = new System.Drawing.Point(69, 80);
            this.textBox_agvStatus.Name = "textBox_agvStatus";
            this.textBox_agvStatus.ReadOnly = true;
            this.textBox_agvStatus.Size = new System.Drawing.Size(100, 23);
            this.textBox_agvStatus.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 16);
            this.label15.TabIndex = 8;
            this.label15.Text = "Status: ";
            // 
            // textBox_agvNode
            // 
            this.textBox_agvNode.Location = new System.Drawing.Point(69, 51);
            this.textBox_agvNode.Name = "textBox_agvNode";
            this.textBox_agvNode.ReadOnly = true;
            this.textBox_agvNode.Size = new System.Drawing.Size(100, 23);
            this.textBox_agvNode.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 16);
            this.label14.TabIndex = 6;
            this.label14.Text = "Node: ";
            // 
            // textBox_agvName
            // 
            this.textBox_agvName.Location = new System.Drawing.Point(69, 22);
            this.textBox_agvName.Name = "textBox_agvName";
            this.textBox_agvName.ReadOnly = true;
            this.textBox_agvName.Size = new System.Drawing.Size(100, 23);
            this.textBox_agvName.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 16);
            this.label13.TabIndex = 4;
            this.label13.Text = "Name: ";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 757);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1450, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip";
            // 
            // deadlockToolStripMenuItem
            // 
            this.deadlockToolStripMenuItem.Name = "deadlockToolStripMenuItem";
            this.deadlockToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.deadlockToolStripMenuItem.Text = "Deadlock";
            this.deadlockToolStripMenuItem.Click += new System.EventHandler(this.deadlockToolStripMenuItem_Click);
            // 
            // AStarDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1450, 779);
            this.Controls.Add(this.tableLayoutPanel_main);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AStarDemo";
            this.Text = "A* Demo";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox_nodeInfo.ResumeLayout(false);
            this.groupBox_nodeInfo.PerformLayout();
            this.groupBox_mapInfo.ResumeLayout(false);
            this.groupBox_mapInfo.PerformLayout();
            this.groupBox_pathPlanning.ResumeLayout(false);
            this.groupBox_pathPlanning.PerformLayout();
            this.tableLayoutPanel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).EndInit();
            this.panel_infos.ResumeLayout(false);
            this.groupBox_rackInfo.ResumeLayout(false);
            this.groupBox_rackInfo.PerformLayout();
            this.groupBox_agvInfo.ResumeLayout(false);
            this.groupBox_agvInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox_mapViewer;
        private System.Windows.Forms.GroupBox groupBox_nodeInfo;
        private System.Windows.Forms.TextBox textBox_selectedNodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_selectedNodeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_mapInfo;
        private System.Windows.Forms.TextBox textBox_mapSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_mapZone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_mapDIM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_mapRefresh;
        private System.Windows.Forms.GroupBox groupBox_pathPlanning;
        private System.Windows.Forms.TextBox textBox_goalNode;
        private System.Windows.Forms.TextBox textBox_startNode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_planningPath;
        private System.Windows.Forms.Button button_startPlanning;
        public  System.Windows.Forms.ComboBox comboBox_planningLayer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_ClearPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_main;
        private System.Windows.Forms.Panel panel_infos;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox_agvInfo;
        private System.Windows.Forms.TextBox textBox_agvHeading;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_agvStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_agvNode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_agvName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAGVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMapEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editRacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRackToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox_rackInfo;
        private System.Windows.Forms.TextBox textBox_rackHeading;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_rackHome;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_rackNode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_rackName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_agvRack;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ToolStripMenuItem deleteRackToolStripMenuItem;
        private System.Windows.Forms.Button button_dropOffRack;
        private System.Windows.Forms.Button button_pickUpRack;
        public System.Windows.Forms.ComboBox comboBox_rackHeading;
        private System.Windows.Forms.Button button_rotateRackTemp;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.Button button_Go;
        private System.Windows.Forms.ToolStripMenuItem testGoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deadlockToolStripMenuItem;
    }
}

