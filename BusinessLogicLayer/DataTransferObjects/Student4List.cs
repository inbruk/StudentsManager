using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer.DataTransferObjects
{
    public class Student4List
    {
        public Guid Id { set; get; }
        public String Name { set; get; }
        public String Surname { set; get; }
        public String Patronomic { set; get; }
        public String SurnameAndInitials { set; get; }
        public String Email { set; get; }
        public String Phone { set; get; }
        public Guid   PriceGroupId { set; get; }
        public String PriceGroupName { set; get; }
        public Int32? CurrentScore1 { set; get; }
        public Int32? CurrentScore2 { set; get; }
        public Int32? CurrentScore3 { set; get; }
        public Int32? FirstTestScore1 { set; get; }
        public Int32? FirstTestScore2 { set; get; }
        public Int32? FirstTestScore3 { set; get; }
        public Guid?   Language1Id { set; get; }
        public String  Language1Name { set; get; }
        public Guid?   Language2Id { set; get; }
        public String  Language2Name { set; get; }
        public Guid?   Language3Id { set; get; }
        public String  Language3Name { set; get; }
        public Guid?   Group1Id { set; get; }
        public String  Group1Name { set; get; }
        public Guid?   Group2Id { set; get; }
        public String  Group2Name { set; get; }
        public Guid?   Group3Id { set; get; }
        public String  Group3Name { set; get; }

        public Student GetStudentData()
        {
            Student result = new Student()
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                Patronomic = this.Patronomic,
                SurnameAndInitials = this.SurnameAndInitials,
                Email = this.Email,
                Phone = this.Phone,
                PriceGroup = this.PriceGroupId,
                Language1 = this.Language1Id,
                CurrentScore1 = this.CurrentScore1,
                FirstTestScore1 = this.FirstTestScore1,
                Group1 = this.Group1Id,
                Language2 = this.Language2Id,
                CurrentScore2 = this.CurrentScore2,
                FirstTestScore2 = this.FirstTestScore2,
                Group2 = this.Group2Id,
                Language3 = this.Language3Id,
                CurrentScore3 = this.CurrentScore3,
                FirstTestScore3 = this.FirstTestScore3,
                Group3 = this.Group3Id
            };
            return result;
        }
    }
}
