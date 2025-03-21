
using CV.DTO;
using CV.Models;
using CV.Repository;

namespace CV.Controllers
{
    public static class IbbiController
    {
        public static void configureIbbiController(this WebApplication app)
        {
            var group = app.MapGroup("/user");
            group.MapGet("/", GetIbbz);
            group.MapPut("/", UpdateIbz);
            group.MapGet("/all", GetWholeIbbi);

            // Projects


            // Social Media
           
        }

        private static async Task<IResult> GetWholeIbbi(IUser repo)
        {
            try
            {
                // Check if repo is null
                if (repo == null)
                {
                    return TypedResults.Problem("The user repository is unavailable.", statusCode: 500);
                }

                var getIbbi = await repo.GetIbbi();

                // Check if the Ibbi object is null (not found in DB)
                if (getIbbi == null)
                {
                    return TypedResults.NotFound("User data could not be retrieved.");
                }

                // Ensure required fields are not null
                if (string.IsNullOrEmpty(getIbbi.FirstName) || string.IsNullOrEmpty(getIbbi.Description))
                {
                    return TypedResults.Problem("Essential user information is missing.", statusCode: 500);
                }

                var simplifiedIbbi = new IbbiDTO
                {
                    FirstName = getIbbi.FirstName,
                    Description = getIbbi.Description,
                    DOB = getIbbi.DOB,
                    Img = getIbbi.Img,
                    Languages = getIbbi.Languages?.Select(ib => new LanguageDTO { Name = ib.Name }).ToList() ?? new List<LanguageDTO>(),
                    Projects = getIbbi.Projects?.Select(ib => new ProjectDTO
                    {
                        Name = ib.Name,
                        Description = ib.Description,
                        Img = ib.Img,
                        Date = ib.Date,
                        Role = ib.Role
                    }).ToList() ?? new List<ProjectDTO>(),
                    ResumeExperiences = getIbbi.resumeExperiences?.Select(ib => new ResumeDto
                    {
                        CompanyName = ib.CompanyName,
                        CompanyLocation = ib.CompanyLocation,
                        Position = ib.Position,
                        Duration = ib.Duration,
                        Description = ib.Description
                    }).ToList() ?? new List<ResumeDto>(),
                    Socials = getIbbi.Socials?.Select(ib => new DTO.NewFolder.SocialDTO
                    {
                        Link = ib.Img,
                        Name = ib.Name
                    }).ToList() ?? new List<DTO.NewFolder.SocialDTO>()
                };

                return TypedResults.Ok(simplifiedIbbi);
            }
            catch (Exception ex)
            {
                // Log the error (optional: log to file, database, or console)
                Console.WriteLine($"Error in GetWholeIbbi: {ex.Message}");

                return TypedResults.Problem("An unexpected error occurred while retrieving user data.", statusCode: 500);
            }
        }

        private static async Task<IResult> UpdateIbz(IUser repo, int id)
        {
            throw null;
        }

        private static async Task<IResult> GetIbbz(IUser repo)
        {

            var me = await repo.GetIbbi();
            if (me == null)
            {
                return TypedResults.NotFound("User not fetched");
            }
            var meToDTO = new GetUserDTO
            {
                FirstName = me.FirstName,
                Description = me.Description,
                DOB = me.DOB
            };
            return  TypedResults.Ok(meToDTO);
        }


      
       
    }
}
