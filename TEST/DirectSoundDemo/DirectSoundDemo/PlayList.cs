using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DirectSoundDemo
{
    public partial class PlayList : Form
    {
        private class PlayListItem
        {
            public string name;
            public int dirIndex;

            public override string ToString()
            {
                return name;
            }
        }
        private List<string> directoryList = new List<string>();
        private List<PlayListItem> songList = new List<PlayListItem>();
  
        public PlayList()
        {
            InitializeComponent();
            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.Images.Add(Properties.Resources.check);
        }
        public string nextSong()
        {
            int index = listView1.SelectedIndices[0];
            if (index < 0)
                index = 0;
            else
                index++;
            if (index >= listView1.Items.Count)
                return "";
            listView1.Items[index].Selected = true;
            return directoryList[songList[index].dirIndex] + songList[index].name;
        }
        public void addSongs(string[] songs, int select = -1)
        {
            foreach (string fname in songs)
            {
                if (File.Exists(fname))
                {
                    PlayListItem pi = new PlayListItem();
                    pi.name = Path.GetFileName(fname);
                    string dir = Path.GetDirectoryName(fname) + Path.DirectorySeparatorChar;
                    pi.dirIndex = directoryList.IndexOf(dir);
                    if(pi.dirIndex < 0)
                    {
                        pi.dirIndex = directoryList.Count;
                        directoryList.Add(dir);
                    }
                    songList.Add(pi);
                    listView1.Items.Add(pi.ToString());
                }
            }
            if (select > -1)
            {
                clearImages();
                ListViewItem item = listView1.Items[listView1.Items.Count - songs.Length + select];
                item.Selected = true;
                item.ImageIndex = 0;
                listView1.Select();
            }
        }
        public void clearImages()
        {
            for (int x = 0; x < listView1.Items.Count; x++)
                listView1.Items[x].ImageIndex = -1;
        }
        public void clearSongs()
        {
            directoryList.Clear();
            songList.Clear();
            listView1.Items.Clear();
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                addSongs(files);
            }
        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listView1.SelectedIndices[0];
            if(index < 0 || index >= listView1.Items.Count)
                return;
            PlayListItem pItem = songList[index];
            string midiFile = directoryList[pItem.dirIndex] + pItem.name;
            if (File.Exists(midiFile))
            {
                if (((MainForm)this.ParentForm).PlaySong(midiFile))
                {
                    clearImages();
                    listView1.Items[index].ImageIndex = 0;
                }
                else
                    MessageBox.Show("Make sure a sound bank is selected before playing.");
            }
        }
        private void PlayList_Resize(object sender, System.EventArgs e)
        {
            listView1.Columns[0].Width = this.Width - 25;
        }
        private void PlayList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    }
}
