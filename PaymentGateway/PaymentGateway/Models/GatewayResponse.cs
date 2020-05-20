using System;

namespace PaymentGateway.Models
{
    public class GatewayResponse : BaseResponse
    {
        public string GatewayId { get; set; }

        public string MerchantId { get; set; }

        public string MerchantReferenceNumber { get; set; }
        
        public string PaymentCurrency { get; set; }

        public double PaymentAmount { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string TruncatedCardNumber { get; set; }
    }
}
