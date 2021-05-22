using dotnet_eksamen_rema1000.Infrastructure;
using dotnet_eksamen_rema1000.Models;
using dotnet_eksamen_rema1000.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_eksamen_rema1000.Services
{
    public class ProductService : IRepositoryManager<Product>
    {
        private readonly DatabaseContext _context;
        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var productToDelete = _context.Products.FirstOrDefault(c => c.ID == id);

                if (productToDelete != null)
                {
                    _context.Products.Remove(productToDelete);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(i => i.ID == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(c => c.Category)
                .Include(s => s.Supplier);
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                if(product != null)
                {
                    _context.Set<Product>().Update(product);
                    await _context.SaveChangesAsync();


                    return true;
                }

                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
