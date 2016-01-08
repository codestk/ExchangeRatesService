using System.Configuration;
using System.Globalization;

namespace ExchangeService.Code
{
    public class Config
    {
        public static CultureInfo GlobalCulture()
        {
            var culture = new CultureInfo("th-TH", false);
            return culture;
        }

        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["WEBAPPConnectionString"].ConnectionString;
        }

        public static string EnableCorsAttribute()
        {
            return ConfigurationManager.AppSettings["EnableCorsAttribute"];
        }

        public static string GetForTest()
        {
            return ConfigurationManager.AppSettings["ForTest"];
        }
    }
}