
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.EndPoints.End
{
    [EnableCors("*", "*", "*")]
    public class EndController : Gale.REST.RestController
    {


        [HttpPost]
        [HierarchicalRoute("endTransacction")]
        public IHttpActionResult consultaEstadoPago(Vo.EndTransactionIn msgIn /*string dato*/)
        {
          /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/

            return new Services.EndTransaccion(msgIn);
        }


        [HttpPost]
        [HierarchicalRoute("voucherSeguro")]
        public IHttpActionResult voucherSeguro(Vo.EndTransactionIn msgIn /*string dato*/)
        {
            /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/

            return new Services.EndTransaccionExterna(msgIn);
        }
        /*
        [HttpPost]
        [HierarchicalRoute("setTracking")]
        public IHttpActionResult  setTracking(API.EndPoints.Tracking.Vo.SetTrackingIN msgIn)
        {
             return new Services.SetTracking(msgIn);
        }
        */

    }
}