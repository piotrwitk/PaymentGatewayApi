using PaymentGateway.PaymentProcessors.Models;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    public interface IPaymentProcessor
    {
        Task<PaymentProcessorResponse> HandlePaymentRequest(PaymentProcessorRequest request);
    }
}
