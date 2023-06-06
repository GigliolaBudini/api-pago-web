using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace API.Endpoints.Log.Vo
{
    [DataContract]
    public class LogResponse
    {
        public LogResponse() { }

        [DataMember]
        public string token
        {
            get;
            set;
        }
        [DataMember]
        public string rut
        {
            get;
            set;
        }
        [DataMember]
        public string ordenes
        {
            get;
            set;
        }
        [DataMember]
        public string codautorizacion
        {
            get;
            set;
        }
        [DataMember]
        public string fecha
        {
            get;
            set;
        }
        [DataMember]
        public decimal monto
        {
            get;
            set;
        }
        [DataMember]
        public string confirmado
        {
            get;
            set;
        }
        [DataMember]
        public string resultado
        {
            get;
            set;
        }



    }
    [DataContract]
    public class LogOut
    {
        // public void LogResponse();
        [DataMember]
        public LogResponse[] logs
        {
            get;
            set;
        }




    }


}