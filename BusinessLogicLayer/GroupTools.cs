using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public class GroupTools
    {
        public static Guid Create(DTO.Group newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblGroup newRec = new tblGroup()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Description = newItem.Description,
                CurrentEducationLevel = newItem.CurrentEducationLevel,
                PrimaryInstructor = newItem.PrimaryInstructor,
                SecondaryInstructor = newItem.SecondaryInstructor
            };

            ctx.tblGroups.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.Group Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblGroup currRec = ctx.tblGroups.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;

            DTO.Group res = new DTO.Group()
            {
                Id = currRec.Id,
                Name = currRec.Name,
                Description = currRec.Description,
                CurrentEducationLevel = currRec.CurrentEducationLevel,
                PrimaryInstructor = currRec.PrimaryInstructor,
                SecondaryInstructor = currRec.SecondaryInstructor
            };
            return res;
        }

        public static List<DTO.Group4List> ReadAllByEducationLevelId(Guid educationLevelId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.Group4List> result = ctx.vwGroup4List.Select
            (
                x => new DTO.Group4List()
                {
                    Id                  = x.GroupId,
                    Name                = x.GroupName,
                    Description         = x.GroupDescription,
                    LanguageId          = x.EducationLevelLanguageId,
                    LanguageName        = x.EducationLevelLanguageName,
                    EducationLevelId    = x.EducationLevelId,
                    EducationLevelName  = x.EducationLevelName,
                    PrimaryInstructor   = x.GroupPrimaryInstructor,
                    SecondaryInstructor = x.GroupSecondaryInstructor
                }
            ).OrderBy(x => (educationLevelId == Guid.Empty || x.EducationLevelId == educationLevelId)).ToList();

            return result;
        }

        public static Boolean Update(DTO.Group item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblGroup currRec = ctx.tblGroups.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id = item.Id;
            currRec.Name = item.Name;
            currRec.Description = item.Description;
            currRec.CurrentEducationLevel = item.CurrentEducationLevel;
            currRec.PrimaryInstructor = item.PrimaryInstructor;
            currRec.SecondaryInstructor = item.SecondaryInstructor;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid itemId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblGroup currRec = ctx.tblGroups.SingleOrDefault(x => x.Id == itemId);
            if (currRec == null) return false;

            ctx.tblGroups.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameExists(Guid exceptId, String name)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblGroup currRec = ctx.tblGroups.SingleOrDefault(x => x.Name == name && x.Id != exceptId);
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
