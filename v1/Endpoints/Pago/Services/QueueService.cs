using API.EndPoints.Pago;
using API.EndPoints.PagoCuentas;
using API.EndPoints.PagoCuentas.Vo;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace API.Endpoints.PagoCuentas.Services
{
    public class QueueService : BackgroundService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var accessKey = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionManager"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accessKey);

            // Create the queue client
            var queueName = "sap-requests";
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            
            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("sap-request");

            // Create the queue if it doesn't already exist
            await queue.CreateIfNotExistsAsync();
            var intents = 0;
            var delayTime = 30;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var queueMessage = await queue.GetMessageAsync();

                    if (queueMessage != null)
                    {   
                        var mes = JsonConvert.DeserializeObject<List<jsonQueue>>(queueMessage.AsString);
                        var message = mes[0];
                        //logger.Info("Queue Message : "+ queueMessage.AsString);

                        //Consulta pagos pendientes por rut 
                        GetCuentasIn msgIn = new GetCuentasIn();
                        msgIn.Codigo = "-1";
                        msgIn.Rut = message.rut;
                        var token = message.token;
                        EndPoints.PagoCuentas.Dao.PagoCuentasDAO dao = EndPoints.PagoCuentas.Dao.PagoCuentasDAO.getInstance();
                        var res = dao.consultaDocumentos(msgIn);
                        
                        var pagos = message.data;
                        var mens = "";
                        // Validar si exiten items pendientes de pago iguales a los items de el mensaje de la cola 
                        List<RegistraPagoWebReference.DT_TransacWebTransaccion> aPagar = new List<RegistraPagoWebReference.DT_TransacWebTransaccion>();
                        foreach (var pago in pagos)
                        {
                            if(res.otrosServicios != null)
                            {
                                foreach (var otroServ in res.otrosServicios)
                                {
                                    if (otroServ.Nrodocumento == pago.nrodocumento)
                                    {
                                        aPagar.Add(pago);
                                        mens += JsonConvert.SerializeObject(pago) + ",";
                                    }
                                    // Descomentar para pruebas
                                    //mens += JsonConvert.SerializeObject(pago) + ",";
                                    //aPagar.Add(pago);
                                }
                            }
                            if(res.cirugias != null)
                            {
                                foreach (var cirugia in res.cirugias)
                                {
                                    if (cirugia.Nrodocumento == pago.nrodocumento)
                                    {
                                        mens += JsonConvert.SerializeObject(pago) + ",";
                                        aPagar.Add(pago);
                                    }
                                }
                            }
                        }
                        intents++;
                        // Se envian a sap los pagos pendientes encontrados
                        if (intents > 3) {
                            try {
                                EndPoints.Pago.Dao.PagoDAO pago = EndPoints.Pago.Dao.PagoDAO.getInstance();
                                pago.Emailclient(intents,"["+ mens+"]",token);
                                await queue.DeleteMessageAsync(queueMessage.Id, queueMessage.PopReceipt);
                                intents = 0;
                                delayTime = 30;
                                aPagar.Clear();
                            }
                            catch(Exception e)
                            {
                                logger.Info("[QueueServiceProcess] Error ocurrido al enviar correo: " + intents);
                            }
                        }
                        // Se envian a sap los pagos pendientes encontrados
                        if (aPagar.Count() > 0)
                        {
                            /* registra pago SAP */
                            RegistraPagoWebReference.SI_os_TransaccionSAPCreateService client = new RegistraPagoWebReference.SI_os_TransaccionSAPCreateService();
                            client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Servicio.PagoWeb.Usuario"], System.Configuration.ConfigurationManager.AppSettings["Servicio.PagoWeb.Password"]);

                            logger.Info("[QueueServiceProcess] Envio pago a SAP intento nro : "+intents);
                            logger.Info("[QueueServiceProcess] Data: " + "[" + mens + "]");
                            try
                            {
                                //Envio de Mensaje a SAP 
                                var responseWS = client.enviaPago(aPagar.ToArray());

                                //logger.Info("Registrado en SAP");
                                
                                delayTime = delayTime * 2;
                            }
                            catch (Exception e)
                            {
                                logger.Error("[QueueServiceProcess] Error SAP : " + e.Message);
                                throw new Exception("Error SAP: " + e.Message);
                                delayTime = delayTime * 2;
                            }

                            
                        }
                        // Se elimina el mensaje de la cola si no se encuentran pagos pendientes
                        else
                        {
                            // Se elimina el mensaje de la cola
                            await queue.DeleteMessageAsync(queueMessage.Id, queueMessage.PopReceipt);
                            intents = 0;
                            delayTime = 30;
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(delayTime));
                }
                catch(Exception e)
                {

                    //Console.WriteLine("Error queue " + e.Message);
                    logger.Error("[QueueServiceProcess] Error queue service: " + e.Message);
                  
                    throw new Exception("[QueueServiceProcess] Error queue service: " + e.Message);
                }


            }

        }
    }
    [JsonObject]
    public class jsonQueue
    {
        [JsonProperty("rut")]
        public string rut { get; set; }
        [JsonProperty("data")]
        public List<RegistraPagoWebReference.DT_TransacWebTransaccion> data { get; set; }
        [JsonProperty("token")]
        public string token { get; set; }

    }
}