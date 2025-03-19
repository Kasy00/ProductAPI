public interface IForbiddenPhraseService
{
    Task<IEnumerable<string>> GetAllForbiddenPhraseAsync();
    Task<ForbiddenPhrase?> GetForbiddenPhraseByIdAsync(int id);
    Task<ForbiddenPhrase> CreateForbiddenPhraseAsync(string phrase);
    Task DeleteForbiddenPhraseAsync(int id);
}