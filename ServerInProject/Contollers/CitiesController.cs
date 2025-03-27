using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerInProject;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;




[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ProrjdContext _context; 

    public CitiesController(ProrjdContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities(string query)
    {
        var cities = await _context.Городаs
            .Where(c => c.НазваниеГорода.StartsWith(query)) 
            .Select(c => new { Id = c.IdГорода, Name = c.НазваниеГорода }) 
            .ToListAsync();

        return Ok(cities);
    }
}
