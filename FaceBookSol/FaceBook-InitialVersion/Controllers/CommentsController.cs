using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceBook_InitialVersion.Data;
using FaceBook_InitialVersion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FaceBook_InitialVersion.Models.Enums;

namespace FaceBook_InitialVersion.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        public CommentsController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> AddComment(String commentBody, int postId)
        {
            var comment = new Comment()
            {

                Body = commentBody,
                CreationDate = DateTime.Now,
                State = CommentStatus.Active

            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            var x = comment.ID;

            var userPostComment = new UserPostComment()
            {
                CommentID = comment.ID,
                PostID = postId,
                userID = _userManager.GetUserId(User)
            };
            _context.UserPostComments.Add(userPostComment);
            _context.SaveChanges();
            //var commentId = _context.Comments.Where(c => c.CreationDate == comment.CreationDate).Select(c => c.ID).FirstOrDefault();
            return PartialView("../Posts/GetAll", await _context.Posts.Include(p => p.User).Include(u => u.UserPostLikes).Include(u => u.UserPostComments).ThenInclude(c => c.Comment).OrderByDescending(p => p.CreationDate).ToListAsync());

        }
    }
}