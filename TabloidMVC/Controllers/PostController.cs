using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITagRepository _tagRepository;
        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        //displays view that list all the posts created by a specific user
        public IActionResult MyPost()
        {
            int userId = GetCurrentUserProfileId();

            List<Post> myposts = _postRepository.GetAllUserPostById(userId);

            if (myposts == null)
            {
                return NotFound();
            }

            return View(myposts);
        }

        //Passes post detail view model to view consisting of Post and Tags 
        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            var addedTags = _tagRepository.GetTagsByPostId(id);
           

            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                addedTags = _tagRepository.GetTagsByPostId(id);
                if (post == null)
                {
                    return NotFound();
                }
            }
            PostDetailViewModel pd = new()
            {
                Post = post,
                AddedTags = addedTags
            };
            return View(pd);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAllCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAllCategories();
                return View(vm);
            }
        }

        // GET: PostController/Edit/postId
        public ActionResult Edit(int id)
        {
            int userProfileId = GetCurrentUserProfileId();
            Post post = _postRepository.GetUserPostById(id, userProfileId);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);

        }

        // POST: PostController/Edit/postId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {
            try
            {
                _postRepository.UpdatePost(post);

                return RedirectToAction("MyPost");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        //Get: PostController/Delete/postId
        public IActionResult Delete(int id)
        {
            int userProfileId = GetCurrentUserProfileId();
            Post post = _postRepository.GetUserPostById(id, userProfileId);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: PostController/Delete/postId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        public ActionResult Comments(int id)
        {
            List<Comment> comments = _commentRepository.GetCommentsByPostId(id);
            Post post = _postRepository.GetPostById(id);

            PostCommentsViewModel vm = new PostCommentsViewModel()
            {
                Comments = comments,
                Post = post
            };

            return View(vm);
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
