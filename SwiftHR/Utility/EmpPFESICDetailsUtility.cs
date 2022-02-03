using Microsoft.AspNetCore.Http;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmpPFESICDetailsUtility
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<EmpPFESICDetails> EmpPFESICDetails { get; set; }
        public EmpPFESICDetailsUtility()
        {

        }
        public string AddEmpPFESICDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            EmpPFESICDetails empPFESICDetails = new EmpPFESICDetails();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                EmpPFESICDetails UpdatePFESIC = new EmpPFESICDetails();

                int PFESICID;
                int.TryParse(collection["ID"], out PFESICID);
                int EmployeeID, BankID, AccountTypeID, PaymentMethod;
                int.TryParse(collection["EmployeeID"], out EmployeeID);
                int.TryParse(collection["BankID"], out BankID);
                int.TryParse(collection["AccountTypeID"], out AccountTypeID);
                int.TryParse(collection["PaymentMethod"], out PaymentMethod);
                DateTime effectiveDate;
                DateTime.TryParse(collection["EffectiveDateFrom"], out effectiveDate);
                Boolean ESICIsApplicable, PFIsApplicable, AllowEPFExcessContribution, AllowEPSExcessContribution;
                Boolean.TryParse(collection["ESICIsApplicable"], out ESICIsApplicable);
                Boolean.TryParse(collection["PFIsApplicable"], out PFIsApplicable);
                Boolean.TryParse(collection["AllowEPFExcessContribution"], out AllowEPFExcessContribution);
                Boolean.TryParse(collection["AllowEPSExcessContribution"], out AllowEPSExcessContribution);


                empPFESICDetails.EmployeeID = EmployeeID;
                empPFESICDetails.EmployeeNumber = collection["EmployeeNumber"];
                empPFESICDetails.EmployeeName = collection["EmployeeName"];
                empPFESICDetails.BankID = BankID;
                empPFESICDetails.BankBranch = collection["BankBranch"];
                empPFESICDetails.AccountTypeID = AccountTypeID;
                empPFESICDetails.PaymentMethod = PaymentMethod;
                empPFESICDetails.AccountNo = collection["AccountNo"];
                empPFESICDetails.IFSCCode = collection["IFSCCode"];
                empPFESICDetails.EmployeeNameAsBankRecords = collection["EmployeeNameAsBankRecords"];
                empPFESICDetails.IBAN = collection["IBAN"];
                empPFESICDetails.ESICIsApplicable = ESICIsApplicable;
                empPFESICDetails.ESICAccountNo = collection["ESICAccountNo"];
                empPFESICDetails.AllowEPFExcessContribution = AllowEPFExcessContribution;
                empPFESICDetails.AllowEPSExcessContribution = AllowEPSExcessContribution;
                empPFESICDetails.PFAccountNo = collection["PFAccountNo"];
                empPFESICDetails.UAN = collection["UAN"];
                empPFESICDetails.ESICAccountNo = collection["ESICAccountNo"];
                empPFESICDetails.IsActive = true;
                //LOPList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                //LOPList.CreatedBy = 1;

                var RecordExists = (from a in _context.EmpPFESICDetails
                                    where a.EmployeeNumber == empPFESICDetails.EmployeeNumber.Trim() & a.IsActive == true
                                    select a.EmployeeNumber).SingleOrDefault();


                if (PFESICID == 0)
                {
                    if (empPFESICDetails.EmployeeNumber != RecordExists)
                    {
                        using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                        {
                            entities.EmpPFESICDetails.Add(empPFESICDetails);
                            entities.SaveChanges();
                            id = empPFESICDetails.ID;
                        }
                        Message = string.Format("Successfully Added LOP Details {0}.\\n Date: {1}", empPFESICDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    }
                    else
                    {
                        Message = string.Format("Record AllReady {0}.\\n Date: {1}", empPFESICDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    }
                }
                else
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        UpdatePFESIC = (from a in _context.EmpPFESICDetails
                                        where a.ID == PFESICID && a.IsActive == true
                                        select a).SingleOrDefault();

                        UpdatePFESIC.EmployeeNumber = collection["EmployeeNumber"];
                        UpdatePFESIC.EmployeeName = collection["EmployeeName"];
                        UpdatePFESIC.BankID = BankID;
                        UpdatePFESIC.BankBranch = collection["BankBranch"];
                        UpdatePFESIC.AccountTypeID = AccountTypeID;
                        UpdatePFESIC.PaymentMethod = PaymentMethod;
                        UpdatePFESIC.AccountNo = collection["AccountNo"];
                        UpdatePFESIC.IFSCCode = collection["IFSCCode"];
                        UpdatePFESIC.EmployeeNameAsBankRecords = collection["EmployeeNameAsBankRecords"];
                        UpdatePFESIC.IBAN = collection["IBAN"];
                        UpdatePFESIC.ESICIsApplicable = ESICIsApplicable;
                        UpdatePFESIC.ESICAccountNo = collection["ESICAccountNo"];
                        UpdatePFESIC.AllowEPFExcessContribution = AllowEPFExcessContribution;
                        UpdatePFESIC.AllowEPSExcessContribution = AllowEPSExcessContribution;
                        UpdatePFESIC.PFAccountNo = collection["PFAccountNo"];
                        UpdatePFESIC.UAN = collection["UAN"];
                        UpdatePFESIC.ESICAccountNo = collection["ESICAccountNo"];
                        UpdatePFESIC.IsActive = true;

                        entities.EmpPFESICDetails.Update(UpdatePFESIC);
                        entities.SaveChanges();
                        id = UpdatePFESIC.ID;
                    }
                    Message = string.Format("Record Update {0}.\\n Date: {1}", UpdatePFESIC.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                }

            }
            return Message;

        }

        public string DeletePFESICRecord(int EMPARREARID)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.EmpPFESICDetails
                              where a.ID == EMPARREARID
                              select a).FirstOrDefault();

                EmpPFESICDetails PFESICDetails = new EmpPFESICDetails();
                PFESICDetails = Record;
                PFESICDetails.IsActive = false;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.EmpPFESICDetails.Update(PFESICDetails);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", PFESICDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

    }
}
