using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> FindById(Guid id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> FindByEmailOrUserName(string emailOrUsername)
        {
            User? user = await FindByEmail(emailOrUsername);
            user ??= await FindByUserName(emailOrUsername);

            return user;
        }

        public async Task<User?> FindByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User?> FindByPhoneNumber(string phoneNumber, string country)
        {
            return await _userManager.Users
                .Where(x => x.PhoneNumber == phoneNumber && x.Country == country)
                .FirstOrDefaultAsync();
        }

        public async Task<string[]> IsUserInUse(User user)
        {
            List<string> errors = [];
            User? byEmail = await FindByEmail(user.Email!);
            if (byEmail is not null && byEmail.Id != user.Id)
            {
                errors.Add("El correo electrónico ya esta en uso");
            }

            User? byPhone = await FindByPhoneNumber(user.PhoneNumber!, user.Country);
            if (byPhone is not null && byPhone.Id != user.Id)
            {
                errors.Add("El número de teléfono ya esta en uso");
            }

            return errors.ToArray();
        }
    }
}
