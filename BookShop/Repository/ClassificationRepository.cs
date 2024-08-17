using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookShop.Repository
{
    public class ClassificationRepository : IClassificationRepository
    {

        private readonly ApplicationDbContext _context;

        public ClassificationRepository()
        {
            _context = new ApplicationDbContext();
        }

        public ClassificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Classification> GetAll()
        {
            return _context.Classifications.ToList().OrderBy(c => c.Name);
        }


        public Classification GetById(int ClassificationID)
        {
            return _context.Classifications.Find(ClassificationID);
        }


        public Classification GetByName(string Name)
        {
            return _context.Classifications.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void Insert(Classification classification)
        {
            _context.Classifications.Add(classification);
        }


        public void Update(Classification classification)
        {
            _context.Entry(classification).State = EntityState.Modified;
        }


        public void Delete(int ClassificationID)
        {
            Classification classification = _context.Classifications.Find(ClassificationID);
            if (classification != null)
            {
                _context.Classifications.Remove(classification);
            }
        }




        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}