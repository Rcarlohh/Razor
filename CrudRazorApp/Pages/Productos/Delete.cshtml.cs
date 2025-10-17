using CrudRazorApp.Data;
using CrudRazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CrudRazorApp.Pages.Productos
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        public DeleteModel(AppDbContext context) => _context = context;

        [BindProperty]
        public Producto Producto { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var pId = new SqlParameter("@Id", id);
            var list = await _context.Productos.FromSqlRaw("EXEC sp_GetProductoById @Id", pId).ToListAsync();
            Producto = list.FirstOrDefault() ?? new Producto();
            if (Producto.Id == 0) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var pId = new SqlParameter("@Id", Producto.Id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteProducto @Id", pId);
            return RedirectToPage("Index");
        }
    }
}
