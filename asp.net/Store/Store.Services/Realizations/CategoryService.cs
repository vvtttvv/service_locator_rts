using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
	public Task<IReadOnlyCollection<Category>> GetAllAsync() => repository.GetAllAsync();

	public Task<Category?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public async Task<Category> CreateAsync(Category category)
	{
		var normalizedName = NormalizeName(category.Name);
		var existingCategories = await repository.GetAllAsync();
		if (existingCategories.Any(x => string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
		{
			throw new InvalidOperationException($"Category '{normalizedName}' already exists.");
		}

		category.Name = normalizedName;
		return await repository.AddAsync(category);
	}

	public async Task<Category> UpdateAsync(int id, Category category)
	{
		var normalizedName = NormalizeName(category.Name);
		var current = await repository.GetByIdAsync(id);
		if (current is null)
		{
			throw new KeyNotFoundException($"Category with id {id} was not found.");
		}

		var existingCategories = await repository.GetAllAsync();
		if (existingCategories.Any(x => x.Id != id && string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
		{
			throw new InvalidOperationException($"Category '{normalizedName}' already exists.");
		}

		current.Name = normalizedName;
		return await repository.UpdateAsync(current)
			?? throw new KeyNotFoundException($"Category with id {id} was not found.");
	}

	public async Task DeleteAsync(int id)
	{
		try
		{
			var deleted = await repository.DeleteAsync(id);
			if (!deleted)
			{
				throw new KeyNotFoundException($"Category with id {id} was not found.");
			}
		}
		catch (Exception ex) when (ex is not KeyNotFoundException and not InvalidOperationException)
		{
			throw new InvalidOperationException("Category cannot be deleted because it is used by products.");
		}
	}

	private static string NormalizeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Category name is required.");
		}

		return name.Trim();
	}
}