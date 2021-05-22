using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_eksamen_rema1000.Models.Repository
{

    public interface IRepositoryManager<T>
    {
        Task<T> Create(T item); // Create
        IEnumerable<T> GetAll(); //Read
        Task<T> Get(int id); //Read single
        Task<bool> Update(T item); // Update
        Task<bool> Delete(int id); // Delete
    }
}
