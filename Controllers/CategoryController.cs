using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
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
    public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", category);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500,"05XE9 - Não foi possível imcuir a categoria");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05XE9 - Falha interna no servido");
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromBody] EditorCategoryViewModel model, [FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(model);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, "05XE8 - Não foi possível alterar a categoria");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05XE11 - Falha interna no servido");
        }
    }


    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }

        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, "05XE7 - Não foi possível excluir a categoria");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05XE12 - Falha interna no servido");
        }
    }


}
