﻿using PaymentGateway.DAL;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.WebApi.Tests
{
    public class InMemoryRepository : IRepository
    {
        private List<GatewayPaymentRequest> requests = new List<GatewayPaymentRequest>();
        private List<GatewayResponse> responses = new List<GatewayResponse>();

        public Task<GatewayPaymentRequest> RegisterPaymentRequest(GatewayPaymentRequest request)
        {
            request.GatewayId = Guid.NewGuid().ToString();
            requests.Add(request);
            return Task.FromResult(request);
        }

        public Task RegisterResponse(GatewayResponse response)
        {
            responses.Add(response);
            return Task.CompletedTask;
        }

        public Task<GatewayResponse> RetrieveDetails(string merchantId, string merchantRef)
        {
            return Task.FromResult(responses.FirstOrDefault(r => r.MerchantId == merchantId && r.MerchantReferenceNumber == merchantRef));
        }

        public Task<GatewayPaymentRequest> RetrieveRequest(string gatewayId)
        {
            return Task.FromResult(requests.FirstOrDefault(r => r.GatewayId == gatewayId));
        }
    }
}
