using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCS.Dialogs
{
    public partial class NewMapDialog : Form
    {
        public byte MapZoneID { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public NewMapDialog()
        {
            InitializeComponent();
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            this.MapZoneID = Byte.Parse(textBox_zoneID.Text);
            this.MapWidth = int.Parse(textBox_width.Text);
            this.MapHeight = int.Parse(textBox_height.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
