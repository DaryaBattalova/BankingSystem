
using System;

namespace BankingSystem.Services.BankManagement
{
    public interface IBankWorkerService
    {
        int GetBankIdOfCurrentWorker(Guid guid);
    }
}
