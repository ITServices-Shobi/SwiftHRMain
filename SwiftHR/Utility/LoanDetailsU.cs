using Microsoft.AspNetCore.Http;
using SwiftHR.Controllers;
using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

namespace SwiftHR.Utility
{
    public class LoanDetailsU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<LookUpDetailsM> LoanTypeList { get; set; }
        public LoanDetailsU()
        {

        }
        public string AddLoanHeader(IFormCollection collection)
        {
            string Message = "";
            int id = 0;
            LoanHeader LoanList = new LoanHeader();

            if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
            {
                LoanHeader UpdateLoanHeader = new LoanHeader();

                int EmployeeID, EmpNO,LoanHId, LoanTypeID, EMINo, MonthlyEMI;
                decimal LoanAmt, InterestRate, PerquisiteRate, PrincipalBal, InterestBal;

                DateTime DOBLoan,SFrom, CompDate;
                
                Boolean CompLoan, DemandPromissory;

                int.TryParse(collection["LoanHeaderId"], out LoanHId);
                int.TryParse(collection["EmployeeID"], out EmployeeID);
                int.TryParse(collection["EmployeeNumber"], out EmpNO);
                int.TryParse(collection["LoanTypeId"], out LoanTypeID);
                int.TryParse(collection["EMINo"], out EMINo);
                int.TryParse(collection["MonthlyEMI"], out MonthlyEMI);

                DateTime.TryParse(collection["DateOfLoan"], out DOBLoan);
                DateTime.TryParse(collection["StartFrom"], out SFrom);
                DateTime.TryParse(collection["ComplateDates"], out CompDate);

                Boolean.TryParse(collection["LoanComplate"], out CompLoan);
                Boolean.TryParse(collection["DemandPromissory"], out DemandPromissory);

                decimal.TryParse(collection["LoanAmt"], out LoanAmt);
                decimal.TryParse(collection["InterestRate"], out InterestRate);
                decimal.TryParse(collection["PerquisiteRate"], out PerquisiteRate);
                decimal.TryParse(collection["PrincipalBal"], out PrincipalBal);
                decimal.TryParse(collection["InterestBal"], out InterestBal);

                if (LoanHId == 0)
                {
                    //if (ReimbursementList.Date != RecordExists)
                    //{
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        LoanList.EmployeeID = EmployeeID;
                        LoanList.EmployeeNumber = EmpNO;
                        LoanList.EmployeeName = collection["EmployeeName"];
                        LoanList.DateOfLoan = collection["DateOfLoan"];
                        LoanList.StartFrom = collection["StartFrom"];
                        LoanList.LoanAmount = LoanAmt;
                        LoanList.LoanCompleted = CompLoan;
                        LoanList.CompletedDate = collection["ComplateDates"];
                        LoanList.LoanType = LoanTypeID;
                        LoanList.NumberOfEMI = EMINo;
                        LoanList.InterestRate = InterestRate;
                        LoanList.MonthlyEMIAmount = MonthlyEMI;
                        LoanList.PerquisiteRate = PerquisiteRate;
                        LoanList.LoanAccountNo = collection["LoanACNo"];
                        LoanList.PrincipalBalance = PrincipalBal;
                        LoanList.InterestBalance = InterestBal;
                        LoanList.Remarks = collection["Remarks"];
                        LoanList.IsActive = false;
                        //LoanList.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                        LoanList.CreatedBy = 1;


                        entities.LoanHeader.Add(LoanList);
                        entities.SaveChanges();
                        id = LoanList.ID;
                    }
                    Message = string.Format("Successfully Added Loan Details {0}.\\n Date: {1}", LoanList.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                    //else
                    //{
                    //    Message = string.Format("Record AllReady {0}.\\n Date: {1}", ReimbursementList.Date, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    //}
                }
                else
                {
                    //using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    //{
                    //    UpdateEmpReimbursement = (from a in _context.EmpReimbursement
                    //                              where a.Id == ReimbId && a.IsActive == false
                    //                              select a).SingleOrDefault();

                    //    UpdateEmpReimbursement.EmployeeId = EmployeeID;
                    //    UpdateEmpReimbursement.EmployeeNumber = EmpNO;
                    //    UpdateEmpReimbursement.EmployeeName = collection["EmployeeName"];
                    //    UpdateEmpReimbursement.Date = Date;
                    //    UpdateEmpReimbursement.Amount = Amt;
                    //    UpdateEmpReimbursement.Remarks = collection["Remarks"];
                    //    UpdateEmpReimbursement.IsActive = false;
                    //    //UpdateEmpReimbursement.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                    //    //UpdateEmpReimbursement.UpdatedBy = 1;

                    //    entities.EmpReimbursement.Update(UpdateEmpReimbursement);
                    //    entities.SaveChanges();
                    //    id = UpdateEmpReimbursement.Id;
                    //}
                    //Message = string.Format("Record Update {0}.\\n Date: {1}", UpdateEmpReimbursement.EmployeeName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                }
            }
            return Message;

        }

        public DataSet GetListOfLoanDetails(string EmployeeId, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {
                SqlConnection con = new SqlConnection(connstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetLoanDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmployeeId);

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
