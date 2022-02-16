using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.DataAccessLayer
{
    public static class ContextHandler
    {
        private static StudentsManagerEntities _currCtx = null;
        public static StudentsManagerEntities Get()
        {
            if (_currCtx == null)
            {
                _currCtx = new StudentsManagerEntities();
            }

            return _currCtx;
        }
    }
}
