using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetChat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetChat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Message> Messages { get; set; }

        public async Task OnGetAsync()
        {
            Messages = await _context.Messages.ToListAsync();
        }
    }
}
