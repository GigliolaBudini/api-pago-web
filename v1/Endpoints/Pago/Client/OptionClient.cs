using API.EndPoints.Pago.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Webpay.Transbank.Library.Wsdl.Mall.Normal;

namespace API.Endpoints.Pago.Client
{
    public class OptionClient
    {
        public OptionClient() { }


        public async Task<bool> callRegistraPago(OptionRequest msgIn)
        {
            string urlServicio = System.Configuration.ConfigurationManager.AppSettings["Url.RegistraPago.Seguros"];
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(double.Parse(System.Configuration.ConfigurationManager.AppSettings["Timeout.RegistraPago.Seguros"]));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            using (httpClient)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(msgIn), Encoding.UTF8, "application/json");
                    HttpResponseMessage respuestaServicio = await httpClient.PostAsync(urlServicio, content);
                    if (respuestaServicio.StatusCode == HttpStatusCode.OK || respuestaServicio.StatusCode == HttpStatusCode.NoContent || respuestaServicio.StatusCode == HttpStatusCode.Created)
                    {
                        //  string responseBody = await respuestaServicio.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        //return false;
                        throw new Exception(respuestaServicio.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                 //   return false;
                    throw new Exception(e.Message);
                }
            }
        }

        public OptionRequest TranfromaMsg(transactionResultOutput msgIn,String token )
        {
            OptionRequest msgOut = new OptionRequest();
            msgOut.sessionId = msgIn.sessionId;
            msgOut.transactionDate = msgIn.transactionDate;
            msgOut.VCI = msgIn.VCI;
            msgOut.accoutingDate = msgIn.accountingDate;
            msgOut.buyOrder = msgIn.buyOrder;
            msgOut.cardExpirationDate = msgIn.cardDetail.cardExpirationDate;
            msgOut.cardNumber = msgIn.cardDetail.cardNumber;
            msgOut.authorizationCode = msgIn.detailOutput[0].authorizationCode;
            msgOut.commerseCode = msgIn.detailOutput[0].commerceCode;
            msgOut.amount = msgIn.detailOutput[0].amount;
            msgOut.paymentTypeCode = msgIn.detailOutput[0].paymentTypeCode;
            msgOut.responseCode = msgIn.detailOutput[0].responseCode;
            msgOut.sharesNumber = msgIn.detailOutput[0].sharesNumber;
            msgOut.tokenTbk = token;
            return msgOut;
        }

    }


    public class OptionRequest
    {

        
        public string tokenTbk { get; set; }
        public string buyOrder { get; set; }
        public string sessionId { get; set; }
        public string accoutingDate { get; set; }
        public DateTime transactionDate { get; set; }
        public string VCI { get; set; }
        public string cardNumber { get; set; }
        public string cardExpirationDate { get; set; }
        public string authorizationCode { get; set; }
        public string paymentTypeCode { get; set; }
        public int responseCode { get; set; }
        public decimal amount { get; set; }
        public int sharesNumber { get; set; }
        public string commerseCode { get; set; }
       
    }


       
}