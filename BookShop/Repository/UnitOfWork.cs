using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookShop.Repository
{
    public class UnitOfWork:IDisposable
    {
        private ApplicationDbContext _dbContext;
        private IClassificationRepository _classificationRepo;
        private IAuthorRepository _authorRepo;
        private IBookRepository _bookRepo;
        private ICommentRepository _commentRepo;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IClassificationRepository ClassificationRepo
        {
            get
            {
                if(_classificationRepo == null)
                {
                    _classificationRepo = new ClassificationRepository(_dbContext);
                }
                return _classificationRepo;
            }
        }

        public IAuthorRepository AuthorRepo
        {
            get
            {
                if (_authorRepo == null)
                {
                    _authorRepo = new AuthorRepository(_dbContext);
                }
                return _authorRepo;
            }
        }

        public IBookRepository BookRepo
        {
            get
            {
                if (_bookRepo == null)
                {
                    _bookRepo = new BookRepository(_dbContext);
                }
                return _bookRepo;
            }
        }

        public ICommentRepository CommentRepo
        {
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new CommentRepository(_dbContext);
                }
                return _commentRepo;
            }
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}