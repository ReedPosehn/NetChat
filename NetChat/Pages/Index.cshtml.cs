using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetChat.Models;
using Microsoft.AspNetCore.Mvc;
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

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Messages.AsQueryable();

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                query = query.Where(m => m.Timestamp.Date == date);
            }

            Messages = await query.ToListAsync();
        }
    }
}
