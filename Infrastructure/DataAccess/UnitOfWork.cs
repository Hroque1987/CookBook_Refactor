using System;
using Domain.Repositories;

namespace Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyRecipyBookDbContext _dbContext;

    public UnitOfWork(MyRecipyBookDbContext dbContext) => _dbContext = dbContext;
    

    public async Task Commit() => await _dbContext.SaveChangesAsync();
    
}
