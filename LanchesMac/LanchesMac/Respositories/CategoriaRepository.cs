using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Respositories.Interfaces;

namespace LanchesMac.Respositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
