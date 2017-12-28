using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggUppg1ER.Models;
using System.Net;
using BloggUppg1ER.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggUppg1ER.Controllers
{
    public class HomeController : Controller
    {
        // The following 2 are part of Dependency injection
        private BlogERContext _context;

        public HomeController(BlogERContext context)
        {
            _context = context;
        }

        // GET: Overview of all posts (Index)
        public IActionResult ShowAllPosts()
        {
            var posts = _context.Posts.ToList();

            return View(posts);
        }

        // GET: Show details of a single post (details)
        public IActionResult ShowPostDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            var postModel = _context.Posts.Include(c => c.Category).Where(c => c.PostId == id).FirstOrDefault();
            if (postModel == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);

            }

            // Create a VM to combine the post & category info
            //var postVM = new PostViewModel()
            //{
            //    Id = postModel.PostId,
            //    Title = postModel.Title,
            //    Content = postModel.Content,
            //    PostedOn = postModel.PostedOn,
            //    CategoryName = postModel.Category.Name,
            //    //  Description = postModel.Category.Description
            //};

            return View(postModel);
        }



        // GET: Home/Create
        public IActionResult CreatePost()
        {
            var myModel = new PostViewModel();
            myModel.PostedOn = DateTime.Now;

            myModel.Categories = _context.Categories
                                         .Select(c => new SelectListItem()
                                                             {
                                                                 Value = c.CategoryId.ToString(),
                                                                 Text = c.Name
                                                             })
                                         .ToList();
            return View(myModel);
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CourseId,Name,Credit")] Course course)
        public IActionResult CreatePost(PostViewModel postVM)
        {
            // postVM.PostedOn = DateTime.Now;

            // the model to be valid MUST have a PostedON field filled @Client side (View)

            if (ModelState.IsValid)
            {
                // Copy the filled VM to the model
                var post = new Posts()
                {
                    PostId = postVM.Id,
                    Title = postVM.Title,
                    Content = postVM.Content,
                    PostedOn = postVM.PostedOn,
                    CategoryId = Convert.ToInt32(postVM.Categories.Where(c => c.Selected == true).Select(c => c.Value))
                    //Category = new Categories()
                    //{
                    //    Name = postVM.Categories.S
                    //    //  Description = postVM.Description
                    //}
                };

                // *** CHECK if the FK CategoryId is automatically generated in Post table after saving to DB
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("ShowAllPosts");
            }

            return View(postVM);
        }

    }
}