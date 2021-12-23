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


namespace SwiftHR.Utility
{
    public class PolicyMaster
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public List<AttandancePolicy> PolicyList { get; set; }
        public List<AttandancePolicyRulesCategory> PolicyCategoryList { get; set; }

        public PolicyMaster() {

        }

        public List<AttandancePolicy>PolicyMasterDataItems { get; set; }
        public string AddPolicyMaster(IFormCollection collection)
        {
            string Message = "";
            int id = 0;

            AttandancePolicy AttandPolicy = new AttandancePolicy();
            AttandPolicy.AttandancePolicyId = 0;

            if (!string.IsNullOrEmpty(collection["Policy"].ToString()))
            {
                AttandPolicy.AttandancePolicyName = collection["Policy"];
                AttandPolicy.Description =collection["Desc"];
                AttandPolicy.IsActive = false;
                AttandPolicy.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                AttandPolicy.CreatedBy = 1;

                var RecordExists = (from a in _context.AttandancePolicies
                                    where a.AttandancePolicyName == AttandPolicy.AttandancePolicyName.Trim() & a.IsActive==false
                                    select a.AttandancePolicyName).SingleOrDefault();

                if (AttandPolicy.AttandancePolicyName != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.AttandancePolicies.Add(AttandPolicy);
                        entities.SaveChanges();
                        id = AttandPolicy.AttandancePolicyId;
                    }
                    Message = string.Format("Successfully Added Policy Name {0}.\\n Date: {1}", AttandPolicy.AttandancePolicyName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", AttandPolicy.AttandancePolicyName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }


            }
            return Message;

        }

        public List<AttandancePolicy> EditPolicyMaster(int policyId)
        {
            var Record = (from a in _context.AttandancePolicies
                                where a.AttandancePolicyId == policyId
                                select a).ToList();
            return Record;
        }

        public string DeletePolicyMaster(int policyId)
        {
            string Message = "";
            try
            {
                var Record = (from a in _context.AttandancePolicies
                              where a.AttandancePolicyId == policyId
                              select a).FirstOrDefault();

                AttandancePolicy AttandPolicy = new AttandancePolicy();
                AttandPolicy = Record;
                AttandPolicy.IsActive = true;
                using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                {
                    entities.AttandancePolicies.Update(AttandPolicy);
                    entities.SaveChanges();
                }
                Message = string.Format("Record Deleted Successfully!! {0}.\\n Date: {1}", AttandPolicy.AttandancePolicyName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
            }
            catch(Exception ex)
            {
                Message = "Record Cannot be Deleted!!";
            }

             return Message;
        }

        public string AddPolicyCategory(IFormCollection collection)
        {
            string Message = "";
            int id = 0;

            AttandancePolicyRulesCategory AttandPolicyCategory = new AttandancePolicyRulesCategory();
            AttandPolicyCategory.AttandancePolicyRulesCategoryId = 0;

            int AttandancePolicyId;
            int.TryParse(collection["PolicyName"], out AttandancePolicyId);

            if (!string.IsNullOrEmpty(collection["PolicyName"].ToString()))
            {
                AttandPolicyCategory.AttandancePolicyId = AttandancePolicyId;
                AttandPolicyCategory.AttandancePolicyRulesCategoryName = collection["Category"];
                AttandPolicyCategory.Description = collection["Desc"];
                AttandPolicyCategory.IsActive = false;
                AttandPolicyCategory.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                AttandPolicyCategory.CreatedBy = 1;

                var RecordExists = (from a in _context.AttandancePolicyRulesCategories
                                    where a.AttandancePolicyRulesCategoryName == AttandPolicyCategory.AttandancePolicyRulesCategoryName.Trim() & a.IsActive == false
                                    select a.AttandancePolicyRulesCategoryName).SingleOrDefault();

                if (AttandPolicyCategory.AttandancePolicyRulesCategoryName != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.AttandancePolicyRulesCategories.Add(AttandPolicyCategory);
                        entities.SaveChanges();
                        id = AttandPolicyCategory.AttandancePolicyRulesCategoryId;
                    }
                    Message = string.Format("Successfully Added Policy Categorories {0}.\\n Date: {1}", AttandPolicyCategory.AttandancePolicyRulesCategoryName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", AttandPolicyCategory.AttandancePolicyRulesCategoryName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }


            }
            return Message;

        }

        public string AddPoliRule(IFormCollection collection)
        {
            string Message = "";
            int id = 0;

            AttandancePolicyRule AttandPolicyRule = new AttandancePolicyRule();
            AttandPolicyRule.AttandancePolicyRuleId = 0;

            int AttandanceCategoryId;
            int.TryParse(collection["PolicyCategory"], out AttandanceCategoryId);

            if (!string.IsNullOrEmpty(collection["RuleName"].ToString()))
            {
                AttandPolicyRule.AttandancePolicyRulesCategoryId = AttandanceCategoryId;
                AttandPolicyRule.AttandancePolicyRuleName = collection["RuleName"];
                AttandPolicyRule.AttandancePolicyRule1 = collection["Rule"];
                AttandPolicyRule.Description = collection["Desc"];
                AttandPolicyRule.IsActive = false;
                AttandPolicyRule.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                AttandPolicyRule.CreatedBy = 1;

                var RecordExists = (from a in _context.AttandancePolicyRules
                                    where a.AttandancePolicyRuleName == AttandPolicyRule.AttandancePolicyRuleName.Trim() & a.IsActive == false
                                    select a.AttandancePolicyRuleName).SingleOrDefault();

                if (AttandPolicyRule.AttandancePolicyRuleName != RecordExists)
                {
                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                    {
                        entities.AttandancePolicyRules.Add(AttandPolicyRule);
                        entities.SaveChanges();
                        id = AttandPolicyRule.AttandancePolicyRuleId;
                    }
                    Message = string.Format("Successfully Added Policy Categorories {0}.\\n Date: {1}", AttandPolicyRule.AttandancePolicyRuleName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else
                {
                    Message = string.Format("Record AllReady {0}.\\n Date: {1}", AttandPolicyRule.AttandancePolicyRuleName, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
            }
            return Message;
        }
    }
}

