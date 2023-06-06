/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/


using System.Web.Http;
using System.Web.Http.Cors;

namespace API.EndPoints.Token
{
   
 
    public class TokenController : Gale.REST.RestController
    {



     /*   [EnableCors(origins: "*", headers: "*", methods: "*")]*/
        [HttpPost]
        [HierarchicalRoute("CreaToken")]
        public IHttpActionResult CreaToken(Vo.DatosExtras msgIn)
        {
            /*
            Vo.TokenIn msgIn = new Vo.TokenIn();
            msgIn.Monto  = monto;
            msgIn.IdOrdens = orden;
            msgIn.Rut = rut;*/
            return new Services.Token(msgIn);
        }

        [HttpPost]
        [HierarchicalRoute("CreaTokenExterno")]
        public IHttpActionResult CreaTokenExterno(Vo.TokenExteroIn msgIn)
        {
            /*
            Vo.TokenIn msgIn = new Vo.TokenIn();
            msgIn.Monto  = monto;
            msgIn.IdOrdens = orden;
            msgIn.Rut = rut;*/
            return new Services.TokenExterno(msgIn);
        }
    }
}