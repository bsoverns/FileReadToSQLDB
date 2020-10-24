using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using FileReadToSQLDB.Classes;

namespace FileReadToSQLDB
{
    class SQLGBSCustomProcess
    {
        #region SQLQueries
//        string _createTableSQL = @"IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GBS_appeals_file_extraction_history')
//BEGIN
//	CREATE TABLE [dbo].[GBS_appeals_file_extraction_history](
//		[image_id] [uniqueidentifier] NOT NULL,
//		[date_extracted] [datetime] NOT NULL,
//	 CONSTRAINT [PK_GBS_appeals_file_extraction_history] PRIMARY KEY CLUSTERED 
//	(
//		[image_id] ASC
//	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [NEXTGEN_CORE]
//	) ON [NEXTGEN_CORE]
//END";

        string Query = "select fa.file_archive_name, fa.image_id, ISNULL(i.password, '') [password] from file_archive fa inner join images i on i.image_id = fa.image_id left join GBS_appeals_file_extraction_history g on g.image_id = i.image_id where fa.file_type = 2 and convert(varchar, fa.create_timestamp, 112) = convert(varchar, @date, 112) and g.image_id is null ORDER BY fa.create_timestamp";
        
        string _insertVoter = @"IF NOT EXISTS(SELECT 1 FROM OHIO where SOSVoterId = @SOSVoterId)
BEGIN
	INSERT INTO OHIO (SOSVoterId, CountyNumber, CountyId, LastName, FirstName, MiddleName, Suffix, DateOfBirth, RegistrationDate, VoterStatus, PartyAffiliation, ResidentialAddress1, ResidentialAddress2, ResidentialCity, ResidentialState, ResidentialZip, ResidentialZipPlus4, ResidentialCountry, ResidentialPostalCode, MailingAddress1, MailingAddress2, MailingCity, MailingState, MailingZip, MailingZipPlus4, MailingCountry, MailingPostalCode, CareerCenter, City, CitySchoolDistrict, CountyCourtDistrict, CongressionalDistrict, CourtofAppeals, EducationServiceCenter, ExemptedVillageSchoolDistrict, LibraryDistrict, LocalSchoolDistrict, MunicipalCourtDistrict, Precinct, PrecinctCode, StateBoardofEducation, StateRepresentativeDistrict, StateSenateDistrict, Township, Village, Ward)
	VALUES (@SOSVoterId, @CountyNumber, @CountyId, @LastName, @FirstName, @MiddleName, @Suffix, @DateOfBirth, @RegistrationDate, @VoterStatus, @PartyAffiliation, @ResidentialAddress1, @ResidentialAddress2, @ResidentialCity, @ResidentialState, @ResidentialZip, @ResidentialZipPlus4, @ResidentialCountry, @ResidentialPostalCode, @MailingAddress1, @MailingAddress2, @MailingCity, @MailingState, @MailingZip, @MailingZipPlus4, @MailingCountry, @MailingPostalCode, @CareerCenter, @City, @CitySchoolDistrict, @CountyCourtDistrict, @CongressionalDistrict, @CourtofAppeals, @EducationServiceCenter, @ExemptedVillageSchoolDistrict, @LibraryDistrict, @LocalSchoolDistrict, @MunicipalCourtDistrict, @Precinct, @PrecinctCode, @StateBoardofEducation, @StateRepresentativeDistrict, @StateSenateDistrict, @Township, @Village, @Ward)
END";
        #endregion SQLQueries

        public void InsertVoter(Voters voter, SQLConnectionClass SQLConnect)
        {
            SqlConnection sqlConnString = new SqlConnection("Data Source=" + SQLConnect.dataSource + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

            try
            {
                if (SQLConnect.useIPAdress == true)
                {
                    IPAddress ip = new IPHelper().get_ip_from_host_name(SQLConnect.dataSource);
                    sqlConnString = new SqlConnection("Data Source=" + ip.ToString() + ";Initial Catalog=" + SQLConnect.initialCatalog + ";User ID=" + SQLConnect.userID + ";Password=" + SQLConnect.password + ";Connect Timeout = 10;");

                    //MessageBox.Show(ip.ToString());
                }

                SqlCommand insertVoter = sqlConnString.CreateCommand();
                insertVoter.CommandText = _insertVoter;
                insertVoter.Parameters.Add("@SOSVoterId", SqlDbType.Char);
                insertVoter.Parameters["@SOSVoterId"].Value = voter.SOSVoterId;

                insertVoter.Parameters.Add("@CountyNumber", SqlDbType.Char);
                insertVoter.Parameters["@CountyNumber"].Value = voter.CountyNumber;

                insertVoter.Parameters.Add("@CountyId", SqlDbType.Char);
                insertVoter.Parameters["@CountyId"].Value = voter.CountyId;

                insertVoter.Parameters.Add("@LastName", SqlDbType.Char);
                insertVoter.Parameters["@LastName"].Value = voter.LastName;

                insertVoter.Parameters.Add("@FirstName", SqlDbType.Char);
                insertVoter.Parameters["@FirstName"].Value = voter.FirstName;

                insertVoter.Parameters.Add("@MiddleName", SqlDbType.Char);
                insertVoter.Parameters["@MiddleName"].Value = voter.MiddleName;

                insertVoter.Parameters.Add("@Suffix", SqlDbType.Char);
                insertVoter.Parameters["@Suffix"].Value = voter.Suffix;

                insertVoter.Parameters.Add("@DateOfBirth", SqlDbType.Char);
                insertVoter.Parameters["@DateOfBirth"].Value = voter.DateOfBirth;

                insertVoter.Parameters.Add("@RegistrationDate", SqlDbType.Char);
                insertVoter.Parameters["@RegistrationDate"].Value = voter.RegistrationDate;

                insertVoter.Parameters.Add("@VoterStatus", SqlDbType.Char);
                insertVoter.Parameters["@VoterStatus"].Value = voter.VoterStatus;

                insertVoter.Parameters.Add("@PartyAffiliation", SqlDbType.Char);
                insertVoter.Parameters["@PartyAffiliation"].Value = voter.PartyAffiliation;

                insertVoter.Parameters.Add("@ResidentialAddress1", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialAddress1"].Value = voter.ResidentialAddress1;

                insertVoter.Parameters.Add("@ResidentialAddress2", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialAddress2"].Value = voter.ResidentialAddress2;
                
                insertVoter.Parameters.Add("@ResidentialCity", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialCity"].Value = voter.ResidentialCity;

                insertVoter.Parameters.Add("@ResidentialState", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialState"].Value = voter.ResidentialState;

                insertVoter.Parameters.Add("@ResidentialZip", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialZip"].Value = voter.ResidentialZip;

                insertVoter.Parameters.Add("@ResidentialZipPlus4", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialZipPlus4"].Value = voter.ResidentialZipPlus4;

                insertVoter.Parameters.Add("@ResidentialCountry", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialCountry"].Value = voter.ResidentialCountry;

                insertVoter.Parameters.Add("@ResidentialPostalCode", SqlDbType.Char);
                insertVoter.Parameters["@ResidentialPostalCode"].Value = voter.ResidentialPostalCode;

                insertVoter.Parameters.Add("@MailingAddress1", SqlDbType.Char);
                insertVoter.Parameters["@MailingAddress1"].Value = voter.MailingAddress1;

                insertVoter.Parameters.Add("@MailingAddress2", SqlDbType.Char);
                insertVoter.Parameters["@MailingAddress2"].Value = voter.MailingAddress2;

                insertVoter.Parameters.Add("@MailingCity", SqlDbType.Char);
                insertVoter.Parameters["@MailingCity"].Value = voter.MailingCity;
                
                insertVoter.Parameters.Add("@MailingState", SqlDbType.Char);
                insertVoter.Parameters["@MailingState"].Value = voter.MailingState;

                insertVoter.Parameters.Add("@MailingZip", SqlDbType.Char);
                insertVoter.Parameters["@MailingZip"].Value = voter.MailingZip;

                insertVoter.Parameters.Add("@MailingZipPlus4", SqlDbType.Char);
                insertVoter.Parameters["@MailingZipPlus4"].Value = voter.MailingZipPlus4;

                insertVoter.Parameters.Add("@MailingCountry", SqlDbType.Char);
                insertVoter.Parameters["@MailingCountry"].Value = voter.MailingCountry;

                insertVoter.Parameters.Add("@MailingPostalCode", SqlDbType.Char);
                insertVoter.Parameters["@MailingPostalCode"].Value = voter.MailingPostalCode;

                insertVoter.Parameters.Add("@CareerCenter", SqlDbType.Char);
                insertVoter.Parameters["@CareerCenter"].Value = voter.CareerCenter;

                insertVoter.Parameters.Add("@City", SqlDbType.Char);
                insertVoter.Parameters["@City"].Value = voter.City;

                insertVoter.Parameters.Add("@CitySchoolDistrict", SqlDbType.Char);
                insertVoter.Parameters["@CitySchoolDistrict"].Value = voter.CitySchoolDistrict;

                insertVoter.Parameters.Add("@CountyCourtDistrict", SqlDbType.Char);
                insertVoter.Parameters["@CountyCourtDistrict"].Value = voter.CountyCourtDistrict;

                insertVoter.Parameters.Add("@CongressionalDistrict", SqlDbType.Char);
                insertVoter.Parameters["@CongressionalDistrict"].Value = voter.CongressionalDistrict;

                insertVoter.Parameters.Add("@CourtofAppeals", SqlDbType.Char);
                insertVoter.Parameters["@CourtofAppeals"].Value = voter.CourtofAppeals;

                insertVoter.Parameters.Add("@EducationServiceCenter", SqlDbType.Char);
                insertVoter.Parameters["@EducationServiceCenter"].Value = voter.EducationServiceCenter;

                insertVoter.Parameters.Add("@ExemptedVillageSchoolDistrict", SqlDbType.Char);
                insertVoter.Parameters["@ExemptedVillageSchoolDistrict"].Value = voter.ExemptedVillageSchoolDistrict;

                insertVoter.Parameters.Add("@LibraryDistrict", SqlDbType.Char);
                insertVoter.Parameters["@LibraryDistrict"].Value = voter.LibraryDistrict;

                insertVoter.Parameters.Add("@LocalSchoolDistrict", SqlDbType.Char);
                insertVoter.Parameters["@LocalSchoolDistrict"].Value = voter.LocalSchoolDistrict;                

                insertVoter.Parameters.Add("@MunicipalCourtDistrict", SqlDbType.Char);
                insertVoter.Parameters["@MunicipalCourtDistrict"].Value = voter.MunicipalCourtDistrict;

                insertVoter.Parameters.Add("@Precinct", SqlDbType.Char);
                insertVoter.Parameters["@Precinct"].Value = voter.Precinct;

                insertVoter.Parameters.Add("@PrecinctCode", SqlDbType.Char);
                insertVoter.Parameters["@PrecinctCode"].Value = voter.PrecinctCode;

                insertVoter.Parameters.Add("@StateBoardofEducation", SqlDbType.Char);
                insertVoter.Parameters["@StateBoardofEducation"].Value = voter.StateBoardofEducation;

                insertVoter.Parameters.Add("@StateRepresentativeDistrict", SqlDbType.Char);
                insertVoter.Parameters["@StateRepresentativeDistrict"].Value = voter.StateRepresentativeDistrict;
                
                insertVoter.Parameters.Add("@StateSenateDistrict", SqlDbType.Char);
                insertVoter.Parameters["@StateSenateDistrict"].Value = voter.StateSenateDistrict;

                insertVoter.Parameters.Add("@Township", SqlDbType.Char);
                insertVoter.Parameters["@Township"].Value = voter.Township;

                insertVoter.Parameters.Add("@Village", SqlDbType.Char);
                insertVoter.Parameters["@Village"].Value = voter.Village;

                insertVoter.Parameters.Add("@Ward", SqlDbType.Char);
                insertVoter.Parameters["@Ward"].Value = voter.Ward;

                if (sqlConnString.State.ToString() == "Closed")
                {
                    sqlConnString.Open();
                }

                insertVoter.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                if (sqlConnString.State.ToString() == "Open")
                {
                    sqlConnString.Close();
                }
            }
        }

        public string cleanEnc(string encNbr)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(encNbr, "");
        }

        public string cleanBody(string body)
        {
            string newBody = body.Replace("\r\n", "\r");
            newBody = newBody.Replace("\n", "\r");
            newBody = newBody.Replace("\r", "\r\n");
            return newBody;
        }
    }
}

