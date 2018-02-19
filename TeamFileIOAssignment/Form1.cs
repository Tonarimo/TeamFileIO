using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamFileIOAssignment
{
    public partial class frmImportFile : Form
    {
        public frmImportFile()
        {
            InitializeComponent();
        }

        private void btnEncryptFile_Click(object sender, EventArgs e)
        {
            EncryptFile();
        }
        
        private void EncryptFile()
        {
            try
            {
                string fileText = txtReadAndDisplayFile.Text;
                byte[] byteCode = Encoding.UTF8.GetBytes(fileText);
                string returnText = Convert.ToBase64String(byteCode);
                txtReadAndDisplayFile.Text = returnText;
            }
            catch
            {
                MessageBox.Show("File is already encrypted.");
            }
        }

        private void btnDecryptFile_Click(object sender, EventArgs e)
        {
            DecryptFile();
        }

        private void DecryptFile()
        {
            try
            {
                string fileText = txtReadAndDisplayFile.Text;
                byte[] byteCode = Convert.FromBase64String(fileText);
                string returnText = Encoding.UTF8.GetString(byteCode);
                txtReadAndDisplayFile.Text = returnText;
            }
            catch
            {
                MessageBox.Show("The file was already decrypted.");
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.ShowDialog();

            string fileName = saveFile.FileName;
            string textToDisplay = txtReadAndDisplayFile.Text;
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(textToDisplay);
                }
            }
            catch
            {
                MessageBox.Show("You didn't save the file!");
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog importFileName = new OpenFileDialog();
            importFileName.ShowDialog();

            //show file name in text box
            txtFileName.Text = importFileName.FileName;

            DisplayFile();
        }

        private void DisplayFile()
        {
            // display the file info in the text box
            try
            {   // open the text file
                using (StreamReader streamReader = new StreamReader(txtFileName.Text))
                {
                    String lineToRead = streamReader.ReadToEnd();
                    txtReadAndDisplayFile.Text = lineToRead;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file you have chosen could not be read.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
