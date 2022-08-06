﻿using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface IAccountService
{
    Task<int> Create(Account account);

    Task Update(Account account);

    Task Delete(int accountId);
}
