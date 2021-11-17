
namespace A_Star_Demo
{
    partial class AStartDemo
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
            this.loadExistingMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_mapViewer = new System.Windows.Forms.PictureBox();
            this.groupBox_nodeInfo = new System.Windows.Forms.GroupBox();
            this.textBox_selectedNodeType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_selectedNodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_mapEditor = new System.Windows.Forms.GroupBox();
            this.button_editType = new System.Windows.Forms.Button();
            this.comboBox_types = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_mapDIM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_mapSN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_mapZone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer_mapRefresh = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).BeginInit();
            this.groupBox_nodeInfo.SuspendLayout();
            this.groupBox_mapEditor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editMapToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1032, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.saveMapToolStripMenuItem,
            this.loadMapToolStripMenuItem,
            this.loadExistingMapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newMapToolStripMenuItem.Text = "New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Enabled = false;
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // loadExistingMapToolStripMenuItem
            // 
            this.loadExistingMapToolStripMenuItem.Name = "loadExistingMapToolStripMenuItem";
            this.loadExistingMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadExistingMapToolStripMenuItem.Text = "Load Existing Map";
            this.loadExistingMapToolStripMenuItem.Click += new System.EventHandler(this.loadExistingMapToolStripMenuItem_Click);
            // 
            // editMapToolStripMenuItem
            // 
            this.editMapToolStripMenuItem.Name = "editMapToolStripMenuItem";
            this.editMapToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.editMapToolStripMenuItem.Text = "Edit Map";
            // 
            // pictureBox_mapViewer
            // 
            this.pictureBox_mapViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_mapViewer.Location = new System.Drawing.Point(12, 27);
            this.pictureBox_mapViewer.Name = "pictureBox_mapViewer";
            this.pictureBox_mapViewer.Size = new System.Drawing.Size(824, 570);
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
            // groupBox_mapEditor
            // 
            this.groupBox_mapEditor.Controls.Add(this.button_editType);
            this.groupBox_mapEditor.Controls.Add(this.comboBox_types);
            this.groupBox_mapEditor.Controls.Add(this.label4);
            this.groupBox_mapEditor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_mapEditor.Location = new System.Drawing.Point(841, 241);
            this.groupBox_mapEditor.Name = "groupBox_mapEditor";
            this.groupBox_mapEditor.Size = new System.Drawing.Size(181, 91);
            this.groupBox_mapEditor.TabIndex = 4;
            this.groupBox_mapEditor.TabStop = false;
            this.groupBox_mapEditor.Text = "Node Type Editor";
            // 
            // button_editType
            // 
            this.button_editType.Location = new System.Drawing.Point(17, 52);
            this.button_editType.Name = "button_editType";
            this.button_editType.Size = new System.Drawing.Size(152, 30);
            this.button_editType.TabIndex = 3;
            this.button_editType.Text = "Strat Editing";
            this.button_editType.UseVisualStyleBackColor = true;
            this.button_editType.Click += new System.EventHandler(this.button_editType_Click);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_mapDIM);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_mapSN);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_mapZone);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(841, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 113);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Information";
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
            // AStartDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 609);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_mapEditor);
            this.Controls.Add(this.groupBox_nodeInfo);
            this.Controls.Add(this.pictureBox_mapViewer);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "AStartDemo";
            this.Text = "A* Demo";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_mapViewer)).EndInit();
            this.groupBox_nodeInfo.ResumeLayout(false);
            this.groupBox_nodeInfo.PerformLayout();
            this.groupBox_mapEditor.ResumeLayout(false);
            this.groupBox_mapEditor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadExistingMapToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox_mapViewer;
        private System.Windows.Forms.GroupBox groupBox_nodeInfo;
        private System.Windows.Forms.TextBox textBox_selectedNodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_selectedNodeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_mapEditor;
        private System.Windows.Forms.ComboBox comboBox_types;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_editType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_mapSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_mapZone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_mapDIM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_mapRefresh;
    }
}

