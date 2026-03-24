using System;
using Domain.Entities;
using Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Respositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRespository
{
    private readonly MyRecipyBookDbContext _dbContext;

    public UserRepository(MyRecipyBookDbContext dbContext) => _dbContext = dbContext;
        
    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email)); 
    
}
