
using System;
using System.Runtime.Serialization;

namespace API.EndPoints.End.Vo
{ 
	[DataContract]
	public class EndTransactionOut
    {
		
        [DataMember]
        public string buyOrder
        {
            get;
            set;
        }

        [DataMember]
        public string accoutingDate
        {
            get;
            set;
        }


        [DataMember]
        public string sessionId
        {
            get;
            set;
        }


        [DataMember]
        public string transactionDate
        {
            get;
            set;
        }


        [DataMember]
        public string VCI
        {
            get;
            set;
        }

        [DataMember]
        public string urlRedirection
        {
            get;
            set;
        }


        [DataMember]
        public string cardDetailsTns
        {
            get;
            set;
        }



    }
}
