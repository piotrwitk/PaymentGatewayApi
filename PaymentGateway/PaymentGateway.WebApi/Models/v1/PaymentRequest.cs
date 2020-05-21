using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.WebApi.Models.v1
{
    /// <summary>
    /// Defines a payload needed to process a payment request
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Merchant's own reference number / transaction Id. Must be unique per each request
        /// </summary>
        [Required]
        public string MerchantReferenceNumber { get; set; }

        /// <summary>
        /// Long number as appearing on the card
        /// </summary>
        [Required]
        public string LongCardNumber { get; set; }

        /// <summary>
        /// Name on the card
        /// </summary>
        [Required]
        public string NameOnCard { get; set; }
        
        /// <summary>
        /// Optional. From when a card used in a payment is valid in form of MM/YYYY
        /// </summary>
        public string ValidFrom { get; set; }


        /// <summary>
        /// To when a card used in a payment is valid in form of MM/YYYY
        /// </summary>
        [Required]
        public string ValidTo { get; set; }

        /// <summary>
        /// CVV number in the back of a card (for 4 digit in from in case of Amex)
        /// </summary>
        [Required]
        public int CVV { get; set; }

        /// <summary>
        /// Which currency is used to perform payment operation, <see cref="https://en.wikipedia.org/wiki/ISO_4217"/>
        /// </summary>
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Amount of the payment to be processed in <seealso cref="Currency"/>
        /// </summary>
        [Required]
        public double Amount { get; set; }
    }
}
