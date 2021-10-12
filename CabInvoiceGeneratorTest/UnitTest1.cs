using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    /// <summary>
    /// class for test cases
    /// </summary>
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        /// <summary>
        /// Test cases for checking Calculate fare function
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;
            //calculate fare
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;
            Assert.AreEqual(expected, fare);
        }
        /// <summary>
        /// Test cases for multiple riding
        /// </summary>
        [Test]
        public void GiveMultipleRidesShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            Assert.AreEqual(expectedSummary, summary);
        }
        /// <summary>
        /// Test cases for checking calculate average fare
        /// </summary>
        [Test]
        public void GiveMultipleRidesShouldReturnAverageInvoiceSummary()
        {
            //Creating Instance of InvoiceGenerator For Normal Rides
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, 15);
            Assert.AreEqual(expectedSummary, summary);
        }
        [Test]
        public void GivenUserId_WhenInvoivceService_ShouldReturnInvoice()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            invoiceGenerator.ADDRides("101", rides);
            InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary("101");
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0, "101");
            Assert.AreEqual(expectedSummary, summary);
        }
    }
}