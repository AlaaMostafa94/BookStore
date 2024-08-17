using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookShop.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author GetById(int AuthorID);
        Author GetByName(string Name);
        void Insert(Author author, HttpPostedFileBase file);
        void Update(Author author,int id, HttpPostedFileBase file);
        void Delete(int authorID);

        string UploadFile(HttpPostedFileBase file, string Folder);
        string GetFileName(string filePath);
    }
}
