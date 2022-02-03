using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace SwiftHR.Utility
{
    public class CreateEmpPayRollMonthU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<LookUpDetailsM> MonthNameList { get; set; }

        public CreateEmpPayRollMonthU()
        {

        }
        public string AddEmpPayRollMonthDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            CreateEmpPayRollMonth EmpPayRollList = new CreateEmpPayRollMonth();

            if (!string.IsNullOrEmpty(collection["MonthName"].ToString()))
            {
                CreateEmpPayRollMonth UpdateEmpPayRollMonth = new CreateEmpPayRollMonth();

                int CreatePayRollId, MonthId;
                int.TryParse(collection[""], out CreatePayRollId);
                int.TryParse(collection["txtMonthNAmeId"], out MonthId);
                
                DateTime FromDate, ToDate;
                
                DateTime.TryParse(collection["FromPeriodDate"], out FromDate);
                DateTime.TryParse(collection["ToPeriodDate"], out ToDate);

                if (CreatePayRollId == 0)
                {
                    //if (ReimbursementList.Date != RecordExists)
                    //{
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {


                        EmpPayRollList.PayRollMonth= MonthId;
                        EmpPayRollList.FromPayRollPeriod = FromDate;
                        EmpPayRollList.ToPayRollPeriod= ToDate;

                        EmpPayRollList.Status = true;
                        EmpPayRollList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE);
                        EmpPayRollList.CreatedBy = 1;


                        entities.CreateEmpPayRollMonth.Add(EmpPayRollList);
                        entities.SaveChanges();
                        id = EmpPayRollList.Id;
                    }
                    Message = string.Format("Successfully Create PayRoll Month {0}.\\n Date: {1}", EmpPayRollList.PayRollMonth, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
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
                        UpdateEmpPayRollMonth = (from a in _context.CreateEmpPayRollMonth
                                                  where a.Id == CreatePayRollId && a.IsActive == true
                                                  select a).SingleOrDefault();

                        UpdateEmpPayRollMonth.PayRollMonth = MonthId;
                        UpdateEmpPayRollMonth.FromPayRollPeriod = FromDate;
                        UpdateEmpPayRollMonth.ToPayRollPeriod = ToDate;

                        UpdateEmpPayRollMonth.Status = true;
                        UpdateEmpPayRollMonth.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE);
                        UpdateEmpPayRollMonth.UpdatedBy = 1;

                        entities.CreateEmpPayRollMonth.Update(UpdateEmpPayRollMonth);
                        entities.SaveChanges();
                        id = UpdateEmpPayRollMonth.Id;
                    }
                    Message = string.Format("Record Update {0}.\\n Date: {1}", UpdateEmpPayRollMonth.PayRollMonth, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                }



            }
            return Message;

        }

    }

}
