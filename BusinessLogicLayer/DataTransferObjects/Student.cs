using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class Student
    {
        public Guid    Id { set; get; }
        public String  Name { set; get; }
        public String  Surname { set; get; }
        public String  Patronomic { set; get; }
        public String  SurnameAndInitials { set; get; }
        public String  Email { set; get; }
        public String  Phone { set; get; }
        public Guid    PriceGroup { set; get; }
        public Guid?   Language1 { set; get; }
        public Int32?  CurrentScore1 { set; get; }
        public Int32?  FirstTestScore1 { set; get; }
        public Guid?   Group1 { set; get; }
        public Guid?   Language2 { set; get; }
        public Int32?  CurrentScore2 { set; get; }
        public Int32?  FirstTestScore2 { set; get; }
        public Guid?   Group2 { set; get; }
        public Guid?   Language3 { set; get; }
        public Int32?  CurrentScore3 { set; get; }
        public Int32?  FirstTestScore3 { set; get; }
        public Guid?   Group3 { set; get; }
    }
}
