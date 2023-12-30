using library.Data.Models;
using Library.Data.interfaces;
using Microsoft.Data.Sqlite;

namespace library.DataBase
{
    ///<summary>
    ///класс для работы с базой данных CatalogsData
    /// </summary>// 
    public class DatabaseHelper
    {
        //получение строки подключения
        private string _connectionString;
      
      
       
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        ///<summary>
        ///Вставка новой записи в таблицу "BibliographicMaterial"
        /// </summary>// 
        public void AddBibliographicMaterial(Bibliographicmaterial material)
        {
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

               
                command.CommandText = "INSERT INTO BibliographicMaterial (Name, Date, Img, AuthorId, PublisherId) VALUES (@Name, @Date, @Img, @AuthorId, @PublisherId)";
                command.Parameters.AddWithValue("@Name", material.Name);
                command.Parameters.AddWithValue("@Date", material.Date);
                command.Parameters.AddWithValue("@Img", material.Img);
                command.Parameters.AddWithValue("@AuthorId", material.Author.Id);
                command.Parameters.AddWithValue("@PublisherId", material.Publisher.Id);

                command.ExecuteNonQuery();

           
            }
        }
        ///<summary>
        ///Вставка новой записи в таблицу "Publisher"
        /// </summary>//  
        public void AddPublisher(Publisher publisher)
        {
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
               
                command.CommandText = "INSERT INTO Publisher (Name, Contacts, Address) VALUES (@Name, @Contacts, @Address)";
                command.Parameters.AddWithValue("@Name", publisher.Name);
                command.Parameters.AddWithValue("@Contacts", publisher.Contacts);
                command.Parameters.AddWithValue("@Address", publisher.Address);

                command.ExecuteNonQuery();

            }
        }
        ///<summary>
        ///Вставка новой записи в таблицу "AddAuthor"
        /// </summary>//  

        public void AddAuthor(Author author)
        {
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

               
                command.CommandText = "INSERT INTO Author (FullName, Contacts, Information) VALUES (@FullName, @Contacts, @Information)";
                command.Parameters.AddWithValue("@FullName", author.FullName);
                command.Parameters.AddWithValue("@Contacts", author.Contacts);
                command.Parameters.AddWithValue("@Information", author.Information);

                command.ExecuteNonQuery();

              
            }
        }
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial()
        {
            List<Bibliographicmaterial> bibliographicmaterialsDatebase = new List<Bibliographicmaterial>();
           
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
                            var id = reader.GetInt32(0);
                            var name = reader.GetString(1);
                            var date = reader.GetString(2);
                            var img = reader.GetString(3);
                            var Authorid = reader.GetInt32(4);
                            var Publisherid = reader.GetInt32(5);
                            var author = SelectAuthor().FirstOrDefault(a => a.Id == Authorid);
                            var publisher = SelectPublisher().FirstOrDefault(p => p.Id == Publisherid);
                            bibliographicmaterialsDatebase.Add(new Bibliographicmaterial()
                            {
                                Id = id,
                                Name = name,
                                Date = date,
                                Img = img,
                                Author = author,
                                Publisher = publisher

                            });



                        }
                        
                    }
                    return bibliographicmaterialsDatebase;
                }
            }
        }
        ///<summary>
        ///перебор всех обьектов Author из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<Author> SelectAuthor()
        {
            List<Author> authorList = new List<Author>();
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
                            var id = reader.GetInt32(0);
                            var fullname = reader.GetString(1);
                            var contacts = reader.GetString(2);
                            var information = reader.GetString(3);
                            authorList.Add(new Author()
                            {
                                Id = id,
                                FullName = fullname,
                                Contacts = contacts,
                                Information = information

                            }); 
                        }
                    }
                    return authorList;
                }
            }
        }
        ///<summary>
        ///перебор всех обьектов Publisher из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<Publisher> SelectPublisher()
        {
            List<Publisher> publisherList = new List<Publisher>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Publisher";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())   
                        {
                            var id = reader.GetInt32(0);
                            var name = reader.GetString(1);
                            var contacts = reader.GetString(2);
                            var address = reader.GetString(3);
                            publisherList.Add(new Publisher()
                            {
                                Id = id,
                                Name = name,
                                Contacts = contacts,
                                Address = address
                            });
                        }
                    }
                    return publisherList;
                }
            }
        }
    }

}