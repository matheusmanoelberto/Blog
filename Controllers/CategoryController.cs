﻿using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet ("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices]BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id ,[FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(category);

        if (category == null)
            return NotFound();

        return Ok(category);        
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
    {
        await context.Categories.AddAsync(model);
        await context.SaveChangesAsync();

        return Created($"v1/categories/{model.Id}", model);
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromBody] Category model, [FromRoute] int id, [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(category);

        if (category == null)
            return NotFound();

        category.Name = category.Name;
        category.Slug = category.Slug;

        context.Categories.Update(category);
        await context.SaveChangesAsync();

        return Ok(model);
    }


    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(category);

        if (category == null)
            return NotFound();

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return Ok(category);
    }


}
