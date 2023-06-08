﻿using Blog.Data;
using Blog.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class PostController : ControllerBase
    {
        [HttpGet("vi/Posts")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            var posts = await context
                .Posts
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Select(x => new ListPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    LastUpdateDate = x.LastUpdateDate,
                    Category = x.Category.Name,
                    Author = $"{x.Author.Name} ({x.Author.Email})",
                })
                .ToListAsync();
            return Ok(posts);
        }
    }
}