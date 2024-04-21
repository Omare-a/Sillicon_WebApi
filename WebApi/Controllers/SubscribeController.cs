using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribeController(ApiContext apicontext) : ControllerBase
{
    private readonly ApiContext _apicontext = apicontext;

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeEntity entity)
    {
        if (ModelState.IsValid)
        {
            var checkValue = await _apicontext.Subscribers.AnyAsync(x => x.Email == entity.Email);
            if (checkValue)
                return Conflict();

            _apicontext.Add(entity);
            await _apicontext.SaveChangesAsync();
            return Ok();
            
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Unsubscribe(string email)
    {
        if (ModelState.IsValid)
        {
            var entity = await _apicontext.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
            if (entity == null)
                return NotFound();

            _apicontext.Remove(entity);
            await _apicontext.SaveChangesAsync();
            return Ok();
        }

        return BadRequest();
    }
}
