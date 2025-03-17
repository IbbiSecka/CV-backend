using CV.Repository;
using CV.DTO;
using CV.Models;
using CV.Data;

namespace CV.Controllers
{
    public static class EducationController
    {

        
        public static void configureEducationController(this WebApplication app)
        {
            var group = app.MapGroup("Education");
            group.MapGet("/", GetEducations);
            group.MapPost("/", CreateEducation);
            group.MapPut("/{userId}/{educationId}", UpdateEducation);
        }
       

        private static async Task<IResult> GetEducations(IEducation repo)
        {
            var educations = await repo.GetAll();
            if(educations == null)
            {
                return TypedResults.BadRequest("List is empty");
            }
            // Convert to DTO
            var educationDtos = educations.Select(edu => new EduDTO
            {
                EducationName = edu.EducationName,
                Description = edu.Description,
                EducationSite = edu.EducationSite,
                Degree = edu.Degree,
                Duration = edu.Duration,
                IbbiId = edu.IbbiId
            });

            return TypedResults.Ok(educationDtos);
        }
        private static async Task<IResult> CreateEducation(IEducation repo, EduDTO dto)
        {
            if (dto.EducationName == null || dto.EducationSite == "" || dto.Degree == "")
            {
                return TypedResults.BadRequest("No empty fields allowed.");
            }
           

            // Convert to DTO before returning
            var education = new Models.Education
            {
                EducationName = dto.EducationName,
                Description = dto.Description,
                EducationSite = dto.EducationSite,
                Degree = dto.Degree,
                Duration = dto.Duration,
                IbbiId = dto.IbbiId,
               
            };

            
            await repo.Create(education);

            return TypedResults.Ok();
        }

        private static async Task<IResult> UpdateEducation(IEducation repo, int userId, int educationId, EduDTO dto )
        {
            if (dto == null) return TypedResults.BadRequest("Invalid data.");

            var updatedEducation = new Models.Education
            {
                EducationName = dto.EducationName,
                Description = dto.Description,
                EducationSite = dto.EducationSite,
                Degree = dto.Degree,
                Duration = dto.Duration,
                IbbiId = dto.IbbiId

            };

            var result = await repo.Update(userId, educationId, updatedEducation);

            if (result == null) return TypedResults.NotFound("Education entry not found or does not belong to the user.");

            var updatedDto = new EduDTO
            {
                EducationName = result.EducationName,
                Description = result.Description,
                EducationSite = result.EducationSite,
                Degree = result.Degree,
                Duration = result.Duration,
                IbbiId = result.IbbiId
            };

            return TypedResults.Ok(updatedDto);
        }
    }
}
