using SwiftHR.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeOnboardingDetails
    {
        SHR_SHOBIGROUP_DBContext dbContext = new SHR_SHOBIGROUP_DBContext();
       
        public string CallingView { get; set; }

        public Employee empDetails { get; set; }
        public EmpAddress empPermamentAddress { get; set; }
        public EmpAddress empTemporaryAddress { get; set; }
        public EmpOnboardingDetail empOnboardingDetails { get; set; }
        public List<EmpEducationDetail> empEducationDetail { get; set; }
        public EmpBankDetail empBankDetail { get; set; }
        public PrevEmploymentDetail prevEmploymentDetail1 { get; set; }
        public PrevEmploymentDetail prevEmploymentDetail2 { get; set; }
        public PrevEmploymentDetail prevEmploymentDetail3 { get; set; }
        public List<EmpDocument> empDocument { get; set; }
        public List<MasterDataItem> empMasterDataItems { get; set; }

        public EmployeeOnboardingDetails()
        {
            
            
        }
        public EmployeeOnboardingDetails(string empId)
        {
            if (empId != null && empId != "")
            {
                Employee empData = new Employee();
                empData = dbContext.Employees.Where(x => x.EmployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();
                empDetails = empData;

                //Permanent Address 
                this.empPermamentAddress = new EmpAddress();
                this.empPermamentAddress = dbContext.EmpAddress.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.IsPermanentAddress == true).ToList().SingleOrDefault();
                //Present Address
                this.empTemporaryAddress = new EmpAddress();
                this.empTemporaryAddress = dbContext.EmpAddress.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.IsPermanentAddress == false).ToList().SingleOrDefault();
                //Master Data Items
                this.empMasterDataItems = new List<MasterDataItem>();
                this.empMasterDataItems = dbContext.MasterDataItems.Where(x => x.ItemTypeId >= 1 && x.ItemTypeId <= 29).ToList();
                //Documents List
                this.empDocument = new List<EmpDocument>();
                this.empDocument = dbContext.EmpDocuments.Where(x => x.DocEmployeeId == Convert.ToInt32(empId)).ToList();
                //Previous Employment detail1
                this.prevEmploymentDetail1 = new PrevEmploymentDetail();
                if (dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 1).Count() > 0)
                {
                    this.prevEmploymentDetail1 = dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 1).ToList().SingleOrDefault();
                }
                //Previous Employment detail2
                this.prevEmploymentDetail2 = new PrevEmploymentDetail();
                if (dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 2).Count() > 0)
                {
                    this.prevEmploymentDetail2 = dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 2).ToList().SingleOrDefault();
                }
                //Previous Employment detail3
                this.prevEmploymentDetail3 = new PrevEmploymentDetail();
                if (dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 3).Count() > 0)
                {
                    this.prevEmploymentDetail3 = dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(empId) && x.PrevEmploymentOrder == 3).ToList().SingleOrDefault();
                }

                //If self enboarding is enabled
                if (Convert.ToBoolean(empData.IsSelfOnboarding))
                {
                    EmpOnboardingDetail empBoardData = new EmpOnboardingDetail();
                    empBoardData = dbContext.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();
                    if (empBoardData != null)
                    {
                        empOnboardingDetails = empBoardData;
                    }
                    else
                    {
                        empOnboardingDetails = new EmpOnboardingDetail();
                    }
                    ResetEmployeeOnboardingData(false);
                }
               

            }

        }

        public int SaveEmployeeData()
        {
            try
            {
                //int? idOnb = null;
                int idEmp = 0;
                //Adding previous employment details...
                if (prevEmploymentDetail1 != null && dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(this.empDetails.EmployeeId) && x.PrevEmploymentOrder == 1).Count() <= 0)
                {
                    if(prevEmploymentDetail1.PrevEmploymentName !=null && prevEmploymentDetail1.PrevEmploymentName != "")
                        dbContext.PrevEmploymentDetails.Add(prevEmploymentDetail1);
                }
                if (prevEmploymentDetail2 != null && dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(this.empDetails.EmployeeId) && x.PrevEmploymentOrder == 2).Count() <= 0)
                {
                    if (prevEmploymentDetail2.PrevEmploymentName != null && prevEmploymentDetail2.PrevEmploymentName != "")
                        dbContext.PrevEmploymentDetails.Add(prevEmploymentDetail2);
                }
                if (prevEmploymentDetail3 != null && dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(this.empDetails.EmployeeId) && x.PrevEmploymentOrder == 3).Count() <= 0)
                {
                    if (prevEmploymentDetail3.PrevEmploymentName != null && prevEmploymentDetail3.PrevEmploymentName != "")
                        dbContext.PrevEmploymentDetails.Add(prevEmploymentDetail3);
                }

                //Saving attached documents
                //SaveSupportingDocuments(empDocument);

                //PrevEmploymentDetail empPrevEmpDetails = new PrevEmploymentDetail();
                //empPrevEmpDetails = dbContext.PrevEmploymentDetails.Where(x => x.PrevEmployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().SingleOrDefault();
                //empData.FirstName = this.empDetails.FirstName;
                //empData.MiddleName = this.empDetails.MiddleName;
                //empData.DateOfBirth = this.empOnboardingDetails.DateOfBirth;

                if (Convert.ToBoolean(empDetails.IsSelfOnboarding))
                {
                    if (dbContext.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).Count() <= 0)
                    {

                        dbContext.EmpOnboardingDetails.Add(empOnboardingDetails);

                    }
                    ResetEmployeeOnboardingData(true);
                    idEmp = dbContext.SaveChanges();
                    ResetEmployeeOnboardingData(false);
                }
                else
                {
                    idEmp = dbContext.SaveChanges();
                }

                return idEmp;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return 0;
            }

        }

        public int ChangeOnboardingStatus(int status)
        {
           
            int idEmp = 0;

            if (Convert.ToBoolean(empDetails.IsSelfOnboarding))
            {
                if (dbContext.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).Count() > 0)
                {
                    
                    switch (status)
                    {
                        case 0:
                            this.empOnboardingDetails.OnboardingStatus = 0;
                            this.empDetails.IsSelfOnboarding = true;
                            break;
                        case 1:
                            break;
                        case 2:
                            this.empOnboardingDetails.OnboardingStatus = 2;
                            this.empDetails.IsSelfOnboarding = true;
                            break;
                        case 3:
                            this.empOnboardingDetails.OnboardingStatus = 3;
                            this.empDetails.IsSelfOnboarding = false;
                            ResetEmployeeOnboardingData(false);
                            break;
                        default:
                            break;

                    }
                }
                idEmp = this.dbContext.SaveChanges();
            }
            else
            {
                idEmp = dbContext.SaveChanges();
            }



            //if (Convert.ToBoolean(empData.IsSelfOnboarding))
            //{

            //    //this.empOnboardingDetails.OnbemployeeId = this.empDetails.EmployeeId;
            //    //this.empOnboardingDetails.FathersName = empData.FathersName;
            //    //this.empOnboardingDetails.MothersName = empData.MothersName;
            //    //this.empOnboardingDetails.SpoucesName = empData.SpouseName;

            //    using (SHR_SHOBIGROUP_DBContext dbOnb = new SHR_SHOBIGROUP_DBContext())
            //    {
            //        if (dbOnb.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().Count() <= 0)
            //        {
            //            dbOnb.EmpOnboardingDetails.Add(empOnboardingDetails);
            //            idOnb = dbOnb.SaveChanges();
            //        }
            //        else
            //        {
            //            EmpOnboardingDetail localEmpOnboardingDetails = new EmpOnboardingDetail();
            //            localEmpOnboardingDetails = dbOnb.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().SingleOrDefault();

            //            localEmpOnboardingDetails.BloodGroup = empOnboardingDetails.BloodGroup;
            //            localEmpOnboardingDetails.DateOfBirth = empOnboardingDetails.DateOfBirth;
            //            localEmpOnboardingDetails.MaritalStatus = empOnboardingDetails.MaritalStatus;
            //            localEmpOnboardingDetails.MarriageDate = empOnboardingDetails.MarriageDate;
            //            localEmpOnboardingDetails.PlaceOfBirth = empOnboardingDetails.PlaceOfBirth;
            //            localEmpOnboardingDetails.MothersName = empOnboardingDetails.SpoucesName;
            //            localEmpOnboardingDetails.FathersName = empOnboardingDetails.FathersName;
            //            localEmpOnboardingDetails.Religion = empOnboardingDetails.FathersName;
            //            localEmpOnboardingDetails.SpoucesName = empOnboardingDetails.SpoucesName;
            //            localEmpOnboardingDetails.PhysicallyChallenged = empOnboardingDetails.MothersName;
            //            localEmpOnboardingDetails.InternationalEmployee = empOnboardingDetails.SpoucesName;
            //            localEmpOnboardingDetails.PresentAddress = empOnboardingDetails.FathersName;
            //            localEmpOnboardingDetails.PermanentAddress = empOnboardingDetails.MothersName;
            //            localEmpOnboardingDetails.AlternateContactNo = empOnboardingDetails.SpoucesName;
            //            localEmpOnboardingDetails.AlternateContactName = empOnboardingDetails.FathersName;
            //            localEmpOnboardingDetails.NomineeName = empOnboardingDetails.MothersName;
            //            localEmpOnboardingDetails.RelationWithNominee = empOnboardingDetails.SpoucesName;
            //            localEmpOnboardingDetails.NomineeDob = empOnboardingDetails.FathersName;
            //            localEmpOnboardingDetails.CreatedDate = empOnboardingDetails.MothersName;
            //            localEmpOnboardingDetails.CreatedBy = empOnboardingDetails.SpoucesName;
            //        idOnb = dbOnb.SaveChanges();
            //        }
            //        //idOnb = empOnboardingDetails.OnbemployeeId;
            //    }
            //}
            //else
            //{

            //    idEmp = dbContext.SaveChanges();

            //}

            return idEmp;


        }

        private String CheckNull(String args)
        {
            if (string.IsNullOrEmpty(args))
            {
                return " ";
            }
            else return args;
        }

        public void SaveSupportingDocuments(List<EmpDocument> empDocumentLocal)
        {
            if(empDocumentLocal.Count>0)
            {
                foreach(var empDoc in empDocumentLocal.ToList())
                {
                    if (dbContext.EmpDocuments.Where(x => x.DocEmployeeId == Convert.ToInt32(this.empDetails.EmployeeId) && x.EmpDoumentName == empDoc.EmpDoumentName && x.DocumentCategory == empDoc.DocumentCategory).Count() <= 0)
                    {
                        dbContext.EmpDocuments.Add(empDoc);
                    }
                    else
                    {

                    }
                }
            }
        }
        private void ResetEmployeeOnboardingData(bool update)
        {
            if(update)
            {
                empDetails.BloodGroup = null;
                empDetails.DateOfBirth = null;
                empDetails.MaritalStatus = null;
                empDetails.MarriageDate = null;
                empDetails.PlaceOfBirth = null;
                empDetails.MothersName = null;
                empDetails.Religion = null;
                empDetails.SpouseName = null;
                //empDetails.empPermamentAddress.Address = null;
                //empDetails.PermanentAddress = null;
                empDetails.EmergencyNumber = null;
                empDetails.EmergencyContactName = null;
                empDetails.NomineeName = null;
                empDetails.NomineeContactNumber = null;
                empDetails.NomineeRelation = null;
                empDetails.NomineeDob = null;
               
            }
            else
            {
                empDetails.BloodGroup = empOnboardingDetails.BloodGroup;
                empDetails.DateOfBirth = empOnboardingDetails.DateOfBirth;
                empDetails.MaritalStatus = empOnboardingDetails.MaritalStatus;
                empDetails.MarriageDate = empOnboardingDetails.MarriageDate;
                empDetails.PlaceOfBirth = empOnboardingDetails.PlaceOfBirth;
                empDetails.MothersName = empOnboardingDetails.MothersName;
                empDetails.Religion = empOnboardingDetails.Religion;
                empDetails.SpouseName = empOnboardingDetails.SpouceName;
                //empDetails.Address = empOnboardingDetails.PresentAddress;
                //empDetails.PermanentAddress = empOnboardingDetails.PermanentAddress;
                empDetails.EmergencyNumber = empOnboardingDetails.AlternateContactNo;
                empDetails.EmergencyContactName = empOnboardingDetails.AlternateContactName;
                empDetails.NomineeName = empOnboardingDetails.NomineeName;
                empDetails.NomineeContactNumber = empOnboardingDetails.NomineeContactNumber;
                empDetails.NomineeRelation = empOnboardingDetails.RelationWithNominee;
                empDetails.NomineeDob = empOnboardingDetails.NomineeDob;
            }

        }
    }
}
