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

namespace API.EndPoints.PagoCuentas
{
    [EnableCors("*", "*", "*")]
    public class DocumentoController : Gale.REST.RestController
    {


        [HttpGet]
        [HierarchicalRoute("{rut}/{codigo}")]
        public IHttpActionResult  ConsultaDocumentos(string rut, string codigo)
        {
            Vo.GetCuentasIn msgIn = new Vo.GetCuentasIn();
            msgIn.Codigo  = codigo;
            msgIn.Rut = rut;
            return new Services.ConsultaDocumentos(msgIn);
        }

 
    }
}