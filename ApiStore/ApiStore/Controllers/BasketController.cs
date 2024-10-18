using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiStore.Data;
using ApiStore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ApiStoreDbContext _context;

        public BasketController(ApiStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add product to basket
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="productId">Product's ID</param>
        /// <param name="quantity">Quantity of product</param>
        /// <returns>ActionResult with status</returns>
        [HttpPost("add-to-basket")]
        public async Task<IActionResult> AddToBasket(int userId, int productId, int quantity)
        {
            // Existing logic for adding product to basket...
            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    BasketItems = new List<BasketItem>()
                };
                _context.Baskets.Add(basket);
                await _context.SaveChangesAsync();
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var basketItem = basket.BasketItems.FirstOrDefault(item => item.ProductId == productId);
            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                basketItem = new BasketItem
                {
                    BasketId = basket.BasketId,
                    ProductId = productId,
                    Quantity = quantity
                };
                basket.BasketItems.Add(basketItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Product added to basket");
        }

        /// <summary>
        /// Get all products in the user's basket
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>List of basket items with products</returns>
        [HttpGet("get-basket")]
        public async Task<ActionResult<IEnumerable<object>>> GetBasket(int userId)
        {
            // Find the user's basket
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
            {
                return NotFound("Basket is empty or not found");
            }

            // Return a list of products in the basket with their quantities
            var basketItems = basket.BasketItems.Select(bi => new
            {
                bi.ProductId,
                bi.Product.Name,
                bi.Product.Price,
                bi.Quantity
            }).ToList();

            return Ok(basketItems);
        }
    }
}
