using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHR.Utility
{
    public class CompanyMaster
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public string AddCompanyMaster(IFormCollection collection)
        {
            string Message = "";
            int id = 0;

            Company Company = new Company();
            Company.CompanyId = 0;

            if (!string.IsNullOrEmpty(collection["Company"].ToString()))
            {
                Company.CompanyName = collection["Company"];
                Company.WebSiteName = collection["WebSite"];
                Company.PhoneNo = collection["Phone"];
                Company.MobNo = collection["Mobile"];
                Company.GSTNO = collection["GST"];
                Company.PANNO = collection["PAN"];
                Company.IsActive = false;
                Company.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                Company.CreatedBy = 1;

                var RecordExists = (from a in _context.CompanyMaster
                                    where a.CompanyName == Company.CompanyName.Trim() & a.IsActive == false
                                    select a.CompanyName).SingleOrDefault();

                if (Company.CompanyName != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.CompanyMaster.Add(Company);
                        entities.SaveChanges();
                        id = Company.CompanyId;
                    }
                    Message = string.Format("Successfully Added Policy Name {0}.\\n Date: {1}", Company.CompanyName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", Company.CompanyName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }


            }
            return Message;

        }

    }

}
