using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Endpoints.Log.Vo
{
    public class LogRequest
    {
        public string rut;
        public string ordenes;
        public DateTime fecha_init;
        public DateTime fecha_fin;
    }
}