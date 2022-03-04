using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SwiftHR.Models
{
    public partial class SHR_SHOBIGROUP_DBContext : DbContext
    {
        private const string ConnectionString = "Server=L-PANKAJ\\SQLEXPRESS;Database=SHR_SHOBIGROUP_DB;Trusted_Connection=True;";

        public SHR_SHOBIGROUP_DBContext()
        {

        }

        public SHR_SHOBIGROUP_DBContext(DbContextOptions<SHR_SHOBIGROUP_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<USpInsertTimeSheetRecord> USpInsertTimeSheetRecord { get; set; }

        public virtual ObjectResult<USpInsertTimeSheetRecord> USpInsertTimeSheetRecordResult(string workorderjson)
        {
            var workorderjsonPara = workorderjson != null ?
                new ObjectParameter("workorderjson", workorderjson) :
            new ObjectParameter("workorderjson", typeof(string));
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USpInsertTimeSheetRecord>("SP_TimeAuditReport", workorderjsonPara);
        }
        public virtual DbSet<TimeRecordingSheet> TimeRecordingSheets { get; set; }
        public virtual DbSet<TimeRecordingSheetDetails> TimeRecordingSheetDetails { get; set; }
        public virtual DbSet<AttandancePolicy> AttandancePolicies { get; set; }
        public virtual DbSet<AttandancePolicyRule> AttandancePolicyRules { get; set; }
        public virtual DbSet<AttandancePolicyRulesCategory> AttandancePolicyRulesCategories { get; set; }
        public virtual DbSet<AttandancePolicyRulesMapping> AttandancePolicyRulesMappings { get; set; }
        public virtual DbSet<AttandancePolicySetup> AttandancePolicySetups { get; set; }
        public virtual DbSet<AuthorizedSignatory> AuthorizedSignatories { get; set; }
        public virtual DbSet<BankMaster> BankMasters { get; set; }
        public virtual DbSet<LookUpM> LookUpM { get; set; }
        public virtual DbSet<EmpLOPDetails> EmpLOPDetails { get; set; }

        public virtual DbSet<EmpReimbursement> EmpReimbursement { get; set; }

        public virtual DbSet<CreateEmpPayRollMonth> CreateEmpPayRollMonth { get; set; }
        public virtual DbSet<LoanHeader> LoanHeader { get; set; }

        public virtual DbSet<LookUpDetailsM> LookUpDetailsM { get; set; }
        public virtual DbSet<EmpArrearDetails> EmpArrearDetails { get; set; }
        public virtual DbSet<EmpPFESICDetails> EmpPFESICDetails { get; set; }
        public virtual DbSet<SalaryHeader> SalaryHeaders { get; set; }
        public virtual DbSet<SalaryMonthlyStatement> SalaryMonthlyStatements { get; set; }
        public virtual DbSet<SalaryDetails> SalaryDetails { get; set; }
        public virtual DbSet<EmpAddress> EmpAddress { get; set; }
        public virtual DbSet<CurrancyMaster> CurrancyMasters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Company> CompanyMaster { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<EmpBankDetail> EmpBankDetails { get; set; }
        public virtual DbSet<EmpDirectoryMapping> EmpDirectoryMappings { get; set; }
        public virtual DbSet<EmpDocument> EmpDocuments { get; set; }
        public virtual DbSet<EmpEducationDetail> EmpEducationDetails { get; set; }

        internal void SavedChanges()
        {
            throw new NotImplementedException();
        }

        public virtual DbSet<EmpGeneralSetting> EmpGeneralSettings { get; set; }
        public virtual DbSet<EmpInfoConfiguration> EmpInfoConfigurations { get; set; }
        public virtual DbSet<EmpInfoSection> EmpInfoSections { get; set; }
        public virtual DbSet<EmpLeavingReason> EmpLeavingReasons { get; set; }
        public virtual DbSet<EmpMasterItem> EmpMasterItems { get; set; }
        public virtual DbSet<EmpMasterItemCategory> EmpMasterItemCategories { get; set; }
        public virtual DbSet<EmpNoSeriesFormatting> EmpNoSeriesFormattings { get; set; }
        public virtual DbSet<EmpOnboardingDetail> EmpOnboardingDetails { get; set; }
        public virtual DbSet<EmpSettingsCategory> EmpSettingsCategories { get; set; }
        public virtual DbSet<EmpSettingsCategoryValue> EmpSettingsCategoryValues { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeFeed> EmployeeFeeds { get; set; }
        public virtual DbSet<FeedsCommentsAndLike> FeedsCommentsAndLikes { get; set; }
        public virtual DbSet<FeedsGroup> FeedsGroups { get; set; }
        public virtual DbSet<LeaveApplyDetail> LeaveApplyDetails { get; set; }
        public virtual DbSet<LeavePolicySetup> LeavePolicySetups { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<LeaveTypeCategory> LeaveTypeCategories { get; set; }
        public virtual DbSet<LeaveTypeMapping> LeaveTypeMappings { get; set; }
        public virtual DbSet<LeaveTypeScheme> LeaveTypeSchemes { get; set; }
        public virtual DbSet<MasterDataItem> MasterDataItems { get; set; }
        public virtual DbSet<MasterDataItemType> MasterDataItemTypes { get; set; }
        public virtual DbSet<PageAccessSetup> PageAccessSetups { get; set; }
        public virtual DbSet<PageModule> PageModules { get; set; }
        public virtual DbSet<PasswordSetupSetting> PasswordSetupSettings { get; set; }
        public virtual DbSet<PrevEmploymentDetail> PrevEmploymentDetails { get; set; }
        public virtual DbSet<PreviousEmploymentDetail> PreviousEmploymentDetails { get; set; }
        public virtual DbSet<PurchaseModuleScheme> PurchaseModuleSchemes { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<SettingsDataItem> SettingsDataItems { get; set; }
        public virtual DbSet<SettingsDataItemType> SettingsDataItemTypes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShiftHoursCalculationScheme> ShiftHoursCalculationSchemes { get; set; }
        public virtual DbSet<ShiftSession> ShiftSessions { get; set; }
        public virtual DbSet<UserActionLog> UserActionLogs { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<Presence> Presences { get; set; }

        public virtual DbSet<LookUpDetailsM> LookupDetailsM { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=VIKI;Database=SHR_SHOBIGROUP_DB;UID=SHOBI_TECH;PWD=SHOBI_TECH;");
                DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseSqlServer(@ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AttandancePolicy>(entity =>
            {
                entity.ToTable("AttandancePolicy");

                entity.Property(e => e.AttandancePolicyName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRule>(entity =>
            {
                entity.Property(e => e.AttandancePolicyRule1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AttandancePolicyRule");

                entity.Property(e => e.AttandancePolicyRuleName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Example)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MarkStatusFor)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRulesCategory>(entity =>
            {
                entity.ToTable("AttandancePolicyRulesCategory");

                entity.Property(e => e.AttandancePolicyRulesCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRulesMapping>(entity =>
            {
                entity.ToTable("AttandancePolicyRulesMapping");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicySetup>(entity =>
            {
                entity.ToTable("AttandancePolicySetup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<SalaryHeader>(entity =>
            {
                entity.ToTable("SalaryHeader");


                entity.Property(e => e.EmployeeID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PFAvailability)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DOJ)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DOB)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastPayrollProceesedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PayoutMonth)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveStartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveEndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VersionNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });


            modelBuilder.Entity<SalaryMonthlyStatement>(entity =>
            {
                entity.ToTable("SalaryMonthlyStatement");


                entity.Property(e => e.MailSendStatus)
                    .HasMaxLength(250)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<SalaryDetails>(entity =>
            {
                entity.ToTable("SalaryDetails");


                entity.Property(e => e.HeaderID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Basic)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.HRA)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Bonus)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OtherAllowance)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Overttime)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProfTax)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Loan)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdvanceSalary)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeContributionPF)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeContributionESIC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerContributionPF)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerContributionESIC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyNetPay)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyGrossPay)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualGrossSalary)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualGrossCTC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<Presence>(entity =>
            {
                entity.ToTable("Presence");

                entity.HasKey(e => e.PresenceId);


                entity.Property(e => e.PresenceName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuthorizedSignatory>(entity =>
            {
                entity.ToTable("AuthorizedSignatory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureImagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.HasKey(e => e.BankMasterDataId);

                entity.ToTable("BankMaster");

                entity.Property(e => e.BankMasterDataId).HasColumnName("bankMasterDataId");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bankCode");

                entity.Property(e => e.BankName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("bankName");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSCCODE");
            });


            modelBuilder.Entity<LookUpM>(entity =>
            {
                entity.ToTable("LookUpM");

                entity.HasKey(e => e.LookUpId);

                entity.Property(e => e.LookUpCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LookUpName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            });



            modelBuilder.Entity<LookUpDetailsM>(entity =>
            {
                entity.HasKey(e => e.LookUpDetailsId);

                entity.ToTable("LookUpDetailsM");

                entity.Property(e => e.LookUpDetailsId).HasColumnName("LookUpDetailsId");

                entity.Property(e => e.LookUpId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LookUpId");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Name");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Description");
            });



            modelBuilder.Entity<EmpLOPDetails>(entity =>
           {
               entity.ToTable("EmpLOPDetails");


               entity.Property(e => e.EmployeeID)
                   .HasMaxLength(250)
                   .IsUnicode(false);

               entity.Property(e => e.EmployeeNumber)
                   .HasMaxLength(50)
                   .IsUnicode(false);

               entity.Property(e => e.EmployeeName)
                   .HasMaxLength(20)
                   .IsUnicode(false);

               entity.Property(e => e.LOPMonth)
                   .HasMaxLength(20)
                   .IsUnicode(false);

               entity.Property(e => e.TotalLOPDays)
                   .HasMaxLength(20)
                   .IsUnicode(false);

               entity.Property(e => e.Remarks)
                   .HasMaxLength(20)
                   .IsUnicode(false);

           });

            modelBuilder.Entity<EmpReimbursement>(entity =>
            {
                entity.ToTable("EmpReimbursement");


                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentsType)
                  .HasMaxLength(250)
                  .IsUnicode(false);

                entity.Property(e => e.EarningsTypeFromLookUp)
                   .HasMaxLength(250)
                   .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(20)
                    .IsUnicode(false);


                entity.Property(e => e.CreatedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);


                entity.Property(e => e.UpdatedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);

                entity.Property(e => e.PaymentEffectedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);

                entity.Property(e => e.Status)
                      .HasMaxLength(20)
                      .IsUnicode(false);


                entity.Property(e => e.ApprovedBy)
                      .HasMaxLength(20)
                      .IsUnicode(false);


                entity.Property(e => e.ApprovedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);

            });

            modelBuilder.Entity<CreateEmpPayRollMonth>(entity =>
            {
                entity.ToTable("CreateEmpPayRollMonth");


                entity.Property(e => e.Id)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PayRollMonth)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FromPayRollPeriod)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ToPayRollPeriod)
                   .HasMaxLength(250)
                   .IsUnicode(false);


                entity.Property(e => e.CreatedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);


                entity.Property(e => e.UpdatedDate)
                      .HasMaxLength(200)
                      .IsUnicode(false);

                entity.Property(e => e.Status)
                      .HasMaxLength(20)
                      .IsUnicode(false);

            });

            modelBuilder.Entity<LoanHeader>(entity =>
            {
                entity.ToTable("LoanHeader");


                entity.Property(e => e.EmployeeID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                //entity.Property(e => e.DateOfLoan)
                //    .HasMaxLength(50)
                //    .IsUnicode(false);

                //entity.Property(e => e.StartFrom)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);

                //entity.Property(e => e.LoanAmount)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.LoanCompleted)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);
                //entity.Property(e => e.CompletedDate)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.LoanType)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);

                //entity.Property(e => e.NumberOfEMI)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.MonthlyEMIAmount)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.InterestRate)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.DemandPromissoryNote)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.PerquisiteRate)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.LoanAccountNo)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.PrincipalBalance)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.InterestBalance)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);

                //entity.Property(e => e.Remarks)
                //    .HasMaxLength(20)
                //    .IsUnicode(false);


                //entity.Property(e => e.CreatedDate)
                //      .HasMaxLength(20)
                //      .IsUnicode(false);


                //entity.Property(e => e.UpdatedDate)
                //      .HasMaxLength(20)
                //      .IsUnicode(false);

            });


            modelBuilder.Entity<EmpArrearDetails>(entity =>
            {
                entity.ToTable("EmpArrearDetails");


                entity.Property(e => e.EmployeeID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PayrollMonth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveDateFrom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });


            modelBuilder.Entity<EmpPFESICDetails>(entity =>
            {
                entity.ToTable("EmpPFESICDetails");


                entity.Property(e => e.EmployeeID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranch)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IFSCCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNameAsBankRecords)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IBAN)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ESICIsApplicable)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ESICAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PFAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UAN)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PFIsApplicable)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AllowEPFExcessContribution)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AllowEPSExcessContribution)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });


            modelBuilder.Entity<SalaryHeader>(entity =>
            {
                entity.ToTable("SalaryHeader");


                entity.Property(e => e.EmployeeID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PFAvailability)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DOJ)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DOB)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastPayrollProceesedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PayoutMonth)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveStartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveEndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<SalaryDetails>(entity =>
            {
                entity.ToTable("SalaryDetails");


                entity.Property(e => e.HeaderID)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Basic)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.HRA)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Bonus)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OtherAllowance)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Overttime)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProfTax)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Loan)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdvanceSalary)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeContributionPF)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeContributionESIC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerContributionPF)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerContributionESIC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyNetPay)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyGrossPay)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualGrossSalary)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualGrossCTC)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            });


            modelBuilder.Entity<EmpAddress>(entity =>
            {
                entity.HasKey(e => e.EmpAddressId);

                entity.ToTable("EmpAddress");

                entity.Property(e => e.EmpAddressId).HasColumnName("EmpAddressId");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeId");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Address");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Country");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("State");

                entity.Property(e => e.City)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("City");

                entity.Property(e => e.Pin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Pin");

                entity.Property(e => e.IsPermanentAddress)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IsPermanentAddress");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedBy");
            });
            modelBuilder.Entity<TimeRecordingSheet>(entity =>
            {
                entity.HasKey(e => e.RecTimeSheetId);

                entity.Property(e => e.RecTimeSheetId).HasColumnName("RecTimeSheetId");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeId");

                entity.Property(e => e.Year)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Year");

                entity.Property(e => e.month)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("month");


                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedBy");
            });

            modelBuilder.Entity<TimeRecordingSheetDetails>(entity =>
            {
                entity.HasKey(e => e.RecTimeSheetDetailsId);

                entity.Property(e => e.RecTimeSheetDetailsId).HasColumnName("RecTimeSheetDetailsId");

                entity.Property(e => e.EmpIn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmpIn");

                entity.Property(e => e.Date)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Date");

                entity.Property(e => e.EmpOut)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EmpOut");

                entity.Property(e => e.Total)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Total");


                entity.Property(e => e.EmpBreak)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmpBreak");

                entity.Property(e => e.Net)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Net");

                entity.Property(e => e.Presence)
                  .HasMaxLength(20)
                  .IsUnicode(false)
                  .HasColumnName("Presence");
            });

            modelBuilder.Entity<CurrancyMaster>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.ToTable("CurrancyMaster");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.ToTable("CompanyMaster");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WebSiteName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MobNo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GSTNO)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PANNO)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.ToTable("Designation");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTemplateHtml).IsUnicode(false);

                entity.Property(e => e.EmailTemplateTitle)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpBankDetail>(entity =>
            {
                entity.HasKey(e => e.EmpBankDetailsId);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DdpayableAt)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DDPayableAt");

                entity.Property(e => e.DocumentFileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.NameAsPerBankRecords)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpDirectoryMapping>(entity =>
            {
                entity.HasKey(e => e.EmpDirectoryFieldMappingId);

                entity.Property(e => e.AnnualCtc).HasColumnName("AnnualCTC");
            });

            modelBuilder.Entity<EmpDocument>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCategory)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentFilePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDoumentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpEducationDetail>(entity =>
            {
                entity.HasKey(e => e.EmpEducationId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Degree)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfInstitute)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PassingYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Percentage)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Program)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpInfoConfiguration>(entity =>
            {
                entity.ToTable("EmpInfoConfiguration");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpInfoConfigItem)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpInfoSection>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpInfoSectionName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpLeavingReason>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmpLeavingReason1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("EmpLeavingReason");

                entity.Property(e => e.Pfcode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PFCode");
            });

            modelBuilder.Entity<EmpMasterItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpMasterItemCategory>(entity =>
            {
                entity.ToTable("EmpMasterItemCategory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpNoSeriesFormatting>(entity =>
            {
                entity.HasKey(e => e.EmployeeNoSeriesId);

                entity.ToTable("EmpNoSeriesFormatting");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSeriesName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Format)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MappingWithEmployeeStatus)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpOnboardingDetail>(entity =>
            {
                entity.HasKey(e => e.EmpOnboardingDetailsId);

                entity.Property(e => e.AlternateContactName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MarriageDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SpouceName)
                   .HasMaxLength(150)
                   .IsUnicode(false);

                entity.Property(e => e.MothersName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeDob)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NomineeDOB");

                entity.Property(e => e.NomineeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeContactNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.OnbemployeeId).HasColumnName("ONBEmployeeId");

                entity.Property(e => e.PermanentAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PresentAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RelationWithNominee)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Religion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OnboardingStatus)
                   .HasColumnName("OnboardingStatus");

            });

            modelBuilder.Entity<EmpSettingsCategory>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpSettingsCategoryValue>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryValue1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EmpSettingsCategoryValue");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");


                entity.Property(e => e.AdharCardName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdharCardNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfJoining)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeProfilePhoto)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FathersName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionalGrade)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                   .HasMaxLength(10)
                   .IsUnicode(false);

                entity.Property(e => e.Religion)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IncludeEsi).HasColumnName("IncludeESI");

                entity.Property(e => e.IncludeLwf).HasColumnName("IncludeLWF");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Level)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MothersName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeDob)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NomineeDOB");

                entity.Property(e => e.NomineeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeContactNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.NomineeRelation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Panname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PANName");

                entity.Property(e => e.Pannumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PANNumber");

                entity.Property(e => e.PassportExpiryDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Pfnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PFNumber");

                entity.Property(e => e.ProbationPeriod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportingManager)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubLevel)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Uannumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UANNumber");
            });

            modelBuilder.Entity<EmployeeFeed>(entity =>
            {
                entity.HasKey(e => e.FeedsId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VisibilityDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeedsCommentsAndLike>(entity =>
            {
                entity.HasKey(e => e.FeedsClid);

                entity.ToTable("FeedsCommentsAndLike");

                entity.Property(e => e.FeedsClid).HasColumnName("FeedsCLId");

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeedsGroup>(entity =>
            {
                entity.ToTable("FeedsGroup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsGroupDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsGroupName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveApplyDetail>(entity =>
            {
                entity.HasKey(e => e.EmpLeaveId);

                entity.Property(e => e.LeaveAppliedOn)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveFromDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveReason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveRejectReason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveStatusChangeDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveToDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ReportingManagerName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeavePolicySetup>(entity =>
            {
                entity.ToTable("LeavePolicySetup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeCategory>(entity =>
            {
                entity.ToTable("LeaveTypeCategory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeMapping>(entity =>
            {
                entity.ToTable("LeaveTypeMapping");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveAllotNoOfDaysPerMonth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveAllotTotalNoDaysInYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeScheme>(entity =>
            {
                entity.ToTable("LeaveTypeScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeScemeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterDataItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MasterDataItemValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterDataItemType>(entity =>
            {
                entity.HasKey(e => e.ItemTypeId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageAccessSetup>(entity =>
            {
                entity.HasKey(e => e.PageAccessId);

                entity.ToTable("PageAccessSetup");

                entity.Property(e => e.ModifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageModule>(entity =>
            {
                entity.Property(e => e.PageModuleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PasswordSetupSetting>(entity =>
            {
                entity.HasKey(e => e.PasswordSettingId);
            });

            modelBuilder.Entity<PrevEmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.PrevEmploymentDetailsId);


                entity.Property(e => e.ContactPerson1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmploymentOrder)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.VerifiedBy)
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.ContactPerson1No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson2No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson3)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson3No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.JoinedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeavingDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeavingReason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmploymentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyAddress)
                   .HasMaxLength(500)
                   .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreviousEmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.PrevEmploymentId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyDesignation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevFromDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrevToDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseModuleScheme>(entity =>
            {
                entity.ToTable("PurchaseModuleScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseModuleCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseModuleName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleMaster");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SettingsDataItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsDataItemName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsDataItemValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SettingsDataItemType>(entity =>
            {
                entity.HasKey(e => e.SettingsItemTypeId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsItemTypeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("Shift");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FullDayMinimumHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HalfDayMinimumHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftCode)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShiftHoursCalculationScheme>(entity =>
            {
                entity.ToTable("ShiftHoursCalculationScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftHoursCalculationSchemeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShiftSession>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GraceInTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GraceOutTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InMarginTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OutMarginTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OutTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftSessionName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserActionLog>(entity =>
            {
                entity.HasKey(e => e.SalogId)
                    .HasName("PK_ActionLogDetails");

                entity.ToTable("UserActionLog");

                entity.Property(e => e.SalogId).HasColumnName("SALogId");

                entity.Property(e => e.Action)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserName, "IX_UserDetails")
                    .IsUnique();

                entity.Property(e => e.Contact)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsPwdChangeFt).HasColumnName("IsPwdChangeFT");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicturePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
