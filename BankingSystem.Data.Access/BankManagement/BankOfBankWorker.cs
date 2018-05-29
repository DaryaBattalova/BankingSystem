using System;

namespace BankingSystem.Data.Access.BankManagement
{
    public class BankOfBankWorker
    {
        /// <summary>
        /// Gets or sets a bank identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets an identifier of a bank, where a woker works.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets a Guid of a worker.
        /// </summary>
        public Guid WorkerGuid { get; set; }
    }
}
