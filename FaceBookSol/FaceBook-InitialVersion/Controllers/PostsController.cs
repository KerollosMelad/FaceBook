using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaceBook_InitialVersion.Data;
using FaceBook_InitialVersion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static FaceBook_InitialVersion.Models.Enums;
using System.Web;

namespace FaceBook_InitialVersion.Controllers
{
    [Authorize(Roles = "Member")]

    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Person> _userManager;
        public PersonModelView PersonMV { get; set; }


        public PostsController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts
        public IActionResult Index()
        {
            // returning list of posts including the User and the Post Like
            // so we can show them and displaying this list descending by Creation Date

            PersonMV = GetPersonModel();
            var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();
            return View(posts);

            //return View(_context.Posts
            //                     .Include(u => u.User)
            //                     .Include(u => u.UserPostLikes)
            //                     .Include(u => u.UserPostComments)
            //                     .ThenInclude(c => c.Comment)
            //                     .Include(u => u.UserPostComments)
            //                     .ThenInclude(c => c.User)
            //                     .ToList()
            //                     .OrderByDescending(p => p.CreationDate));
        }


        private PersonModelView GetPersonModel()
        {

            var _currentUser = _context.Users
                               .Include(P => P.Posts)
                               .Include("Posts.UserPostLikes")
                               .Include("Posts.UserPostComments")
                               .Include("Posts.UserPostComments.Comment")
                               .Include("Posts.UserPostComments.User")
                               .Include("FriendsRequest.User")
                               .Include("MyRequests.Friend")
                               .Where(P => P.UserName == User.Identity.Name)
                               .FirstOrDefault();

            var friendsId = _currentUser.MyRequests
                                        .Where(Fr => Fr.friendShipStatus == Enums.FriendShipStatus.Accepted)
                                        .Select(P => P._friendID)
                                        .Union
                                        (
                                         _currentUser.FriendsRequest
                                         .Where(F => F.friendShipStatus == Enums.FriendShipStatus.Accepted)
                                         .Select(P => P._userID)
                                        );

            PersonModelView modelView = new PersonModelView()
            {
                CurrentUser = _currentUser,
                MyPosts = _context.Posts
                                 .Where(u => u.User.UserName == User.Identity.Name)
                                 .Include(u => u.User)
                                 .Include(u => u.UserPostLikes)
                                 .Include(u => u.UserPostComments)
                                 .ThenInclude(c => c.Comment)
                                 .Include(u => u.UserPostComments)
                                 .ThenInclude(c => c.User)
                                 .OrderByDescending(p => p.CreationDate).ToList(),
            //MyPosts = _currentUser?.Posts.OrderByDescending(P => P.CreationDate).ToList(),
                FriendPosts = _context.Posts
                                        .Where(P => (friendsId.Contains(P.UserID)) && User.Identity.Name == User.Identity.Name)
                                        .Include(u => u.User)
                                        .Include(u => u.UserPostLikes).ThenInclude(l => l.User).ThenInclude(u => u.FriendsRequest)
                                        .Include(u => u.UserPostLikes).ThenInclude(l => l.User).ThenInclude(u => u.MyRequests)
                                        .Include(u => u.UserPostComments)
                                        .ThenInclude(c => c.Comment)
                                        .Include(u => u.UserPostComments)
                                        .ThenInclude(c => c.User)
                                        .OrderByDescending(P => P.CreationDate).ToList()                                              // true


            };

            return modelView;
        }


        // GET: Posts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Body")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserID = _userManager.GetUserId(User);
                post.CreationDate = DateTime.Now;
                post.State = PostStatus.Active;
                _context.Add(post);
                await _context.SaveChangesAsync();
                ////return RedirectToAction(nameof(Index));
                ///
                PersonMV = GetPersonModel();

                var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();

                return PartialView("GetAll", posts);
                //return PartialView("GetAll", await _context.Posts
                //                                            .Include(p => p.User)
                //                                            .Include(u => u.UserPostLikes)
                //                                            .Include(u => u.UserPostComments)
                //                                            .ThenInclude(c => c.Comment)
                //                                            .Include(u => u.UserPostComments)
                //                                            .ThenInclude(c => c.User)
                //                                            .OrderByDescending(p => p.CreationDate)
                //                                            .ToListAsync());

            }
            //return View(post);
            return RedirectToAction(nameof(Index));

        }

        //// GET: Posts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts.FindAsync(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Body,CreationDate,State")] Post post)
        //{
        //    if (id != post.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(post);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostExists(post.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(post);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            //_context.Posts.Remove(post);
            post.State = PostStatus.Deleted;
            _context.Update(post);
            await _context.SaveChangesAsync();
            //return PartialView("GetAll", await _context.Posts.Include(p => p.User).Include(u => u.UserPostLikes).ToListAsync());
            PersonMV = GetPersonModel();

            var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();

            return PartialView("GetAll", posts);

            //return PartialView("GetAll", await _context.Posts
            //                                            .Include(p => p.User)
            //                                            .Include(u => u.UserPostLikes)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.Comment)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.User)
            //                                            .OrderByDescending(p => p.CreationDate)
            //                                            .ToListAsync());

        }

        public async Task<IActionResult> Like(int id)
        {
            var post = await _context.Posts.Include(l => l.UserPostLikes).Where(p => p.ID == id).FirstOrDefaultAsync();

            // Check if the user already liked this post ... if not add his ID and Post's ID to the UserPostLikes
            if (!post.UserPostLikes.Where(u => u.PostID == id && u.UserID == _userManager.GetUserId(User)).Any())
            {
                post.UserPostLikes.Add(new UserPostLike
                {
                    PostID = post.ID,
                    UserID = _userManager.GetUserId(User)
                });

                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            else
            {
                post.UserPostLikes.Remove(
                    post.UserPostLikes.FirstOrDefault(u => u.PostID == post.ID && u.UserID == _userManager.GetUserId(User)));
                await _context.SaveChangesAsync();
            }

            //return PartialView("GetAll", await _context.Posts.Include(p => p.User).Include(u => u.UserPostLikes).ToListAsync());
            PersonMV = GetPersonModel();

            var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();

            return PartialView("GetAll", posts);

            //return PartialView("GetAll", await _context.Posts
            //                                            .Include(p => p.User)
            //                                            .Include(u => u.UserPostLikes)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.Comment)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.User)
            //                                            .OrderByDescending(p => p.CreationDate)
            //                                            .ToListAsync());

        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }


        public async Task<IActionResult> AddComment(String commentBody, int postId)
        {
            var comment = new Comment()
            {

                Body = HttpUtility.HtmlEncode(commentBody),
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
            PersonMV = GetPersonModel();

            var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();

            return PartialView("GetAll", posts);

            //return PartialView("GetAll", await _context.Posts
            //                                            .Include(p => p.User)
            //                                            .Include(u => u.UserPostLikes)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.Comment)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.User)
            //                                            .OrderByDescending(p => p.CreationDate)
            //                                            .ToListAsync());
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            comment.State = CommentStatus.Deleted;
            _context.Update(comment);
            await _context.SaveChangesAsync();
            PersonMV = GetPersonModel();

            var posts = PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList();

            return PartialView("GetAll", PersonMV.MyPosts.Union(PersonMV.FriendPosts).ToList());

            //return PartialView("GetAll", await _context.Posts
            //                                            .Include(p => p.User)
            //                                            .Include(u => u.UserPostLikes)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.Comment)
            //                                            .Include(u => u.UserPostComments)
            //                                            .ThenInclude(c => c.User)
            //                                            .OrderByDescending(p => p.CreationDate)
            //                                            .ToListAsync());
        }

    }
}
