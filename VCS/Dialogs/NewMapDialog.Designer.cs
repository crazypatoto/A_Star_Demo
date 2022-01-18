
namespace VCS.Dialogs
{
    partial class NewMapDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_create = new System.Windows.Forms.Button();
            this.textBox_zoneID = new System.Windows.Forms.TextBox();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zone:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Height:";
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(20, 190);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(185, 116);
            this.button_create.TabIndex = 3;
            this.button_create.Text = "Create Map";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // textBox_zoneID
            // 
            this.textBox_zoneID.Location = new System.Drawing.Point(105, 30);
            this.textBox_zoneID.Name = "textBox_zoneID";
            this.textBox_zoneID.Size = new System.Drawing.Size(100, 29);
            this.textBox_zoneID.TabIndex = 4;
            // 
            // textBox_width
            // 
            this.textBox_width.Location = new System.Drawing.Point(105, 81);
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.Size = new System.Drawing.Size(100, 29);
            this.textBox_width.TabIndex = 5;
            // 
            // textBox_height
            // 
            this.textBox_height.Location = new System.Drawing.Point(105, 130);
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Size = new System.Drawing.Size(100, 29);
            this.textBox_height.TabIndex = 6;
            // 
            // NewMapDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 328);
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.textBox_width);
            this.Controls.Add(this.textBox_zoneID);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMapDialog";
            this.Text = "Create New Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.TextBox textBox_zoneID;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.TextBox textBox_height;
    }
}