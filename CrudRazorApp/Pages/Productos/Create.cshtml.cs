using CrudRazorApp.Data;
using CrudRazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CrudRazorApp.Pages.Productos
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context) => _context = context;

        [BindProperty]
        public Producto Producto { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var pNombre = new SqlParameter("@Nombre", Producto.Nombre);
            var pPrecio = new SqlParameter("@Precio", Producto.Precio);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertProducto @Nombre, @Precio", pNombre, pPrecio);
            return RedirectToPage("Index");
        }
    }
}
