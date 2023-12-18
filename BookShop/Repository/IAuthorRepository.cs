using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author GetById(int AuthorID);
        Author GetByName(string Name);
        void Insert(Author author);
        void Update(Author author);
        void Delete(int authorID);
        void Save();
    }
}
