using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/cards
public class CardsController : ControllerBase
{
    private readonly DataContext _context;

    public CardsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Card>>> GetUsers()
    {
        var cards = await _context.Cards.ToListAsync();
        
        return cards; 
    }
    
    [HttpGet("{id}")] // /api/user/2
    public async Task<ActionResult<Card>> GetCard(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
}
