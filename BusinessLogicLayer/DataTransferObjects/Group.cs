using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class Group
    {
        public Guid   Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Guid?  CurrentEducationLevel { set; get; }
        public Guid?  PrimaryInstructor { set; get; }
        public Guid?  SecondaryInstructor { set; get; }
    }
}
