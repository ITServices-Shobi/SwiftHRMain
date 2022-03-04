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

    public class EmpReimbursementU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<EmpReimbursement> ReimbursementDetailsList { get; set; }
        public List<Employee> EmployeeDetailsList { get; set; }

        public List<LookUpDetailsM> EarningscomponentsList { get; set; }

        public List<UspEmpReimbursementDetailsViewModel> EarningscomponentsListData { get; set; }
        public EmpReimbursementU()
        {

        }
        public string AddReimbursementDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            EmpReimbursement ReimbursementList = new EmpReimbursement();

            if (!string.IsNullOrEmpty(collection["EmpCodeName"].ToString()))
            {
                EmpReimbursement UpdateEmpReimbursement = new EmpReimbursement();

                int ReimbId,EarningType;
                int.TryParse(collection["ReimbursementId"], out ReimbId);
                int.TryParse(collection["EarningType"], out EarningType);
                int EmployeeID, EmpNO;
                decimal Amt;
                DateTime CreatedDate, Date, PaymentEffectedDate;
                int.TryParse(collection["EmployeeID"], out EmployeeID);

                int.TryParse(collection["EmpCodeName"], out EmpNO);
                int.TryParse(collection["Id"], out ReimbId);
                DateTime.TryParse(collection["Date"], out Date);
                DateTime.TryParse(collection["EffectedDate"], out PaymentEffectedDate);

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
                        ReimbursementList.ComponentsType = collection["ComponentsType"];
                        ReimbursementList.EarningsTypeFromLookUp = EarningType;
                        ReimbursementList.Date = Date;
                        ReimbursementList.Amount = Amt;
                        ReimbursementList.Remarks = collection["Remarks"];
                        
                        ReimbursementList.IsActive = true;
                        ReimbursementList.PaymentEffectedDate = PaymentEffectedDate;
                        ReimbursementList.Status = "Apply";
                        ReimbursementList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE);
                        ReimbursementList.CreatedBy = 1;


                        entities.EmpReimbursement.Add(ReimbursementList);
                        entities.SaveChanges();
                        id = ReimbursementList.Id;
                    }
                    Message = string.Format("Successfully Added Reimbursement Details {0}.\\n Date: {1}", ReimbursementList.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
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
                                                  where a.Id == ReimbId && a.IsActive == true
                                                  select a).SingleOrDefault();

                        UpdateEmpReimbursement.EmployeeId = EmployeeID;
                        UpdateEmpReimbursement.EmployeeNumber = EmpNO;

                        UpdateEmpReimbursement.EmployeeName = collection["EmployeeName"];
                        UpdateEmpReimbursement.Date = Date;
                        UpdateEmpReimbursement.Amount = Amt;
                        UpdateEmpReimbursement.Remarks = collection["Remarks"];
                        UpdateEmpReimbursement.IsActive = true;
                        UpdateEmpReimbursement.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE);
                        UpdateEmpReimbursement.UpdatedBy = 1;

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

        public string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public DataSet GetListOfReimbursementDetails(int EmpId,string EmpCode,int MonthId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
                //string xmlString = ConvertObjectToXMLString(timerecord);
                //XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetEmpReimbursementApproved", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
                cmd.Parameters.AddWithValue("@MonthId", MonthId);
                //cmd.Parameters.Add("@Parameters", SqlDbType.Xml).Value = Convert.ToString(xElement);

                cmd.CommandTimeout = 150000;
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = 0;
            }
            return ds;
        }
        public string ApproveRecord(int ReimbursementRecId)
        {
            string Message = "";
            int id = 0;
            EmpReimbursement TimeSheet = new EmpReimbursement();

            using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
            {
                TimeSheet = (from a in _context.EmpReimbursement
                             where a.Id == ReimbursementRecId
                             select a).SingleOrDefault();

                TimeSheet.Status = "Approved";
                TimeSheet.ApprovedDate = System.DateTime.Now;
                
                entities.EmpReimbursement.Update(TimeSheet);
                entities.SaveChanges();
                id = TimeSheet.Id;
            }
            Message = string.Format("Record Update {0}.\\n Date: {1}", TimeSheet.Date, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            //}
            return Message;

        }
        public DataSet ApproveAllRecord(string timeSheetDetailsIds, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);

                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_ApproveAllEmployeeReimbursementDetalis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReimbursementDetailsIds", timeSheetDetailsIds);


                cmd.CommandTimeout = 150000;
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = 0;
            }
            return ds;
        }

        public DataSet GetListOfReimbursementDetailsById(int EmpRemId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
                //string xmlString = ConvertObjectToXMLString(timerecord);
                //XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetEmpReimbursementApproved", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReimbursementRecId", EmpRemId);
                //cmd.Parameters.Add("@Parameters", SqlDbType.Xml).Value = Convert.ToString(xElement);

                cmd.CommandTimeout = 150000;
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = 0;
            }
            return ds;
        }

        public DataSet GetReimbursementDetails(string EmployeeId,string ReimbId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {
                SqlConnection con = new SqlConnection(connstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("EmpReimbursementDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmployeeId);
                cmd.Parameters.AddWithValue("@Id", ReimbId);
                cmd.CommandTimeout = 150000;
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = 0;
            }
            return ds;
        }

        public string DeleteReimbusment(int ReimbId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.EmpReimbursement
                              where a.Id == ReimbId
                              select a).FirstOrDefault();

                EmpReimbursement Reimbu = new EmpReimbursement();
                Reimbu = Record;
                Reimbu.IsActive = false;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.EmpReimbursement.Update(Reimbu);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", Reimbu.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

    }
}
