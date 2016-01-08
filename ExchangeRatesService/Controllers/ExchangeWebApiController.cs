using ExchangeRatesService.Code.Bu;
using ExchangeService;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace ExchangeRatesService.Controllers
{
    [CacheOutput(ClientTimeSpan = 180, ServerTimeSpan = 180, AnonymousOnly = true)]
    public class ExchangeWebApiController : ApiController
    {
        [Route("api/ServiceVersion")]
        [HttpGet]
        public string ServiceVersion()
        {
            return "1.0.0.0";
        }

        [Route("api/GetDayInMonth/{Year}/{Month}")]
        [HttpGet]
        public P_fxrates_update[] GetDayInMonth(int Year, int Month)
        {
            ExchangeDb Db = new ExchangeDb();
            return Db.GetDayInMonth(Year, Month);
        }

        [Route("api/GetUpdateInMonth/{dd}/{mm}/{yyyy}")]
        [HttpGet]
        public P_fxrates_update[] GetUpdateInMonth(int dd, int mm, int yyyy)
        {
            ExchangeDb Db = new ExchangeDb();
            return Db.GetUpdateInMonth(dd, mm, yyyy);
        }

        [Route("api/GetfxratesLastUpdate")]
        [HttpGet]
        public P_fxrates_update[] GetfxratesLastUpdate()
        {
            ExchangeDb Db = new ExchangeDb();
            return Db.GetfxratesLastUpdate();
        }

        [Route("api/GetLatestfxrates")]
        [HttpGet]
        public P_fxrates[] GetLatestfxrates()
        {
            ExchangeDb Db = new ExchangeDb();

            return Db.GetLatestfxrates();
        }

        [Route("api/Getfxrates/{dd}/{mm}/{yyyy}/{upd}/{Lang}")]
        [HttpGet]
        public P_fxrates[] Getfxrates(int dd, int mm, int yyyy, int upd, string Lang)
        {
            ExchangeDb Db = new ExchangeDb();

            return Db.Getfxrates(dd, mm, yyyy, upd, Lang);
        }

        [IgnoreCacheOutput]
        [Route("api/GetChartfxrates/{Fdd}/{Fmm}/{Fyyyy}/{Tdd}/{Tmm}/{Tyyyy}/{Family}/{Lang}")]
        [HttpGet]
        public P_fxrates[] GetChartfxrates(int Fdd, int Fmm, int Fyyyy, int Tdd, int Tmm, int Tyyyy, string Family, string Lang)
        {
            ExchangeDb Db = new ExchangeDb();

            return Db.GetChartfxrates(Fdd, Fmm, Fyyyy, Tdd, Tmm, Tyyyy, Family, Lang);
        }

        [IgnoreCacheOutput]
        [Route("api/GetDownloadfxrates/{Fdd}/{Fmm}/{Fyyyy}/{Tdd}/{Tmm}/{Tyyyy}/{Family}/{Lang}")]
        public P_fxrates[] GetDownloadfxrates(int Fdd, int Fmm, int Fyyyy, int Tdd, int Tmm, int Tyyyy, string Family, string Lang)
        {
            ExchangeDb Db = new ExchangeDb();

            return Db.GetDownloadfxrates(Fdd, Fmm, Fyyyy, Tdd, Tmm, Tyyyy, Family, Lang);
        }

        [Route("api/Getfxfamily")]
        [HttpGet]
        public P_fxfamily[] Getfxfamily()
        {
            ExchangeDb Db = new ExchangeDb();
            return Db.Getfxfamily();
        }
    }
}