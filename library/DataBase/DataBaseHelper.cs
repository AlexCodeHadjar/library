using library.Data.Models;
using Microsoft.Data.Sqlite;

namespace library.DataBase
{
    public class DatabaseHelper
    {
        ///<summary>
        ///получение строки подключения
        /// </summary>// 
        private string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddBibliographicMaterial(Bibliographicmaterial material)
        {
            ///<summary>
            ///Вставка новой записи в таблицу "BibliographicMaterial"
            /// </summary>// 
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

               
                command.CommandText = "INSERT INTO BibliographicMaterial (Name, Date, Img, AuthorId, PublisherId) VALUES (@Name, @Date, @Img, @AuthorId, @PublisherId)";
                command.Parameters.AddWithValue("@Name", material.name);
                command.Parameters.AddWithValue("@Date", material.date);
                command.Parameters.AddWithValue("@Img", material.img);
                command.Parameters.AddWithValue("@AuthorId", material.author.id);
                command.Parameters.AddWithValue("@PublisherId", material.publisher.id);

                command.ExecuteNonQuery();

           
            }
        }
        public void AddPublisher(Publisher publisher)
        {
            ///<summary>
            ///Вставка новой записи в таблицу ""Publisher"
            /// </summary>//  
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
               
                command.CommandText = "INSERT INTO Publisher (Name, Contacts, Address) VALUES (@Name, @Contacts, @Address)";
                command.Parameters.AddWithValue("@Name", publisher.name);
                command.Parameters.AddWithValue("@Contacts", publisher.contacts);
                command.Parameters.AddWithValue("@Address", publisher.address);

                command.ExecuteNonQuery();

            }
        }
        public void AddAuthor(Author author)
        {
            ///<summary>
            ///Вставка новой записи в таблицу ""Author"
            /// </summary>//  
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

               
                command.CommandText = "INSERT INTO Author (FullName, Contacts, Information) VALUES (@FullName, @Contacts, @Information)";
                command.Parameters.AddWithValue("@FullName", author.fullName);
                command.Parameters.AddWithValue("@Contacts", author.contacts);
                command.Parameters.AddWithValue("@Information", author.information);

                command.ExecuteNonQuery();

              
            }
        }
    }

}