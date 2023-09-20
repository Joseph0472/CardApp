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
    public async Task<ActionResult<Card>> Register(
        string creditNumber, 
        string cvc,
        int expiryDateMonth,
        int expiryDateDay
        )
    {
        using var hmac = new HMACSHA512();
        var card = new Card
        {
            CreditNumber = creditNumber,
            CVCHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(cvc)),
            CVCSalt = hmac.Key,
            ExpiryDateMonth = expiryDateMonth,
            ExpiryDateDay = expiryDateDay
        };

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return card;
    }
}
