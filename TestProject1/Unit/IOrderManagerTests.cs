using System;
using System.Collections.Generic;
using TestplanLib;
using Xunit;

namespace TestplanTests.Unit
{
    public class IOrderManagerTests
    {

        [Fact]
        public void PlaceOrder_implementationAbidesSpecification()
        {
            //arange
            IOrderManager manager = new OrderManager();
            string[] emptyItemArray = { };
            string[] emptyItemId = { string.Empty };
            string[] nullItemId = { null };
            string[] nonexistingValidItemId = { "00000000-0000-0000-0000-000000000001" };
            string[] existingValidItemId = { "00000000-0000-0000-0000-000000000000" };
            string guidPattern = @"([\d\w]{8})-([\d\w]{4})-([\d\w]{4})-([\d\w]{4})-([\d\w]{12})";
            string returnedOrderId;

            //act
            Action tryForceArgumentExceptionWithEmptyArray = () => manager.PlaceOrder(emptyItemArray);
            Action tryForceArgumentNullExceptionWithEmtpyString = () => manager.PlaceOrder(emptyItemId);
            Action tryForceArgumentNullExceptionWithNullString = () => manager.PlaceOrder(nullItemId);
            Action tryForceKeyNotFoundExceptionWithNonExisitingItemId = () => manager.PlaceOrder(nonexistingValidItemId);
            returnedOrderId = manager.PlaceOrder(existingValidItemId);

            //assert
            Assert.Throws<ArgumentException>(tryForceArgumentExceptionWithEmptyArray);
            Assert.Throws<ArgumentNullException>(tryForceArgumentNullExceptionWithEmtpyString);
            Assert.Throws<ArgumentNullException>(tryForceArgumentNullExceptionWithNullString);
            Assert.Throws<KeyNotFoundException>(tryForceKeyNotFoundExceptionWithNonExisitingItemId);
            Assert.Matches(guidPattern, returnedOrderId);
        }
    }
}
