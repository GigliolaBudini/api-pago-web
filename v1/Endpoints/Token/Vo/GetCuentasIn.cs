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

namespace API.EndPoints.Token.Vo
{ 
	[DataContract]
	public class TokenIn
    {
        [DataMember]
        public List<Orden> Pagos
        {
            get;
            set;
        } 

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
        public string Correo
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Orden
    {
        [DataMember]
        public string CodCirujia
        {
            get;
            set;
        }

        [DataMember]
        public string NroDocumento
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
        public decimal Monto
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
        public string NombrePrestacion
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
        public string TipoServicio
        {
            get;
            set;
        }

    }

}