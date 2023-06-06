
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace API.EndPoints.Pago.Vo
{ 
	[DataContract]
	public class TransactionResultOut
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
        public DateTime transactionDate
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
        public CardDetail cardDetail
        {
            get;
            set;
        }

        [DataMember]
        public List<TransactionDetail> transactionDetail
        {
            get;
            set;
        }

    }

    [DataContract]
    public class CardDetail
    {
        [DataMember]
        public string cardNumber
        {
            get;
            set;
        }

        [DataMember]
        public string cardExpirationDate
        {
            get;
            set;
        }

  
    }

    [DataContract]
    public class TransactionDetail
    {
     /*   [DataMember]
        public bool sharesAmountSpecified
        {
            get;
            set;
        }
        */
        [DataMember]
        public decimal sharesAmount
        {
            get;
            set;
        }
      
        [DataMember]
        public string authorizationCode
        {
            get;
            set;
        }
        [DataMember]
        public decimal sharesNumber
        {
            get;
            set;
        }
        [DataMember]
        public decimal amount
        {
            get;
            set;
        }
        [DataMember]
        public string commerceCode
        {
            get;
            set;
        }
        [DataMember]
        public string buyOrder
        {
            get;
            set;
        }
        [DataMember]
        public string paymentTypeCode
        {
            get;
            set;
        }
        [DataMember]
        public int responseCode
        {
            get;
            set;
        }
        [DataMember]
        public string responseReazon
        {
            get;
            set;
        }

        [DataMember]
        public List<Token.Vo.Episodios> items
        {
            get;
            set;
        }

    }

    
 


}
