using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Repository
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
    }
}
