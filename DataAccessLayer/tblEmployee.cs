//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentsManager.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmployee
    {
        public tblEmployee()
        {
            this.tblEvents = new HashSet<tblEvent>();
            this.tblEvents1 = new HashSet<tblEvent>();
            this.tblGroups = new HashSet<tblGroup>();
            this.tblGroups1 = new HashSet<tblGroup>();
            this.tblEducationLevels = new HashSet<tblEducationLevel>();
            this.tblGroups2 = new HashSet<tblGroup>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomic { get; set; }
        public string SurnameAndInitails { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public System.Guid EmployeeType { get; set; }
    
        public virtual tblDictionary tblDictionary { get; set; }
        public virtual ICollection<tblEvent> tblEvents { get; set; }
        public virtual ICollection<tblEvent> tblEvents1 { get; set; }
        public virtual ICollection<tblGroup> tblGroups { get; set; }
        public virtual ICollection<tblGroup> tblGroups1 { get; set; }
        public virtual ICollection<tblEducationLevel> tblEducationLevels { get; set; }
        public virtual ICollection<tblGroup> tblGroups2 { get; set; }
    }
}
