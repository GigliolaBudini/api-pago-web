/***************************************************************
*                                                              *
*              C�digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace API.EndPoints.PagoCuentas.Vo
{
    [DataContract]
    public class GetCuentasOut
    {
        [DataMember]
        public string Rut
        {
            get;
            set;
        }
        [DataMember]
        public string Nombre
        {
            get;
            set;
        }

        [DataMember]
        public List<Cirugias> cirugias
        {
            get;
            set;
        }

        [DataMember]
        public List<OtrosServicios> otrosServicios
        {
            get;
            set;
        }


    }

    [DataContract]
    public class OtrosServicios
    {

        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public string Codcirugia
        {
            get;
            set;
        }
        [DataMember]
        public string Nomcirugia
        {
            get;
            set;
        }
        [DataMember]
        public string Monto
        {
            get;
            set;
        }

        [DataMember]
        public string Fechavencimiento
        {
            get;
            set;
        }
        [DataMember]
        public string Sociedad
        {
            get;
            set;
        }
        [DataMember]
        public string Nrodocumento
        {
            get;
            set;
        }
        [DataMember]
        public string Ejercicio
        {
            get;
            set;
        }
        [DataMember]
        public string Posicion
        {
            get;
            set;
        }

        [DataMember]
        public string Estadocuenta
        {
            get;
            set;
        }

        [DataMember]
        public string DoctoBase64
        {
            get;
            set;
        }

    }


    [DataContract]
    public class Cirugias
    {

        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public string Codcirugia
        {
            get;
            set;
        }
        [DataMember]
        public string Nomcirugia
        {
            get;
            set;
        }
        [DataMember]
        public string Monto
        {
            get;
            set;
        }

        [DataMember]
        public string Fechavencimiento
        {
            get;
            set;
        }
        [DataMember]
        public string Sociedad
        {
            get;
            set;
        }
        [DataMember]
        public string Nrodocumento
        {
            get;
            set;
        }
        [DataMember]
        public string Ejercicio
        {
            get;
            set;
        }
        [DataMember]
        public string Posicion
        {
            get;
            set;
        }

        [DataMember]
        public string Estadocuenta
        {
            get;
            set;
        }

        [DataMember]
        public string DoctoBase64
        {
            get;
            set; 
        }


    }
}
