using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class Group4List
    {
        public Guid   Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Guid?  LanguageId { set; get; }
        public String LanguageName { set; get; }
        public Guid?  EducationLevelId { set; get; }
        public String EducationLevelName { set; get; }
        public Guid?  PrimaryInstructor { set; get; }
        public Guid?  SecondaryInstructor { set; get; }
    }
}
