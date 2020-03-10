using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IService<TEntity> where TEntity:class
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        List<TEntity> SortAsc();
        List<TEntity> SortDesc();
        List<TEntity> Paging(int indexOfPage, int pageSize);
        List<TEntity> Filter(string filter);
    }
}
