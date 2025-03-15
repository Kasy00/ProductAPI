public interface IForbiddenPhraseRepository : IGenericRepository<ForbiddenPhrase>
{
    Task<IEnumerable<string>> GetAllForbiddenPhrasesAsync();
}