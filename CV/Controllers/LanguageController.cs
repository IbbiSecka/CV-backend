using CV.DTO;
using CV.Repository;

namespace CV.Controllers
{
    public static class LanguageController
    {
        public static void configureLanguageController(this WebApplication app)
        {
          
                var group = app.MapGroup("language");
                group.MapGet("/", GetLanguages);
                group.MapPost("/", CreateLanguage);
                group.MapDelete("/{id}", DeleteLanguage);


        }
        private static async Task<IResult> GetLanguages(ILanguage repo)
        {
            var languages = await repo.GetAll();

            // Convert to DTO
            var languageDtos = languages.Select(lang => new LanguageDTO
            {
                Name = lang.Name
            });

            return TypedResults.Ok(languageDtos); return TypedResults.Ok(languages);
        }

        private static async Task<IResult> CreateLanguage(ILanguage repo, Models.Language language)
        {
            var newLanguage = await repo.Create(language);

            // Convert to DTO before returning
            var languageDto = new LanguageDTO
            {
                Name = newLanguage.Name
            };
            return TypedResults.Ok();
        }

        private static async Task<IResult> DeleteLanguage(ILanguage repo, int id)
        {
            var success = await repo.Delete(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
