using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;
using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class DictionaryTools
    {
        public static Guid Create(DTO.DictionaryItem newItem)
        { 
            StudentsManagerEntities ctx = ContextHandler.Get();
            
            tblDictionary newRec = new tblDictionary()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Description = newItem.Description,
                DictionaryType = newItem.DictionaryType
            };

            ctx.tblDictionaries.Add(newRec);
            ctx.SaveChanges();

            Guid resId = newRec.Id;
            return resId;
        }

        public static DTO.DictionaryItem Read(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblDictionary currRec = ctx.tblDictionaries.SingleOrDefault( x => x.Id==Id);
            if( currRec==null ) return null;

            DTO.DictionaryItem res = new DTO.DictionaryItem()
            {
                Id = currRec.Id,
                Name = currRec.Name,
                Description = currRec.Description,
                DictionaryType = currRec.DictionaryType
            };
            return res;
        }

        public static List<DTO.DictionaryItem4List> ReadAllByTypeId(Guid typeId)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            List<DTO.DictionaryItem4List> result = ctx.vwDictionary4List.Where(x => x.DictionaryTypeId == typeId).Select
            (
                x => new DTO.DictionaryItem4List()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DictionaryTypeId = x.DictionaryTypeId,
                    DictionaryTypeName = x.DictionaryTypeName
                }                
            ).OrderBy( x => x.Name ).ToList();

            return result;
        }

        public static Boolean Update(DTO.DictionaryItem dicItem)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblDictionary currRec = ctx.tblDictionaries.SingleOrDefault( x => x.Id == dicItem.Id );
            if( currRec==null ) return false;

            currRec.Name           = dicItem.Name;
            currRec.Description    = dicItem.Description;
            currRec.DictionaryType = dicItem.DictionaryType;
            ctx.SaveChanges();

            return true;
        }
        public static Boolean Delete(Guid Id)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblDictionary currRec = ctx.tblDictionaries.SingleOrDefault(x => x.Id == Id);
            if (currRec == null) return false;

            ctx.tblDictionaries.Remove(currRec);
            ctx.SaveChanges();

            return true;
        }

        public static Boolean IsNameExists(Guid exceptId, String name)
        {
            StudentsManagerEntities ctx = ContextHandler.Get();
            tblDictionary currRec = ctx.tblDictionaries.SingleOrDefault(x => x.Name == name && x.Id != exceptId);
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
