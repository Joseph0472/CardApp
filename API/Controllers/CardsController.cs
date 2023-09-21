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
    
    [HttpGet("{creditNumber}")] // Get a card by Id /api/card/1
    public async Task<ActionResult<Card>> GetCard(string creditNumber)
    {
        return await _context.Cards.FirstOrDefaultAsync(e => e.CreditNumber == creditNumber);
    }

    [HttpPost("add")] // POST: api/account/register
    public async Task<ActionResult<Card>> Register(InputCard card)
    {
        using var hmac = new HMACSHA512();
        // Validation (because of time limits, implementing in simple ways)
        if(card.Name.Length > 50)
        {
            return BadRequest("Invalid name.");
        }
        if(!int.TryParse(card.ExpiryDateYear, out int ExpiryDateYear) 
            || ExpiryDateYear < 0 || ExpiryDateYear > 99)
        {
            return BadRequest("Invalid year.");
        }
        if(!int.TryParse(card.ExpiryDateMonth, out int parsedMonth) 
            || parsedMonth < 1 || parsedMonth > 12)
        {
            return BadRequest("Invalid month.");
        }
        if(!int.TryParse(card.CVC, out int cvc))
        {
            return BadRequest("Invalid month.");
        }
        var cardToAdd = new Card
        {
            Name = card.Name,
            CreditNumber = card.CreditNumber,
            CVCHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(card.CVC)),
            CVCSalt = hmac.Key,
            ExpiryDateYear = card.ExpiryDateYear,
            ExpiryDateMonth = card.ExpiryDateMonth
        };

        _context.Cards.Add(cardToAdd);
        await _context.SaveChangesAsync();
        return cardToAdd;
    }
}
