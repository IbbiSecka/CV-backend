using CV.Repository;
using CV.DTO;

namespace CV.Controllers
{
    public static class ResumeController
    {
        public static void configureResumeController(this WebApplication app)
        {
          
                var group = app.MapGroup("resume");
                group.MapGet("/", GetResumeExperiences);
                group.MapPost("/", CreateResumeExperience);
                group.MapPut("/{id}", UpdateResumeExperience);
                group.MapDelete("/{id}", DeleteResumeExperience);

        }
        private static int ExtractStartYear(string duration)
        {
            var parts = duration.Split('-');
            if (parts.Length < 1) return 0;

            var startYear = parts[0].Trim().Split(' ')[0]; // Extract "2024" from "2024 Aug -"
            return int.TryParse(startYear, out int year) ? year : 0;
        }

        private static async Task<IResult> GetResumeExperiences(IResume repo)
        {
            var experiences = await repo.GetResumeExperiences();

            // Convert to DTO and sort by start year (latest first)
            var experienceDtos = experiences.Select(exp => new ResumeDto
            {
                CompanyName = exp.CompanyName,
                CompanyLocation = exp.CompanyLocation,
                Position = exp.Position,
                Duration = exp.Duration,
                Description = exp.Description,
                IbbiId = exp.IbbiId
            })
            .OrderByDescending(x => ExtractStartYear(x.Duration)) // Sort by start year

            .ToList(); // Execute sorting

            return TypedResults.Ok(experienceDtos);
        }

        private static async Task<IResult> CreateResumeExperience(IResume repo, ResumeDto newExperience)
        {

            // Convert to DTO before returning
            var experience = new Models.ResumeExperience
            {
                CompanyName = newExperience.CompanyName,
                CompanyLocation = newExperience.CompanyLocation,
                Position = newExperience.Position,
                Duration = newExperience.Duration,
                Description = newExperience.Description,
                IbbiId = newExperience.IbbiId

            };
           
            var ret = await repo.AddExperience(experience);
            return TypedResults.Ok(ret);

        }

        private static async Task<IResult> UpdateResumeExperience(IResume repo, int id, ResumeDto newExperience)
        {
            if (newExperience == null) return TypedResults.BadRequest("Invalid data.");

            var updatedExperience = new Models.ResumeExperience
            {
                CompanyName = newExperience.CompanyName,
                CompanyLocation = newExperience.CompanyLocation,
                Position = newExperience.Position,
                Duration = newExperience.Duration,
                Description = newExperience.Description
            };

            var result = await repo.UpdateExperience(id, updatedExperience);
            if (result == null) return TypedResults.NotFound("Resume experience not found.");

            // Convert back to DTO before returning
            var updatedDto = new ResumeDto
            {
                CompanyName = result.CompanyName,
                CompanyLocation = result.CompanyLocation,
                Position = result.Position,
                Duration = result.Duration,
                Description = result.Description
            };

            return TypedResults.Ok(updatedDto);
        }

        private static async Task<IResult> DeleteResumeExperience(IResume repo, int id)
        {
            var success = await repo.DeleteExperience(id);
            return success ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
