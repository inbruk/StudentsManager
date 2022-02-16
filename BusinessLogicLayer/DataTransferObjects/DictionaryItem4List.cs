using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class DictionaryItem4List
    {
        public Guid   Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Guid   DictionaryTypeId { set; get; }
        public String DictionaryTypeName { set; get; }
    }
}
