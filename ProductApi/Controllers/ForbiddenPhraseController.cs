using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ForbiddenPhraseController : ControllerBase
{
        private readonly IForbiddenPhraseService _forbiddenPhraseService;

    public ForbiddenPhraseController(IForbiddenPhraseService forbiddenPhraseService)
    {
        _forbiddenPhraseService = forbiddenPhraseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllForbiddenPhrases()
    {
        var forbiddenPhrases = await _forbiddenPhraseService.GetAllForbiddenPhraseAsync();
        return Ok(forbiddenPhrases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetForbiddenPhraseById(int id)
    {
        var forbiddenPhrase = await _forbiddenPhraseService.GetForbiddenPhraseByIdAsync(id);
        if (forbiddenPhrase != null)
        {
            return Ok(forbiddenPhrase);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateForbiddenPhrase([FromBody] ForbiddenPhrase forbiddenPhrase)
    {
        try
        {
            var createdPhrase = await _forbiddenPhraseService.CreateForbiddenPhraseAsync(forbiddenPhrase.Phrase);
            return CreatedAtAction(nameof(GetForbiddenPhraseById), new { id = createdPhrase.Id }, createdPhrase);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForbiddenPhrase(int id)
    {
        await _forbiddenPhraseService.DeleteForbiddenPhraseAsync(id);
        return NoContent();
    }
}