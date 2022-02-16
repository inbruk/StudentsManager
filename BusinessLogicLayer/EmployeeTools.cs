using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class EmployeeTools
    {
        public static Guid Create(DTO.Employee newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblEmployee newRec = new tblEmployee()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Surname = newItem.Surname,
                Patronomic = newItem.Patronomic,
                SurnameAndInitails = newItem.SurnameAndInitails,
                Email = newItem.Email,                
                Phone = newItem.Phone,
                EmployeeType = newItem.EmployeeType,
            };

            ctx.tblEmployees.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.Employee Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEmployee currRec = ctx.tblEmployees.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;

            DTO.Employee res = new DTO.Employee()
            {
                Id = currRec.Id,
                Name = currRec.Name,
                Surname = currRec.Surname,
                Patronomic = currRec.Patronomic,
                SurnameAndInitails = currRec.SurnameAndInitails,
                Email = currRec.Email,
                Phone = currRec.Phone,
                EmployeeType = currRec.EmployeeType,
            };
            return res;
        }

        public static List<DTO.Employee4List> ReadAllByTypeId(Guid currEmployeeTypeId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.Employee4List> result = ctx.vwEmployee4List.Where(x => x.EmployeeTypeId == currEmployeeTypeId).Select
            (
                x => new DTO.Employee4List()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronomic = x.Patronomic,
                    SurnameAndInitails = x.SurnameAndInitails,
                    Email = x.Email,
                    Phone = x.Phone,
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeTypeName
                }
            ).OrderBy(x => x.Name).ToList();

            return result;
        }

        public static Boolean Update(DTO.Employee item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEmployee currRec = ctx.tblEmployees.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id = item.Id;
            currRec.Name = item.Name;
            currRec.Surname = item.Surname;
            currRec.Patronomic = item.Patronomic;
            currRec.SurnameAndInitails = item.SurnameAndInitails;
            currRec.Email = item.Email;
            currRec.Phone = item.Phone;
            currRec.EmployeeType = item.EmployeeType;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid itemId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEmployee currRec = ctx.tblEmployees.SingleOrDefault(x => x.Id == itemId);
            if (currRec == null) return false;

            ctx.tblEmployees.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameAndSurnameExists(Guid exceptId, String name, String surname)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEmployee currRec = ctx.tblEmployees.SingleOrDefault(x => x.Name == name && x.Surname == surname && x.Id != exceptId);
            if (currRec == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
