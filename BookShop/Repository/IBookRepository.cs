using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookShop.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int bookID);
        Book GetByTitle(string Name);
        void Insert(Book book, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile);
        void Update(Book book,int id, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile);
        void Delete(int bookID);
        string UploadFile(HttpPostedFileBase file, string Folder);
        string GetFileName(string filePath);
        string GetFilePath(string FileName);
    }
}
