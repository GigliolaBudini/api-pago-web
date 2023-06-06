
using System;
using System.Runtime.Serialization;

namespace API.EndPoints.Pago.Vo
{ 
	[DataContract]
	public class TransactionResultIn
    {


        [DataMember]
        public string token_ws
        {
            get;
            set;
        }

    }
}
