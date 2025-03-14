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
        private static async Task<IResult> GetResumeExperiences(IResume repo)
        {
            var experiences = await repo.GetResumeExperiences();

            // Convert to DTO
            var experienceDtos = experiences.Select(exp => new ResumeDto
            {
                CompanyName = exp.CompanyName,
                CompanyLocation = exp.CompanyLocation,
                Position = exp.Position,
                Duration = exp.Duration,
                Description = exp.Description
            });

            return TypedResults.Ok(experienceDtos);
        }

        private static async Task<IResult> CreateResumeExperience(IResume repo, Models.ResumeExperience experience)
        {
            var newExperience = await repo.AddExperience(experience);

            // Convert to DTO before returning
            var experienceDto = new ResumeDto
            {
                CompanyName = newExperience.CompanyName,
                CompanyLocation = newExperience.CompanyLocation,
                Position = newExperience.Position,
                Duration = newExperience.Duration,
                Description = newExperience.Description
            };

            return TypedResults.Created($"/resume/{newExperience.Id}", experienceDto);
        }

        private static async Task<IResult> UpdateResumeExperience(IResume repo, int id, Models.ResumeExperience updatedExperience)
        {
            var result = await repo.UpdateExperience(id, updatedExperience);
            if (result == null) return TypedResults.NotFound();

            // Convert to DTO before returning
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
