using CV.DTO;
using CV.Models;
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
                Name = lang.Name,
                IbbiId = lang.IbbiId
            });

            return TypedResults.Ok(languageDtos); 
        }

        private static async Task<IResult> CreateLanguage(ILanguage repo, LanguageDTO dto)
        {
            

            // Convert to DTO before returning
            var language = new Models.Language
            {
                Name = dto.Name,
                IbbiId = dto.IbbiId
            };
            await repo.Create(language);
            return TypedResults.Ok(language);
        }

        private static async Task<IResult> DeleteLanguage(ILanguage repo, int id)
        {
            var success = await repo.Delete(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
