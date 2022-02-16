using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class EducationLevelTools
    {
        public static int GetMinimumScoreByPositionAndMaxScore(Guid currLanguage, int pos, int maxScore)
        {
            int resultScore;
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currItem = ctx.tblEducationLevels.Where(x => x.Language==currLanguage && x.Position < pos).OrderByDescending(x => x.Position).FirstOrDefault();
            if( currItem!=null )
            {
                resultScore = currItem.MaxScore + 1;
            }
            else
            {
                resultScore = 0;
            }

            return resultScore;
        }

        public static Guid Create(DTO.EducationLevel newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblEducationLevel newRec = new tblEducationLevel()
            {
                Id          = Guid.NewGuid(),
                Name        = newItem.Name,
                Language    = newItem.Language,
                Position    = newItem.Position,
                MinScore    = newItem.MinScore,
                MaxScore    = newItem.MaxScore,
                Description = newItem.Description
            };

            ctx.tblEducationLevels.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.EducationLevel Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currRec = ctx.tblEducationLevels.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;

            DTO.EducationLevel res = new DTO.EducationLevel()
            {
                Id          = currRec.Id,
                Name        = currRec.Name,
                Language    = (Guid)currRec.Language,
                Position    = currRec.Position,
                MinScore    = (int)currRec.MinScore,
                MaxScore    = currRec.MaxScore,
                Description = currRec.Description
            };
            return res;
        }

        public static DTO.EducationLevel ReadByLanguageIdAndScore(Guid? langId, Int32 score)
        {
            if ( langId == null ) return null;

            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currRec = ctx.tblEducationLevels.FirstOrDefault(x => x.Language == (Guid)langId && x.MinScore<=score && x.MaxScore>=score);
            if (currRec == null) return null;

            DTO.EducationLevel res = new DTO.EducationLevel()
            {
                Id = currRec.Id,
                Name = currRec.Name,
                Language = (Guid)currRec.Language,
                Position = currRec.Position,
                MinScore = (int)currRec.MinScore,
                MaxScore = currRec.MaxScore,
                Description = currRec.Description
            };
            return res;
        }
        
        public static List<DTO.EducationLevel4List> ReadAll()
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.EducationLevel4List> result = ctx.vwEducationLevel4List.Select
            (
                x => new DTO.EducationLevel4List()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Position = x.Position,
                    MinScore = (int)x.MinScore,
                    MaxScore = x.MaxScore,
                    Description = x.Description,
                    LanguageId = x.LanguageId,
                    LanguageName = x.LanguageName
                }
            ).OrderBy(x => x.Position).ToList();

            return result;
        }

        public static List<DTO.EducationLevel4List> ReadAllByLaguageId(Guid languageId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.EducationLevel4List> result = ctx.vwEducationLevel4List.Where( x => x.LanguageId == languageId ).Select
            (
                x => new DTO.EducationLevel4List()
                {
                    Id           = x.Id,
                    Name         = x.Name,
                    Position     = x.Position,
                    MinScore     = (int)x.MinScore,
                    MaxScore     = x.MaxScore,
                    Description  = x.Description,
                    LanguageId   = x.LanguageId,
                    LanguageName = x.LanguageName
                }
            ).OrderBy(x => x.Position).ToList();

            return result;
        }

        public static Boolean Update(DTO.EducationLevel item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currRec = ctx.tblEducationLevels.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id          = item.Id;
            currRec.Name        = item.Name;
            currRec.Language    = item.Language;
            currRec.Position    = item.Position;
            currRec.MinScore    = item.MinScore;
            currRec.MaxScore    = item.MaxScore;
            currRec.Description = item.Description;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid itemId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currRec = ctx.tblEducationLevels.SingleOrDefault(x => x.Id == itemId);
            if (currRec == null) return false;

            ctx.tblEducationLevels.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameExists(Guid exceptId, String name)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblEducationLevel currRec = ctx.tblEducationLevels.SingleOrDefault(x => x.Name == name && x.Id != exceptId);
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
