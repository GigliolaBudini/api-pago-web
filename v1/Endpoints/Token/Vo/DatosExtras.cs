using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace API.EndPoints.Token.Vo
{
    [DataContract]
    public class DatosExtras
    {

        [DataMember]
        public List<Episodios> Pagos
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


    public class Episodios
    {


        [DataMember]
        public string CodCirujia
        {
            get;
            set;
        }
         
        [DataMember] 
        public string CodigoComercio
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