using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// InvoiceGenerator class generator Invoice
    /// </summary>
    public class InvoiceGenerator
    {
        //variables
        RideType rideType;
        private RideRepository rideRepository;
        //constant
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;
        /// <summary>
        /// Constructor to create RideRepository Instance
        /// </summary>
        /// <param name="rideType"></param>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                //if Ride Type is Premium then Rate set as premium else Normal
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }

            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
            }
        }
        /// <summary>
        /// Calcuating the fare
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoiceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid time");

                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }

        /// <summary>
        /// Calculate total fare and Generating Summary of Multiple Ride
        /// </summary>
        /// <param name="rides"></param>
        /// <returns></returns>
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                //Clculate total fare for all rides
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);

                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "rides are null");
                }

            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        /// <summary>
        /// Add ride for UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        public void ADDRides(string userId, Ride[] rides)
        {
            try
            {
                //Adding Ride To the Specified User
                rideRepository.AddRide(userId, rides);
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "rides are null");
                }
            }
        }
        public InvoiceSummary CalculateFare(Ride[] rides, int numOfRides, double averageFarePerRide)
        {
            double totalFare = 0;
            numOfRides = 0;
            averageFarePerRide = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
                averageFarePerRide = totalFare / numOfRides;
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare, averageFarePerRide);
        }
        /// <summary>
        /// To Get Summary By user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public InvoiceSummary GetInvoiceSummary(String userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.getRides(userId));
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid user id");
            }
        }
    }
}