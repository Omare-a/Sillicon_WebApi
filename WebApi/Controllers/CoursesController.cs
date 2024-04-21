using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(ApiContext apiContext) : ControllerBase
{
    private readonly ApiContext _ApiContext = apiContext;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _ApiContext.Courses.ToListAsync();
        return Ok(courses);
    }
}
