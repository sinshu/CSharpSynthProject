namespace DirectSoundDemo
{
    partial class PianoControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.keyBoardControl1 = new SynthControls.KeyBoardControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Note Off All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Reset Controllers";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(198, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // keyBoardControl1
            // 
            this.keyBoardControl1.KeyScale = 1;
            this.keyBoardControl1.Location = new System.Drawing.Point(4, 0);
            this.keyBoardControl1.Name = "keyBoardControl1";
            this.keyBoardControl1.Octaves = 10;
            this.keyBoardControl1.Size = new System.Drawing.Size(705, 44);
            this.keyBoardControl1.TabIndex = 0;
            this.keyBoardControl1.PianoKey_Down += new SynthControls.KeyBoardControl.PianoKeyDown(this.keyBoardControl1_PianoKey_Down);
            this.keyBoardControl1.PianoKey_Up += new SynthControls.KeyBoardControl.PianoKeyUp(this.keyBoardControl1_PianoKey_Up);
            // 
            // PianoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 67);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keyBoardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PianoControl";
            this.Text = "PianoControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PianoControl_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PianoControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PianoControl_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private SynthControls.KeyBoardControl keyBoardControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}