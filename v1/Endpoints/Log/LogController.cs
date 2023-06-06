
using API.Endpoints.Log.Vo;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.EndPoints.Log
{
    [EnableCors("*", "*", "*")]
    public class LogController : Gale.REST.RestController
    {


        // [HttpGet]
        [HttpPost]
        [HierarchicalRoute("getLog")]
        public IHttpActionResult consultaEstadoPago(LogRequest msgIn /*string dato*/)
        {
          /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/

            return new Services.GetLog(msgIn);
        }



    }
}