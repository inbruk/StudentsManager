using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class EducationLevel
    {
        public Guid   Id { set; get; }
	    public String Name { set; get; }
	    public String Description { set; get; }
        public Guid   Language { set; get; }
	    public Byte   Position { set; get; }
        public Int32  MinScore { set; get; }
        public Int32  MaxScore { set; get; }
    }
}
