using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMS.Dialogs
{
    public partial class ConnectionDialog : Form
    {
        public string IPA { get; private set; }
        public int Port { get; private set; }
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            this.IPA = textBox_IPA.Text;
            this.Port = int.Parse(textBox_Port.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
