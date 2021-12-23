using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmpMasters
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public List<MasterDataItem> empMasterDataItems { get; set; }
        //public List<Employee> empAllDetails { get; set; }
        public List<UserDetail> reportingMgrList { get; set; }

        public EmpMasters()
        {
            //empAllDetails= new List<Employee> where()

            //    empAllDetails = _context.Employees.Join(_context.Departments,x=> x.EmployeeId,y=y.)
            //    empAllDetails = _context.Employees.Where(x => x.IsActive == true
            //    join ).ToList();

            //((from a in _context.PageAccessSetups
            //                                         join c in _context.PageModules on a.PageModuleId equals c.PageModuleId
            //                                         where a.RoleId == roleId & c.PageModuleName == pageName
            //                                         select a.IsAllow).SingleOrDefault());

              //var empData = _context.Employees.
              //  Join(_context.Departments, u => u.Department, uir => uir.DepartmentId.ToString(),
              //  (u, uir) => new { u, uir }).
              //  Join(_context.Designations, r => r.u.Designation, ro => ro.DesignationId, (r, ro) => new { r, ro })
              //  .Select(m => new AddUserToRole
              //  {
              //      UserName = m.r.u.UserName,
              //      RoleName = m.ro.RoleName
              //  });

              }

    }
}
