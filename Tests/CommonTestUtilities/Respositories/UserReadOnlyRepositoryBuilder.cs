using System;
using Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Respositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

    public void ExistsActiveUserWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);//repository.ExistsActiveUserWithEmail(It.IsAny<string>())
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
