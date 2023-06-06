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

namespace API.EndPoints.Token.Services
{

    


	        public class Token :  Gale.REST.Http.HttpCreateActionResult<Vo.DatosExtras>
            {
                private static Logger log = LogManager.GetCurrentClassLogger();
                public Token(Vo.DatosExtras model) : base(model) 
                {
                    
                }
                public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
                {
                        Vo.TokenOut _outMessage =null;
                        Dao.TokenDAO dao = Dao.TokenDAO.getInstance();
			            _outMessage=dao.getToken(this.Model);
                        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                        {
                            Content = new ObjectContent<Object>(
                                    _outMessage, new System.Net.Http.Formatting.JsonMediaTypeFormatter() /*,
                                    System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()*/
                            )
                        };
                        return Task.FromResult(response);
                }
            }

        public class TokenExterno : Gale.REST.Http.HttpCreateActionResult<Vo.TokenExteroIn>
        {
            private static Logger log = LogManager.GetCurrentClassLogger();
            public TokenExterno(Vo.TokenExteroIn model) : base(model)
            {

            }

            public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
            {
                Vo.TokenOut _outMessage = null;
                Dao.TokenDAO dao = Dao.TokenDAO.getInstance();
                _outMessage = dao.getToken(this.Model);
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ObjectContent<Object>(
                            _outMessage, new System.Net.Http.Formatting.JsonMediaTypeFormatter() /*,
                                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()*/
                    )
                };
                return Task.FromResult(response);
            }
        }

}