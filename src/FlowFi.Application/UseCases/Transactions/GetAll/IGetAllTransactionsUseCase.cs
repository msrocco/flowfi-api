﻿using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Transactions.GetAll;

public interface IGetAllTransactionsUseCase
{
    Task<ResponseTransactionsJson> Execute(int? month = null, int? year = null, Guid? bankAccountId = null, string? type = null);
}
