using System;

namespace Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
}
