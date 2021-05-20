using System.Web;
using System.Web.Mvc;

namespace Sencillo_Project_DSW
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
