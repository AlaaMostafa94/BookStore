using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public void Insert(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Update(Author author)
        {
            _context.Entry(author).State=EntityState.Modified;
        }

        public void Delete(int authorID)
        {
            Author author = _context.Authors.Find(authorID);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}