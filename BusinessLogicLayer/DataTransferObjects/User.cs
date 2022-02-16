﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class User
    {
	    public Guid   Id { set; get; }
        public String SurnameAndInitials { set; get; }
        public String Login { set; get; }
        public String Password { set; get; }
        public String EMail { set; get; }
        public String Phone { set; get; }
        public Guid   UserType { set; get; }
    }
}
