namespace Business.Concrete;

using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly DataAccess.Abstract.IOperationClaimDal _operationClaimDal;
    private readonly DataAccess.Abstract.IUserOperationClaimDal _userOperationClaimDal;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, DataAccess.Abstract.IOperationClaimDal operationClaimDal, DataAccess.Abstract.IUserOperationClaimDal userOperationClaimDal)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _operationClaimDal = operationClaimDal;
        _userOperationClaimDal = userOperationClaimDal;
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

        var createdUser = _userService.GetByMail(user.Email);

        AssignDefaultClaimsForUser(createdUser.Id, userForRegisterDto.AccountType);

        return new SuccessDataResult<User>(createdUser, Messages.UserRegistered);
    }

    private void AssignDefaultClaimsForUser(int userId, Entities.Enums.AccountType accountType)
    {
        var allClaims = new List<string>
        {
            "Admin",
            "product.add","product.update","product.delete","product.getall","product.getbyid",
            "category.add","category.update","category.delete","category.getall",
            "brand.add","brand.update","brand.delete","brand.getall",
            "store.add","store.update","store.delete","store.getall","store.getbyid","store.approve",
            "order.add","order.update","order.delete","order.cancel","order.getall","order.getbyid","order.update-status",
            "cart.add","cart.update","cart.delete","cart.get","cart.clear",
            "user.getall","user.getbyid","user.update","user.delete","user.change-password",
            "claim.add","claim.update","claim.delete","claim.assign-to-user",
            "review.add","review.update","review.delete","review.approve",
            "address.add","address.update","address.delete","address.getallbyuser",
            "variant.add","variant.update","variant.delete","stock.update"
        };

        List<string> assign;
        if (accountType == Entities.Enums.AccountType.Admin)
        {
            assign = allClaims;
        }
        else if (accountType == Entities.Enums.AccountType.StoreOwner)
        {
            assign = new List<string>
            {
                "StoreOwner",
                "product.add","product.update","product.delete","product.getall","product.getbyid",
                "variant.add","variant.update","variant.delete","stock.update",
                "store.add","store.update","store.getall","store.getbyid",
                "brand.add","brand.update","brand.delete","brand.getall",
                "order.getall","order.getbyid","order.update-status",
                "review.add","review.update","review.delete",
                "address.add","address.update","address.getallbyuser"
            };
        }
        else
        {
            assign = new List<string>
            {
                "User",
                "product.getall","product.getbyid","category.getall","brand.getall",
                "order.add","order.getbyid","cart.add","cart.update","cart.delete","cart.get","cart.clear",
                "user.getbyid","user.update","address.add","address.update","address.getallbyuser",
                "review.add"
            };
        }

        foreach (var claimName in assign.Distinct())
        {
            var claim = _operationClaimDal.Get(c => c.Name == claimName);
            if (claim == null)
            {
                claim = new Core.Entities.Concrete.OperationClaim { Name = claimName };
                _operationClaimDal.Add(claim);
            }

            var existing = _userOperationClaimDal.Get(uoc => uoc.UserId == userId && uoc.OperationClaimId == claim.Id);
            if (existing == null)
            {
                _userOperationClaimDal.Add(new Core.Entities.Concrete.UserOperationClaim { UserId = userId, OperationClaimId = claim.Id });
            }
        }
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByMail(userForLoginDto.Email);
        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email) != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }
        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }

    public IDataResult<Entities.Dtos.Users.UserDetailDto> GetMe()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0)
        {
            return new ErrorDataResult<Entities.Dtos.Users.UserDetailDto>("Kullanıcı oturumu bulunamadı.");
        }

        var user = _userService.GetById(userId);
        if (user == null)
        {
            return new ErrorDataResult<Entities.Dtos.Users.UserDetailDto>(Messages.UserNotFound);
        }

        var claims = _userService.GetClaims(user).Select(c => c.Name).ToList();

        var detail = new Entities.Dtos.Users.UserDetailDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = claims
        };

        return new SuccessDataResult<Entities.Dtos.Users.UserDetailDto>(detail);
    }
}