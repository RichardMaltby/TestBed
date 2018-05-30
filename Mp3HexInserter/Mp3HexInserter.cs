using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// Mp3HexInserter,
/// 
/// select Mp3 save file path to Mp3FilePath;
/// select CloudCoins save to CloudCoinFilePath;
/// 
/// 
/// 
/// 
/// </summary>
namespace Mp3HexInserter
{
    public partial class Mp3HexInserter : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        private static readonly int CHUNK_SIZE = 1024;
        string Mp3FilePath;
        string CloudCoinFilePath;
        byte[] bytes = { };

        public Mp3HexInserter()
        {
            InitializeComponent();
        }

        //Retrieve an Mp3 byte file
        //Iterate through the file searching for CloudCoin Markers,
        //When a marker is found push bytes to array.
        private void GetCoins_Click(object sender, EventArgs e)
        {

        }

        //Retrieve a Mp3 byte file.
        //Retrieve the CloudCoin byte file.
        //Parse Mp3 frames
        //Insert CloudCoin markers
        //Append CloudCoin to markers
        private void StoreCoins_Click(object sender, EventArgs e)
        {

        }

        //On clicking 'Mp3 File' Button displays browse window, 
        //Filters contents to Mp3 only,
        //Calls Function to save the file path.
        private void GetMp3_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Mp3|*.mp3";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SetMp3FilePath(ofd.FileName);
            
            };
        }

        //On clicking 'Add Coin' Button displays browse window, 
        //Filters contents to .txt only,
        //Calls Function to save the file path.
        private void AddCoin_Click(object sender, EventArgs e)
        {
            {
                ofd.Filter = "Text|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SetCloudCoinFilePath(ofd.FileName);
                };
            }
        }

        //Called when saving a new Mp3 file-path.
        public void SetMp3FilePath(string Mp3FilePath_)
        {
            Mp3FilePath = Mp3FilePath_;
            OutputText.Text = "MP3 file-path saved!";
            DumpMp3(Mp3FilePath);
        }

        //Called when saveing a new CloudCoin file-path.
        public void SetCloudCoinFilePath(string CloudCoinFilePath_)
        {
            CloudCoinFilePath = CloudCoinFilePath_;
            OutputText.Text = "CloudCoin file-path saved!";
        }

        public void DumpMp3(string filePath)
        {
            string path = filePath;
            Console.WriteLine(path);
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs, new ASCIIEncoding()))
                {
                    byte[] chunk;
                    OutputText.AppendText(Environment.NewLine);
                   
                    chunk = br.ReadBytes(CHUNK_SIZE);
                    while (chunk.Length > 0)
                    {
                        OutputText.AppendText(Environment.NewLine);
                        DumpBytes(chunk, chunk.Length);
                        chunk = br.ReadBytes(CHUNK_SIZE);
                    }
                }
            }
            
        }

        public void DumpBytes(byte[] bdata, int len)
        {
            int i;
            int j = 0;
            char dchar;

            // 3 * 16 chars for hex display, 16 chars for text and 8 chars
            // for the 'gutter' int the middle.
            StringBuilder dumptext = new StringBuilder("         ", len + 8);

            for (i = 0; i < len; i++)
            {
                dumptext.Insert(j * 3, String.Format("{0:X} ", (int)bdata[i]));
                dchar = (char)bdata[i];

                //' replace 'non-printable' chars with a '.'.
                if (Char.IsWhiteSpace(dchar) || Char.IsControl(dchar))
                {
                    dchar = '-';
                }
                dumptext.Append(dchar);
                j++;

                if (j == 16)
                {
                    OutputText.AppendText(dumptext.ToString());
                    dumptext.Length = 0;
                    dumptext.Append("        ");
                    j = 0;
                }

            }

            // display the remaining line
            if (j > 0)
            {
                for (i = j; i < 16; i++)
                {
                    dumptext.Insert(j * 3, "   ");
                }
                OutputText.AppendText(dumptext.ToString());
            }
            
        }
        //       //Using the saved file paths to create a byte array. 
        //       private void ConvertToByteArr()
        //       {
        //
        //       }
    }
}
