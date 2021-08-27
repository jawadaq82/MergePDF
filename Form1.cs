using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergePDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK
                || folderBrowserDialog1.SelectedPath.Length < 10
                )
            {
                MessageBox.Show("Please select a folder", Text, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()!= DialogResult.OK
                || saveFileDialog1.FileName.Length < 10
                )
            {
                MessageBox.Show("Please select destination file", Text, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }
            textBox2.Text = saveFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length<10 || textBox2.Text.Length < 10)
            {
                MessageBox.Show("Please select source folder and destination file", Text, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

            ProcessFiles();
        }

        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        void ProcessFiles()
        {
            var files = System.IO.Directory.GetFiles(textBox1.Text, "*.pdf");
            if (files.Length == 0)
            {
                MessageBox.Show("No PDF files found in the source folder", Text, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }
            using (PdfDocument outPdf = new PdfDocument())
            {
                foreach (var item in files)
                {
                    using (PdfDocument one = PdfReader.Open(item, PdfDocumentOpenMode.Import))
                    {
                        CopyPages(one, outPdf); 
                    }
                }
                outPdf.Save(textBox2.Text);
            }
            MessageBox.Show("All PDF files are merged successfully", Text, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
        }
    }
}
