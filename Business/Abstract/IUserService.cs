namespace Business.Abstract;

using Core.Entities.Concrete;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User? GetByMail(string email);
    User? GetById(int id);
}