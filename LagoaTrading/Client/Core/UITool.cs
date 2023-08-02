using System.Web;
using System.Xml.Linq;

namespace LagoaTrading.Client.Core
{
    public class UITool
    {
        public static string GetQueryString(object query)
        {
            var properties = query.GetType().GetProperties()
                .Where(p => p.GetValue(query, null) != null)
                .Select(p => p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(query, null).ToString()));

            return String.Join("&", properties.ToArray());
        }
    }
}
