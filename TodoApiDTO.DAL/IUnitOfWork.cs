using System;
using System.Threading.Tasks;
using TodoApiDTO.DAL.Repositories;

namespace TodoApiDTO.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        Task<int> CommitAsync();
    }
}