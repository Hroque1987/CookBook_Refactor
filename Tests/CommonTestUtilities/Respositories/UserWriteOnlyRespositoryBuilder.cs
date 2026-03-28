using System;
using Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Respositories;

public class UserWriteOnlyRespositoryBuilder
{
    public static IUserWriteOnlyRespository Builder()
    {
        return new Mock<IUserWriteOnlyRespository>().Object;
    }

}
