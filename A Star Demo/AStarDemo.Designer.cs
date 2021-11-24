
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
            this.pictureBox_mapViewer = new System.Windows.Forms.PictureBox();
            this.groupBox_nodeInfo = new System.Windows.Forms.GroupBox();
            this.textBox_selectedNodeType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_selectedNodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_nodeTypeEditor = new System.Windows.Forms.GroupBox();
            this.button_startEditingNode = new System.Windows.Forms.Button();
            this.comboBox_types = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_mapInfo = new System.Windows.Forms.GroupBox();
            this.textBox_mapDIM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_mapSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mapZone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer_mapRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBox_edgeConstraintsEditor = new System.Windows.Forms.GroupBox();
            this.button_deleteLayer = new System.Windows.Forms.Button();
            this.button_addLayer = new System.Windows.Forms.Button();
            this.comboBox_constraintLayers = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_passingRestrictions = new System.Windows.Forms.ComboBox();
            this.button_startEditingEdge = new System.Windows.Forms.Button();
            this.checkBox_showConstraints = new System.Windows.Forms.CheckBox();
            this.textBox_edgeNode2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_edgeNode1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox_pathPlanning = new System.Windows.Forms.GroupBox();
            this.comboBox_planningLayer = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_planningPath = new System.Windows.Forms.TextBox();
            this.button_startPlanning = new System.Windows.Forms.Button();
            this.textBox_goalNode = new System.Windows.Forms.TextBox();
            this.textBox_startNode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox_disallowTurning = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).BeginInit();
            this.groupBox_nodeInfo.SuspendLayout();
            this.groupBox_nodeTypeEditor.SuspendLayout();
            this.groupBox_mapInfo.SuspendLayout();
            this.groupBox_edgeConstraintsEditor.SuspendLayout();
            this.groupBox_pathPlanning.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1210, 24);
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
            // pictureBox_mapViewer
            // 
            this.pictureBox_mapViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_mapViewer.Location = new System.Drawing.Point(12, 27);
            this.pictureBox_mapViewer.Name = "pictureBox_mapViewer";
            this.pictureBox_mapViewer.Size = new System.Drawing.Size(824, 573);
            this.pictureBox_mapViewer.TabIndex = 2;
            this.pictureBox_mapViewer.TabStop = false;
            this.pictureBox_mapViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseDown);
            this.pictureBox_mapViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_mapViewer_MouseMove);
            // 
            // groupBox_nodeInfo
            // 
            this.groupBox_nodeInfo.Controls.Add(this.textBox_selectedNodeType);
            this.groupBox_nodeInfo.Controls.Add(this.label2);
            this.groupBox_nodeInfo.Controls.Add(this.textBox_selectedNodeName);
            this.groupBox_nodeInfo.Controls.Add(this.label1);
            this.groupBox_nodeInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_nodeInfo.Location = new System.Drawing.Point(841, 146);
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
            this.label2.Size = new System.Drawing.Size(39, 16);
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
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // groupBox_nodeTypeEditor
            // 
            this.groupBox_nodeTypeEditor.Controls.Add(this.checkBox_disallowTurning);
            this.groupBox_nodeTypeEditor.Controls.Add(this.button_startEditingNode);
            this.groupBox_nodeTypeEditor.Controls.Add(this.comboBox_types);
            this.groupBox_nodeTypeEditor.Controls.Add(this.label4);
            this.groupBox_nodeTypeEditor.Enabled = false;
            this.groupBox_nodeTypeEditor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_nodeTypeEditor.Location = new System.Drawing.Point(841, 241);
            this.groupBox_nodeTypeEditor.Name = "groupBox_nodeTypeEditor";
            this.groupBox_nodeTypeEditor.Size = new System.Drawing.Size(181, 116);
            this.groupBox_nodeTypeEditor.TabIndex = 4;
            this.groupBox_nodeTypeEditor.TabStop = false;
            this.groupBox_nodeTypeEditor.Text = "Node Type Editor";
            // 
            // button_startEditingNode
            // 
            this.button_startEditingNode.Location = new System.Drawing.Point(17, 78);
            this.button_startEditingNode.Name = "button_startEditingNode";
            this.button_startEditingNode.Size = new System.Drawing.Size(152, 30);
            this.button_startEditingNode.TabIndex = 3;
            this.button_startEditingNode.Text = "Strat Editing";
            this.button_startEditingNode.UseVisualStyleBackColor = true;
            this.button_startEditingNode.Click += new System.EventHandler(this.button_startEditingNode_Click);
            // 
            // comboBox_types
            // 
            this.comboBox_types.FormattingEnabled = true;
            this.comboBox_types.Location = new System.Drawing.Point(69, 22);
            this.comboBox_types.Name = "comboBox_types";
            this.comboBox_types.Size = new System.Drawing.Size(100, 24);
            this.comboBox_types.TabIndex = 1;
            this.comboBox_types.SelectedIndexChanged += new System.EventHandler(this.comboBox_types_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type: ";
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
            this.groupBox_mapInfo.Location = new System.Drawing.Point(841, 27);
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
            this.label6.Size = new System.Drawing.Size(35, 16);
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
            this.label3.Size = new System.Drawing.Size(33, 16);
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
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Zone:";
            // 
            // timer_mapRefresh
            // 
            this.timer_mapRefresh.Enabled = true;
            this.timer_mapRefresh.Interval = 41;
            this.timer_mapRefresh.Tick += new System.EventHandler(this.timer_mapRefresh_Tick);
            // 
            // groupBox_edgeConstraintsEditor
            // 
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.button_deleteLayer);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.button_addLayer);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.comboBox_constraintLayers);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.label9);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.comboBox_passingRestrictions);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.button_startEditingEdge);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.checkBox_showConstraints);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.textBox_edgeNode2);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.label8);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.textBox_edgeNode1);
            this.groupBox_edgeConstraintsEditor.Controls.Add(this.label7);
            this.groupBox_edgeConstraintsEditor.Enabled = false;
            this.groupBox_edgeConstraintsEditor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_edgeConstraintsEditor.Location = new System.Drawing.Point(842, 363);
            this.groupBox_edgeConstraintsEditor.Name = "groupBox_edgeConstraintsEditor";
            this.groupBox_edgeConstraintsEditor.Size = new System.Drawing.Size(181, 237);
            this.groupBox_edgeConstraintsEditor.TabIndex = 6;
            this.groupBox_edgeConstraintsEditor.TabStop = false;
            this.groupBox_edgeConstraintsEditor.Text = "Edge Constraints Editor";
            // 
            // button_deleteLayer
            // 
            this.button_deleteLayer.Location = new System.Drawing.Point(94, 142);
            this.button_deleteLayer.Name = "button_deleteLayer";
            this.button_deleteLayer.Size = new System.Drawing.Size(75, 23);
            this.button_deleteLayer.TabIndex = 18;
            this.button_deleteLayer.Text = "Delete";
            this.button_deleteLayer.UseVisualStyleBackColor = true;
            this.button_deleteLayer.Click += new System.EventHandler(this.button_deleteLayer_Click);
            // 
            // button_addLayer
            // 
            this.button_addLayer.Location = new System.Drawing.Point(9, 142);
            this.button_addLayer.Name = "button_addLayer";
            this.button_addLayer.Size = new System.Drawing.Size(75, 23);
            this.button_addLayer.TabIndex = 17;
            this.button_addLayer.Text = "Add";
            this.button_addLayer.UseVisualStyleBackColor = true;
            this.button_addLayer.Click += new System.EventHandler(this.button_addLayer_Click);
            // 
            // comboBox_constraintLayers
            // 
            this.comboBox_constraintLayers.FormattingEnabled = true;
            this.comboBox_constraintLayers.Location = new System.Drawing.Point(69, 110);
            this.comboBox_constraintLayers.Name = "comboBox_constraintLayers";
            this.comboBox_constraintLayers.Size = new System.Drawing.Size(100, 24);
            this.comboBox_constraintLayers.TabIndex = 16;
            this.comboBox_constraintLayers.SelectedIndexChanged += new System.EventHandler(this.comboBox_constraintLayers_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Layer:";
            // 
            // comboBox_passingRestrictions
            // 
            this.comboBox_passingRestrictions.FormattingEnabled = true;
            this.comboBox_passingRestrictions.Location = new System.Drawing.Point(9, 171);
            this.comboBox_passingRestrictions.Name = "comboBox_passingRestrictions";
            this.comboBox_passingRestrictions.Size = new System.Drawing.Size(160, 24);
            this.comboBox_passingRestrictions.TabIndex = 14;
            // 
            // button_startEditingEdge
            // 
            this.button_startEditingEdge.Location = new System.Drawing.Point(9, 201);
            this.button_startEditingEdge.Name = "button_startEditingEdge";
            this.button_startEditingEdge.Size = new System.Drawing.Size(166, 30);
            this.button_startEditingEdge.TabIndex = 13;
            this.button_startEditingEdge.Text = "Strat Editing";
            this.button_startEditingEdge.UseVisualStyleBackColor = true;
            this.button_startEditingEdge.Click += new System.EventHandler(this.button_startEditingEdge_Click);
            // 
            // checkBox_showConstraints
            // 
            this.checkBox_showConstraints.AutoSize = true;
            this.checkBox_showConstraints.Location = new System.Drawing.Point(9, 26);
            this.checkBox_showConstraints.Name = "checkBox_showConstraints";
            this.checkBox_showConstraints.Size = new System.Drawing.Size(171, 20);
            this.checkBox_showConstraints.TabIndex = 10;
            this.checkBox_showConstraints.Text = "Show Constraints on Map";
            this.checkBox_showConstraints.UseVisualStyleBackColor = true;
            // 
            // textBox_edgeNode2
            // 
            this.textBox_edgeNode2.Location = new System.Drawing.Point(69, 81);
            this.textBox_edgeNode2.Name = "textBox_edgeNode2";
            this.textBox_edgeNode2.ReadOnly = true;
            this.textBox_edgeNode2.Size = new System.Drawing.Size(100, 23);
            this.textBox_edgeNode2.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Node 2:";
            // 
            // textBox_edgeNode1
            // 
            this.textBox_edgeNode1.Location = new System.Drawing.Point(69, 52);
            this.textBox_edgeNode1.Name = "textBox_edgeNode1";
            this.textBox_edgeNode1.ReadOnly = true;
            this.textBox_edgeNode1.Size = new System.Drawing.Size(100, 23);
            this.textBox_edgeNode1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Node 1: ";
            // 
            // groupBox_pathPlanning
            // 
            this.groupBox_pathPlanning.Controls.Add(this.comboBox_planningLayer);
            this.groupBox_pathPlanning.Controls.Add(this.label12);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_planningPath);
            this.groupBox_pathPlanning.Controls.Add(this.button_startPlanning);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_goalNode);
            this.groupBox_pathPlanning.Controls.Add(this.textBox_startNode);
            this.groupBox_pathPlanning.Controls.Add(this.label10);
            this.groupBox_pathPlanning.Controls.Add(this.label11);
            this.groupBox_pathPlanning.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_pathPlanning.Location = new System.Drawing.Point(1028, 30);
            this.groupBox_pathPlanning.Name = "groupBox_pathPlanning";
            this.groupBox_pathPlanning.Size = new System.Drawing.Size(174, 570);
            this.groupBox_pathPlanning.TabIndex = 7;
            this.groupBox_pathPlanning.TabStop = false;
            this.groupBox_pathPlanning.Text = "Path Planning";
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
            this.label12.Size = new System.Drawing.Size(41, 16);
            this.label12.TabIndex = 19;
            this.label12.Text = "Layer:";
            // 
            // textBox_planningPath
            // 
            this.textBox_planningPath.Location = new System.Drawing.Point(6, 152);
            this.textBox_planningPath.Multiline = true;
            this.textBox_planningPath.Name = "textBox_planningPath";
            this.textBox_planningPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_planningPath.Size = new System.Drawing.Size(160, 412);
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
            this.label10.Size = new System.Drawing.Size(38, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Goal:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "Start:";
            // 
            // checkBox_disallowTurning
            // 
            this.checkBox_disallowTurning.AutoSize = true;
            this.checkBox_disallowTurning.Location = new System.Drawing.Point(33, 52);
            this.checkBox_disallowTurning.Name = "checkBox_disallowTurning";
            this.checkBox_disallowTurning.Size = new System.Drawing.Size(120, 20);
            this.checkBox_disallowTurning.TabIndex = 4;
            this.checkBox_disallowTurning.Text = "Disallow Turning";
            this.checkBox_disallowTurning.UseVisualStyleBackColor = true;
            // 
            // AStarDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1210, 607);
            this.Controls.Add(this.groupBox_pathPlanning);
            this.Controls.Add(this.groupBox_edgeConstraintsEditor);
            this.Controls.Add(this.groupBox_mapInfo);
            this.Controls.Add(this.groupBox_nodeTypeEditor);
            this.Controls.Add(this.groupBox_nodeInfo);
            this.Controls.Add(this.pictureBox_mapViewer);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "AStarDemo";
            this.Text = "A* Demo";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).EndInit();
            this.groupBox_nodeInfo.ResumeLayout(false);
            this.groupBox_nodeInfo.PerformLayout();
            this.groupBox_nodeTypeEditor.ResumeLayout(false);
            this.groupBox_nodeTypeEditor.PerformLayout();
            this.groupBox_mapInfo.ResumeLayout(false);
            this.groupBox_mapInfo.PerformLayout();
            this.groupBox_edgeConstraintsEditor.ResumeLayout(false);
            this.groupBox_edgeConstraintsEditor.PerformLayout();
            this.groupBox_pathPlanning.ResumeLayout(false);
            this.groupBox_pathPlanning.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox_nodeTypeEditor;
        private System.Windows.Forms.ComboBox comboBox_types;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_startEditingNode;
        private System.Windows.Forms.GroupBox groupBox_mapInfo;
        private System.Windows.Forms.TextBox textBox_mapSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_mapZone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_mapDIM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_mapRefresh;
        private System.Windows.Forms.GroupBox groupBox_edgeConstraintsEditor;
        private System.Windows.Forms.TextBox textBox_edgeNode2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_edgeNode1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_showConstraints;
        private System.Windows.Forms.Button button_startEditingEdge;
        private System.Windows.Forms.ComboBox comboBox_constraintLayers;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_passingRestrictions;
        private System.Windows.Forms.Button button_deleteLayer;
        private System.Windows.Forms.Button button_addLayer;
        private System.Windows.Forms.GroupBox groupBox_pathPlanning;
        private System.Windows.Forms.TextBox textBox_goalNode;
        private System.Windows.Forms.TextBox textBox_startNode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_planningPath;
        private System.Windows.Forms.Button button_startPlanning;
        private System.Windows.Forms.ComboBox comboBox_planningLayer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox_disallowTurning;
    }
}

