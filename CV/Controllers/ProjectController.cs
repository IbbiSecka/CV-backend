using CV.DTO;
using CV.Repository;

namespace CV.Controllers
{
    public static class ProjectController
    {
        public static void configureProjectController(this WebApplication app)
        {
           
                var group = app.MapGroup("projects");
                group.MapGet("/", GetProjects);
                group.MapPost("/", CreateProject);
                group.MapPut("/{id}", UpdateProject);


        }

        private static async Task<IResult> GetProjects(IProject repo)
        {
            var projects = await repo.GetAll();

            // Convert to DTO
            var projectDtos = projects.Select(proj => new ProjectDTO
            {
                Name = proj.Name,
                Img = proj.Img,
                Description = proj.Description,
                Date = proj.Date,
                Role = proj.Role
            });

            return TypedResults.Ok(projectDtos);
        }

        private static async Task<IResult> CreateProject(IProject repo, Models.Project project)
        {
            var newProject = await repo.Create(project);

            // Convert to DTO before returning
            var projectDto = new ProjectDTO
            {
                Name = newProject.Name,
                Img = newProject.Img,
                Description = newProject.Description,
                Date = newProject.Date,
                Role = newProject.Role
            };

            return TypedResults.Created($"/projects/{newProject.Id}", projectDto);
        }

        private static async Task<IResult> UpdateProject(IProject repo, int id, Models.Project updatedProject)
        {
            var result = await repo.Update(id, updatedProject);
            if (result == null) return TypedResults.NotFound();

            // Convert to DTO before returning
            var updatedDto = new ProjectDTO
            {
                Name = result.Name,
                Img = result.Img,
                Description = result.Description,
                Date = result.Date,
                Role = result.Role
            };

            return TypedResults.Ok(updatedDto);
        }
    }
}

