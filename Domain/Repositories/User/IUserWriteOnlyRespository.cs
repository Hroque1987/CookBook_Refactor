using System;

namespace Domain.Repositories.User;

public interface IUserWriteOnlyRespository
{
    public Task Add(Entities.User user);
}
