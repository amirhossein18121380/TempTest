using Application.Contracts.Persistence;
using Domain.Entities.Product;
using Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;


internal class ProductRepository(ApplicationDbContext dbContext) : BaseAsyncRepository<Product>(dbContext), IProductRepository
{
    public async Task AddProductAsync(Product product)
    {
        await base.AddAsync(product);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await base.TableNoTracking.ToListAsync();
    }

    public async Task<List<Product>> GetAllProductsWithRelatedUserAsync()
    {
        var products = await base.TableNoTracking.Include(c => c.User).ToListAsync();

        return products;
    }
    public async Task<Product> GetProductsByEmailAsync(string email, bool trackEntity)
    {
        var product = await base.TableNoTracking.FirstOrDefaultAsync(c => c.ManufactureEmail == email);

        if (product is not null && trackEntity)
            base.DbContext.Attach(product);

        return product;
    }

    public async Task<Product> GetProductByEmailAndDateAsync(string email, DateTime date, bool trackEntity)
    {
        var product = await base.TableNoTracking.FirstOrDefaultAsync(c => c.ProduceDate == date && c.ManufactureEmail == email);

        if (product is not null && trackEntity)
            base.DbContext.Attach(product);

        return product;
    }


    public async Task<Product> GetProductsByIdAsync(int productId, bool trackEntity)
    {
        var product = await base.TableNoTracking.FirstOrDefaultAsync(c => c.Id == productId);

        if (product is not null && trackEntity)
            base.DbContext.Attach(product);
        return product;
    }

    public async Task HardDeleteProductAsync(int productId)
    {
        var product = await base.TableNoTracking.FirstOrDefaultAsync(c => c.Id == productId);

        if (product is not null)
            await base.DeleteAsync(x => x.Id == productId);
    }

    public async Task DeleteProductAsync(int userId)
    {
        await UpdateAsync(c => c.CreatedByUserId == userId, p => p.SetProperty(product => product.IsDeleted, true));
    }
}