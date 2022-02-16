using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace StudentsManager.PresentationLayer.Tools
{
    public static class StorageEngine
    {
        public static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
    }
}