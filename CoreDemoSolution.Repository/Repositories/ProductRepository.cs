
namespace CoreDemoSolution.Repository.Repositories
{
    using CoreDemoSolution.Data;
    using CoreDemoSolution.Data.Classes;
    using CoreDemoSolution.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _db.Products;
        }

        public async Task<Product> GetProductById(int ProductId)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.ProductId == ProductId);
        }
    }
}
