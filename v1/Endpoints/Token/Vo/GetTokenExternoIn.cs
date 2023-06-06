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
	public class TokenExteroIn
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
        public decimal Monto
        {
            get;
            set;
        }

        [DataMember]
        public string NroOrdenCompra
        {
            get;
            set;
        }

        [DataMember]
        public List<Item> Items
        {
            get;
            set;
        }

    }

    public class Item
    {

             

        [DataMember]
        public string Codigo
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
        public string Nombre
        {
            get;
            set;
        }

    

    }


}