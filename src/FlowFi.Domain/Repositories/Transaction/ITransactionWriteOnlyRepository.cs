﻿namespace FlowFi.Domain.Repositories.Transaction;

public interface ITransactionWriteOnlyRepository
{
    Task Add(Entities.Transaction transaction);
}
