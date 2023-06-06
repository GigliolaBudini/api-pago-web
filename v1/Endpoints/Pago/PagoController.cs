
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.EndPoints.Pago
{
    [EnableCors("*", "*", "*")]
    public class PagoController : Gale.REST.RestController
    {

        [HttpPost]
        [HierarchicalRoute("consultaEstadoPago")]
        public IHttpActionResult consultaEstadoPago(Vo.TransactionResultIn msgIn /*string dato*/)
        {
          /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/

            return new Services.ConsultaEstadoTransaccion(msgIn);
        }

        [HttpPost]
        [HierarchicalRoute("registraPagoTransbank")]
        public async System.Threading.Tasks.Task<IHttpActionResult> registraPagoTransbankAsync(Vo.TransactionResultIn msgIn /*string dato*/)
        {
            /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/
           // var response = await new Services.RegistraPagoTransbankExterno(msgIn);
            return new Services.RegistraPagoTransbankExterno(msgIn);
        }

        [HttpGet]
        [HierarchicalRoute("getTransaccion/{token}")]
        public IHttpActionResult getTransaccion(string token)
        {
            /*  Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();*/
            Vo.TransactionResultIn msgIn = new Vo.TransactionResultIn();
            msgIn.token_ws = token;
            var respuesta= new Services.GetTransaccion(msgIn);
            return respuesta;
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