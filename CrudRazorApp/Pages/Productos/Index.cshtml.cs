using CrudRazorApp.Data;
using CrudRazorApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CrudRazorApp.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context) => _context = context;

        public IList<Producto> ListaProductos { get; set; } = new List<Producto>();

        public async Task OnGetAsync()
        {
            ListaProductos = await _context.Productos
                .FromSqlRaw("EXEC sp_GetProductos")
                .ToListAsync();
        }
    }
}
