using Microsoft.EntityFrameworkCore;

public class ForbiddenPhraseRepository : GenericRepository<ForbiddenPhrase>, IForbiddenPhraseRepository
{
    public ForbiddenPhraseRepository(ProductDbContext context) : base(context) { }

    public async Task<IEnumerable<string>> GetAllForbiddenPhrasesAsync()
    {
        return await _dbSet
            .Select(p => p.Phrase)
            .ToListAsync();
    }
}