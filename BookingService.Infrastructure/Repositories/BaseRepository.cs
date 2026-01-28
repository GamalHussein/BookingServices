using BookingService.Application.Interfaces;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace BookingService.Infrastructure.Repositories;
public class BaseRepository<T> : IBaseRpository<T> where T : class
{
	protected readonly ApplicationDbContext _context;
	protected readonly DbSet<T> _dbSet;

	public BaseRepository(ApplicationDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}



	public virtual async Task<T> CreateAsync(T entity)
	{
		// Set Id if entity has Id property
		var idProperty = entity.GetType().GetProperty("Id");
		if (idProperty != null && idProperty.PropertyType == typeof(Guid))
		{
			var currentId = (Guid)idProperty.GetValue(entity);
			if (currentId == Guid.Empty)
			{
				idProperty.SetValue(entity, Guid.NewGuid());
			}
		}

		// Set CreatedAt if entity has CreatedAt property
		var createdAtProperty = entity.GetType().GetProperty("CreatedAt");
		if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
		{
			createdAtProperty.SetValue(entity, DateTime.UtcNow);
		}

		await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();

		return entity;

	}

	public virtual async Task<bool> DeleteAsync(Guid id)
	{
		var entity = await _dbSet.FindAsync(id);
		if (entity == null) return false;

		_dbSet.Remove(entity);
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<bool> ExistsByIdAsync(Guid id)
	{
		return await _dbSet.FindAsync(id) != null;
	}

	public virtual async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet.AsNoTracking().ToListAsync();
	}

	public virtual async  Task<T> GetByIdAsync(Guid id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<bool> UpdateAsync(T entity)
	{
		// Set UpdatedAt if entity has UpdatedAt property
		var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
		if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime?))
		{
			updatedAtProperty.SetValue(entity, DateTime.UtcNow);
		}

		_dbSet.Update(entity);
		return await _context.SaveChangesAsync() > 0;
	}
}
