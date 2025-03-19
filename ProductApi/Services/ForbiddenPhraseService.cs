public class ForbiddenPhraseService : IForbiddenPhraseService
{
    private readonly IForbiddenPhraseRepository _forbiddenPhraseRepository;

    public ForbiddenPhraseService(IForbiddenPhraseRepository forbiddenPhraseRepository)
    {
        _forbiddenPhraseRepository = forbiddenPhraseRepository;
    }

    public async Task<IEnumerable<string>> GetAllForbiddenPhraseAsync()
    {
        return await _forbiddenPhraseRepository.GetAllForbiddenPhrasesAsync();
    }

    public async Task<ForbiddenPhrase?> GetForbiddenPhraseByIdAsync(int id)
    {
        return await _forbiddenPhraseRepository.GetByIdAsync(id);
    }

    public async Task<ForbiddenPhrase> CreateForbiddenPhraseAsync(string phrase)
    {
        var forbiddenPhrase = new ForbiddenPhrase { Phrase = phrase };
        await _forbiddenPhraseRepository.AddAsync(forbiddenPhrase);
        return forbiddenPhrase;
    }

    public async Task DeleteForbiddenPhraseAsync(int id)
    {
        await _forbiddenPhraseRepository.DeleteAsync(id);
    }
}