using System.Windows.Forms;

namespace DirectSoundDemo
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }
        public void addString(string value)
        {
            textBox1.AppendText(value);
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    }
}
