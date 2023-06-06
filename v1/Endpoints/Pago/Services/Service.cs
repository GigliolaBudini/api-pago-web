
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace API.EndPoints.Pago.Services
{
    public class ConsultaEstadoTransaccion : Gale.REST.Http.HttpCreateActionResult<Vo.TransactionResultIn>
    {
        public ConsultaEstadoTransaccion(Vo.TransactionResultIn model) : base(model)
        {

        }
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            Vo.TransactionResultOut _outMessage = null;
            Dao.PagoDAO dao = Dao.PagoDAO.getInstance();

            _outMessage = dao.ResgitraPagoExternoTransbankAsync(this.Model);
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



    public class RegistraPagoTransbankExterno : Gale.REST.Http.HttpCreateActionResult<Vo.TransactionResultIn>
    {
        public RegistraPagoTransbankExterno(Vo.TransactionResultIn model) : base(model)
        {

        }
        public override async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            Vo.TransactionResultOut _outMessage = null;
            Dao.PagoDAO dao = Dao.PagoDAO.getInstance();
            _outMessage =await dao.ResgitraPagoExternoTransbank(this.Model);
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<Object>(
                        _outMessage,
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                )
            };
            return await Task.FromResult(response);
        }

    }



    public class GetTransaccion : Gale.REST.Http.HttpCreateActionResult<Vo.TransactionResultIn>
    {
        public GetTransaccion(Vo.TransactionResultIn model) : base(model)
        {

        }
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {

            Dao.PagoDAO dao = Dao.PagoDAO.getInstance();
            //var _response = dao.GetResponse(this.Model);
            Dao.PagoDAO.ResponseTransactionJson _response = dao.getTransactionResultApi(Model.token_ws).Result;
            Dao.PagoDAO.DatosExtraTransactionJson _datosExtras = dao.getDatosExtraTransactionApi(Model.token_ws).Result;
            try
            {
                foreach (var r in _response.transactionDetail)
                {
                    foreach (var d in _datosExtras.Pagos)
                    {
                        if (r.commerceCode == d.CodigoComercio)
                        {
                            if (r.items == null)
                                r.items = new System.Collections.Generic.List<Dao.PagoDAO.PagosJson>();
                            r.items.Add(d);
                        }
                    }
                }
            }
            catch (Exception e)
            {
            /*    foreach (var d in _datosExtras.Pagos)
                {
                    cancellationToken.
                    if (r.commerceCode == d.CodigoComercio)
                    {
                        if (r.items == null)
                            r.items = new System.Collections.Generic.List<Token.Vo.Episodios>();
                        r.items.Add(d);
                    }
                }*/
            }
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {               
                 Content = new ObjectContent<Object>(
                        _response,
                        System.Web.Http.GlobalConfiguration.Configuration.Formatters.KqlFormatter()
                )
            };
            return Task.FromResult(response);
        }

    }

}