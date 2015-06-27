using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Stripe.Tests
{
    public class RefundTests
    {
        private StripeClient _client;

        public RefundTests()
        {
            _client = new StripeClient(Constants.ApiKey);
        }
        
        public void Create_Refund()
        {

        }
    }
}
