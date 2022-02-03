using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Mail;

using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using SwiftHR.Utility;
using System.IO;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SwiftHR.Utility
{
    public class TimeRecordingSheetMaster
    {
        // private readonly IRepository<USpInsertTimeSheetRecord> _USpInsertTimeSheetRecordRespository;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<TimeRecordingSheetDetails> TimeSheetList { get; set; }

        private Task<List<USpInsertTimeSheetRecord>> ABC(string v, SqlParameter sqlParameter1, SqlParameter sqlParameter2)
        {
            throw new NotImplementedException();
        }

        public string InsertTimeSheetData(string TextIn, string TextOut, string TextTotal, string TextBreak, string TextNet, string TextPresence, int EmpId, int DayNo, string Day,int Month, DateTime Date)
        {
            string Message = "";
            int id = 0;

            TimeRecordingSheetDetails TimeSheet = new TimeRecordingSheetDetails();
            

            var RecordExists = (from a in _context.TimeRecordingSheetDetails
                                where a.Date == Date && a.EmpId == EmpId && a.IsActive == false
                                select a.RecTimeSheetDetailsId).SingleOrDefault();
 
            if (RecordExists == 1)
            {
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    TimeSheet = (from a in _context.TimeRecordingSheetDetails
                                 where a.RecTimeSheetDetailsId == RecordExists && a.IsActive==false
                                 select a).SingleOrDefault();
                    
                    TimeSheet.EmpId = EmpId;
                    TimeSheet.Day = Day;
                    TimeSheet.Month = Month;
                    TimeSheet.DayNo = DayNo;
                    TimeSheet.EmpIn = TextIn;
                    TimeSheet.EmpOut = TextOut;
                    TimeSheet.Total = TextTotal;
                    TimeSheet.EmpBreak = TextBreak;
                    TimeSheet.Net = TextNet;
                    TimeSheet.Presence = TextPresence;
                    TimeSheet.IsActive = false;
                    TimeSheet.IsSubmitData = false;
                    TimeSheet.Date = Convert.ToDateTime(Date);

                    entities.TimeRecordingSheetDetails.Update(TimeSheet);
                    entities.SaveChanges();
                    id = TimeSheet.RecTimeSheetDetailsId;
                }
                Message = string.Format("Record Update {0}.\\n Date: {1}", TimeSheet.Day, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

            }
            else
            {
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    TimeSheet.RecTimeSheetDetailsId = 0;

                    TimeSheet.EmpId = EmpId;
                    TimeSheet.Day = Day;
                    TimeSheet.DayNo = DayNo;
                    TimeSheet.Month = Month;
                    TimeSheet.EmpIn = TextIn;
                    TimeSheet.EmpOut = TextOut;
                    TimeSheet.Total = TextTotal;
                    TimeSheet.EmpBreak = TextBreak;
                    TimeSheet.Net = TextNet;
                    TimeSheet.Presence = TextPresence;
                    TimeSheet.IsActive = false;
                    TimeSheet.IsSubmitData = false;
                    TimeSheet.Date = Convert.ToDateTime(Date);
                    entities.TimeRecordingSheetDetails.Add(TimeSheet);
                    entities.SaveChanges();
                    id = TimeSheet.RecTimeSheetDetailsId;
                }

                Message = string.Format("Record Save {0}.\\n Date: {1}", TimeSheet.Day, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }


            //}
            return Message;

        }

        public string SubmitRecord(int HiddenRecodId)
        {
            string Message = "";
            int id = 0;
            TimeRecordingSheetDetails TimeSheet = new TimeRecordingSheetDetails();

                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    TimeSheet = (from a in _context.TimeRecordingSheetDetails
                                 where a.RecTimeSheetDetailsId == HiddenRecodId && a.IsActive == false
                                 select a).SingleOrDefault();

                    TimeSheet.IsSubmitData = true;
                    
                    entities.TimeRecordingSheetDetails.Update(TimeSheet);
                    entities.SaveChanges();
                    id = TimeSheet.RecTimeSheetDetailsId;
                }
                Message = string.Format("Record Update {0}.\\n Date: {1}", TimeSheet.Day, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            //}
            return Message;
        }


        public string ApproveRecord(int timeSheetRecId)
        {
            string Message = "";
            int id = 0;
            TimeRecordingSheetDetails TimeSheet = new TimeRecordingSheetDetails();

            using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
            {
                TimeSheet = (from a in _context.TimeRecordingSheetDetails
                             where a.RecTimeSheetDetailsId == timeSheetRecId
                             select a).SingleOrDefault();

                TimeSheet.IsApprove = true;

                entities.TimeRecordingSheetDetails.Update(TimeSheet);
                entities.SaveChanges();
                id = TimeSheet.RecTimeSheetDetailsId;
            }
            Message = string.Format("Record Update {0}.\\n Date: {1}", TimeSheet.Day, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
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
                SqlCommand cmd = new SqlCommand("RPT_ApproveAllEmployeeTimeSheetDetalis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@timeSheetDetailsIds", timeSheetDetailsIds);
               

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

        public DataSet GetListOfTimeSheetDetails(SpGetTimeSheetDetailsViewModel timerecord, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {
                
                SqlConnection con = new SqlConnection(connstring);
                string xmlString = ConvertObjectToXMLString(timerecord);
                XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetTimeSheetDetalis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Parameters", SqlDbType.Xml).Value = Convert.ToString(xElement);         
                
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


        public DataSet GetListOfEmployeeTimeSheetDetails(int EmpId,int month,int year, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
               
                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetEmployeeTimeSheetDetalis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@MonthId", month);
                cmd.Parameters.AddWithValue("@YearId", year);

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
        public DataSet GetListOfEmployeeTimeSheetDetailsByRecordId(int timeSheetRecId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);

                con.Open();
                SqlCommand cmd = new SqlCommand("RPT_GetEmployeeTimeSheetDetalisByRecordId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@timeSheetRecId", timeSheetRecId);

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
    }
}

