
using Domain.Entities;
using Domain.Interface;
using Persistence.Data;

namespace Application.Repository;

public class MarcaRepository : BaseRepository<Marca>, IMarca
{
    public MarcaRepository(PDFGeneratorContext context) : base(context)
    {
    }
    
}
