using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public int? CompanyId { get; set; }
        public int? EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyNumber { get; set; }
        public string Email { get; set; }
        public string PersonalEmail { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string EmployeeProfilePhoto { get; set; }
        public string SpouseName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string NomineeName { get; set; }
        public string NomineeContactNumber { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeDob { get; set; }
        public string ProbationPeriod { get; set; }
        public string ReportingManager { get; set; }
        public string DateOfJoining { get; set; }
        public string ConfirmationDate { get; set; }
        public string EmployeeStatus { get; set; }
        public string MaritalStatus { get; set; }
        public string MarriageDate { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string FunctionalGrade { get; set; }
        public string Level { get; set; }
        public string SubLevel { get; set; }
        public string Location { get; set; }
        public string CostCenter { get; set; }
        public string Pannumber { get; set; }
        public string Panname { get; set; }
        public string AdharCardNumber { get; set; }
        public string AdharCardName { get; set; }
        public string PassportNumber { get; set; }
        public string PassportExpiryDate { get; set; }
        public string Pfnumber { get; set; }
        public string Uannumber { get; set; }
        public bool? IncludeEsi { get; set; }
        public bool? IncludeLwf { get; set; }
        public string PaymentMethod { get; set; }
        public bool? IsSelfOnboarding { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string DateOfResignation { get; set; }
        public string DateOfLastWorking { get; set; }
    }
}
