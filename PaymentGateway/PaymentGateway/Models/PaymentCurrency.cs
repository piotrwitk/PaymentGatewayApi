namespace PaymentGateway.Models
{
    public class PaymentCurrency
    {
        /// <summary>        
        /// Currency symbol expressed in ISO 4217
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/ISO_4217"/>
        public string CurrencySymbol { get; set;}
                
        public double CurrencyAmount { get; set; }
    }
}
