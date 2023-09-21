using System.Globalization;
using System.Security.Cryptography;
using System.Text;
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

    [HttpGet] // Get all stored cards: /api/cards
    public async Task<ActionResult<IEnumerable<Card>>> GetCards()
    {
        var cards = await _context.Cards.ToListAsync();
        
        return cards; 
    }
    
    [HttpGet("{id}")] // Get a card by Id /api/card/1
    public async Task<ActionResult<Card>> GetCard(int id)
    {
        return await _context.Cards.FindAsync(id);
    }

    [HttpPost("add")] // POST: api/account/register
    public async Task<ActionResult<Card>> Register(InputCard card)
    {
        using var hmac = new HMACSHA512();
        var cardToAdd = new Card
        {
            CreditNumber = card.CreditNumber,
            CVCHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(card.CVC)),
            CVCSalt = hmac.Key,
            ExpiryDateMonth = card.ExpiryDateMonth,
            ExpiryDateDay = card.ExpiryDateDay
        };

        _context.Cards.Add(cardToAdd);
        await _context.SaveChangesAsync();
        return cardToAdd;
    }
}
