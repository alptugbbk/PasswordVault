using Microsoft.EntityFrameworkCore;
using PasswordVaultAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		DbSet<T> Table { get; }

		// read repository
		IQueryable<T> GetAll(bool tracking = true);

		IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

		Task<T> GetByIdAsync(string id, bool tracking = true);





		// write repository
		Task<bool> AddAsync(T model);

		Task<bool> AddRangeAsync(List<T> datas);

		bool Remove(T model);

		bool RemoveRange(List<T> datas);

		Task<bool> RemoveAsync(string id);

		Task<bool> UpdateAsync(T model);

		Task<int> SaveAsync();
	}
}
