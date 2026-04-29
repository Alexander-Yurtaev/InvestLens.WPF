using AutoMapper;
using InvestLens.Common.Helpers;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.User;
using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Services;

public class AuthService(IUserRepository userRepository, IMapper mapper) : IAuthService
{
    private readonly IMapper _mapper = mapper;

    public async Task<bool> CheckLoginUniqueAsync(string login)
    {
        return await userRepository.CheckLoginUnique(login);
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model)
    {
        try
        {
            var user = _mapper.Map<User>(model);
            user.Password = PasswordHelper.HashPassword(PasswordHelper.GetPasswordAsString(model.Password));
            var success = await userRepository.CreateUser(user);
            return success ? (true, "") : (false, "Данные не сохранились");
        }
        catch (Exception)
        {
            return (false, "Ошибка при регистрации");
        }
    }

    public async Task<(bool Success, UserModel? User, string ErrorMessage)> LoginAsync(LoginModel model)
    {
        var user = await userRepository.GetUserByLogin(model.Login);
        if (user is null)
        {
            return (false, null, "Ошибка при авторизации");
        }

        var userModel = _mapper.Map<UserModel>(user);

        var isAuth = PasswordHelper.VerifyPassword(PasswordHelper.GetPasswordAsString(model.Password), user.Password);

        return isAuth ? (true, userModel, "") : (false, null, "Ошибка при авторизации");
    }
}