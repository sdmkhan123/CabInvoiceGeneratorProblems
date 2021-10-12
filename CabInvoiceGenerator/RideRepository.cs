using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// class for Ride List
    /// </summary>
    class RideRepository
    {
        /// <summary>
        /// Dictionary to store UserID and Rides into list
        /// </summary>
        Dictionary<string, List<Ride>> userRides = null;

        /// <summary>
        /// Constructor to create dictionary
        /// </summary>
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }

        /// <summary>
        /// Add Ride list to specified UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                if (!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRides.Add(userId, list);
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
            }
        }

        /// <summary>
        /// Get Ride list as an array for specified UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Ride[] getRides(string userId)
        {

            try
            {
                return this.userRides[userId].ToArray();
            }
            catch (Exception)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid user ID");
            }
        }
    }
}