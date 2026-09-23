namespace Business.Concrete;

using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;

using Entities.DTOs;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        var user = new User
        {
            Email = userForRegisterDto.Email,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            PhoneNumber = userForRegisterDto.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
            CreatedAt = DateTime.UtcNow
        };

        _userService.Add(user);
        return new SuccessDataResult<User>(user, "Kayıt başarıyla gerçekleşti.");
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByMail(userForLoginDto.Email);
        if (userToCheck == null)
        {
            return new ErrorDataResult<User>("Kullanıcı bulunamadı.");
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>("Şifre hatalı.");
        }

        return new SuccessDataResult<User>(userToCheck, "Giriş başarılı.");
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email) != null)
        {
            return new ErrorResult("Bu e-posta adresiyle zaten bir kullanıcı mevcut.");
        }
        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, "Token başarıyla oluşturuldu.");
    }
}