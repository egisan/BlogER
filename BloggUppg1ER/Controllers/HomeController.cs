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
using System.Collections;

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

        // In this Action I need to enbed the Search function for both Title and Category!
        public IActionResult ShowAllPosts(string titleSearch, string categorySearch)
        {
            //ViewBag.sortTerm = String.IsNullOrEmpty(sortTerm) ? "" : sortTerm;
            //ViewBag.titleSearch = String.IsNullOrEmpty(titleSearch) ? "" : titleSearch;
            //ViewBag.categorySearch = String.IsNullOrEmpty(categorySearch) ? "" : categorySearch;

            string filterHeader = "";
            IQueryable<Posts> posts = _context.Posts.Include("Category").OrderByDescending(c => c.PostedOn) as IQueryable<Posts>;

            // Filtering by Title
            if (!String.IsNullOrEmpty(titleSearch))
            {
                posts = posts.Where(c => c.Title.ToLower().Contains(titleSearch.ToLower())).OrderByDescending(c => c.PostedOn);
                filterHeader = String.Format(@"Filtered posts on title ""{0}""", titleSearch);
            }

            // Filtering by Category
            if (!String.IsNullOrEmpty(categorySearch))
            {
                posts = posts.Where(c => c.Category.Name.ToLower().Contains(categorySearch.ToLower())).OrderByDescending(c => c.PostedOn);
                if (filterHeader != "")
                {
                    filterHeader = filterHeader + String.Format(@" and on category ""{0}""", categorySearch);
                }
                else
                {
                    filterHeader = String.Format(@"Filtered posts on category ""{0}""", categorySearch);
                }
            }

            ViewBag.Title = filterHeader;

            return View(posts.ToList());
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

            // myModel.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
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
                    CategoryId = postVM.CategoryID
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