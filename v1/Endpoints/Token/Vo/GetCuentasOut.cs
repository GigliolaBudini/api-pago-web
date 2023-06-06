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

namespace API.EndPoints.Token.Vo
{ 
	[DataContract]
	public class TokenOut
    {


        [DataMember]
        public string token
        {
            get;
            set;
        }
        [DataMember]
        public string url
        {
            get;
            set;
        }
    }


}
