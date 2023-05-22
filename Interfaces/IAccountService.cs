using KanS.Models;

namespace KanS.Services;

public interface IAccountService {
    Task RegisterUser(UserRegisterDto dto);
    Task<string> GenerateJwt(UserLoginDto dto);

}