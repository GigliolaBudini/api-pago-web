/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using NLog;
using API.Helper;

namespace API.EndPoints.PagoCuentas.Services
{

    


	    public class ConsultaDocumentos :  Gale.REST.Http.HttpCreateActionResult<Vo.GetCuentasIn>
        {
            private static Logger log = LogManager.GetCurrentClassLogger();
            public ConsultaDocumentos(Vo.GetCuentasIn model) : base(model) 
            {
                    
            }
            public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
            {
                    Vo.GetCuentasOut _outMessage =null;
                    Dao.PagoCuentasDAO dao = Dao.PagoCuentasDAO.getInstance();
			        _outMessage=dao.consultaDocumentos(this.Model);
                    var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<Object>(
                                _outMessage,
                                System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                        )
                    };
                    return Task.FromResult(response);
            }
        }



}