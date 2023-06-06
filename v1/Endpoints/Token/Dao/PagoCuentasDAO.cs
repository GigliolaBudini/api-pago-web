
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Webpay.Transbank.Library;
using Webpay.Transbank.Library.Wsdl.Normal;

namespace API.EndPoints.Token.Dao
{ 

	public class TokenDAO
    {

		private static TokenDAO singleton = null;
		private static Configuration configuration = null;

        static Dictionary<string, string> store_codes()
        {

            /** Crea un Dictionary para almacenar los datos de tiendas */
            Dictionary<string, string> store_codes = new Dictionary<string, string>();

            /** Agregar datos de integración a Dictionary */
            /*597044444402
            store_codes.Add("ME11", "597034263329");
            store_codes.Add("ME31", "597034263345");
            store_codes.Add("ME33", "597034263639");*/
            store_codes.Add("ME11", "597044444402");
            store_codes.Add("ME31", "597044444403");
            store_codes.Add("ME33", "597044444402");
            return store_codes;

        }
        private TokenDAO() {


            configuration = new Configuration();

            configuration.Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            configuration.CommerceCode = System.Configuration.ConfigurationManager.AppSettings["CommerceCode"];
            configuration.PublicCert = System.Configuration.ConfigurationManager.AppSettings["PublicCert"];
            configuration.WebpayCert = System.Configuration.ConfigurationManager.AppSettings["WebpayCert"];
            configuration.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            configuration.StoreCodes = store_codes();
        }

        public static TokenDAO getInstance() {
			if (singleton == null) {
				singleton = new TokenDAO();
			}
			return singleton;
		}


        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


        public Vo.TokenOut getToken(Vo.DatosExtras msgIn) {

            Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);

            string urlReturn = System.Configuration.ConfigurationManager.AppSettings["UrlReturn"] /*"http://192.168.2.104:80/Services/Pago/consultaEstadoPago"*/
            ;
            string urlFinal = System.Configuration.ConfigurationManager.AppSettings["UrlFinal"] /*"http://192.168.2.104:80/Services/End/endTransacction"*/;
            Pago.Dao.PagoDAO dao = Pago.Dao.PagoDAO.getInstance();
            //string sesionId = dao.addRegistroPago(0, msgIn.Rut, "", msgIn.IdOrden, msgIn.Monto, null);
            var nroOc = (DateTime.Now.Ticks/1000)+"";
            //var nroOc = Math.Round(n,0x4);
            
            String timeStamp =  System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(nroOc));

            Dictionary<string, string[]> stores = new Dictionary<string, string[]>();
            decimal MontoTotal=0;
            foreach (var row in msgIn.Pagos )
            {
                try
                {
                    var codigoComercio = store_codes()[row.Sociedad.Trim()];
                    row.CodigoComercio = codigoComercio;
                    stores.Add(codigoComercio, new string[] {
                               codigoComercio,                // storeCode
                                row.Monto.ToString(), // amount
                                nroOc, // buyOrder
                                timeStamp.ToString(), // sessionId 
                            });
                }
                catch
                {
                    var codigoComercio = store_codes()[row.Sociedad.Trim()];
                    var data=stores[codigoComercio];
                    data[1] = (Decimal.Parse(data[1]) + row.Monto).ToString();
                    stores[codigoComercio] = data;

               }
                
                MontoTotal += row.Monto;
            }

           // webpay.getMallNormalTransaction().initTransaction()
            var result = webpay.getMallNormalTransaction().initTransaction(nroOc, timeStamp.ToString(), urlReturn, urlFinal, stores);
            
            Vo.TokenOut msgOut = new Vo.TokenOut();
            msgOut.token = result.token;
            msgOut.url = result.url;
            dao.AddRegistroPago(msgIn.Rut, msgOut.token, nroOc, MontoTotal, null,msgIn);
            Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();
            log.AddLogPago(msgOut.token, msgIn.Rut, nroOc, null, MontoTotal, DateTime.Now, 0, "CreaToken");
            return msgOut;
		}

        public Vo.TokenOut getToken(Vo.TokenExteroIn msgIn)
        {

            Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);

            string urlReturn = System.Configuration.ConfigurationManager.AppSettings["UrlReturnSeguros"];
            string urlFinal = System.Configuration.ConfigurationManager.AppSettings["UrlFinalSeguros"];
            Pago.Dao.PagoDAO dao = Pago.Dao.PagoDAO.getInstance();

            var sessionId = (DateTime.Now.Ticks / 1000) + "";
            Dictionary<string, string[]> stores = new Dictionary<string, string[]>();
         
            var codigoComercio = System.Configuration.ConfigurationManager.AppSettings["CodigoSociedadPagoExterno"];
                
            stores.Add(codigoComercio, new string[] {
                        codigoComercio,   // storeCode
                        msgIn.Monto.ToString(), // amount
                        msgIn.NroOrdenCompra, // buyOrder
                        sessionId, // sessionId 
                    });
      
            

            // webpay.getMallNormalTransaction().initTransaction()
            var result = webpay.getMallNormalTransaction().initTransaction(msgIn.NroOrdenCompra, sessionId, urlReturn, urlFinal, stores);

            Vo.TokenOut msgOut = new Vo.TokenOut();
            msgOut.token = result.token;
            msgOut.url = result.url;
            dao.AddRegistroPago(msgIn.Rut, msgOut.token, msgIn.NroOrdenCompra, msgIn.Monto, null, msgIn);

            Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();
            log.AddLogPago(msgOut.token, msgIn.Rut, msgIn.NroOrdenCompra, null, msgIn.Monto, DateTime.Now, 0, "CreaToken");

            return msgOut;
        }

    }
}