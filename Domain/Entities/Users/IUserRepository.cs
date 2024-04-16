using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users;
public interface IUserRepository
{
    public Task<Result<User>> Get(int id);
    public Task<List<User>> GetByCondition(int page, int pageSize, Expression<Func<User, bool>> expression);
    public Task<List<User>> GetByCondition(Expression<Func<User, bool>> expression);
    public Task<Result<int>> Create(User user);
    public Task<int> Remove(int id);
    public Task<Result<int>> Update(User user);
    public Task<int> GetCountByCondition(Expression<Func<User, bool>> expression);
}
