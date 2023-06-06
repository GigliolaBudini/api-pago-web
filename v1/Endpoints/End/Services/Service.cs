
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace API.EndPoints.End.Services
{

    

	
    public class EndTransaccion :  Gale.REST.Http.HttpCreateActionResult<Vo.EndTransactionIn>
    {
		public EndTransaccion(Vo.EndTransactionIn model) : base(model) 
        {
                    
        }
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
                Vo.EndTransactionOut _outMessage =null;
                Dao.EndDAO dao = Dao.EndDAO.getInstance();
			    _outMessage=dao.ConsultaEstadoTransaccion(this.Model);
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

    public class EndTransaccionExterna : Gale.REST.Http.HttpCreateActionResult<Vo.EndTransactionIn>
    {
        public EndTransaccionExterna(Vo.EndTransactionIn model) : base(model)
        {

        }
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            Vo.EndTransactionOut _outMessage = null;
            Dao.EndDAO dao = Dao.EndDAO.getInstance();
            _outMessage = dao.ConsultaEstadoTransaccionExterna(this.Model);
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