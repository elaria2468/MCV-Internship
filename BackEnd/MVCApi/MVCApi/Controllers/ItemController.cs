using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApi.Models;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace MVCApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 لازم يكون فيه Token عشان يدخل
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.Include(i => i.Category).ToListAsync();
        }

        // ✅ GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) return NotFound();
            return item;
        }

        // ✅ POST: api/items
        [HttpPost]
        [Authorize(Roles = "Admin")] // 👈 بس الـ Admin يقدر يضيف
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // ✅ PUT: api/items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
