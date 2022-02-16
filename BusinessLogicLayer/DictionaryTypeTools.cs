using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentsManager.DataAccessLayer;

using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;

namespace StudentsManager.BusinessLogicLayer
{
    public static class DictionaryTypeTools
    {
        private static List<DTO.DictionaryType> _currData = null;

        public static List<DTO.DictionaryType> ReadAll()
        {
            if (_currData == null)
            {
                StudentsManagerEntities ctx = ContextHandler.Get();

                _currData = ctx.tblDictionaryTypes.Select
                (x =>
                    new DTO.DictionaryType()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }
                ).OrderBy(x => x.Name).ToList();
            }

            return _currData;
        }

        public static DTO.DictionaryType Read(Guid Id)
        {
            List<DTO.DictionaryType> currData = ReadAll();
            DTO.DictionaryType result = currData.SingleOrDefault( x => x.Id == Id);
            return result;
        }

        public static DTO.DictionaryType Read(String name)
        {
            List<DTO.DictionaryType> currData = ReadAll();
            DTO.DictionaryType result = currData.SingleOrDefault(x => x.Name == name);
            return result;
        }
    }
}
