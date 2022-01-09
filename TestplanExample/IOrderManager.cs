namespace TestplanLib
{
    /// <summary>
    /// Manages orders placed by a given user.
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Trys to place an order containing the items matching the given item ID's.
        /// </summary>
        /// <param name="itemIDs">The items to put in the order.</param>
        /// <returns>A string representing the order id (GUID formated)</returns>
        /// <exception cref="ArgumentException">At least one item ID needs to be specified.</exception>
        /// <exception cref="ArgumentNullException">Item ID can not be null nor empty.</exception>
        /// <exception cref="KeyNotFoundException">Item ID does not exist.</exception>
        public string PlaceOrder(string[] itemIDs);
    }
}