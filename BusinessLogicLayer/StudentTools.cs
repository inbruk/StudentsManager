using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class StudentTools
    {
        public static Guid Create(DTO.Student newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblStudent newRec = new tblStudent()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Surname = newItem.Surname,
                Patronomic = newItem.Patronomic,
                SurnameAndInitials = newItem.SurnameAndInitials,
                Email = newItem.Email,
                Phone = newItem.Phone,

                PriceGroup = newItem.PriceGroup,
                CurrentScore1 = newItem.CurrentScore1,
                FirstTestScore1 = newItem.FirstTestScore1,
                Language1 = newItem.Language1,
                Group1 = newItem.Group1,
                CurrentScore2 = newItem.CurrentScore2,
                FirstTestScore2 = newItem.FirstTestScore2,
                Language2 = newItem.Language2,
                Group2 = newItem.Group2,
                CurrentScore3 = newItem.CurrentScore3,
                FirstTestScore3 = newItem.FirstTestScore3,
                Language3 = newItem.Language3,
                Group3 = newItem.Group3
            };

            // интерпретируем Guid.Empty как null
            if( newRec.Language1==Guid.Empty ) newRec.Language1 = null;
            if( newRec.Language2==Guid.Empty ) newRec.Language2 = null;
            if( newRec.Language3==Guid.Empty ) newRec.Language3 = null;
            if (newRec.Group1 == Guid.Empty) newRec.Group1 = null;
            if (newRec.Group2 == Guid.Empty) newRec.Group2 = null;
            if (newRec.Group3 == Guid.Empty) newRec.Group3 = null;

            ctx.tblStudents.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.Student Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblStudent currRec = ctx.tblStudents.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;

            DTO.Student res = new DTO.Student()
            {
                Id = currRec.Id,
                Name = currRec.Name,
                Surname = currRec.Surname,
                Patronomic = currRec.Patronomic,
                Email = currRec.Email,
                Phone = currRec.Phone,
                PriceGroup = currRec.PriceGroup,

                CurrentScore1 = currRec.CurrentScore1,
                FirstTestScore1 = currRec.FirstTestScore1,
                Language1 = currRec.Language1,
                Group1 = currRec.Group1,
                CurrentScore2 = currRec.CurrentScore2,
                FirstTestScore2 = currRec.FirstTestScore2,
                Language2 = currRec.Language2,
                Group2 = currRec.Group2,
                CurrentScore3 = currRec.CurrentScore3,
                FirstTestScore3 = currRec.FirstTestScore3,
                Language3 = currRec.Language3,
                Group3 = currRec.Group3
            };

            return res;
        }

        public static Boolean Update(DTO.Student item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblStudent currRec = ctx.tblStudents.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id             = item.Id;
            currRec.Name           = item.Name;
            currRec.Surname        = item.Surname;
            currRec.Patronomic     = item.Patronomic;
            currRec.SurnameAndInitials = item.SurnameAndInitials;
            currRec.Email          = item.Email;
            currRec.Phone          = item.Phone;
            currRec.PriceGroup     = item.PriceGroup;

            currRec.CurrentScore1 = item.CurrentScore1;
            currRec.FirstTestScore1 = item.FirstTestScore1;
            currRec.Language1 = item.Language1;
            currRec.Group1 = item.Group1;
            currRec.CurrentScore2 = item.CurrentScore2;
            currRec.FirstTestScore2 = item.FirstTestScore2;
            currRec.Language2 = item.Language2;
            currRec.Group2 = item.Group2;
            currRec.CurrentScore3 = item.CurrentScore3;
            currRec.FirstTestScore3 = item.FirstTestScore3;
            currRec.Language3 = item.Language3;
            currRec.Group3 = item.Group3;

            // интерпретируем Guid.Empty как null
            if (currRec.Language1 == Guid.Empty) currRec.Language1 = null;
            if (currRec.Language2 == Guid.Empty) currRec.Language2 = null;
            if (currRec.Language3 == Guid.Empty) currRec.Language3 = null;
            if (currRec.Group1 == Guid.Empty) currRec.Group1 = null;
            if (currRec.Group2 == Guid.Empty) currRec.Group2 = null;
            if (currRec.Group3 == Guid.Empty) currRec.Group3 = null;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid itemId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblStudent currRec = ctx.tblStudents.SingleOrDefault(x => x.Id == itemId);
            if (currRec == null) return false;

            ctx.tblStudents.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameSurnameAndPatronomicExists(Guid exceptId, String name, String surname, String patronomic)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblStudent currRec = ctx.tblStudents.SingleOrDefault(x => x.Name == name && x.Surname == surname && x.Patronomic == patronomic && x.Id != exceptId);
            if (currRec == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static List<DTO.Student4List> ReadAllWithFilters
        (
            Guid filterLanguageId, 
            Guid filterEducationLevel,
            Guid filterGroupId,
            Guid filterPriceGroupId,
            String filterSurname,
            String filterName
        )
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            IQueryable<vwStudent4List> queryable1 = null;
            IQueryable<vwStudent4List> queryable2 = null;
            
            if( filterGroupId != Guid.Empty ) // фильтр по группе
            {
                queryable1 = ctx.vwStudent4List.Where(x => x.Group1Id==filterGroupId || x.Group2Id==filterGroupId || x.Group3Id==filterGroupId );
            }
            else
            {
                tblEducationLevel currEdLevel = null;
                if (filterEducationLevel != Guid.Empty)
                {
                    currEdLevel = ctx.tblEducationLevels.SingleOrDefault( x => x.Id==filterEducationLevel );
                }

                if (currEdLevel != null) // или фильтр по уровню образования (вместо группы)
                {
                    Int32 minScore = (currEdLevel.MinScore!=null) ? (Int32)currEdLevel.MinScore : 0;
                    Int32 maxScore = (currEdLevel.MaxScore!=null) ? (Int32)currEdLevel.MaxScore : 0;

                    queryable1 = ctx.vwStudent4List.Where
                    ( x => 
                        (x.Language1Id==currEdLevel.Language && x.CurrentScore1 > minScore && x.CurrentScore1 < maxScore) ||
                        (x.Language2Id==currEdLevel.Language && x.CurrentScore2 > minScore && x.CurrentScore2 < maxScore) ||
                        (x.Language3Id==currEdLevel.Language && x.CurrentScore3 > minScore && x.CurrentScore3 < maxScore)
                    );
                }
                else
                {
                    if( filterLanguageId!=Guid.Empty ) // фильтр по языку (вместо группы или ур. образования)
                    {
                        queryable1 = ctx.vwStudent4List.Where(x => x.Language1Id == filterLanguageId || x.Language2Id == filterLanguageId || x.Language3Id == filterLanguageId);
                    }
                }
            }

            if (filterPriceGroupId != Guid.Empty) // фильтр по группе цен
            {
                if (queryable1 == null) // это все равно что filterGroupId != Guid.Empty && currEdLevel != null && filterLanguageId != Guid.Empty
                {
                    queryable2 = ctx.vwStudent4List.Where(x => x.PriceGroupId == filterPriceGroupId);
                }
                else
                {
                    queryable2 = queryable1.Where(x => x.PriceGroupId == filterPriceGroupId);
                }
            }
            else
            {
                if (queryable1 == null) // это все равно что filterGroupId != Guid.Empty && currEdLevel != null && filterLanguageId != Guid.Empty
                {
                    queryable2 = ctx.vwStudent4List;
                }
                else
                {
                    queryable2 = queryable1;
                }
            }

            List<DTO.Student4List> result = queryable2.Where
            ( 
                x => ( String.IsNullOrEmpty(filterSurname) == true || x.Surname.Contains(filterSurname) ) && 
                     ( String.IsNullOrEmpty(filterName) == true || x.Name.Contains(filterName) ) 
            )
            .Select
            (
                x => new DTO.Student4List()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronomic = x.Patronomic,
                    SurnameAndInitials = x.SurnameAndInitials,
                    Email = x.Email,
                    Phone = x.Phone,
                    PriceGroupId = x.PriceGroupId,
                    PriceGroupName = x.PriceGroupName,
                    CurrentScore1 = x.CurrentScore1,
                    CurrentScore2 = x.CurrentScore2,
                    CurrentScore3 = x.CurrentScore3,
                    FirstTestScore1 = x.FirstTestScore1,
                    FirstTestScore2 = x.FirstTestScore2,
                    FirstTestScore3 = x.FirstTestScore3,
                    Language1Id = x.Language1Id,
                    Language1Name = x.Language1Name,
                    Language2Id = x.Language2Id,
                    Language2Name = x.Language2Name,
                    Language3Id = x.Language3Id,
                    Language3Name = x.Language3Name,
                    Group1Id = x.Group1Id,
                    Group1Name = x.Group1Name,
                    Group2Id = x.Group2Id,
                    Group2Name = x.Group2Name,
                    Group3Id = x.Group3Id,
                    Group3Name = x.Group3Name
                }
            ).OrderBy(x => x.Surname).ThenBy(x => x.Name).ToList();

            return result;
        }
    }
}
