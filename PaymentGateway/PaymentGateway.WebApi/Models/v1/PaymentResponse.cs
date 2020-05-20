using Newtonsoft.Json;
using System;

namespace PaymentGateway.WebApi.Models.v1
{
    public class PaymentResponse
    {
        public string MerchantId { get; set; }
        
        public string MerchantReferenceNumber { get; set; }

        public string PaymentCurrency { get; set; }
        
        public double PaymentAmount { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string TruncatedCardNumber { get; set; }

        public bool IsSuccess { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FailureReason { get; set; }
    }
}
