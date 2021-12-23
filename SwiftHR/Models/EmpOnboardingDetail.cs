
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpOnboardingDetail
    {
       
        public int EmpOnboardingDetailsId { get; set; }
        public int? OnbemployeeId { get; set; }
        public string BloodGroup { get; set; }
        public string DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string MarriageDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MothersName { get; set; }
        //public string FathersName { get; set; }
        public string SpouceName { get; set; }
        public string Religion { get; set; }
        public bool? PhysicallyChallenged { get; set; }
        public bool? InternationalEmployee { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string AlternateContactNo { get; set; }
        public string AlternateContactName { get; set; }
        public string NomineeName { get; set; }
        public string NomineeContactNumber { get; set; }
        public string RelationWithNominee { get; set; }
        public string NomineeDob { get; set; }
        public int OnboardingStatus { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

    }
}
