using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace bookstore.Models.Repositories
{
    public  interface IBookRepository<TEntity>
    {IList <TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id,TEntity entity);
        void Delete(int id);
        public List<TEntity> Search(string term);


    }
}
