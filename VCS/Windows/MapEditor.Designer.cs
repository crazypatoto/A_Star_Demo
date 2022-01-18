namespace VCS.Windows
{
    partial class MapEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            this.groupBox_nodeTypeEditor = new System.Windows.Forms.GroupBox();
            this.checkBox_disallowWaitingOnNode = new System.Windows.Forms.CheckBox();
            this.button_startEditingNode = new System.Windows.Forms.Button();
            this.comboBox_types = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_edgeConstraintsEditor.SuspendLayout();
            this.groupBox_nodeTypeEditor.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox_edgeConstraintsEditor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_edgeConstraintsEditor.Location = new System.Drawing.Point(12, 131);
            this.groupBox_edgeConstraintsEditor.Name = "groupBox_edgeConstraintsEditor";
            this.groupBox_edgeConstraintsEditor.Size = new System.Drawing.Size(181, 237);
            this.groupBox_edgeConstraintsEditor.TabIndex = 8;
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
            this.label9.Size = new System.Drawing.Size(40, 16);
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
            this.checkBox_showConstraints.Size = new System.Drawing.Size(170, 20);
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
            this.label8.Size = new System.Drawing.Size(53, 16);
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
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Node 1: ";
            // 
            // groupBox_nodeTypeEditor
            // 
            this.groupBox_nodeTypeEditor.Controls.Add(this.checkBox_disallowWaitingOnNode);
            this.groupBox_nodeTypeEditor.Controls.Add(this.button_startEditingNode);
            this.groupBox_nodeTypeEditor.Controls.Add(this.comboBox_types);
            this.groupBox_nodeTypeEditor.Controls.Add(this.label4);
            this.groupBox_nodeTypeEditor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_nodeTypeEditor.Location = new System.Drawing.Point(12, 12);
            this.groupBox_nodeTypeEditor.Name = "groupBox_nodeTypeEditor";
            this.groupBox_nodeTypeEditor.Size = new System.Drawing.Size(181, 116);
            this.groupBox_nodeTypeEditor.TabIndex = 7;
            this.groupBox_nodeTypeEditor.TabStop = false;
            this.groupBox_nodeTypeEditor.Text = "Node Type Editor";
            // 
            // checkBox_disallowWaitingOnNode
            // 
            this.checkBox_disallowWaitingOnNode.AutoSize = true;
            this.checkBox_disallowWaitingOnNode.Location = new System.Drawing.Point(33, 52);
            this.checkBox_disallowWaitingOnNode.Name = "checkBox_disallowWaitingOnNode";
            this.checkBox_disallowWaitingOnNode.Size = new System.Drawing.Size(140, 20);
            this.checkBox_disallowWaitingOnNode.TabIndex = 4;
            this.checkBox_disallowWaitingOnNode.Text = "Disallow Waiting On";
            this.checkBox_disallowWaitingOnNode.UseVisualStyleBackColor = true;           
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
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type: ";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 378);
            this.Controls.Add(this.groupBox_edgeConstraintsEditor);
            this.Controls.Add(this.groupBox_nodeTypeEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapEditor_FormClosing);
            this.groupBox_edgeConstraintsEditor.ResumeLayout(false);
            this.groupBox_edgeConstraintsEditor.PerformLayout();
            this.groupBox_nodeTypeEditor.ResumeLayout(false);
            this.groupBox_nodeTypeEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_edgeConstraintsEditor;
        private System.Windows.Forms.Button button_deleteLayer;
        private System.Windows.Forms.Button button_addLayer;
        public System.Windows.Forms.ComboBox comboBox_constraintLayers;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBox_passingRestrictions;
        private System.Windows.Forms.Button button_startEditingEdge;
        public System.Windows.Forms.CheckBox checkBox_showConstraints;
        public System.Windows.Forms.TextBox textBox_edgeNode2;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBox_edgeNode1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox_nodeTypeEditor;
        public System.Windows.Forms.CheckBox checkBox_disallowWaitingOnNode;
        private System.Windows.Forms.Button button_startEditingNode;
        public System.Windows.Forms.ComboBox comboBox_types;
        private System.Windows.Forms.Label label4;
    }
}