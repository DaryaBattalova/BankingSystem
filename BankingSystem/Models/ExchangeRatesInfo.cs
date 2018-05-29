namespace BankingSystem.Models
{
    public class ExchangeRatesInfo
    {
        /// <summary>
        /// Gets or sets a USD purchase rate.
        /// </summary>
        public string USDPurchase { get; set; }

        /// <summary>
        /// Gets or sets a USD sale rate.
        /// </summary>
        public string USDSale { get; set; }

        /// <summary>
        /// Gets or sets a EUR purchase rate.
        /// </summary>
        public string EURPurchase { get; set; }

        /// <summary>
        /// Gets or sets a EUR sale rate.
        /// </summary>
        public string EURSale { get; set; }
    }
}