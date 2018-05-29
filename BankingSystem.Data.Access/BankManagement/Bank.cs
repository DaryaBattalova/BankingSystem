namespace BankingSystem.Data.Access.BankManagement
{
    public class Bank
    {
        /// <summary>
        /// Gets or sets a bank identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a name of a bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an address of a bank.
        /// </summary>
        public string Address { get; set; }
    }
}
