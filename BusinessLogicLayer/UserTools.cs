using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public class UserTools
    {
        private static DTO.User EF2DTO(tblUser currRec)
        {
            DTO.User res = new DTO.User()
            {
                Id = currRec.Id,
                SurnameAndInitials = currRec.SurnameAndInitials,
                Login = currRec.Login,
                Password = currRec.Password,
                EMail = currRec.EMail,
                Phone = currRec.Phone,
                UserType = currRec.UserType,
            };
            return res;
        }

        public static Guid Create(DTO.User newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblUser newRec = new tblUser()
            {
                Id = Guid.NewGuid(),
                SurnameAndInitials = newItem.SurnameAndInitials,
                Login = newItem.Login,
                Password = newItem.Password,
                EMail = newItem.EMail,
                Phone = newItem.Phone,
                UserType = newItem.UserType,
            };

            ctx.tblUsers.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.User Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblUser currRec = ctx.tblUsers.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;
            return EF2DTO(currRec);
        }

        public static DTO.User Read(String login, String password)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblUser currRec = ctx.tblUsers.SingleOrDefault(x => x.Login==login && x.Password==password);
            if (currRec == null) return null;
            return EF2DTO(currRec);
        }


        public static List<DTO.User4List> ReadAllByTypeId(Guid currUserTypeId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.User4List> result = ctx.vwUser4List.Where(x => x.UserTypeId== currUserTypeId).Select
            (
                x => new DTO.User4List()
                {
                    Id = x.Id,
                    SurnameAndInitials = x.SurnameAndInitials,
                    Login = x.Login,
                    Password = x.Password,
                    EMail = x.EMail,
                    Phone = x.Phone,
                    UserTypeId = x.UserTypeId,
                    UserTypeName = x.UserTypeName,
                }
            ).ToList();
            return result;
        }

        public static Boolean Update(DTO.User item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblUser currRec = ctx.tblUsers.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id = item.Id;
            currRec.SurnameAndInitials = item.SurnameAndInitials;
            currRec.Login = item.Login;
            currRec.Password = item.Password;
            currRec.EMail = item.EMail;
            currRec.Phone = item.Phone;
            currRec.UserType = item.UserType;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid userId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblUser currRec = ctx.tblUsers.SingleOrDefault(x => x.Id == userId);
            if (currRec == null) return false;

            ctx.tblUsers.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsLoginExists(Guid exceptId, String login)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblUser currRec = ctx.tblUsers.SingleOrDefault( x => x.Login == login && x.Id != exceptId );
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
