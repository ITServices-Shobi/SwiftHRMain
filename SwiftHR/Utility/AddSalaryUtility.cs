using Microsoft.AspNetCore.Http;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwiftHR.Utility
{
    public class AddSalaryUtility
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<SalaryHeader> salaryHeaders { get; set; }
        public List<SalaryDetails> salaryDetails { get; set; }
        public AddSalaryUtility()
        {

        }

        public string AddSalaryDetails(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            SalaryHeader SalaryHeaderList = new SalaryHeader();
            SalaryDetails SalaryDetailsList = new SalaryDetails();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                int EmployeeID;
                int SalaryID, EmployeeType, Gender, PayoutMonth;
                Boolean PFAvailability;
                DateTime DOB, DOJ, LastPayrollProceesedDate, EffectiveStartDate, EffectiveEndDate;
                int.TryParse(collection["ID"], out SalaryID);
                int.TryParse(collection["EmployeeID"], out EmployeeID);
                int.TryParse(collection["EmployeeType"], out EmployeeType);
                int.TryParse(collection["Gender"], out Gender);
                int.TryParse(collection["PayoutMonth"], out PayoutMonth);
                Boolean.TryParse(collection["PFAvailability"], out PFAvailability);

                //DateTime.TryParse(collection["LastPayrollProceesedDate"], out LastPayrollProceesedDate);
                DateTime.TryParse(collection["DOB"], out DOB);
                DateTime.TryParse(collection["DOJ"], out DOJ);
                DateTime.TryParse(collection["EffectiveStartDate"], out EffectiveStartDate);
                DateTime.TryParse(collection["EffectiveEndDate"], out EffectiveEndDate);


                decimal Basic, HRA, Bonus, OtherAllowance, Overttime, ProfTax, Loan, AdvanceSalary, EmployeeContributionPF, EmployeeContributionESIC, EmployerContributionPF,
                    EmployerContributionESIC, MonthlyNetPay, MonthlyGrossPay, AnnualGrossSalary, AnnualGrossCTC;

                decimal.TryParse(collection["Basic"], out Basic);
                decimal.TryParse(collection["HRA"], out HRA);
                decimal.TryParse(collection["Bonus"], out Bonus);
                decimal.TryParse(collection["OtherAllowance"], out OtherAllowance);
                decimal.TryParse(collection["Overttime"], out Overttime);
                decimal.TryParse(collection["ProfTax"], out ProfTax);
                decimal.TryParse(collection["Loan"], out Loan);
                decimal.TryParse(collection["AdvanceSalary"], out AdvanceSalary);
                decimal.TryParse(collection["EmployeeContributionPF"], out EmployeeContributionPF);
                decimal.TryParse(collection["EmployeeContributionESIC"], out EmployeeContributionESIC);
                decimal.TryParse(collection["EmployerContributionPF"], out EmployerContributionPF);
                decimal.TryParse(collection["EmployerContributionESIC"], out EmployerContributionESIC);
                decimal.TryParse(collection["MonthlyNetPay"], out MonthlyNetPay);
                decimal.TryParse(collection["MonthlyGrossPay"], out MonthlyGrossPay);
                decimal.TryParse(collection["AnnualGrossSalary"], out AnnualGrossSalary);
                decimal.TryParse(collection["AnnualGrossCTC"], out AnnualGrossCTC);


                SalaryHeaderList.EmployeeNumber = collection["EmployeeNumber"];
                SalaryHeaderList.EmployeeName = collection["EmployeeName"];
                SalaryHeaderList.EmployeeID = EmployeeID;
                SalaryHeaderList.EmployeeType = EmployeeType;
                SalaryHeaderList.Gender = Gender;
                SalaryHeaderList.PFAvailability = PFAvailability;
                //SalaryHeaderList.LastPayrollProceesedDate = LastPayrollProceesedDate;
                
                if (DateTime.TryParse(collection["LastPayrollProceesedDate"], out LastPayrollProceesedDate))
                {
                    SalaryHeaderList.LastPayrollProceesedDate = LastPayrollProceesedDate;
                }
                else
                {
                    // Aww.. :(
                }
                SalaryHeaderList.DOB = DOB;
                SalaryHeaderList.DOJ = DOJ;
                SalaryHeaderList.PFAvailability = PFAvailability;
                SalaryHeaderList.PFAvailability = PFAvailability;
                SalaryHeaderList.PFAvailability = PFAvailability;
                SalaryHeaderList.PayoutMonth = PayoutMonth;
                SalaryHeaderList.EffectiveStartDate = EffectiveStartDate;
                SalaryHeaderList.EffectiveEndDate = EffectiveEndDate;
                SalaryHeaderList.Remarks = collection["Remarks"];
                SalaryHeaderList.Status = "Submit";
                SalaryHeaderList.IsActive = true;
                //SalaryHeaderList.Description = collection["Description"];

                //var RecordExists = (from a in _context.SalaryHeaders
                //                    where a.EmployeeID == SalaryHeaderList.EmployeeID & a.IsActive == true
                //                    select a.EmployeeID).SingleOrDefault();

                //if (SalaryHeaderList.EmployeeID != RecordExists)
                //{
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.SalaryHeaders.Add(SalaryHeaderList);
                        entities.SaveChanges();
                        id = SalaryHeaderList.ID;

                        SalaryDetailsList.HeaderID = id;
                        SalaryDetailsList.Basic = Basic;
                        SalaryDetailsList.HRA = HRA;
                        SalaryDetailsList.Bonus = Bonus;
                        SalaryDetailsList.OtherAllowance = OtherAllowance;
                        SalaryDetailsList.Overttime = Overttime;
                        SalaryDetailsList.ProfTax = ProfTax;
                        SalaryDetailsList.Loan = Loan;
                        SalaryDetailsList.AdvanceSalary = AdvanceSalary;
                        SalaryDetailsList.EmployeeContributionPF = EmployeeContributionPF;
                        SalaryDetailsList.EmployeeContributionESIC = EmployeeContributionESIC;
                        SalaryDetailsList.EmployerContributionPF = EmployerContributionPF;
                        SalaryDetailsList.EmployerContributionESIC = EmployerContributionESIC;
                        SalaryDetailsList.MonthlyNetPay = MonthlyNetPay;
                        SalaryDetailsList.MonthlyGrossPay = MonthlyGrossPay;
                        SalaryDetailsList.AnnualGrossSalary = AnnualGrossSalary;
                        SalaryDetailsList.AnnualGrossCTC = AnnualGrossCTC;
                        SalaryDetailsList.IsActive = true;

                        entities.SalaryDetails.Add(SalaryDetailsList);
                        entities.SaveChanges();
                    }
                    Message = string.Format("Successfully Added for {0}.\\n Date: {1}", SalaryHeaderList.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                //}
                //else
                //{
                //    Message = string.Format("Record AllReady {0}.\\n Date: {1}", SalaryHeaderList.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                //}



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



        public DataSet GetListOfSalaryHeader(string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
                //string xmlString = ConvertObjectToXMLString(timerecord);
                //XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UspSalaryHeader", con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataSet GetListOfSalaryDetails(int SalHeaderId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
                //string xmlString = ConvertObjectToXMLString(timerecord);
                //XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("UspSalaryDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SalHeaderId", SalHeaderId);
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

        public string RejectRecord(int salHeaderId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.SalaryHeaders
                              where a.ID == salHeaderId
                              select a).FirstOrDefault();

                SalaryHeader RejRecord = new SalaryHeader();
                RejRecord = Record;
                RejRecord.Status = "Reject";
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.SalaryHeaders.Update(RejRecord);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Reject Successfully!! {0}.\\n Date: {1}", RejRecord.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }
        public string ApproveRecord(int salHeaderId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.SalaryHeaders
                              where a.ID == salHeaderId
                              select a).FirstOrDefault();

                SalaryHeader ApproveRecord = new SalaryHeader();
                ApproveRecord = Record;
                ApproveRecord.Status = "Approved";
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.SalaryHeaders.Update(ApproveRecord);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Approved Successfully!! {0}.\\n Date: {1}", ApproveRecord.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch (Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

            return Message;
        }

        public DataSet GetSalEmpDetails(string EmpNumber, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {
                SqlConnection con = new SqlConnection(connstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("SAL_GetEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpNo", EmpNumber);

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

        public DataSet GetSalayStatementDetails(string PayoutMonth, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {
                SqlConnection con = new SqlConnection(connstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("SAL_GetSalaryMonthlyStatement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", PayoutMonth);

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
