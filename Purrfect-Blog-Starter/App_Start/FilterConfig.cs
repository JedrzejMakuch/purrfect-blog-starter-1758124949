using System.Web;
using System.Web.Mvc;

namespace Purrfect_Blog_Starter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
