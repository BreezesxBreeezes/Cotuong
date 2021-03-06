using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Board
{
    public partial class Sound_Options : Form
    {
        public Sound_Options()
        {
            InitializeComponent();
            if (Management.isSound) 
                Sound.Checked = true;
            if (Management.isSoundTrack) 
                BgMusic.Checked = true;
            if (BgMusic.Checked == false)
            {
                path.Enabled = false;
                Browse.Enabled = false;
            }
            path.Text = Management.Path_NhacNen;
        }

        private void Sound_CheckedChanged(object sender, EventArgs e)
        {
            if (Sound.Checked == true) Management.isSound = true;
            else Management.isSound = false;
        }

        private void BgMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (BgMusic.Checked == true)
            {
                Management.isSoundTrack = true;
                path.Enabled = true;
                Browse.Enabled = true;
            }
            else
            {
                Management.isSoundTrack = false;
                path.Enabled = false;
                Browse.Enabled = false;
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Mp3 Files|*.Mp3";
            openFileDialog1.Title = "Chọn nhạc";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "openFileDialog1") path.Text = openFileDialog1.FileName;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            FileStream saveOptions = File.Create("Options.cco");
            StreamWriter fileWriter = new StreamWriter(saveOptions);

            Management.Path_NhacNen = path.Text;
            if (Management.isSoundTrack)
            {
                Management.mciSendString("close MediaFile", null, 0, IntPtr.Zero);
                Management.mciSendString("open \"" + Management.Path_NhacNen + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                Management.mciSendString("play MediaFile REPEAT", null, 0, IntPtr.Zero);
            }
            if (!Management.isSoundTrack) Management.mciSendString("close MediaFile", null, 0, IntPtr.Zero);
            
            //Ghi options AmThanh
            if(Management.isSound) 
                fileWriter.WriteLine("1");
            else 
                fileWriter.WriteLine("0");

            //Ghi options NhacNen
            if (Management.isSoundTrack) 
                fileWriter.WriteLine("1");
            else 
                fileWriter.WriteLine("0");
            fileWriter.WriteLine(Management.Path_NhacNen);
            fileWriter.Close();
            saveOptions.Close();
            this.Close();
        }        
    }
}