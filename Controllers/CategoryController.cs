using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE5 - Não foi possível obter a categoria!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE6 - Falha interna do servidor!");
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE7 - Não foi possível Incluir a categoria!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE8 - Falha interna do servidor!");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE9 - Não foi possível incluir a categoria!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE10 - Falha interna do servidor!");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE11 - Não foi possível excluir a categoria!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE12 - Falha interna do servidor!");
            }
        }
    }
}