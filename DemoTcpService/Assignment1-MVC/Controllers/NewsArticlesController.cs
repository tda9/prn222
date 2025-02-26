﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1_MVC.Models;
using System.Net;

namespace Assignment1_MVC.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly FunewsManagementContext _context;

        public NewsArticlesController(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchQuery, string tag)
        {
			var role = HttpContext.Request.Cookies["Role"]; // Retrieve the cookie value
			bool isAdmin = role == "1"; // Check if the role is "1"

			IQueryable<NewsArticle> funewsManagementContext = _context.NewsArticles
				.Where(n => isAdmin || n.NewsStatus==true) // Apply filtering based on the role
				.Include(n => n.Category)
				.Include(n => n.CreatedBy)
				.Include(n => n.Tags);

			if (!string.IsNullOrEmpty(searchQuery))
            {
                funewsManagementContext = funewsManagementContext.Where(n =>
                    (n.Headline != null && n.Headline.Contains(searchQuery)) ||
                    (n.Category != null && n.Category.CategoryName != null && n.Category.CategoryName.Contains(searchQuery)) ||
                    (n.NewsContent != null && n.NewsContent.Contains(searchQuery)) ||
                    (n.NewsTitle != null && n.NewsTitle.Contains(searchQuery))
                );
            }

            if (!string.IsNullOrEmpty(tag))
            {
                funewsManagementContext = funewsManagementContext.Where(n =>
                    n.Tags.Any(t => t.TagName == tag) // Assuming `Tags` is a collection
                );
            }

            return View(await funewsManagementContext.ToListAsync());
        }

            // GET: NewsArticles/Details/5
            public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
				.Include(n => n.Tags)
				.FirstOrDefaultAsync(m => m.NewsArticleId == id);

            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId");
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
			if (await _context.NewsArticles.AnyAsync(c => c.NewsArticleId == newsArticle.NewsArticleId))
			{
				ModelState.AddModelError("NewsArticleId", "This NewsArticle ID already exists.");
			}
			if (ModelState.IsValid)
            {
                _context.Add(newsArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(newsArticle.NewsArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", newsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId", newsArticle.CreatedById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(string id)
        {
            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
        }
    }
}
