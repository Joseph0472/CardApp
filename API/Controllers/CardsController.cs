using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// /api/cards
public class CardsController : BaseApiController
{
    private readonly DataContext _context;

    public CardsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Card>>> GetCards()
    {
        var cards = await _context.Cards.ToListAsync();
        
        return cards; 
    }
    
    [HttpGet("{id}")] // /api/card/2
    public async Task<ActionResult<Card>> GetCard(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
}
