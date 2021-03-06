﻿using Newtonsoft.Json;
using System;

namespace PaymentGateway.WebApi.Models.v1
{
    /// <summary>
    /// Response produced by either payment request or when retrieving details 
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Id of the merchant as registered in the system
        /// </summary>
        public string MerchantId { get; set; }
        
        /// <summary>
        /// Mertchant's own reference number used in payment request
        /// </summary>
        public string MerchantReferenceNumber { get; set; }


        /// <summary>
        /// Which currency was used for payment operation, <see cref="https://en.wikipedia.org/wiki/ISO_4217"/>        
        /// </summary>
        public string PaymentCurrency { get; set; }
        
        /// <summary>
        /// The amount of payment, in <see cref="PaymentCurrency"/>
        /// </summary>
        public double PaymentAmount { get; set; }

        /// <summary>
        /// Server's timestamp of registering the original payment request
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// A truncated number of the card used in transaction
        /// </summary>
        public string TruncatedCardNumber { get; set; }

        /// <summary>
        /// Indicating if acquiring bank accepted the request
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// In case of failue, a description explaining why payment was not processed
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FailureReason { get; set; }
    }
}
