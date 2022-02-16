using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class DictionaryType
    {
        public Guid Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
    }
}
