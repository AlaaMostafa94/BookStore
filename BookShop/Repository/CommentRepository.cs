
using BookShop.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookShop.Repository
{
    public class CommentRepository:ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository()
        {
            _context = new ApplicationDbContext();
        }

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
 
        }
    }
}