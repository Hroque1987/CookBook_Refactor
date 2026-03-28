using System;
using Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Respositories;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        return new Mock<IUnitOfWork>().Object;
    }
}
