using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace V6proto
{
    public partial class Form1 : Form
    {

        int maxbytes = 0;
        int copied = 0;
        int total = 0;
       

        public Form1()
        {
            InitializeComponent();
           

            listView1.View = View.Details;

            listView1.Columns.Add("구분");
            listView1.Columns.Add("이름");

           
        }

        
    

        public void Copy1(string sourceDirectory)
        {

            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            //DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            //Gets size of all files present in source folder.
            GetSize(diSource);
            maxbytes = maxbytes / 1024;

            progressBar1.Maximum = maxbytes;
            CopyAll(diSource);
           
        }

        public void CopyAll(DirectoryInfo source)
        {


            foreach (FileInfo fi in source.GetFiles())
            {

                //fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                //Console.WriteLine(fi.Name);
                if (Path.GetExtension(fi.Name) == ".php")
                {

                    ListViewItem lvi1 = new ListViewItem();
            
                    lvi1.Text = "Searched";
                    lvi1.SubItems.Add(fi.Name);
                    listView1.Items.Add(lvi1);

                }
                
                total += (int)fi.Length;

                copied += (int)fi.Length;
                copied /= 1024;
                progressBar1.Step = copied;

                progressBar1.PerformStep();
                label1.Text = (total / 1048576).ToString() + "MB of " + (maxbytes / 1024).ToString() + "MB Searched";



                label1.Refresh();
            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {



                //DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir);
            }
            
        }

        public void GetSize(DirectoryInfo source)
        {


            
            foreach (FileInfo fi in source.GetFiles())
            {
                maxbytes += (int)fi.Length;//Size of File


            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                //DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                GetSize(diSourceSubDir);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {



                textBox1.Text = folderBrowserDialog1.SelectedPath;

                // sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maxbytes = 0;
            copied = 0;
            total = 0;

            Copy1(textBox1.Text);
            //MessageBox.Show("Done");
        }

       





    }
}
