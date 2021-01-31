using System;
using System.Drawing;
using System.Windows.Forms;

namespace SynthControls
{
    public partial class KeyBoardControl : UserControl
    {
        private int octaves;
        private int keyScale;
        public delegate void PianoKeyDown(int key);
        public event PianoKeyDown PianoKey_Down;
        public delegate void PianoKeyUp(int key);
        public event PianoKeyUp PianoKey_Up;

        public int Octaves
        {
            get { return octaves; }
            set 
            {             
                value = Math.Min(10, value);
                value = Math.Max(1, value);
                if (value == octaves)
                    return;
                octaves = value; 
                updateKeys();
            }
        }
        public int KeyScale
        {
            get { return keyScale; }
            set 
            {
                value = Math.Min(5, value);
                value = Math.Max(1, value);
                if (value == keyScale)
                    return;
                keyScale = value;
                updateKeys();
            }
        }

        public KeyBoardControl()
        {
            InitializeComponent();
            Octaves = 4;
            keyScale = 1;
            updateKeys();
        }
        public bool isKeyDown(int key)
        {
            return this.Controls[key].BackColor == Color.Red;
        }
        public void PressKey(int key)
        {
            PictureBox p = ((PictureBox)this.Controls[key]);
            p.Tag = p.BackColor;
            p.BackColor = Color.Red;
            if (PianoKey_Down != null)
                PianoKey_Down(key);
        }
        public void ReleaseKey(int key)
        {
            PictureBox p = ((PictureBox)this.Controls[key]);
            p.BackColor = (Color)p.Tag;
            if (PianoKey_Up != null)
                PianoKey_Up(key);
        }
        private void Keyboard_Down(object sender, EventArgs e)
        {
            PictureBox p = ((PictureBox)sender);
            p.BackColor = Color.Red;
            int key = int.Parse(p.Name.Substring(10));
            if (PianoKey_Down != null)
                PianoKey_Down(key);
        }
        private void Keyboard_Up(object sender, EventArgs e)
        {
            PictureBox p = ((PictureBox)sender);
            p.BackColor = (Color)p.Tag;
            int key = int.Parse(p.Name.Substring(10));
            if (PianoKey_Up != null)
                PianoKey_Up(key);
        }
        private void updateKeys()
        {
            foreach (PictureBox p in this.Controls)
            {
                p.MouseDown -= new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp -= new MouseEventHandler(this.Keyboard_Up);
            }
            this.Controls.Clear();
            int x = 0;
            int y = 0;
            int nameId = 0;
            for (int i = 0; i < octaves; i++)
            {
                PictureBox p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 8 * keyScale;
                p.Height = 25 * keyScale;
                p.BackColor = Color.Black;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                p.BringToFront();
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 8 * keyScale;
                p.Height = 25 * keyScale;
                p.BackColor = Color.Black;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                p.BringToFront();
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 10 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 8 * keyScale;
                p.Height = 25 * keyScale;
                p.BackColor = Color.Black;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                p.BringToFront();
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 8 * keyScale;
                p.Height = 25 * keyScale;
                p.BackColor = Color.Black;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                p.BringToFront();
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 8 * keyScale;
                p.Height = 25 * keyScale;
                p.BackColor = Color.Black;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                p.BringToFront();
                x += 5 * keyScale;
                p = new PictureBox();
                p.Name = "pictureBox" + nameId++;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Width = 10 * keyScale;
                p.Height = 40 * keyScale;
                p.BackColor = Color.White;
                p.Location = new Point(x, y);
                p.MouseDown += new MouseEventHandler(this.Keyboard_Down);
                p.MouseUp += new MouseEventHandler(this.Keyboard_Up);
                p.Tag = p.BackColor;
                this.Controls.Add(p);
                x += 10 * keyScale;
            }
        }
    }
}
