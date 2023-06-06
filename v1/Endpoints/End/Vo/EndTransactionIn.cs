
using System;
using System.Runtime.Serialization;

namespace API.EndPoints.End.Vo
{ 
	[DataContract]
	public class EndTransactionIn
    {


        [DataMember]
        public string token_ws
        {
            get;
            set;
        }


        [DataMember]
        public string TBK_ID_SESION
        {
            get;
            set;
        }



        [DataMember]
        public string TBK_ORDEN_COMPRA
        {
            get;
            set;
        }


        [DataMember]
        public string TBK_TOKEN
        {
            get;
            set;
        }
    }
}
