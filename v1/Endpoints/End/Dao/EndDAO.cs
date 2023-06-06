/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using API.EndPoints.End.Vo;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Webpay.Transbank.Library;
using Webpay.Transbank.Library.Wsdl.Normal;

namespace API.EndPoints.End.Dao
{ 

	public class EndDAO {

		private static EndDAO singleton = null;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Configuration configuration = null;

        private EndDAO() {
            configuration = new Configuration();
            configuration.Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            configuration.CommerceCode = System.Configuration.ConfigurationManager.AppSettings["CommerceCode"];
            configuration.PublicCert = System.Configuration.ConfigurationManager.AppSettings["PublicCert"];
            configuration.WebpayCert = System.Configuration.ConfigurationManager.AppSettings["WebpayCert"];
            configuration.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

        }

        public static EndDAO getInstance() {
			if (singleton == null) {
				singleton = new EndDAO();
			}
			return singleton;
		}


		
		public EndTransactionOut ConsultaEstadoTransaccion(EndTransactionIn msgIn)  {
            EndTransactionOut msgOut = new EndTransactionOut();
            try {
                Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);
                logger.Info("[ConsultaEstadoTransaccion] Token: " + msgIn.token_ws);
                string token;
                if (msgIn.token_ws != null)
                    token = msgIn.token_ws;
                else
                {
                    
                    token = msgIn.TBK_TOKEN;
                   // var result = webpay.getMallNormalTransaction().getTransactionResult(token);
                    Pago.Vo.TransactionResultOut resultTBK = new Pago.Vo.TransactionResultOut();
                    resultTBK.accoutingDate = new DateTime().ToString();
                    resultTBK.buyOrder = msgIn.TBK_ORDEN_COMPRA;
                    resultTBK.cardDetail = new Pago.Vo.CardDetail();

                    resultTBK.sessionId = msgIn.TBK_ID_SESION;
                    resultTBK.transactionDate = DateTime.Now;
                    resultTBK.transactionDetail = new List<Pago.Vo.TransactionDetail>();
                    var detalle = new Pago.Vo.TransactionDetail();
                    detalle.responseCode = -1;
                    resultTBK.transactionDetail.Add(detalle);

                    Pago.Dao.PagoDAO endDao = Pago.Dao.PagoDAO.getInstance();
                    endDao.AddRegistroPago("", token, "", 0, resultTBK, null);

                    Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();

                    log.AddLogPago(token, "", "", resultTBK.transactionDetail[0].authorizationCode, resultTBK.transactionDetail[0].amount,  DateTime.Now, 0, ResponseReazon(resultTBK.transactionDetail[0].responseCode));

                }

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                StringBuilder s = new StringBuilder();
                s.Append("<html>");
                s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                s.AppendFormat("<form name='form' action='{0}' method='GET'>", System.Configuration.ConfigurationManager.AppSettings["UrlConfirmacionPagoWeb"]  + token);             
                s.Append("</form></body></html>");
                response.Write(s.ToString());
                response.End();

            } catch (Exception e) {			        
                    throw new Exception("Error: " + e.Message);
			} finally {
				
				
			}
			return msgOut;
		}

        public EndTransactionOut ConsultaEstadoTransaccionExterna(EndTransactionIn msgIn)
        {
            EndTransactionOut msgOut = new EndTransactionOut();
            try
            {
                Webpay.Transbank.Library.Webpay webpay = new Webpay.Transbank.Library.Webpay(configuration);
                logger.Info("[ConsultaEstadoTransaccionExterna] Token: " + msgIn.token_ws);
                string token;
                if (msgIn.token_ws != null)
                    token = msgIn.token_ws;
                else
                {

                    token = msgIn.TBK_TOKEN;
                    // var result = webpay.getMallNormalTransaction().getTransactionResult(token);
                    Pago.Vo.TransactionResultOut resultTBK = new Pago.Vo.TransactionResultOut();
                    resultTBK.accoutingDate = new DateTime().ToString();
                    resultTBK.buyOrder = msgIn.TBK_ORDEN_COMPRA;
                    resultTBK.cardDetail = new Pago.Vo.CardDetail();

                    resultTBK.sessionId = msgIn.TBK_ID_SESION;
                    resultTBK.transactionDate = DateTime.Now;
                    resultTBK.transactionDetail = new List<Pago.Vo.TransactionDetail>();
                    var detalle = new Pago.Vo.TransactionDetail();
                    detalle.responseCode = -1;
                    resultTBK.transactionDetail.Add(detalle);

                    Pago.Dao.PagoDAO endDao = Pago.Dao.PagoDAO.getInstance();
                    endDao.AddRegistroPago("", token, "", 0, resultTBK, null);

                    Log.Dao.LogDAO log = Log.Dao.LogDAO.getInstance();

                    log.AddLogPago(token, "", "", resultTBK.transactionDetail[0].authorizationCode, resultTBK.transactionDetail[0].amount,  DateTime.Now, 0, ResponseReazon(resultTBK.transactionDetail[0].responseCode));


                }

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                StringBuilder s = new StringBuilder();
                s.Append("<html>");
                s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                s.AppendFormat("<form name='form' action='{0}' method='GET'>", System.Configuration.ConfigurationManager.AppSettings["UrlConfirmacionPagoWebSeguro"] + msgIn.token_ws);
                s.Append("</form></body></html>");
                response.Write(s.ToString());
                response.End();


            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }
            finally
            {


            }
            return msgOut;
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
    }
}