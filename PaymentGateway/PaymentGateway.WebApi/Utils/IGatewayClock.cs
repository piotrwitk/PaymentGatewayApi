using System;

namespace PaymentGateway.WebApi.Utils
{
    public interface IGatewayClock
    {
        DateTimeOffset GetCurrentUtcTimestamp();
    }
}
