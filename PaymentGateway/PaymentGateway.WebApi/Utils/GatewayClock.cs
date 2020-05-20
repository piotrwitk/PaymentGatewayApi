using System;

namespace PaymentGateway.WebApi.Utils
{
    public class GatewayClock : IGatewayClock
    {
        public DateTimeOffset GetCurrentUtcTimestamp()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
