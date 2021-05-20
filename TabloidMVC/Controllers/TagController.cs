using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        // GET: TagController
        public ActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            
            return View(tags);
        }

        // GET: TagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.Add(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
            
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            try
            {
                _tagRepository.UpdateTag(tag);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                return View(tag);
            }
            
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.DeleteTag(id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(tag);
            }
        }
        //Gets list of tags available to add to a post, creates view model consisting of tags and a list to store selected tags
        public ActionResult ViewTagsToAdd(int id)
        {
            List<Tag> tags = _tagRepository.GetAllTags();
            List<Tag> addedTags = _tagRepository.GetTagsByPostId(id);
            PostTag pt = new()
            {
                Tags = tags,
                AddedTags = addedTags,
                AddTagIds = new List<int>(),
                DeleteTagIds = new List<int>()
            };
            return View(pt);
        }

        //Receives associated Post ID from route and a list of Tag IDs from multi select and passes them to tag repo to create instances of PostTags
        [HttpPost]
        public ActionResult ViewTagsToAdd(int id, List<int> addTagIds, List<int> deleteTagIds)
        {
            if (addTagIds.Count != 0)
            {
                try
                {

                    _tagRepository.AddTagToPost(id, addTagIds);
                    return RedirectToAction("Details", "Post", new { id = id });
                }
                catch
                {
                    return RedirectToAction("ViewTagsToAdd", new { id = id });
                }
            }
            if (deleteTagIds.Count != 0)
            {
                try
                {
                    _tagRepository.DeletePostTag(id, deleteTagIds);
                    return RedirectToAction("Details", "Post", new { id = id });
                }
                catch
                {
                    return RedirectToAction("ViewTagsToAdd", new { id = id });
                }
            }
            return RedirectToAction("Details", "Post", new { id = id });
        }
    }
    
}
