using library.Data.Models;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;

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
       public void SelectBibliographicmaterial()
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Bibliographicmaterial";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())   
                        {
                            var id = reader.GetValue(0);
                            var name = reader.GetValue(1);
                            var date = reader.GetValue(2);
                            var img = reader.GetValue(3);
                            var Authorid = reader.GetValue(4);
                            var Publisherid = reader.GetValue(5);

                            Console.WriteLine($"{id} \t {name} \t {date} \t{img} \t{Authorid}\t{Publisherid}");
                        }
                    }
                }
            }
        }
        public void SelectAuthor()
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Author";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())   
                        {
                            var id = reader.GetValue(0);
                            var fullname = reader.GetValue(1);
                            var contacts = reader.GetValue(2);
                            var information = reader.GetValue(3);

                            Console.WriteLine($"{id} \t {fullname} \t {contacts} \t {{information");
                        }
                    }
                }
            }
        }
        public void SelectPublisher()
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Publisher";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            var name = reader.GetValue(1);
                            var contacts = reader.GetValue(2);
                            var address = reader.GetValue(3);

                            Console.WriteLine($"{id} \t {name} \t {contacts} \t {address}");
                        }
                    }
                }
            }
        }
    }

}