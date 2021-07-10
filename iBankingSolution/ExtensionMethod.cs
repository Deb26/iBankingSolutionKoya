using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iBankingSolution
{
    public static class ExtensionMethod
    {
        public static DateTime? ToNullableDateTime(this string val)
        {
            DateTime temp;
            return DateTime.TryParse(val, out temp) ? (DateTime?)temp : null;
        }
    }
}