namespace CV.Repository
{
    public interface ILanguage
    {
        Task<IEnumerable<Models.Language>> GetAll();
        Task<Models.Language> Create(Models.Language language);
        Task<bool> Delete(int id);
    }
}
