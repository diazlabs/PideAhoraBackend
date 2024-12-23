﻿using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<User?> FindById(Guid id);
        Task<User?> FindByEmail(string email);
        Task<User?> FindByUserName(string userName);
        Task<User?> FindByEmailOrUserName(string emailOrUserName);
        Task<User?> FindByPhoneNumber(string phoneNumber, string country);
        Task<string[]> IsUserInUse(User user);
    }
}
