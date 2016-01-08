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

        [Route("api/GetDayInMonth/{year}/{month}")]
        [HttpGet]
        public P_fxrates_update[] GetDayInMonth(int year, int month)
        {
            ExchangeDb db = new ExchangeDb();
            return db.GetDayInMonth(year, month);
        }

        [Route("api/GetUpdateInMonth/{dd}/{mm}/{yyyy}")]
        [HttpGet]
        public P_fxrates_update[] GetUpdateInMonth(int dd, int mm, int yyyy)
        {
            ExchangeDb db = new ExchangeDb();
            return db.GetUpdateInMonth(dd, mm, yyyy);
        }

        [Route("api/GetfxratesLastUpdate")]
        [HttpGet]
        public P_fxrates_update[] GetfxratesLastUpdate()
        {
            ExchangeDb db = new ExchangeDb();
            return db.GetfxratesLastUpdate();
        }

        [Route("api/GetLatestfxrates")]
        [HttpGet]
        public P_fxrates[] GetLatestfxrates()
        {
            ExchangeDb db = new ExchangeDb();

            return db.GetLatestfxrates();
        }

        [Route("api/Getfxrates/{dd}/{mm}/{yyyy}/{upd}/{lang}")]
        [HttpGet]
        public P_fxrates[] Getfxrates(int dd, int mm, int yyyy, int upd, string lang)
        {
            ExchangeDb db = new ExchangeDb();

            return db.Getfxrates(dd, mm, yyyy, upd, lang);
        }

        [IgnoreCacheOutput]
        [Route("api/GetChartfxrates/{fdd}/{fmm}/{fyyyy}/{tdd}/{tmm}/{tyyyy}/{family}/{lang}")]
        [HttpGet]
        public P_fxrates[] GetChartfxrates(int fdd, int fmm, int fyyyy, int tdd, int tmm, int tyyyy, string family, string lang)
        {
            ExchangeDb db = new ExchangeDb();

            return db.GetChartfxrates(fdd, fmm, fyyyy, tdd, tmm, tyyyy, family, lang);
        }

        [IgnoreCacheOutput]
        [Route("api/GetDownloadfxrates/{fdd}/{fmm}/{fyyyy}/{tdd}/{tmm}/{tyyyy}/{family}/{lang}")]
        public P_fxrates[] GetDownloadfxrates(int fdd, int fmm, int fyyyy, int tdd, int tmm, int tyyyy, string family, string lang)
        {
            ExchangeDb db = new ExchangeDb();

            return db.GetDownloadfxrates(fdd, fmm, fyyyy, tdd, tmm, tyyyy, family, lang);
        }

        [Route("api/Getfxfamily")]
        [HttpGet]
        public P_fxfamily[] Getfxfamily()
        {
            ExchangeDb db = new ExchangeDb();
            return db.Getfxfamily();
        }
    }
}