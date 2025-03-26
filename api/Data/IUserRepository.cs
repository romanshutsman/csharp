namespace api.Data
{
    public interface IUserRepository {
        public bool SaveChanges();
        public void Add<T>(T entity);
        public void Remove<T>(T entity);
    }
}