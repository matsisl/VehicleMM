using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<TEntity> where TEntity:class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(TEntity entity);
        Task<List<TEntity>> SortAsc();
        Task<List<TEntity>> SortDesc();
        Task<List<TEntity>> Paging(int indexOfPage, int pageSize);
        Task<List<TEntity>> Filter(string filter);
    }
}
