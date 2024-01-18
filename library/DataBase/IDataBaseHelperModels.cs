
namespace library.DataBase
{
    public interface IDataBaseHelperModels<T>
    {
        /// <summary>
        /// Выборка объектов в базе данных
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<T> Select(T model = default(T) );
        /// <summary>
        /// Удаление объекта в базе данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        /// Обновление объекта в базе данных 
        /// </summary>
        /// <param name="model"></param>
        public void Update(T model = default(T) );
        /// <summary>
        /// Добавление нового объекта в базу данных
        /// </summary>
        /// <param name="model"></param>
        public void Insert(T model = default(T) );
     
    }
}
