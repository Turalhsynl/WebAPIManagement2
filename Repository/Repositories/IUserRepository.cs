﻿using Domain.Entities;

namespace Repository.Repositories
{
    public interface IUserRepository
    {
        Task RegisterAsync(User user);
        void Update(User user);
        bool Remove(int id, int deletedBy);
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);

    }
}
