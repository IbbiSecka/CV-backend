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
                Role = proj.Role,
                IbbiId = proj.IbbiId,
                PriorityView = proj.PriorityView
                
            });

            return TypedResults.Ok(projectDtos);
        }

        private static async Task<IResult> CreateProject(IProject repo, ProjectDTO dto)
        {

            var project = new Models.Project
            {
                Name = dto.Name,
                Img = dto.Img,
                Description = dto.Description,
                Date = dto.Date,
                Role = dto.Role,
                IbbiId = dto.IbbiId

            };
            await repo.Create(project);
            return TypedResults.Ok(project);
            
        }

        private static async Task<IResult> UpdateProject(IProject repo, int id, ProjectDTO updatedProject)
        {
            if (updatedProject == null)
                return TypedResults.BadRequest("Invalid data.");

            var project = await repo.GetOne(id);
            if (project == null)
                return TypedResults.NotFound("Project not found.");

            // Update existing project instead of creating a new one
            project.Name = updatedProject.Name;
            project.Img = updatedProject.Img;
            project.Description = updatedProject.Description;
            project.Date = updatedProject.Date;
            project.Role = updatedProject.Role;

            var result = await repo.Update(id, project);
            if (result == null)
                return TypedResults.Problem("Failed to update project.");

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

