using Microsoft.AspNetCore.Http;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmpArrearDetailsUtility
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<EmpArrearDetails> EmpArrearDetails { get; set; }
        public EmpArrearDetailsUtility()
        {

        }
        public string AddEmpArrearDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            EmpArrearDetails empArrearDetails = new EmpArrearDetails();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                EmpArrearDetails UpdateArrears = new EmpArrearDetails();

                int ArrearID;
                int.TryParse(collection["ID"], out ArrearID);
                int EmployeeID, PayrollMonth;
                int.TryParse(collection["EmployeeID"], out EmployeeID);
                int.TryParse(collection["PayrollMonth"], out PayrollMonth);
                DateTime effectiveDate;
                DateTime.TryParse(collection["EffectiveDateFrom"], out effectiveDate);
                decimal amount;
                decimal.TryParse(collection["Amount"], out amount);


                empArrearDetails.EmployeeID = EmployeeID;
                empArrearDetails.EmployeeNumber = collection["EmployeeNumber"];
                empArrearDetails.EmployeeName = collection["EmployeeName"];
                empArrearDetails.PayrollMonth = PayrollMonth;
                empArrearDetails.EffectiveDateFrom =  effectiveDate;
                empArrearDetails.Amount = amount;
                empArrearDetails.Remarks = collection["Remarks"];
                empArrearDetails.IsActive = true;
                //LOPList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                //LOPList.CreatedBy = 1;

                var RecordExists = (from a in _context.EmpArrearDetails
                                    where a.EmployeeNumber == empArrearDetails.EmployeeNumber.Trim() & a.EffectiveDateFrom == empArrearDetails.EffectiveDateFrom 
                                    & a.Amount == empArrearDetails.Amount & a.IsActive == false select a.EmployeeNumber).SingleOrDefault();


                if (ArrearID == 0)
                {
                    //if (empArrearDetails.EmployeeNumber != RecordExists)
                    //{
                        using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                        {
                            entities.EmpArrearDetails.Add(empArrearDetails);
                            entities.SaveChanges();
                            id = empArrearDetails.ID;
                        }
                        Message = string.Format("Successfully Added LOP Details {0}.\\n Date: {1}", empArrearDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                    //else
                    //{
                    //    Message = string.Format("Record AllReady {0}.\\n Date: {1}", empArrearDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                }
                else
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        UpdateArrears = (from a in _context.EmpArrearDetails
                                     where a.ID == ArrearID && a.IsActive == false select a).SingleOrDefault();

                        UpdateArrears.EmployeeNumber = collection["EmployeeNumber"];
                        UpdateArrears.EmployeeName = collection["EmployeeName"];
                        UpdateArrears.PayrollMonth = PayrollMonth;
                        UpdateArrears.EffectiveDateFrom = effectiveDate;
                        UpdateArrears.Amount = amount;
                        UpdateArrears.Remarks = collection["Remarks"];
                        UpdateArrears.IsActive = true;

                        entities.EmpArrearDetails.Update(UpdateArrears);
                        entities.SaveChanges();
                        id = UpdateArrears.ID;
                    }
                    Message = string.Format("Record Update {0}.\\n Date: {1}", UpdateArrears.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                }

            }
            return Message;

        }

        public string DeleteArrearsRecord(int EMPARREARID)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.EmpArrearDetails
                              where a.ID == EMPARREARID
                              select a).FirstOrDefault();

                EmpArrearDetails arrearDetails = new EmpArrearDetails();
                arrearDetails = Record;
                arrearDetails.IsActive = false;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.EmpArrearDetails.Update(arrearDetails);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", arrearDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }


    }
}
