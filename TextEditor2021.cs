// Author: Willem 
// Last Modified: March 28th, 2021
// Description: This program is a lab I did for NETD 2202 where I made my own version of notepad
// It allows me ot open files, create new files, save files, as well as copy, cut, and paste.
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

namespace TextEditor2021
{
    public partial class formTextEditor2021 : Form
    {
        //Variables
        //Filepath for the active file, if applicable.
        string filePath = String.Empty;
        //Allows you to open previously made files
        OpenFileDialog openFile = new OpenFileDialog();

        public formTextEditor2021()
        {
            InitializeComponent();
        }

        #region "File"
        /// <summary>
        /// Updates the title of the form at the top
        /// </summary>
        public void UpdateTitle()
        {
            this.Text = "Willem's Text Editor";

            if (filePath != String.Empty)
            {
                this.Text += " - " + filePath;
            }
        }

        /// <summary>
        /// This clears the textbox, voids the current filepath, and updates title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileNew_Click(object sender, EventArgs e)
        {
            textBoxEditor.Clear();
            filePath = String.Empty;
        }

        /// <summary>
        /// This opens a previously made file and inserts it into your form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenClick(object sender, EventArgs e)
        {
            openFile.Filter = "TXT|*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                StreamReader openReader = new StreamReader(openFile.FileName, Encoding.Default);
                textBoxEditor.Text = openReader.ReadToEnd();
                openReader.Close();
            }

            openFile.Dispose();
        }

        /// <summary>
        /// Saving over the file you've already created, or if you haven't saved yet, triggering Save As
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveClick(object sender, EventArgs e)
        {
            //If a filepath does not already exist
            if (filePath == String.Empty)
            {
                FileSaveAs(sender, e);
            }
            //If a filepath DOES exist
            else
            {
                SaveTextFile(filePath);
            }
        }

        /// <summary>
        /// This allows the users to name and save their files to a specific location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveDialog.FileName;

                SaveTextFile(filePath);

                UpdateTitle();
            }
        }

        /// <summary>
        /// This method is called whenever a file needs to be saved to a textfile.
        /// </summary>
        /// <param name="path">The path of the file to write to</param>
        public void SaveTextFile(string path)
        {
            FileStream myFile = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(myFile);

            writer.Write(textBoxEditor.Text);

            writer.Close();
        }

        /// <summary>
        /// This closes the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region "Edit"
        /// <summary>
        /// Copies the selected text in the textbox into your clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyClick(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxEditor.SelectedText);
        }

        /// <summary>
        /// Cuts the selected text from the textbox and into your keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutClick(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxEditor.SelectedText);
            textBoxEditor.SelectedText = string.Empty;
        }

        /// <summary>
        /// Pasting whatever text is in you clipboard onto the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteClick(object sender, EventArgs e)
        {
            string pasteData = Clipboard.GetText();
            textBoxEditor.Text = textBoxEditor.Text.Insert(textBoxEditor.SelectionStart, pasteData);
        }
        #endregion

        #region "Help"
        /// <summary>
        /// The popup that appears when the user clicks on the "Help About" button on the form design.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpAbout(object sender, EventArgs e)
        {
            MessageBox.Show("This is a program that allows the user to input their own text into their own text file! But this isn't notepad," +
                " just making that very clear to you... Not notepad. This is cooler than notepad! You can open files, save files, make new files," +
                " as well as copy, cut, and paste!", "About the program", MessageBoxButtons.OK);
        }


        #endregion

    }
}
