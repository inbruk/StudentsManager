using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentsManager.PresentationLayer.Tools
{
    public static class ControlTools
    {
        public static IEnumerable<Control> GetAllSubControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;
                foreach (Control descendant in GetAllSubControls(control))
                {
                    yield return descendant;
                }
            }
        }
    }
}