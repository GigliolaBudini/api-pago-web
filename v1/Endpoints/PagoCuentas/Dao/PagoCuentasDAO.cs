/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

namespace API.EndPoints.PagoCuentas.Dao
{ 

	public class PagoCuentasDAO
    {

		private static PagoCuentasDAO singleton = null;
		private static string connectionName=null;
		private PagoCuentasDAO() {
		 connectionName=ConfigurationManager.ConnectionStrings["SQLServerConexionString"].ConnectionString; 
		}

		public static PagoCuentasDAO getInstance() {
			if (singleton == null) {
				singleton = new PagoCuentasDAO();
			}
			return singleton;
		}


	
		public Vo.GetCuentasOut consultaDocumentos(Vo.GetCuentasIn msgIn)  {
            Vo.GetCuentasOut msgOut = new Vo.GetCuentasOut();

     
            PagoEpisodioWebReference.SI_os_PagoEpisodioWebQueryService cliente = new PagoEpisodioWebReference.SI_os_PagoEpisodioWebQueryService();
            
            cliente.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Servicio.PagoWeb.Usuario"], ConfigurationManager.AppSettings["Servicio.PagoWeb.Password"]);

            PagoEpisodioWebReference.DT_Paciente request = new PagoEpisodioWebReference.DT_Paciente();
            request.rut = msgIn.Rut;
            request.episodio = "";// msgIn.Codigo;
            var response = cliente.consultaEpisodio(request);


            if (response.proceso.estado.TrimEnd() == "0")
            {
                var i = 0;
                if(response.pago!=null)
                foreach (var linea in response.pago)
                {
                    i++;
                    if(linea.tipoprestacion.Trim() =="2" )
                        {
                            if (msgOut.otrosServicios == null)
                                msgOut.otrosServicios = new List<Vo.OtrosServicios>();

                            Vo.OtrosServicios cuenta = new Vo.OtrosServicios();
                            cuenta.Codcirugia = linea.codcirugia;
                            cuenta.Id = i;
                            cuenta.Ejercicio = linea.ejercicio;
                            cuenta.Estadocuenta = linea.estadocuenta;
                            cuenta.Fechavencimiento = linea.fechavencimiento;
                            cuenta.Monto = linea.monto;
                            cuenta.Nomcirugia = linea.nomcirugia;
                            cuenta.Nrodocumento = linea.nrodocumento;
                            cuenta.Posicion = linea.posicion;
                            cuenta.Sociedad = linea.sociedad;
                            msgOut.Rut = linea.rutpaciente;
                            msgOut.Nombre = linea.nombrepaciente;
                            cuenta.DoctoBase64 = linea.doctoBase64;
                            msgOut.otrosServicios.Add(cuenta);

                        }
                        else
                        {
                            if(msgOut.cirugias==null)
                                msgOut.cirugias = new List<Vo.Cirugias>();
                           
                            // Modificación 5/7/2023 se le restan 20 días a la fecha de vencimiento 
                            string shortdate = substractDays(linea.fechavencimiento); 
                            Vo.Cirugias cuenta = new Vo.Cirugias();
                            cuenta.Codcirugia = linea.codcirugia;
                            cuenta.Id = i;
                            cuenta.Ejercicio = linea.ejercicio;
                            cuenta.Estadocuenta = linea.estadocuenta;
                            cuenta.Fechavencimiento = shortdate;
                            cuenta.Monto = linea.monto;
                            cuenta.Nomcirugia = linea.nomcirugia;
                            cuenta.Nrodocumento = linea.nrodocumento;
                            cuenta.Posicion = linea.posicion;
                            cuenta.Sociedad = linea.sociedad;
                            msgOut.Rut = linea.rutpaciente;
                            msgOut.Nombre = linea.nombrepaciente;
                            cuenta.DoctoBase64 = linea.doctoBase64;
                            msgOut.cirugias.Add(cuenta);
                        }

                    }
            }
            else
            {
                throw new Exception(response.proceso.mensaje);
            }


            return msgOut;
		}

        public string addCero(int num)
        {
            if (num < 10)
            {
                return "0" + num.ToString();
            }
            else
            {
                return num.ToString();
            }

        }

        public string substractDays(string date)
        {
            DateTime newdate;
            string year, month, day;
            if (date.IndexOf("/") != -1)
            {
                var arrDate = date.Split('/');

                year = arrDate[2];
                month = arrDate[1];
                day = arrDate[0];
            } 
            else
            {
                year = date.Substring(0, 4);
                month = date.Substring(4, 2);
                day = date.Substring(6, 2);
            }
            newdate = Convert.ToDateTime(year + "-" + month + "-" + day);
            newdate = newdate.AddDays(-20);
            return addCero(newdate.Day) + "/" + addCero(newdate.Month) + "/" + newdate.Year.ToString();


        }

	}
}