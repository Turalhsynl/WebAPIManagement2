﻿using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
}
