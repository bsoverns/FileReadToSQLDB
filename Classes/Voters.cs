using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReadToSQLDB.Classes
{
    public class Voters
    {
        private string _SOSVoterId;
        private string _CountyNumber;
        private string _CountyId;
        private string _LastName;
        private string _FirstName;
        private string _MiddleName;
        private string _Suffix;
        private string _DateOfBirth;
        private string _RegistrationDate;
        private string _VoterStatus;
        private string _PartyAffiliation;
        private string _ResidentialAddress1;
        private string _ResidentialAddress2;
        private string _ResidentialCity;
        private string _ResidentialState;
        private string _ResidentialZip;
        private string _ResidentialZipPlus4;
        private string _ResidentialCountry;
        private string _ResidentialPostalCode;
        private string _MailingAddress1;
        private string _MailingAddress2;
        private string _MailingCity;
        private string _MailingState;
        private string _MailingZip;
        private string _MailingZipPlus4;
        private string _MailingCountry;
        private string _MailingPostalCode;
        private string _CareerCenter;
        private string _City;
        private string _CitySchoolDistrict;
        private string _CountyCourtDistrict;
        private string _CongressionalDistrict;
        private string _CourtofAppeals;
        private string _EducationServiceCenter;
        private string _ExemptedVillageSchoolDistrict;
        private string _LibraryDistrict;
        private string _LocalSchoolDistrict;
        private string _MunicipalCourtDistrict;
        private string _Precinct;
        private string _PrecinctCode;
        private string _StateBoardofEducation;
        private string _StateRepresentativeDistrict;
        private string _StateSenateDistrict;
        private string _Township;
        private string _Village;
        private string _Ward;

        public Voters(string SOSVoterId, string CountyNumber, string CountyId, string LastName, string FirstName, string MiddleName, string Suffix, string DateOfBirth, string RegistrationDate, string VoterStatus, string PartyAffiliation, string ResidentialAddress1, string ResidentialAddress2, string ResidentialCity, string ResidentialState, string ResidentialZip, string ResidentialZipPlus4, string ResidentialCountry, string ResidentialPostalCode, string MailingAddress1, string MailingAddress2, string MailingCity, string MailingState, string MailingZip, string MailingZipPlus4, string MailingCountry, string MailingPostalCode, string CareerCenter, string City, string CitySchoolDistrict, string CountyCourtDistrict, string CongressionalDistrict, string CourtofAppeals, string EducationServiceCenter, string ExemptedVillageSchoolDistrict, string LibraryDistrict, string LocalSchoolDistrict, string MunicipalCourtDistrict, string Precinct, string PrecinctCode, string StateBoardofEducation, string StateRepresentativeDistrict, string StateSenateDistrict, string Township, string Village, string Ward)
        {
            this._SOSVoterId = SOSVoterId;
            this._CountyNumber = CountyNumber;
            this._CountyId = CountyId;
            this._LastName = LastName;
            this._FirstName = FirstName;
            this._MiddleName = MiddleName;
            this._Suffix = Suffix;
            this._DateOfBirth = DateOfBirth;
            this._RegistrationDate = RegistrationDate;
            this._VoterStatus = VoterStatus;
            this._PartyAffiliation = PartyAffiliation;
            this._ResidentialAddress1 = ResidentialAddress1;
            this._ResidentialAddress2 = ResidentialAddress2;
            this._ResidentialCity = ResidentialCity;
            this._ResidentialState = ResidentialState;
            this._ResidentialZip = ResidentialZip;
            this._ResidentialZipPlus4 = ResidentialZipPlus4;
            this._ResidentialCountry = ResidentialCountry;
            this._ResidentialPostalCode = ResidentialPostalCode;
            this._MailingAddress1 = MailingAddress1;
            this._MailingAddress2 = MailingAddress2;
            this._MailingCity = MailingCity;
            this._MailingState = MailingState;
            this._MailingZip = MailingZip;
            this._MailingZipPlus4 = MailingZipPlus4;
            this._MailingCountry = MailingCountry;
            this._MailingPostalCode = MailingPostalCode;
            this._CareerCenter = CareerCenter;
            this._City = City;
            this._CitySchoolDistrict = CitySchoolDistrict;
            this._CountyCourtDistrict = CountyCourtDistrict;
            this._CongressionalDistrict = CongressionalDistrict;
            this._CourtofAppeals = CourtofAppeals;
            this._EducationServiceCenter = EducationServiceCenter;
            this._ExemptedVillageSchoolDistrict = ExemptedVillageSchoolDistrict;
            this._LibraryDistrict = LibraryDistrict;
            this._LocalSchoolDistrict = LocalSchoolDistrict;
            this._MunicipalCourtDistrict = MunicipalCourtDistrict;
            this._Precinct = Precinct;
            this._PrecinctCode = PrecinctCode;
            this._StateBoardofEducation = StateBoardofEducation;
            this._StateRepresentativeDistrict = StateRepresentativeDistrict;
            this._StateSenateDistrict = StateSenateDistrict;
            this._Township = Township;
            this._Village = Village;
            this._Ward = Ward;
        }

        public string SOSVoterId
        {
            get { return _SOSVoterId; }
            set { this._SOSVoterId = value; }
        }

        public string CountyNumber
        {
            get { return _CountyNumber; }
            set { this._CountyNumber = value; }
        }

        public string CountyId
        {
            get { return _CountyId; }
            set { this._CountyId = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { this._LastName = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { this._FirstName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { this._MiddleName = value; }
        }

        public string Suffix
        {
            get { return _Suffix; }
            set { this._Suffix = value; }
        }

        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { this._DateOfBirth = value; }
        }

        public string RegistrationDate
        {
            get { return _RegistrationDate; }
            set { this._RegistrationDate = value; }
        }

        public string VoterStatus
        {
            get { return _VoterStatus; }
            set { this._VoterStatus = value; }
        }

        public string PartyAffiliation
        {
            get { return _PartyAffiliation; }
            set { this._PartyAffiliation = value; }
        }

        public string ResidentialAddress1
        {
            get { return _ResidentialAddress1; }
            set { this._ResidentialAddress1 = value; }
        }

        public string ResidentialAddress2
        {
            get { return _ResidentialAddress2; }
            set { this._ResidentialAddress2 = value; }
        }

        public string ResidentialCity
        {
            get { return _ResidentialCity; }
            set { this._ResidentialCity = value; }
        }

        public string ResidentialState
        {
            get { return _ResidentialState; }
            set { this._ResidentialState = value; }
        }

        public string ResidentialZip
        {
            get { return _ResidentialZip; }
            set { this._ResidentialZip = value; }
        }

        public string ResidentialZipPlus4
        {
            get { return _ResidentialZipPlus4; }
            set { this._ResidentialZipPlus4 = value; }
        }

        public string ResidentialCountry
        {
            get { return _ResidentialCountry; }
            set { this._ResidentialCountry = value; }
        }

        public string ResidentialPostalCode
        {
            get { return _ResidentialPostalCode; }
            set { this._ResidentialPostalCode = value; }
        }

        public string MailingAddress1
        {
            get { return _MailingAddress1; }
            set { this._MailingAddress1 = value; }
        }

        public string MailingAddress2
        {
            get { return _MailingAddress2; }
            set { this._MailingAddress2 = value; }
        }

        public string MailingCity
        {
            get { return _MailingCity; }
            set { this._MailingCity = value; }
        }

        public string MailingState
        {
            get { return _MailingState; }
            set { this._MailingState = value; }
        }

        public string MailingZip
        {
            get { return _MailingZip; }
            set { this._MailingZip = value; }
        }

        public string MailingZipPlus4
        {
            get { return _MailingZipPlus4; }
            set { this._MailingZipPlus4 = value; }
        }

        public string MailingCountry
        {
            get { return _MailingCountry; }
            set { this._MailingCountry = value; }
        }

        public string MailingPostalCode
        {
            get { return _MailingPostalCode; }
            set { this._MailingPostalCode = value; }
        }

        public string CareerCenter
        {
            get { return _CareerCenter; }
            set { this._CareerCenter = value; }
        }

        public string City
        {
            get { return _City; }
            set { this._City = value; }
        }

        public string CitySchoolDistrict
        {
            get { return _CitySchoolDistrict; }
            set { this._CitySchoolDistrict = value; }
        }

        public string CountyCourtDistrict
        {
            get { return _CountyCourtDistrict; }
            set { this._CountyCourtDistrict = value; }
        }

        public string CongressionalDistrict
        {
            get { return _CongressionalDistrict; }
            set { this._CongressionalDistrict = value; }
        }

        public string CourtofAppeals
        {
            get { return _CourtofAppeals; }
            set { this._CourtofAppeals = value; }
        }

        public string EducationServiceCenter
        {
            get { return _EducationServiceCenter; }
            set { this._EducationServiceCenter = value; }
        }

        public string ExemptedVillageSchoolDistrict
        {
            get { return _ExemptedVillageSchoolDistrict; }
            set { this._ExemptedVillageSchoolDistrict = value; }
        }

        public string LibraryDistrict
        {
            get { return _LibraryDistrict; }
            set { this._LibraryDistrict = value; }
        }

        public string LocalSchoolDistrict
        {
            get { return _LocalSchoolDistrict; }
            set { this._LocalSchoolDistrict = value; }
        }

        public string MunicipalCourtDistrict
        {
            get { return _MunicipalCourtDistrict; }
            set { this._MunicipalCourtDistrict = value; }
        }

        public string Precinct
        {
            get { return _Precinct; }
            set { this._Precinct = value; }
        }

        public string PrecinctCode
        {
            get { return _PrecinctCode; }
            set { this._PrecinctCode = value; }
        }

        public string StateBoardofEducation
        {
            get { return _StateBoardofEducation; }
            set { this._StateBoardofEducation = value; }
        }

        public string StateRepresentativeDistrict
        {
            get { return _StateRepresentativeDistrict; }
            set { this._StateRepresentativeDistrict = value; }
        }

        public string StateSenateDistrict
        {
            get { return _StateSenateDistrict; }
            set { this._StateSenateDistrict = value; }
        }

        public string Township
        {
            get { return _Township; }
            set { this._Township = value; }
        }

        public string Village
        {
            get { return _Village; }
            set { this._Village = value; }
        }

        public string Ward
        {
            get { return _Ward; }
            set { this._Ward = value; }
        }

    }
}
