namespace BankingSystem.Models.BankManagement
{
    public class ExchangeRates
    {
        /// <summary>
        /// Gets or sets an exchange rate identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a bank identifier to which exchange rates are related to.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets a USD purchase rate.
        /// </summary>
        public double USDPurchase { get; set; }

        /// <summary>
        /// Gets or sets a USD sale rate.
        /// </summary>
        public double USDSale { get; set; }

        /// <summary>
        /// Gets or sets a EUR purchase rate.
        /// </summary>
        public double EURPurchase { get; set; }

        /// <summary>
        /// Gets or sets a EUR sale rate.
        /// </summary>
        public double EURSale { get; set; }

        /// <summary>
        /// Gets or sets a bank to which exchange rates are related to.
        /// </summary>
        public Bank Bank { get; set; }
    }
}
