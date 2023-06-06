/***************************************************************
*                                                              *
*              C??digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using API.Endpoints.Log.Vo;
using Newtonsoft.Json;
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

namespace API.EndPoints.Log.Dao
{

    public class LogDAO
    {

        private static LogDAO singleton = null;

        private static string connectionName = null;



        private LogDAO()
        {

            connectionName = System.Configuration.ConfigurationManager.ConnectionStrings["SQLServerConexionString"].ConnectionString;

        }

        public static LogDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LogDAO();
            }
            return singleton;
        }
        
        public void AddLogPago(string @token, string @rut, string ordenes, string codautorizacion, decimal monto, DateTime fecha , int sap, string resultado )
        {

            SqlCommand cmd = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionName);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "registra_log";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@rut", rut);
                cmd.Parameters.AddWithValue("@ordenes", ordenes);
                cmd.Parameters.AddWithValue("@codautorizacion", codautorizacion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("monto", monto);
                cmd.Parameters.AddWithValue("@pagado_sap", sap);
                cmd.Parameters.AddWithValue("@resultado", resultado);
              


                conn.Open();
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);





            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception("SQLException: " + e.Message);
            }
            catch (Exception e)
            {
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

        public LogOut getLog(string @rut, string @ordenes, DateTime fecha_init, DateTime fecha_fin)
        {
            SqlCommand cmd = null;
            SqlConnection conn = null;
            //string fechaI = Convert.ToString(fecha_init);
            //string FechaF = Convert.ToString(fecha_fin);


            //if (fecha_init.Equals(String.Empty))
            //    fecha_init = Convert.ToString(DateTime.Now);

            //if (fecha_fin.Equals(String.Empty))
            //    fecha_fin = Convert.ToString(DateTime.Now);

            try
            {
                conn = new SqlConnection(connectionName);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "getLog";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rut", rut);
                cmd.Parameters.AddWithValue("@ordenes", ordenes);
                cmd.Parameters.AddWithValue("@fecha_ini", fecha_init);
                cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);

                conn.Open();
                SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                LogOut _out = new LogOut();

                List<Endpoints.Log.Vo.LogResponse> cursor = new List<Endpoints.Log.Vo.LogResponse>();
                while (rs.Read())
                {
                    Endpoints.Log.Vo.LogResponse c1 = new Endpoints.Log.Vo.LogResponse();
                    c1.token = InnoHelperSQLServer.GetStringNullCheck(rs, 0);
                    c1.rut = InnoHelperSQLServer.GetStringNullCheck(rs, 1);
                    c1.ordenes = InnoHelperSQLServer.GetStringNullCheck(rs, 2);
                    c1.monto = InnoHelperSQLServer.GetDecimalNullCheck(rs, 3);
                    c1.fecha = InnoHelperSQLServer.GetStringNullCheck(rs, 4);
                    c1.confirmado = InnoHelperSQLServer.GetStringNullCheck(rs, 5);
                    c1.resultado = InnoHelperSQLServer.GetStringNullCheck(rs, 6);
                    c1.codautorizacion = InnoHelperSQLServer.GetStringNullCheck(rs, 7);
                    cursor.Add(c1);
                }
                _out.logs = cursor.ToArray();
                return _out;


                }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception("SQLException: " + e.Message);
            }
            catch (Exception e)
            {
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
    }
}