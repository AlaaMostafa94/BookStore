using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BookShop.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository()
        {
            _context = new ApplicationDbContext();
        }

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList().OrderBy(b => b.Title);
        }

        public Book GetById(int bookID)
        {
            return _context.Books.Find(bookID);
        }

        public Book GetByTitle(string title)
        {
            return _context.Books.FirstOrDefault(b => b.Title == title);
        }




        public void Insert(Book book, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile)
        {

            if (coverPictureFile == null)
            {
                book.CoverPicture = "images (8).jpg";
            }
            else
            {
                string coverPicturePath = UploadFile(coverPictureFile, "~/Uploads/BooksCovers");
                string coverPictureFileName = GetFileName(coverPicturePath);
                book.CoverPicture = coverPictureFileName;

            }



            if (bookContentFile == null)
            {
                book.Content = null;
            }
            else
            {
                string bookContentPath = UploadFile(bookContentFile, "~/Uploads/BooksContent");
                string bookContentFileName = GetFileName(bookContentPath);
                book.Content = bookContentFileName;

            }
            book.DownloadNumber = 0;
            book.UploadDate = DateTime.Today;
            _context.Books.Add(book);
        }


        public void Update(Book book,int id, HttpPostedFileBase coverPictureFile, HttpPostedFileBase bookContentFile)
        {
            Book Book = GetById(id);
            Book.AuthorID = book.AuthorID;
            Book.Title = book.Title;
            Book.Description = book.Description;
            Book.ClassificationID = book.ClassificationID;
            

            if (coverPictureFile == null)
            {
                Book.CoverPicture = GetById(book.ID).CoverPicture;
            }
            else
            {
                string newCoverPicturePath = UploadFile(coverPictureFile, "~/Uploads/BooksCovers");
                string coverPictureName = GetFileName(newCoverPicturePath);
                Book.CoverPicture = coverPictureName;
            }

            if (bookContentFile == null)
            {
                Book.Content = GetById(book.ID).Content;
            }
            else
            {
                string contentPath = UploadFile(bookContentFile, "~/Uploads/BooksContent");
                string contentFileName = GetFileName(contentPath);
                Book.Content = contentFileName;
            }
        }


        public void Delete(int bookID)
        {
            Book book = _context.Books.Find(bookID);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
    

        public string UploadFile(HttpPostedFileBase file, string Folder)
        {
            string fileName = Path.GetFileName(file.FileName);
            string guid = Guid.NewGuid().ToString();
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Folder), guid + fileName);
            file.SaveAs(path);
            return path;



        }

        public string GetFileName(string filePath)
        {
            string fl = filePath.Substring(filePath.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string fileName = split[1];
            return fileName;
        }

        public string GetFilePath(string FileName)
        {
            string filePath = "~/Uploads/BooksContent/" + FileName;
            return filePath;
        }
    }
}