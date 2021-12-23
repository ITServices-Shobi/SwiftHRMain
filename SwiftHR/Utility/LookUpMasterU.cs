using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHR.Utility
{
    public class LookUpMasterU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<LookUpM> LookUpList { get; set; }
        public List<LookUpDetailsM> LookUpDetailsList { get; set; }
        public LookUpMasterU()
        {

        }
        public string AddLookUp(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            LookUpM LookupList = new LookUpM();

            if (!string.IsNullOrEmpty(collection["LookUpName"].ToString()))
            {
                LookupList.LookUpName = collection["LookUpName"];
                LookupList.LookUpCode = collection["Code"];
                LookupList.IsActive = false;
                LookupList.Description = collection["Description"];

                var RecordExists = (from a in _context.LookUpM
                                    where a.LookUpName == LookupList.LookUpName.Trim() & a.IsActive == false
                                    select a.LookUpName).SingleOrDefault();

                if (LookupList.LookUpName != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.LookUpM.Add(LookupList);
                        entities.SaveChanges();
                        id = LookupList.LookUpId;
                    }
                    Message = string.Format("Successfully Added Policy Name {0}.\\n Date: {1}", LookupList.LookUpName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", LookupList.LookUpName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }


            }
            return Message;

        }
        public string AddLookUpDeatisl(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            LookUpDetailsM LookUpListDetails = new LookUpDetailsM();

            int LookUpId;
            int.TryParse(collection["LookUpId"], out LookUpId);

            if (!string.IsNullOrEmpty(collection["LookUpDetails"].ToString()))
            {
                LookUpListDetails.LookUpId = LookUpId;
                LookUpListDetails.Name = collection["LookUpDetails"];
                LookUpListDetails.Description = collection["DetailsDesc"];
                LookUpListDetails.IsActive = false;
                var RecordExists = (from a in _context.LookUpDetailsM
                                    where a.Name == LookUpListDetails.Name.Trim() & a.IsActive == false
                                    select a.Name).SingleOrDefault();

                if (LookUpListDetails.Name != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.LookUpDetailsM.Add(LookUpListDetails);
                        entities.SaveChanges();
                        id = LookUpListDetails.LookUpId;
                    }
                    Message = string.Format("Successfully Added Policy Name {0}.\\n Date: {1}", LookUpListDetails.Name, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", LookUpListDetails.Name, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }


            }
            return Message;

        }

        public string DeleteLookUp(int LookUpId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.LookUpM
                              where a.LookUpId == LookUpId
                              select a).FirstOrDefault();

                LookUpM LookUp = new LookUpM();
                LookUp = Record;
                LookUp.IsActive = true;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.LookUpM.Update(LookUp);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", LookUp.LookUpName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

        public string DeleteLookUpDetails(int LookUpDetailsId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.LookUpDetailsM
                              where a.LookUpDetailsId == LookUpDetailsId
                              select a).FirstOrDefault();

                LookUpDetailsM LookUpDetails = new LookUpDetailsM();
                LookUpDetails = Record;
                LookUpDetails.IsActive = true;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.LookUpDetailsM.Update(LookUpDetails);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", LookUpDetails.Name, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }
    }

}
