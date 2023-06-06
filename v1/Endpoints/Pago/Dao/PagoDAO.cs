/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using API.Endpoints.Pago.Client;
using API.EndPoints.Pago.Vo;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Webpay.Transbank.Library;
using Webpay.Transbank.Library.Wsdl.Mall.Normal;
using System.Net.Http;
using System.Net.Http.Headers;

namespace API.EndPoints.Pago.Dao
{ 
    public class respuesta
    {

    }
	public class PagoDAO {

		private static PagoDAO singleton = null;

        private static Configuration configuration = null;
        private static string connectionName = null;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly HttpClient client = new HttpClient();


        private PagoDAO() {

            connectionName = System.Configuration.ConfigurationManager.ConnectionStrings["SQLServerConexionString"].ConnectionString;

            configuration = new Configuration();
            configuration.Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            configuration.CommerceCode = System.Configuration.ConfigurationManager.AppSettings["CommerceCode"];
            configuration.PublicCert = System.Configuration.ConfigurationManager.AppSettings["PublicCert"];
            configuration.WebpayCert = System.Configuration.ConfigurationManager.AppSettings["WebpayCert"];
            configuration.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        }

        public static PagoDAO getInstance() {
			if (singleton == null) {
				singleton = new PagoDAO();
			}
			return singleton;
		}


		
		public TransactionResultOut ResgitraPagoExternoTransbankAsync(TransactionResultIn msgIn)  {
            TransactionResultOut msgOut = new TransactionResultOut();
            //TransactionResultOut result = new TransactionResultOut();

            try {
                //var sinError = true;
                ResponseTransactionJson result = null;
                Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);
                logger.Info("[ResgitraPagoExternoTransbankAsync] Token: " + msgIn.token_ws);

                /* inicio transaccion mall */
                //var result = webpay.getMallNormalTransaction().getTransactionResult(msgIn.token_ws);

                try
                {
                    result = getTransactionResultApi(msgIn.token_ws).Result;
                    Console.WriteLine();
                }
                catch(Exception exce) {
                    logger.Info("[ResgitraPagoExternoTransbankAsync] Excepcion: " + exce.Message);
                }
                
                Console.WriteLine(result);

                Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();
                log.AddLogPago(msgIn.token_ws, "", "", result.detailOutput[0].authorizationCode, result.detailOutput[0].amount, DateTime.Now, 0, ResponseReazon(result.detailOutput[0].responseCode));
                
                msgOut.accoutingDate = result.accountingDate;
                msgOut.buyOrder = result.buyOrder;
                msgOut.cardDetail = new CardDetail();
                msgOut.cardDetail.cardExpirationDate = result.cardDetail.cardExpirationDate;
                msgOut.cardDetail.cardNumber = result.cardDetail.cardNumber;


                //msgOut.cardDetail.transactionDetail = result.cardDetail;

                /* EXTRAE INFORMACIÓN DE BD LOCAL */
                
                DatosExtraTransactionJson datosExtras = getDatosExtraTransactionApi(msgIn.token_ws).Result;
                //var datosExtras = GetDatosExtras(msgIn.token_ws);

                /* CREA RERQUEST DEL MENSAJE  */
                List<RegistraPagoWebReference.DT_TransacWebTransaccion> requestArray = new List<RegistraPagoWebReference.DT_TransacWebTransaccion>();
                msgOut.transactionDetail = new List<TransactionDetail>();
                foreach (var salida in result.detailOutput)
                {
                    var row= new TransactionDetail();
                    row.amount = salida.amount;
                    row.buyOrder = salida.buyOrder;
                    row.commerceCode = salida.commerceCode;
                    row.paymentTypeCode = salida.paymentTypeCode;
                    row.responseCode = salida.responseCode;
                    row.sharesAmount = decimal.Parse(salida.sharesAmount);
                    row.sharesNumber = decimal.Parse(salida.sharesNumber);
                    row.responseReazon = ResponseReazon(salida.responseCode);
                    row.authorizationCode = salida.authorizationCode;                    
                    msgOut.transactionDetail.Add(row);

                    if (row.responseCode != 0)
                    {
                        logger.Error("[ResgitraPagoExternoTransbankAsync] Transbank: Rechazó de transacción. ");
                        throw new Exception("Transbank: Rechazó de transacción.");
                    }
                    else
                    {
                        logger.Info("[ResgitraPagoExternoTransbankAsync] Transbank: Transaccion OK ");


                        foreach (var extra in datosExtras.Pagos)
                        {
                            //if (extra.CodigoComercio == row.commerceCode)
                               if (extra.NroOrdenCompra == row.buyOrder)
                                {
                                logger.Info("Datos a enviar =>  "+ extra);
                                RegistraPagoWebReference.DT_TransacWebTransaccion lineaRequest = new RegistraPagoWebReference.DT_TransacWebTransaccion();
                                lineaRequest.posicion = extra.Posicion;
                                lineaRequest.sociedad = extra.Sociedad;
                                lineaRequest.ejercicio = extra.Ejercicio;
                                lineaRequest.nrodocumento = extra.NroDocumento;
                                lineaRequest.codprestacion = extra.CodCirujia;
                                lineaRequest.accoutingDate = result.accountingDate;
                                lineaRequest.Amount = "" + salida.amount;
                                lineaRequest.authorizationCode = salida.commerceCode;
                                lineaRequest.buyOrder = result.buyOrder;
                                lineaRequest.cardExpirationDate = "" + result.cardDetail.cardExpirationDate;
                                lineaRequest.cardNumber = result.cardDetail.cardNumber;
                                lineaRequest.commerceCode = salida.commerceCode;

                                lineaRequest.paymentTypeCode = salida.paymentTypeCode;

                                lineaRequest.responseCode = "" + salida.responseCode;
                                lineaRequest.sessionId = result.sessionId;
                                lineaRequest.sharesNumber = "" + salida.sharesNumber;


                                lineaRequest.transactionDate = "" + result.transactionDate;
                                lineaRequest.urlRedirection = result.urlRedirection;
                                lineaRequest.VCI = result.VCI;
                                lineaRequest.posicion = "1";
                                requestArray.Add(lineaRequest);
                            }
                        }


                    }



                }

                msgOut.sessionId = result.sessionId;
                msgOut.transactionDate = result.transactionDate;
                msgOut.urlRedirection = result.urlRedirection;
                msgOut.VCI = result.VCI;
                logger.Info("result.VCI: " + result.VCI);
                logger.Info("Result: "+ result.ToString());
                AddRegistroPago("", msgIn.token_ws, "", result.detailOutput[0].amount, msgOut, null);

                


                //  result.detailOutput[0].

                /* redireccionamiento automatico */
                /* validar reglas */


                
                RegistraPagoWebReference.DT_TransacWebRespTransaccionsap[] responseWS = null;


                /* registra pago SAP */
                RegistraPagoWebReference.SI_os_TransaccionSAPCreateService client = new RegistraPagoWebReference.SI_os_TransaccionSAPCreateService();
                client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Servicio.PagoWeb.Usuario"], System.Configuration.ConfigurationManager.AppSettings["Servicio.PagoWeb.Password"]);
                 logger.Info("Request array: "+ requestArray.ToString());
                try
                {
                    responseWS=client.enviaPago(requestArray.ToArray());

                    logger.Info("Registrado en SAP");
                    //webpay.getMallNormalTransaction().acknowledgeTransaction(msgIn.token_ws);
                    logger.Info("Confirmado en Trasnabank");
                    log.AddLogPago(msgIn.token_ws, "", "", "", 0, DateTime.Now, 1, "");
                }
                catch (Exception e)
                {

                    logger.Error("Error SAP: " + e.Message);
                    //logger.Error("Sin Confirmar en Tranbank...");
                    throw new Exception("Error SAP: " + e.Message);
                }
                
                /* CONFRIMA TRANSACCION  TBK */
             
                
    
                
                HttpResponse response_http = HttpContext.Current.Response;
                response_http.Clear();
                StringBuilder s = new StringBuilder();
                s.Append("<html>");
                s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                s.AppendFormat("<form name='form' action='{0}' method='post'>", result.urlRedirection);
                s.AppendFormat("<input type='hidden' name='token_ws' value='{0}' />", msgIn.token_ws );              
                s.Append("</form></body></html>");
                response_http.Write(s.ToString());
                response_http.End();
                

            } catch (Exception e) {

                logger.Error("Error: " +e.Message);
                throw new Exception(e.Message);
			} finally {
				
				
			}
			return msgOut;
		}


        public async Task<TransactionResultOut> ResgitraPagoExternoTransbank(TransactionResultIn msgIn)
        {
            TransactionResultOut msgOut = new TransactionResultOut();
            logger.Info("[ResgitraPagoExternoTransbank] Token: " + msgIn.token_ws);
            try
            {
                

                Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);

                /* inicio transaccion mall */
                transactionResultOutput result = webpay.getMallNormalTransaction().getTransactionResult(msgIn.token_ws);
                Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();
                log.AddLogPago(msgIn.token_ws, "", "", result.detailOutput[0].authorizationCode, result.detailOutput[0].amount, DateTime.Now, 0, ResponseReazon(result.detailOutput[0].responseCode));
                OptionClient optionClient = new OptionClient();
                var requestOption = optionClient.TranfromaMsg(result,msgIn.token_ws);



                var content = JsonConvert.SerializeObject(requestOption);
                /* REGISTRO PAGO */
                try
                {
                    await optionClient.callRegistraPago(requestOption);
                    logger.Info("Registra Pago Option");
                }
                catch (Exception e)
                {
                    logger.Error("Error [Seguros Option]: " + e.Message);
                    throw new Exception("Error [Seguros Option]: " + e.Message);
                }

                /* CONFRIMA TRANSACCION  TBK */
                try
                {
                    logger.Info("Confirmado en TBK..." );
                    webpay.getMallNormalTransaction().acknowledgeTransaction(msgIn.token_ws);
                    logger.Error("Confirmado en TBK!");

                    log.AddLogPago(msgIn.token_ws, "", "", "", 0, DateTime.Now, 1, "");

                }
                catch (Exception e)
                {
                    logger.Error("Error [Trasnback]: " + e.Message);
                    throw new Exception("Error [Trasnback]: " + e.Message);
                }
               


                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                StringBuilder s = new StringBuilder();
                s.Append("<html>");
                s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                s.AppendFormat("<form name='form' action='{0}' method='post'>", result.urlRedirection);
                s.AppendFormat("<input type='hidden' name='token_ws' value='{0}' />", msgIn.token_ws);
                s.Append("</form></body></html>");
                response.Write(s.ToString());
                response.End();


            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
            finally
            {


            }
            return msgOut;
        }



        public void AddRegistroPago(string rut,string token, string ordenes,decimal monto, Vo.TransactionResultOut transactionResultOut, Object msgIn)
        {
          
            SqlCommand cmd = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionName);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "registaPago";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rut", rut);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@ordenes", ordenes);
                cmd.Parameters.AddWithValue("@monto", monto);
                if (transactionResultOut != null)
                {
                    cmd.Parameters.AddWithValue("@codautorizacion", transactionResultOut.transactionDetail[0].authorizationCode);
                    cmd.Parameters.AddWithValue("@fecha", transactionResultOut.transactionDate);

                    cmd.Parameters.AddWithValue("@tipoPago", transactionResultOut.transactionDetail[0].paymentTypeCode);
                    cmd.Parameters.AddWithValue("@cuotas", transactionResultOut.transactionDetail[0].sharesNumber);
                    cmd.Parameters.AddWithValue("@response", JsonConvert.SerializeObject(transactionResultOut));
                    cmd.Parameters.AddWithValue("@datosExtras", "");

                }
                else
                {
                    cmd.Parameters.AddWithValue("@codautorizacion", "");
                    cmd.Parameters.AddWithValue("@fecha", "");

                    cmd.Parameters.AddWithValue("@tipoPago", "");
                    cmd.Parameters.AddWithValue("@cuotas", "");
                    cmd.Parameters.AddWithValue("@response", "");
                    cmd.Parameters.AddWithValue("@datosExtras", JsonConvert.SerializeObject(msgIn));

                }
               
    
                conn.Open();
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              




            }
            catch (System.Data.SqlClient.SqlException e)
            {
                logger.Error(e.Message);
                throw new Exception("SQLException: " + e.Message);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception("Error SQL: " + e.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    try
                    {
                        cmd.Dispose();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
            }
        }


        public Vo.TransactionResultOut GetResponse(TransactionResultIn msgIn)
        {
            Vo.TransactionResultOut transactionResultOut = new TransactionResultOut();

            SqlCommand cmd = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionName);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "getResponse";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@token", msgIn.token_ws);

                cmd.Parameters.Add(new SqlParameter("@response", InnoDbTypeSQLServer.String(), -1));
                cmd.Parameters["@response"].Direction = ParameterDirection.Output;


                conn.Open();
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                var response = cmd.Parameters["@response"].Value.ToString();
                transactionResultOut = JsonConvert.DeserializeObject<TransactionResultOut>(response);
                return transactionResultOut;


            }
            catch (System.Data.SqlClient.SqlException e)
            {
                logger.Error(e.Message);
                throw new Exception("Error SQL: " + e.Message);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception("Error: " + e.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    try
                    {
                        cmd.Dispose();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
            }
        }

         public Token.Vo.DatosExtras GetDatosExtras(string token)
        {
            Token.Vo.DatosExtras datosExtrasOut = new Token.Vo.DatosExtras();

            SqlCommand cmd = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionName);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "getDatosExtras";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@token", token);

                cmd.Parameters.Add(new SqlParameter("@datosExtras", InnoDbTypeSQLServer.String(), -1));
                cmd.Parameters["@datosExtras"].Direction = ParameterDirection.Output;


                conn.Open();
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                var response = cmd.Parameters["@datosExtras"].Value.ToString();
                datosExtrasOut = JsonConvert.DeserializeObject<Token.Vo.DatosExtras>(response);
                return datosExtrasOut;


            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception("Error SQL: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    try
                    {
                        cmd.Dispose();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch (Exception ignored)
                    {
                    }
                }
            }
        }

        public string ResponseReazon(int codigo)
        {
            if (codigo == 0)
                return "Transacción aprobada.";
            else if (codigo == -1)
                return "Rechazo de transacción.";
            else if (codigo == -2)
                return "Transacción debe reintentarse.";
            else if (codigo == -3)
                return " Error en transacción.";
            else if (codigo == -4)
                return "Rechazo de transacción.";
            else if (codigo == -5)
                return "Rechazo por error de tasa.";
            else if (codigo == -6)
                return "Excede límite diario por transacción.";
            else if (codigo == -7)
                return "TExcede cupo máximo mensual.";
            else if (codigo == -8)
                return "Rubro no autorizado.";
            else return "";
        }

        [JsonObject]
        public class DetailResponseContent
        {
            [JsonProperty("amount")]
            public int amount { get; set; }

            [JsonProperty("authorizationCode")]
            public string authorizationCode { get; set; }

            [JsonProperty("responseCode")]
            public int responseCode { get; set; }

            [JsonProperty("sharesAmount")]
            public string sharesAmount { get; set; }

            [JsonProperty("buyOrder")]
            public string buyOrder { get; set; }

            [JsonProperty("commerceCode")]
            public string commerceCode { get; set; }

            [JsonProperty("paymentTypeCode")]
            public string paymentTypeCode { get; set; }

            [JsonProperty("sessionId")]
            public string sessionId { get; set; }

            [JsonProperty("sharesNumber")]
            public string sharesNumber { get; set; }

            [JsonProperty("items")]
            public List<PagosJson> items { get; set; }
        }

     
        [JsonObject]
        public class CardDetailJson
        {
            [JsonProperty("cardExpirationDate")]
            public string cardExpirationDate { get; set; }

            [JsonProperty("cardNumber")]
            public string cardNumber { get; set; }
        }

        [JsonObject]
        public class ResponseTransactionJson
        {
            [JsonProperty("vci")]
            public string vci { get; set; }

            [JsonProperty("detailOutput")]
            public List<DetailResponseContent> detailOutput { get; set; }

            [JsonProperty("buy_order")]
            public string buy_order { get; set; }

            [JsonProperty("session_id")]
            public string session_id { get; set; }

            [JsonProperty("cardDetail")]
            public CardDetailJson cardDetail { get; set; }

            [JsonProperty("buyOrder")]
            public string buyOrder { get; set; }

            [JsonProperty("accountingDate")]
            public string accountingDate { get; set; }

            [JsonProperty("transactionDate")]
            public DateTime transactionDate { get; set; }

            [JsonProperty("authorizationCode")]
            public string authorizationCode { get; set; }

            [JsonProperty("VCI")]
            public string VCI { get; set; }

            [JsonProperty("sessionId")]
            public string sessionId { get; set; }

            [JsonProperty("urlRedirection")]
            public string urlRedirection { get; set; }

            [JsonProperty("transactionDetail")]
            public List<DetailResponseContent> transactionDetail { get; set; }

        }

        [JsonObject]
        public class PagosJson
        {
            [JsonProperty("CodCirujia")]
            public string CodCirujia { get; set; }

            [JsonProperty("NombrePrestacion")]
            public string NombrePrestacion { get; set; }

            [JsonProperty("Monto")]
            public string Monto { get; set; }

            [JsonProperty("Ejercicio")]
            public string Ejercicio { get; set; }

            [JsonProperty("NroDocumento")]
            public string NroDocumento { get; set; }

            [JsonProperty("NroOrdenCompra")]
            public string NroOrdenCompra { get; set; }

            [JsonProperty("Sociedad")]
            public string Sociedad { get; set; }

            [JsonProperty("Posicion")]
            public string Posicion { get; set; }

            [JsonProperty("CodigoComercio")]
            public string CodigoComercio { get; set; }
        }
        [JsonObject]
        public class DatosExtraTransactionJson
        {
            [JsonProperty("vci")]
            public string vci { get; set; }

            [JsonProperty("Pagos")]
            public List<PagosJson> Pagos { get; set; }

            [JsonProperty("Nombre")]
            public string Nombre { get; set; }

            [JsonProperty("Rut")]
            public string Rut { get; set; }

            [JsonProperty("Posicion")]
            public string Posicion { get; set; }
        }

        public async Task<ResponseTransactionJson> getTransactionResultApi(string token)
        {
            ResponseTransactionJson transInfo = null;
            using (var client = new HttpClient())
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        string uri = System.Configuration.ConfigurationManager.AppSettings["UrlGetResponseNewTransbankApi"];

                        var task = httpClient.GetAsync(uri+token);
                        task.Wait();
                        var taskResponse = task.Result.Content.ReadAsAsync<Object>();
                        taskResponse.Wait();
                        dynamic resultado = taskResponse.Result;

                        transInfo = JsonConvert.DeserializeObject<ResponseTransactionJson>(resultado.ToString());

                        //transInfo = content;
                        Console.Write("");
                        //transInfo.accountingDate = resultado[0];
                        return transInfo;
                    }
                }
                catch (HttpRequestException e) {
                    logger.Info("[PagoDAO] Response nueva pasarela de pago: " + e.Message);
                    throw new Exception(e.Message);
                }
                catch (ArgumentNullException a)
                {
                    logger.Info("[PagoDAO] Response nueva pasarela de pago: " + a.Message);
                    throw new Exception(a.Message);
                
                }
            }
        }

        public async Task<DatosExtraTransactionJson> getDatosExtraTransactionApi(string token)
        {
            DatosExtraTransactionJson transInfo = null;
            using (var client = new HttpClient())
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        string uri = System.Configuration.ConfigurationManager.AppSettings["UrlGetContentNewTransbankApi"];
                        var task = httpClient.GetAsync(uri + token);
                        task.Wait();
                        var taskResponse = task.Result.Content.ReadAsAsync<Object>();
                        taskResponse.Wait();
                        dynamic resultado = taskResponse.Result;                      

                        transInfo = JsonConvert.DeserializeObject<DatosExtraTransactionJson>(resultado.ToString());

                        return transInfo;
                    }
                }
                catch (HttpRequestException e)
                {
                    logger.Info("[PagoDAO] Response nueva pasarela de pago: " + e.Message);
                    throw new Exception(e.Message);
                }
                catch (ArgumentNullException a)
                {
                    logger.Info("[PagoDAO] Response nueva pasarela de pago: " + a.Message);
                    throw new Exception(a.Message);

                }
            }
        }

    }
}