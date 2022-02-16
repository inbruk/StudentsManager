using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class PriceGroupTools
    {
        public static Guid Create(DTO.PriceGroup newItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();

            tblPriceGroup newRec = new tblPriceGroup()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Position = newItem.Position,
                IndexInPercents = newItem.IndexInPercents,
                Description = newItem.Description
            };

            ctx.tblPriceGroups.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.PriceGroup Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblPriceGroup currRec = ctx.tblPriceGroups.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return null;

            DTO.PriceGroup res = new DTO.PriceGroup()
            {
                Id = currRec.Id,
                Name = currRec.Name,                
                Position = currRec.Position,
                IndexInPercents = currRec.IndexInPercents,
                Description = currRec.Description
            };
            return res;
        }

        public static List<DTO.PriceGroup> ReadAll()
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.PriceGroup> result = ctx.tblPriceGroups.Select
            (
                x => new DTO.PriceGroup()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Position = x.Position,
                    IndexInPercents = x.IndexInPercents,
                    Description = x.Description,
                }
            ).OrderBy(x => x.Position).ToList();

            return result;
        }

        public static Boolean Update(DTO.PriceGroup item)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblPriceGroup currRec = ctx.tblPriceGroups.SingleOrDefault(x => x.Id == item.Id);
            if (currRec == null) return false;

            currRec.Id = item.Id;
            currRec.Name = item.Name;
            currRec.Position = item.Position;
            currRec.IndexInPercents = item.IndexInPercents;
            currRec.Description = item.Description;

            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid itemId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblPriceGroup currRec = ctx.tblPriceGroups.SingleOrDefault(x => x.Id == itemId);
            if (currRec == null) return false;

            ctx.tblPriceGroups.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameExists(Guid exceptId, String name)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblPriceGroup currRec = ctx.tblPriceGroups.SingleOrDefault(x => x.Name == name && x.Id != exceptId);
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
