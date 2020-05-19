using System;

namespace PaymentGateway.Models
{
    public class BaseRequest
    {
        /// <summary>
        /// The ID under which a calling merchant is registered in our system
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Merchant's own reference number for this call / transaction.
        /// This should be used to retrieve payment details on merchant's request
        /// </summary>        
        public string MerchantReferenceNumber { get; set; }

        /// <summary>
        /// Internal server timestamp of when the request was received
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }

    }
}
