using Domain.Entities.Common;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly OESAppApiDbContext _context;

    public UserRepository(OESAppApiDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Create(User user)
    {
        var entry = _context.User.Add(user);
        try
        {
            await _context.SaveChangesAsync();
        } 
        catch
        {
            return Result<int>.Failure();
        }
        return Result<int>.Success(entry.Entity.Id);
    }

    public async Task<Result<User>> Get(int id)
    {
        User? user = await _context.User.SingleOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return Result<User>.Failure();

        return Result<User>.Success(user);
    }

    public async Task<List<User>> GetByCondition(Expression<Func<User, bool>> expression)
    {
        return await _context.User
            .Where(expression)
            .ToListAsync();
    }

    public async Task<List<User>> GetByCondition(int page, int pageSize, Expression<Func<User, bool>> expression)
    {
        return await _context.User
            .Where(expression)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountByCondition(Expression<Func<User, bool>> expression)
    {
        return await _context.User.Where(expression).CountAsync();
    }

    public async Task<int> Remove(int id) => 
        await _context.User.Where(x => x.Id == id).ExecuteDeleteAsync();

    public Task<Result<int>> Update(User user)
    {
        throw new NotImplementedException();
    }
}
