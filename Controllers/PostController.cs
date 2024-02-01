﻿using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    [HttpGet("v1/posts")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        var posts = await context
            .Posts
            .AsNoTracking()
            .Select(x => new
            {

            })
            .ToListAsync();
        return Ok(posts);
    }
}
