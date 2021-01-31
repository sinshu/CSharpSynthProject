using System;
using System.Windows.Forms;
using AudioSynthesis.Bank;
using AudioSynthesis.Bank.Patches;
using AudioSynthesis.Synthesis;

namespace DirectSoundDemo
{
    public partial class PatchSelect : Form
    {
        private PatchBank bank;

        public PatchSelect(PatchBank bank)
        {
            InitializeComponent();
            this.bank = bank;
            int[] banks = bank.GetLoadedBanks();
            if (banks.Length > 0)
            {
                for (int x = 0; x < banks.Length; x++)
                    comboBox1.Items.Add(banks[x]);
                comboBox1.SelectedIndex = 0;
            }
            updatePatchList();
        }

        private void updatePatchList()
        {
            checkedListBox1.Items.Clear();
            int bNum = int.Parse(comboBox1.SelectedItem.ToString());
            for (int x = 0; x < PatchBank.BankSize; x++)
            {
                Patch p = bank.GetPatch(bNum, x);
                if (p == null)
                    checkedListBox1.Items.Add(x + " : Null");
                else
                    checkedListBox1.Items.Add(x + " : " + p.Name);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int bNum = int.Parse(comboBox1.SelectedItem.ToString());
                for (int x = 0; x < checkedListBox1.CheckedIndices.Count; x++)
                {
                    int i = checkedListBox1.CheckedIndices[x];
                    bank.LoadPatch(ofd.FileName, bNum, i, i);
                }
                updatePatchList();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int[] chk = new int[checkedListBox1.CheckedIndices.Count];
            checkedListBox1.CheckedIndices.CopyTo(chk, 0);
            for (int x = 0; x < chk.Length; x++)
            {
                checkedListBox1.SetItemChecked(chk[x], false);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < checkedListBox1.Items.Count; x++)
                checkedListBox1.SetItemChecked(x, !checkedListBox1.GetItemChecked(x));
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePatchList();
        }

    }
}
