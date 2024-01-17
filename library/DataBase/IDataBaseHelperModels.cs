namespace library.DataBase
{
    public interface IDataBaseHelperModels<T>
    {
        public IEnumerable<T> Select(T model = default(T) );
        public void Delete(int id);
        public void Update(T model = default(T) );
        public void Insert(T model = default(T) );
    }
}
