using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using StudentsManager.PresentationLayer.Debug;

namespace StudentsManager.PresentationLayer
{
    public partial class TestNotificationRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RequestLogger.LogIt(Request);
        }
    }
}