using KanS.Entities;
using KanS.Models;
using Microsoft.AspNetCore.Identity;

namespace KanS.Services;

public class AccountService : IAccountService {
    private readonly KansDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountService(KansDbContext context, IPasswordHasher<User> passwordHasher) {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task RegisterUser(UserRegisterDto dto) {
        var newUser = new User() {
            Name = dto.Name,
            Email = dto.Email,
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }
}
