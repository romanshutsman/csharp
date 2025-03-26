namespace api.Data {
    public class UserRepository : IUserRepository {
        DataContextEF _entityFramework;    

        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }
        public bool SaveChanges() {
            return this._entityFramework.SaveChanges() > 0;
        }
        public void Add<T>(T entity) {
            if (entity != null) {
                _entityFramework.Add(entity);
            }
        }
        public void Remove<T>(T entity) {
            if (entity != null) {
                _entityFramework.Remove(entity);
            }
        }
    }
}