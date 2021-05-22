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
    public class SupplierService : IRepositoryManager<Supplier>
    {
        private readonly DatabaseContext _context;
        public SupplierService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();

                return supplier;
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
                var supplierToDelete = _context.Suppliers.FirstOrDefault(c => c.ID == id);

                if (supplierToDelete != null)
                {
                    _context.Suppliers.Remove(supplierToDelete);
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

        public async Task<Supplier> Get(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(i => i.ID == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            try
            {
                if (supplier != null)
                {
                    _context.Set<Supplier>().Update(supplier);
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
