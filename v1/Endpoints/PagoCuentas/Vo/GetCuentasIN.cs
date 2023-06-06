/***************************************************************
*                                                              *
*              C�digo Generado Automaticamente                 *
*                Generado Con WitiCode v1.0.0                  *
*                       Por WiTI SpA.                          *
*                     http://www.witi.cl                       *
*                                                              *
***************************************************************/

using System;
using System.Runtime.Serialization;

namespace API.EndPoints.PagoCuentas.Vo
{ 
	[DataContract]
	public class GetCuentasIn
    {

		[DataMember]
		public string Rut
		{
			get; 
			set; 
		}


        [DataMember]
        public string Codigo
        {
            get;
            set;
        }
    }
}
