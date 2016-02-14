using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.HelperMethods
{
    public class Helper
    {
        public string ConvertToTitleCase(string title)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            title = textInfo.ToTitleCase(title);

            return title;
        }
    }
}