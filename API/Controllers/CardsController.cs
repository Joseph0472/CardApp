using API.Data;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<IEnumerable<Card>> GetUsers()
    {
        var cards = _context.Cards.ToList();
        
        return cards; 
    }
    
    [HttpGet("{id}")] // /api/user/2
    public ActionResult<Card> GetCard(int id)
    {
        return _context.Cards.Find(id);
    }
}
