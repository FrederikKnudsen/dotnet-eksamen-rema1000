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
    public class CategoryService : IRepositoryManager<Category>
    {
        private readonly DatabaseContext _context;
        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return category;
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
                var categoryToDelete = _context.Categories.FirstOrDefault(c => c.ID == id);

                if (categoryToDelete != null)
                {
                    _context.Categories.Remove(categoryToDelete);
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

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.ID == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                if(category != null)
                {
                    _context.Set<Category>().Update(category);
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
