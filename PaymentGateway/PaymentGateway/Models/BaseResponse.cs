namespace PaymentGateway.Models
{
    public class BaseResponse
    {
        /// <summary>
        /// Defines if a corresponding request was a success
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// In case of failing request this field contains explanation what caused the failure
        /// </summary>
        public string FailureReason { get; set; }
    }
}
