
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

            // Projects


            // Social Media
           
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
            return  TypedResults.Ok(me);
        }

      
       
    }
}
