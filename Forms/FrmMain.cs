using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using FileReadToSQLDB.Classes;

namespace FileReadToSQLDB
{
    public partial class FrmMain : Form
    {
        public List<Voters> _voters = new List<Voters>();
        string DataSource; //used to save the datasource from the dsnNames
        string InitialCatalog; //used to save the InitialCatalog        
        string ReportRow;
        string ReportField;
        int threadCountToUse = 0;
        int fileProcessThreadCount = 0;        
        SQLConnectionClass SQLConnect = new SQLConnectionClass("", "", "", "", false);

        public FrmMain()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            cmbDelimiter.SelectedItem = "comma (,)";
            cmbQuoted.SelectedItem = "True";
            cmbThreadCount.SelectedItem = "1";

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
                openFileDialog.FilterIndex = 3;
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
                    grpFileImport.Enabled = true;
                    grpFolderImport.Enabled = true;
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
            grpFileImport.Enabled = false;
            grpFolderImport.Enabled = false;
            //Thread process_file = new Thread(processFile);
            Thread process_file = new Thread(() => processFile(txtFile.Text.ToString()));
            process_file.IsBackground = true;
            process_file.Start();
            //processFile(txtFile.Text.ToString());
        }

        //private void processFile(string fileToProcess)
        private void processFile(string fileInformation)
        {
            //string fileToProcess = txtFile.Text.ToString();
            string fileToProcess = fileInformation;
            System.IO.FileInfo fileData = new System.IO.FileInfo(fileToProcess);
            LogWriter log = new LogWriter();
            //MessageBox.Show(fileData.Name);

            try
            {
                if (File.Exists(fileData.FullName))
                {
                    using (TextFieldParser parser = new TextFieldParser(fileData.FullName))
                    {
                        int row = 1;
                        int column = 1;
                        string SOSVoterId = null;
                        string CountyNumber = null;
                        string CountyId = null;
                        string LastName = null;
                        string FirstName = null;
                        string MiddleName = null;
                        string Suffix = null;
                        string DateOfBirth = null;
                        string RegistrationDate = null;
                        string VoterStatus = null;
                        string PartyAffiliation = null;
                        string ResidentialAddress1 = null;
                        string ResidentialAddress2 = null;
                        string ResidentialCity = null;
                        string ResidentialState = null;
                        string ResidentialZip = null;
                        string ResidentialZipPlus4 = null;
                        string ResidentialCountry = null;
                        string ResidentialPostalCode = null;
                        string MailingAddress1 = null;
                        string MailingAddress2 = null;
                        string MailingCity = null;
                        string MailingState = null;
                        string MailingZip = null;
                        string MailingZipPlus4 = null;
                        string MailingCountry = null;
                        string MailingPostalCode = null;
                        string CareerCenter = null;
                        string City = null;
                        string CitySchoolDistrict = null;
                        string CountyCourtDistrict = null;
                        string CongressionalDistrict = null;
                        string CourtofAppeals = null;
                        string EducationServiceCenter = null;
                        string ExemptedVillageSchoolDistrict = null;
                        string LibraryDistrict = null;
                        string LocalSchoolDistrict = null;
                        string MunicipalCourtDistrict = null;
                        string Precinct = null;
                        string PrecinctCode = null;
                        string StateBoardofEducation = null;
                        string StateRepresentativeDistrict = null;
                        string StateSenateDistrict = null;
                        string Township = null;
                        string Village = null;
                        string Ward = null;
                        string empty = null;

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

                                if (row != 1)
                                {
                                    switch (column)
                                    {
                                        case 1:
                                            SOSVoterId = field;
                                            break;
                                        case 2:
                                            CountyNumber = field;
                                            break;
                                        case 3:
                                            CountyId = field;
                                            break;
                                        case 4:
                                            LastName = field;
                                            break;
                                        case 5:
                                            FirstName = field;
                                            break;
                                        case 6:
                                            MiddleName = field;
                                            break;
                                        case 7:
                                            Suffix = field;
                                            break;
                                        case 8:
                                            DateOfBirth = field;
                                            break;
                                        case 9:
                                            RegistrationDate = field;
                                            break;
                                        case 10:
                                            VoterStatus = field;
                                            break;
                                        case 11:
                                            PartyAffiliation = field;
                                            break;
                                        case 12:
                                            ResidentialAddress1 = field;
                                            break;
                                        case 13:
                                            ResidentialAddress2 = field;
                                            break;
                                        case 14:
                                            ResidentialCity = field;
                                            break;
                                        case 15:
                                            ResidentialState = field;
                                            break;
                                        case 16:
                                            ResidentialZip = field;
                                            break;
                                        case 17:
                                            ResidentialZipPlus4 = field;
                                            break;
                                        case 18:
                                            ResidentialCountry = field;
                                            break;
                                        case 19:
                                            ResidentialPostalCode = field;
                                            break;
                                        case 20:
                                            MailingAddress1 = field;
                                            break;
                                        case 21:
                                            MailingAddress2 = field;
                                            break;
                                        case 22:
                                            MailingCity = field;
                                            break;
                                        case 23:
                                            MailingState = field;
                                            break;
                                        case 24:
                                            MailingZip = field;
                                            break;
                                        case 25:
                                            MailingZipPlus4 = field;
                                            break;
                                        case 26:
                                            MailingCountry = field;
                                            break;
                                        case 27:
                                            MailingPostalCode = field;
                                            break;
                                        case 28:
                                            CareerCenter = field;
                                            break;
                                        case 29:
                                            City = field;
                                            break;
                                        case 30:
                                            CitySchoolDistrict = field;
                                            break;
                                        case 31:
                                            CountyCourtDistrict = field;
                                            break;
                                        case 32:
                                            CongressionalDistrict = field;
                                            break;
                                        case 33:
                                            CourtofAppeals = field;
                                            break;
                                        case 34:
                                            EducationServiceCenter = field;
                                            break;
                                        case 35:
                                            ExemptedVillageSchoolDistrict = field;
                                            break;
                                        case 36:
                                            LibraryDistrict = field;
                                            break;
                                        case 37:
                                            LocalSchoolDistrict = field;
                                            break;
                                        case 38:
                                            MunicipalCourtDistrict = field;
                                            break;
                                        case 39:
                                            Precinct = field;
                                            break;
                                        case 40:
                                            PrecinctCode = field;
                                            break;
                                        case 41:
                                            StateBoardofEducation = field;
                                            break;
                                        case 42:
                                            StateRepresentativeDistrict = field;
                                            break;
                                        case 43:
                                            StateSenateDistrict = field;
                                            break;
                                        case 44:
                                            Township = field;
                                            break;
                                        case 45:
                                            Village = field;
                                            break;
                                        case 46:
                                            Ward = field;
                                            break;                                        
                                        default:
                                            empty = field;
                                            break;
                                    }                                   
                                     
                                }

                                //MessageBox.Show("ColumnTest " + field.ToString());
                                column++;
                            }

                            if (row != 1)
                            {
                                Voters voter = new Voters(SOSVoterId, CountyNumber, CountyId, LastName, FirstName, MiddleName, Suffix, DateOfBirth, RegistrationDate, VoterStatus, PartyAffiliation, ResidentialAddress1, ResidentialAddress2, ResidentialCity,
                                       ResidentialState, ResidentialZip, ResidentialZipPlus4, ResidentialCountry, ResidentialPostalCode, MailingAddress1, MailingAddress2, MailingCity, MailingState, MailingZip, MailingZipPlus4, MailingCountry,
                                       MailingPostalCode, CareerCenter, City, CitySchoolDistrict, CountyCourtDistrict, CongressionalDistrict, CourtofAppeals, EducationServiceCenter, ExemptedVillageSchoolDistrict, LibraryDistrict, LocalSchoolDistrict,
                                       MunicipalCourtDistrict, Precinct, PrecinctCode, StateBoardofEducation, StateRepresentativeDistrict, StateSenateDistrict, Township, Village, Ward);


                                SQLGBSCustomProcess VoterInsert = new SQLGBSCustomProcess();
                                VoterInsert.InsertVoter(voter, SQLConnect);
                                
                                
                            }

                            //log.write_log(logDirectory, "Create note for encounter: " + enc_nbr.ToString() + "...." + DateTime.Now.ToString("hh:mm:ss"));
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
                fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
                fileData.MoveTo(@"C:\Users\brads\Desktop\Voters\Files\Backup\" + fileData.Name.ToString());
                //if (fileProcessThreadCount < (threadCountToUse))                
                //{
                //    MessageBox.Show("Done");
                //    grpFileImport.Enabled = true;
                //    grpFolderImport.Enabled = true;
                //}
            }            
        }

        //UI delegate
        public delegate void UIUpdate();

        public void StartUpdate()
        {
            lblTestLabel.Text = (ReportRow.ToString() + " - " + ReportField.ToString());
            this.lblTestLabel.Refresh();
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            //Files
            var fileContent = string.Empty;
            var filePath = string.Empty;

            FolderBrowserDialog diag = new FolderBrowserDialog();

            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folder = diag.SelectedPath;  //selected folder path
                txtFolder.Text = folder;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            threadCountToUse = Process.GetCurrentProcess().Threads.Count + 1 + Convert.ToInt32(cmbThreadCount.SelectedItem.ToString());
            cmbDelimiter.Enabled = false;
            cmbQuoted.Enabled = false;
            grpFileImport.Enabled = false;
            grpFolderImport.Enabled = false;
            Thread process_folder = new Thread(processFolder);
            process_folder.IsBackground = true;
            process_folder.Start();
            //processFile(txtFile.Text.ToString());
        }

        private void processFolder()
        {           
            if (!ValidateDirectory())
            {
                MessageBox.Show("Doesn't exists");
            }

            else
            {

                System.IO.DirectoryInfo noteRoot = new DirectoryInfo(System.IO.Path.Combine(txtFolder.Text));
                System.IO.FileInfo[] noteFiles = noteRoot.GetFiles("*.txt");

                foreach (var noteFile in noteFiles)
                {
                    fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;

                    if (File.Exists(noteFile.FullName))
                    {
                        string rowNumber = "1";
                        ReportRow = rowNumber;
                        ReportField = noteFile.Name;
                        Invoke(new UIUpdate(StartUpdate));

                        while (fileProcessThreadCount >= (threadCountToUse))
                        {
                            fileProcessThreadCount = Process.GetCurrentProcess().Threads.Count;
                            Thread.Sleep(15000);
                        }

                        Thread process_file = new Thread(() => processFile(noteFile.FullName));
                        process_file.IsBackground = true;
                        process_file.Start();
                    }
                }

            }
        }

        private bool ValidateDirectory()
        {
            try
            {
                if (!Directory.Exists(this.txtFolder.Text))
                    return false;

                return true;
            }

            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Validate directory being monitored: " + ex.Message, "NextGen Note File Monitor", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
