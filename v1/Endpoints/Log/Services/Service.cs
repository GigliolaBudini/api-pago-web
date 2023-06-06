
using API.Endpoints.Log.Vo;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace API.EndPoints.Log.Services
{

    

	
    public class GetLog :  Gale.REST.Http.HttpCreateActionResult<LogRequest>
    {
		public GetLog(LogRequest model) : base(model) 
        {
                    
        }
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
                LogOut _outMessage =null;
                Dao.LogDAO dao = Dao.LogDAO.getInstance();
			    _outMessage=dao.getLog(this.Model.rut,this.Model.ordenes,this.Model.fecha_init,this.Model.fecha_fin);
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