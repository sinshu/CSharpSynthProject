using System;
using System.Drawing;
using System.Windows.Forms;

namespace SynthControls
{
    public partial class TrackBar : UserControl
    {
        private Pen back = new Pen(Brushes.DimGray, 15);
        private Pen front = new Pen(Brushes.Black, 3);
        private Font font = new Font(FontFamily.GenericSansSerif, 10);
        private string name = "Null";
        private float value = 0;
        private bool show_percent = false;
        private bool show_name = true;

        public float Value
        {
            get { return value; }
            set 
            {
                value = Math.Max(0f, value);
                value = Math.Min(1.0f, value);
                if (this.value == value)
                    return;
                this.value = value;
                this.Invalidate();
            }
        }
        public string StringDescription
        {
            get { return name; }
            set 
            { 
                name = value;
                this.Invalidate();
            }
        }
        public bool ShowPercentage
        {
            get { return show_percent; }
            set 
            { 
                show_percent = value;
                this.Invalidate();
            }
        }
        public bool ShowName
        {
            get { return show_name; }
            set
            {
                show_name = value;
                this.Invalidate();
            }
        }

        public TrackBar()
        {
            InitializeComponent();
        }
        private void TrackBar_Paint(object sender, PaintEventArgs e)
        {
            const int TRACK_WIDTH = 6;
            const int TRACK_HEIGHT = 10;
            int yloc = this.Height / 2;
            int mwid = this.Width - TRACK_WIDTH;
            if(show_name)
                e.Graphics.DrawString(name + (show_percent ? " " + value * 100f + "%" : ""), font, Brushes.Blue, 0, 0);
            e.Graphics.DrawLine(back, 0, yloc, this.Width, yloc);
            e.Graphics.DrawLine(front, TRACK_WIDTH, yloc, mwid, yloc);
            e.Graphics.FillRectangle(Brushes.Red, TRACK_WIDTH + value * (mwid - 2*TRACK_WIDTH), yloc - (TRACK_HEIGHT / 2), TRACK_WIDTH, TRACK_HEIGHT);
        }
    }
}
