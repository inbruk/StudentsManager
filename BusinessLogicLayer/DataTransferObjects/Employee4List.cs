using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class Employee4List
    {
        public Guid   Id { set; get; }
	    public String Name { set; get; }
	    public String Surname { set; get; }
	    public String Patronomic { set; get; }
	    public String SurnameAndInitails { set; get; }
	    public String Email { set; get; }
	    public String Phone { set; get; }
        public Guid   EmployeeTypeId { set; get; }
        public String EmployeeTypeName { set; get; }
    }
}

