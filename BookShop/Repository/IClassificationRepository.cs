using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Repository
{
    public interface IClassificationRepository
    {
        IEnumerable<Classification> GetAll();
        Classification GetById(int ClassificationID);
        Classification GetByName(string Name);
        void Insert(Classification classification);
        void Update(Classification classification);
        void Delete(int ClassificationID);
    }
}
