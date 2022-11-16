using LanchesMac.Models;

namespace LanchesMac.Respositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias {get;}
    }
}
