namespace BaseNet.Libs.Data.SDK.Repositories
{
    public interface UnitOfWork
    {
        public Task Commit();
    }
}