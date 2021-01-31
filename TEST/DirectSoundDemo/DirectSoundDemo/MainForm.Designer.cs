namespace DirectSoundDemo
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBankFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadCurrentMidiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewToolStripMenuItem,
            this.toolsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openBankFileToolStripMenuItem,
            this.openPatchToolStripMenuItem,
            this.unloadCurrentMidiToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(35, 20);
            this.fileMenu.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openToolStripMenuItem.Text = "&Open Midi";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
            // 
            // openBankFileToolStripMenuItem
            // 
            this.openBankFileToolStripMenuItem.Name = "openBankFileToolStripMenuItem";
            this.openBankFileToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openBankFileToolStripMenuItem.Text = "Open BankFile";
            this.openBankFileToolStripMenuItem.Click += new System.EventHandler(this.openBankFileToolStripMenuItem_Click);
            // 
            // openPatchToolStripMenuItem
            // 
            this.openPatchToolStripMenuItem.Name = "openPatchToolStripMenuItem";
            this.openPatchToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openPatchToolStripMenuItem.Text = "Patch Manager";
            this.openPatchToolStripMenuItem.Click += new System.EventHandler(this.openPatchToolStripMenuItem_Click);
            // 
            // unloadCurrentMidiToolStripMenuItem
            // 
            this.unloadCurrentMidiToolStripMenuItem.Name = "unloadCurrentMidiToolStripMenuItem";
            this.unloadCurrentMidiToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.unloadCurrentMidiToolStripMenuItem.Text = "Unload Current Midi";
            this.unloadCurrentMidiToolStripMenuItem.Click += new System.EventHandler(this.unloadCurrentMidiToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(164, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playListToolStripMenuItem,
            this.midiControlsToolStripMenuItem,
            this.keyBoardToolStripMenuItem,
            this.logToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // playListToolStripMenuItem
            // 
            this.playListToolStripMenuItem.CheckOnClick = true;
            this.playListToolStripMenuItem.Name = "playListToolStripMenuItem";
            this.playListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playListToolStripMenuItem.Text = "PlayList";
            this.playListToolStripMenuItem.CheckedChanged += new System.EventHandler(this.playListToolStripMenuItem_CheckedChanged);
            // 
            // midiControlsToolStripMenuItem
            // 
            this.midiControlsToolStripMenuItem.CheckOnClick = true;
            this.midiControlsToolStripMenuItem.Name = "midiControlsToolStripMenuItem";
            this.midiControlsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.midiControlsToolStripMenuItem.Text = "MidiControls";
            this.midiControlsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.midiControlsToolStripMenuItem_CheckedChanged);
            // 
            // keyBoardToolStripMenuItem
            // 
            this.keyBoardToolStripMenuItem.CheckOnClick = true;
            this.keyBoardToolStripMenuItem.Name = "keyBoardToolStripMenuItem";
            this.keyBoardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.keyBoardToolStripMenuItem.Text = "KeyBoard";
            this.keyBoardToolStripMenuItem.CheckedChanged += new System.EventHandler(this.keyBoardToolStripMenuItem_CheckedChanged);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.CheckOnClick = true;
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.CheckedChanged += new System.EventHandler(this.logToolStripMenuItem_CheckedChanged);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(45, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.debugInfoToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(41, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // debugInfoToolStripMenuItem
            // 
            this.debugInfoToolStripMenuItem.Name = "debugInfoToolStripMenuItem";
            this.debugInfoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.debugInfoToolStripMenuItem.Text = "Debug Info";
            this.debugInfoToolStripMenuItem.Click += new System.EventHandler(this.debugInfoToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 551);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel1.Text = "Status2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "DirectSound Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBankFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midiControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadCurrentMidiToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem openPatchToolStripMenuItem;
    }
}



