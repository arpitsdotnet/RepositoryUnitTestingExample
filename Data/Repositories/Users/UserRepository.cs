using System.Linq.Expressions;
using Core.Models;
using Data.Abstracts.Users;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Users;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = _dbContext.Users;
    }


    public async Task<User?> Create(User entity, CancellationToken cancellationToken)
    {
        await _users.AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<User?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await _users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        _users.Remove(user);

        return user;
    }

    public async Task<List<User>?> GetAll(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        var users = await _users.Where(expression).ToListAsync(cancellationToken);

        if (users is null)
        {
            return null;
        }

        return users;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> Get(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        var user = await _users.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return user;
    }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
