
namespace CoreDemoSolution.Repository.Interfaces
{
    using CoreDemoSolution.Data.Classes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductList();

        Task<Product> GetProductById(int ProductId);
    }
}
