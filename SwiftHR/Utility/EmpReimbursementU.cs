using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SwiftHR.Utility
{

    public class EmpReimbursementU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<EmpReimbursement> ReimbursementDetailsList { get; set; }
        public EmpReimbursementU()
        {

        }
        public string AddReimbursementDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            EmpReimbursement ReimbursementList = new EmpReimbursement();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                EmpReimbursement UpdateEmpReimbursement = new EmpReimbursement();

                int ReimbId;
                int.TryParse(collection["ReimbursementId"], out ReimbId);
                int EmployeeID, EmpNO;
                decimal Amt;
                DateTime Date;
                int.TryParse(collection["EmployeeID"], out EmployeeID);

                int.TryParse(collection["EmployeeNumber"], out EmpNO);
                int.TryParse(collection["ReimbursementId"], out ReimbId);
                DateTime.TryParse(collection["Date"], out Date);

                decimal.TryParse(collection["Amount"], out Amt);

                //var RecordExists = (from a in _context.EmpReimbursement
                //                    where a.Date == ReimbursementList.Date.Trim() & a.IsActive == false
                //                    select a.Date).SingleOrDefault();


                if (ReimbId == 0)
                {
                    //if (ReimbursementList.Date != RecordExists)
                    //{
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {


                        ReimbursementList.EmployeeId = EmployeeID;
                        ReimbursementList.EmployeeNumber = EmpNO;
                        ReimbursementList.EmployeeName = collection["EmployeeName"];
                        ReimbursementList.Date = Date;
                        ReimbursementList.Amount = Amt;
                        ReimbursementList.Remarks = collection["Remarks"];
                        ReimbursementList.IsActive = false;
                        //ReimbursementList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                        //ReimbursementList.CreatedBy = 1;


                        entities.EmpReimbursement.Add(ReimbursementList);
                        entities.SaveChanges();
                        id = ReimbursementList.Id;
                    }
                    Message = string.Format("Successfully Added LOP Details {0}.\\n Date: {1}", ReimbursementList.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                    //else
                    //{
                    //    Message = string.Format("Record AllReady {0}.\\n Date: {1}", ReimbursementList.Date, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                }
                else
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        UpdateEmpReimbursement = (from a in _context.EmpReimbursement
                                                  where a.Id == ReimbId && a.IsActive == false
                                                  select a).SingleOrDefault();

                        UpdateEmpReimbursement.EmployeeId = EmployeeID;
                        UpdateEmpReimbursement.EmployeeNumber = EmpNO;
                        UpdateEmpReimbursement.EmployeeName = collection["EmployeeName"];
                        UpdateEmpReimbursement.Date = Date;
                        UpdateEmpReimbursement.Amount = Amt;
                        UpdateEmpReimbursement.Remarks = collection["Remarks"];
                        UpdateEmpReimbursement.IsActive = false;
                        //UpdateEmpReimbursement.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                        //UpdateEmpReimbursement.UpdatedBy = 1;

                        entities.EmpReimbursement.Update(UpdateEmpReimbursement);
                        entities.SaveChanges();
                        id = UpdateEmpReimbursement.Id;
                    }
                    Message = string.Format("Record Update {0}.\\n Date: {1}", UpdateEmpReimbursement.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                }



            }
            return Message;

        }

        public string DeleteReimbursementRecord(int EMPReimbID)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.EmpReimbursement
                              where a.Id == EMPReimbID
                              select a).FirstOrDefault();

                EmpReimbursement ReimbursementDetails = new EmpReimbursement();
                ReimbursementDetails = Record;
                ReimbursementDetails.IsActive = true;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.EmpReimbursement.Update(ReimbursementDetails);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", ReimbursementDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

    }
}
