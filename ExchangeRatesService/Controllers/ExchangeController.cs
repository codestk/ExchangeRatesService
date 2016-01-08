using ExchangeService.Code;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace ExchangeService.Controllers
{
    // [CacheOutput(ClientTimeSpan = 180, ServerTimeSpan = 180, AnonymousOnly = true)]
    [CacheOutput(ClientTimeSpan = 180, ServerTimeSpan = 180, AnonymousOnly = true)]
    public class ExchangeController : ApiController
    {
        //=IHttpActionResult
        //[Route("api/LastestIHttpActionResultLinq")]
        //[HttpGet]
        //public IHttpActionResult LastestIHttpActionResultLinq()
        //{
        //    ExchnageLinq exchange = new ExchnageLinq();
        //    var product = exchange.GetLatestfxrates();

        //    if (product == null)
        //    {
        //        return NotFound(); // Returns a NotFoundResult
        //    }
        //    return Ok(product); // Returns an OkNegotiatedContentResult
        //}

        [Route("api/LastestIHttpActionResultOleDB")]
        [HttpGet]
        public IHttpActionResult LastestIHttpActionResultOleDB()
        {
            ExchangeDb exchange = new ExchangeDb();
            var product = exchange.GetLatestfxratesOLE();

            if (product == null)
            {
                return NotFound(); // Returns a NotFoundResult
            }
            return Ok(product); // Returns an OkNegotiatedContentResult
        }

        //tHttpResponse=========================================================================================================

        //[Route("api/LastestHttpResponseMessageLinq")]
        //[HttpGet]
        //public HttpResponseMessage LastestHttpResponseMessageLinq()
        //{
        //    ExchnageLinq exchange = new ExchnageLinq();

        //    // Get a list of products from a database.
        //    IEnumerable<P_fxrates> products = exchange.GetLatestfxrates();

        //    // Write the list to the response body.
        //    var response = Request.CreateResponse(HttpStatusCode.OK, products);
        //    return response;
        //}

        [Route("api/LastestHttpResponseMessageOLE")]
        [HttpGet]
        public HttpResponseMessage LastestHttpResponseMessageOLE()
        {
            ExchangeDb exchnage = new ExchangeDb();

            IEnumerable<P_fxrates> products = exchnage.GetLatestfxratesOLE();

            // Write the list to the response body.
            var response = Request.CreateResponse(HttpStatusCode.OK, products);
            return response;
        }

        //Other===================================================================================================

        //[Route("api/GetLatestfxratesOtherLinq")]
        //[HttpGet]
        //public P_fxrates[] GetLatestfxratesOtherLinq()
        //{
        //    ExchnageLinq exchange = new ExchnageLinq();

        //    return exchange.GetLatestfxrates();
        //}

        [Route("api/GetLatestfxratesOtherOle")]
        [HttpGet]
        public P_fxrates[] GetLatestfxrates()
        {
            ExchangeDb exchnage = new ExchangeDb();

            return exchnage.GetLatestfxratesOLE();
        }

        //================================================================================================================================
    }
}