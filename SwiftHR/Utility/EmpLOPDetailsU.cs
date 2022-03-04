using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHR.Utility
{

    public class EmpLOPDetailsU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<EmpLOPDetails> LOPDetailsList { get; set; }
        public List<LookUpDetailsM> LoanTypeList { get; set; }
        public EmpLOPDetailsU()
        {

        }
        public string AddEmpLOPDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            EmpLOPDetails LOPList = new EmpLOPDetails();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                EmpLOPDetails UpdateLOP = new EmpLOPDetails();


                int EmployeeID, LOPMonth,LOPId;
                int.TryParse(collection["ID"], out LOPId);
                int.TryParse(collection["EmployeeID"], out EmployeeID);
                int.TryParse(collection["LOPMonth"], out LOPMonth);
                

                LOPList.EmployeeID = EmployeeID;
                LOPList.EmployeeNumber = collection["EmployeeNumber"];
                LOPList.EmployeeName= collection["EmployeeName"];
                LOPList.LOPMonth = LOPMonth;
                LOPList.TotalLOPDays = collection["TotalLOPDays"];
                LOPList.Remarks = collection["Remarks"];
                LOPList.IsActive = true;
                //LOPList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                //LOPList.CreatedBy = 1;

                var RecordExists = (from a in _context.EmpLOPDetails
                                    where a.EmployeeNumber == LOPList.EmployeeNumber.Trim() & a.IsActive == true
                                    select a.EmployeeNumber).SingleOrDefault();


                if (LOPId == 0)
                {
                    if (LOPList.EmployeeNumber != RecordExists)
                    {
                        using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                        {
                            entities.EmpLOPDetails.Add(LOPList);
                            entities.SaveChanges();
                            id = LOPList.ID;
                        }
                        Message = string.Format("Successfully Added LOP Details {0}.\\n Date: {1}", LOPList.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    }
                    else
                    {
                        Message = string.Format("Record AllReady {0}.\\n Date: {1}", LOPList.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    }
                }
                else
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        UpdateLOP = (from a in _context.EmpLOPDetails
                                     where a.ID == LOPList.ID && a.IsActive == true
                                     select a).SingleOrDefault();
                        LOPList.EmployeeID = EmployeeID;
                        LOPList.EmployeeNumber = collection["EmployeeNumber"];
                        LOPList.EmployeeName = collection["EmployeeName"];
                        LOPList.LOPMonth = LOPMonth;
                        LOPList.TotalLOPDays = collection["TotalLOPDays"];
                        LOPList.Remarks = collection["Remarks"];
                        LOPList.IsActive = true;

                        entities.EmpLOPDetails.Update(LOPList);
                        entities.SaveChanges();
                        id = LOPList.ID;
                    }
                    Message = string.Format("Record Update {0}.\\n Date: {1}", LOPList.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
            }
            return Message;
        }

        public string DeleteLOPRecord(int EMPLOPID)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.EmpLOPDetails
                              where a.ID == EMPLOPID
                              select a).FirstOrDefault();

                EmpLOPDetails LOPDetails = new EmpLOPDetails();
                LOPDetails = Record;
                LOPDetails.IsActive = false;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.EmpLOPDetails.Update(LOPDetails);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", LOPDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

    }
}
