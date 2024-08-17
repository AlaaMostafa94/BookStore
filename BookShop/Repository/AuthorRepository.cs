using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BookShop.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository()
        {
            _context = new ApplicationDbContext();
        }

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList().OrderBy(a => a.Name);
        }

        public Author GetById(int AuthorID)
        {
            return _context.Authors.Find(AuthorID);
        }

        public Author GetByName(string Name)
        {
            return _context.Authors.Where(a => a.Name == Name).FirstOrDefault();
        }


        public void Insert(Author author, HttpPostedFileBase file)
        {
            if (file == null)
            {
                author.Image = "images.png";
            }
            else
            {
                string filePath = UploadFile(file, "~/Uploads/AuthorsPhotos");
                string fileName = GetFileName(filePath);
                author.Image = fileName;

            }
            _context.Authors.Add(author);
        }

        public void Update(Author author,int id, HttpPostedFileBase file)
        {
            Author auth = GetById(id);
            auth.About = author.About;
            auth.Name = auth.Name;

            if (file == null)
            {
                auth.Image = GetById(id).Image;
            }
            else if (file.ContentLength > 0)
            {
                string filePath = UploadFile(file, "~/Uploads/AuthorsPhotos");
                string fileName = GetFileName(filePath);
                auth.Image = fileName;


            }
        }

        public void Delete(int authorID)
        {
            Author author = _context.Authors.Find(authorID);
            if (author != null)
            {
                _context.Authors.Remove(author);
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
    }
}