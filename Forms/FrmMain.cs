using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace FileReadToSQLDB
{
    public partial class FrmMain : Form
    {
        string DataSource; //used to save the datasource from the dsnNames
        string InitialCatalog; //used to save the InitialCatalog        
        string ReportRow;
        string ReportField;
        SQLConnectionClass SQLConnect = new SQLConnectionClass("", "", "", "", false);

        public FrmMain()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            cmbDelimiter.SelectedItem = "comma (,)";
            cmbQuoted.SelectedItem = "Yes";

#if DEBUG
            txtInstance.Text = @"COMPUTER46";
            txtDatabase.Text = @"Voter_Database";
            txtLogin.Text = @"sa";
            txtPassword.Text = @"";
#endif
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Files
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    txtFile.Text = filePath;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtInstance.Text != "" && txtDatabase.Text != "" && txtLogin.Text != "" && txtPassword.Text != "")
            {
                btnConnect.Enabled = false;
                txtDatabase.Enabled = false;
                txtInstance.Enabled = false;
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
                ckbxSQL_DirectIP.Enabled = false;
                btnConnect.Text = "Please wait";

                SQLConnect.dataSource = txtInstance.Text;
                SQLConnect.initialCatalog = txtDatabase.Text;
                SQLConnect.userID = txtLogin.Text;
                SQLConnect.password = txtPassword.Text;
                SQLConnect.useIPAdress = ckbxSQL_DirectIP.Checked;

                try
                {
                    DataSource = txtInstance.Text;
                    InitialCatalog = txtDatabase.Text;

                    SqlConnection sqlConnString = new SqlConnection("Data Source=" + SQLConnect.dataSource + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                    if (SQLConnect.useIPAdress == true)
                    {
                        IPAddress ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
                        sqlConnString = new SqlConnection("Data Source=" + ip.ToString() + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                        //MessageBox.Show(ip.ToString());
                    }

                    //version
                    SqlDataAdapter read_version = new SqlDataAdapter("select top 1 TABLE_NAME from INFORMATION_SCHEMA.TABLES", sqlConnString);
                    DataTable version_table = new DataTable();

                    sqlConnString.Open();
                    read_version.Fill(version_table);
                    sqlConnString.Close();

                    btnConnect.Text = "Valid";
                    grpImport.Enabled = true;
                    grpBoxExportType.Enabled = true;
                    version_table.Dispose();

                    //SQLGBSCustomProcess Tables = new SQLGBSCustomProcess();
                    //Tables.CreateTables();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error connecting to database");
                    btnConnect.Enabled = true;
                    btnConnect.Text = "Connect";
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            cmbDelimiter.Enabled = false;
            cmbQuoted.Enabled = false;
            btnProcess.Enabled = false;
            Thread process_file = new Thread(processFile);
            process_file.IsBackground = true;
            process_file.Start();
            //processFile(txtFile.Text.ToString());
        }

        //private void processFile(string fileToProcess)
        private void processFile()
        {
            string fileToProcess = txtFile.Text.ToString();
            System.IO.FileInfo fileData = new System.IO.FileInfo(fileToProcess);
            //MessageBox.Show(fileData.Name);

            try
            {
                if (File.Exists(fileData.FullName))
                {
                    using (TextFieldParser parser = new TextFieldParser(fileData.FullName))
                    {
                        int row = 1;
                        int column = 1;

                        parser.TextFieldType = FieldType.Delimited;
                        parser.HasFieldsEnclosedInQuotes = true;
                        parser.SetDelimiters(",");

                        while(!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            foreach(string field in fields)
                            {
                                if (column == 1)
                                {
                                    ReportRow = row.ToString();
                                    ReportField = field.ToString();
                                    Invoke(new UIUpdate(StartUpdate));
                                }
                                //MessageBox.Show("ColumnTest " + field.ToString());
                                column++;
                            }
                            column = 1;
                            row++;                            
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                MessageBox.Show("Done");
                btnProcess.Enabled = false;
            }            
        }

        //UI delegate
        public delegate void UIUpdate();

        public void StartUpdate()
        {
            lblTestLabel.Text = (ReportRow.ToString() + " - " + ReportField.ToString());
            this.lblTestLabel.Refresh();
        }
    }
}
