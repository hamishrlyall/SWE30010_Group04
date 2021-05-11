using System.Web;
using System.Web.Mvc;

namespace SWE30010_Group04_WebApplication
{
   public class FilterConfig
   {
      public static void RegisterGlobalFilters( GlobalFilterCollection filters )
      {
         filters.Add( new HandleErrorAttribute( ) );
      }
   }
}
