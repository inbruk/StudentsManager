using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class EducationLevel4List
    {
        public Guid Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Byte Position { set; get; }
        public Int32 MinScore { set; get; }
        public Int32 MaxScore { set; get; }
        public Guid  LanguageId { set; get; }
        public String LanguageName { set; get; }
    }
}
