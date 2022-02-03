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
    public class SalaryMonthlyStatementU
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<USpSalaryMonthlyStatement> SalaryProcessData { get; set; }

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
        public DataSet ProcessData(int Month, int Yeaer, string connstring)
        {
            DataSet ds = new DataSet();
            int flag = 0;
            try
            {

                SqlConnection con = new SqlConnection(connstring);
                //string xmlString = ConvertObjectToXMLString(timerecord);
                //XElement xElement = XElement.Parse(xmlString);
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_RPT_ProcessSalaryMonthStatment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Yeaer);
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
    }

}
