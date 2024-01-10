using library.Data.Models;
using library.Data.interfaces;
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
            if (material.Author.Id == null || material.Publisher.Id == null || material.Date == null || material.Name == null || material.Img == null)
            {
              
                throw new ArgumentException("Author and Publisher must be set for the Bibliographicmaterial.");
            }
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
        ///Вставка новой записи в таблицу "AddUser"
        /// </summary>//
        public void AddUser(User material)
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;


                command.CommandText = "INSERT INTO User (Login, Password,Admin) VALUES (@login, @password,@Admin)";
                command.Parameters.AddWithValue("@login", material.Login);
                command.Parameters.AddWithValue("@password", material.Password);
                command.Parameters.AddWithValue("@Admin", material.Admin);


                command.ExecuteNonQuery();


            }
        }
        ///<summary>
        ///перебор всех обьектов Bibliographicmaterial из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial()
        {
            List<Bibliographicmaterial> bibliographicmaterialsDatebase = new List<Bibliographicmaterial>();
            string sqlExpression;


            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                sqlExpression = "SELECT * FROM Bibliographicmaterial";

             
               
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
        ///перебор всех обьектов User из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<User> SelectUser()
        {
            List<User> userList = new List<User>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM User";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id= reader.GetInt32(0);
                            var login = reader.GetString(1);
                            var password = reader.GetString(2);
                            var admin = reader.GetString(3);

                            userList.Add(new User()
                            {
                                Id = id,
                                Login = login,
                                Password = password,
                                Admin = admin
                              

                            });
                        }
                    }
                    return userList;
                }
            }
        }
        ///<summary>
        ///перебор всех обьектов Author из базы данных Catalogsdata
        /// </summary>
        public IEnumerable<Author> SelectAuthor(string nameAuthor =null)
        {
            List<Author> authorList = new List<Author>();
            string sqlExpression;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                if (nameAuthor == null)
                {
                     sqlExpression = "SELECT * FROM Author ";
                }
                else
                {
                    sqlExpression = $"SELECT * FROM Author WHERE fullname = '{nameAuthor}'";
                }
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
        public IEnumerable<Publisher> SelectPublisher(string namePublisher = null)
        {
            List<Publisher> publisherList = new List<Publisher>();
            string sqlExpression;

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                if (namePublisher == null)
                {
                    sqlExpression = "SELECT * FROM Publisher";
                }
                else
                {
                    sqlExpression = $"SELECT * FROM Publisher WHERE Name = '{namePublisher}'";
                }
              
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

        public void UpdateBibliographicmaterial(int idBibliographicmaterial, string nameBibliographicmaterial = null,
       string nameAuthor = null, string namePublisher = null, string date = null)
        {
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                if (nameBibliographicmaterial == null && date == null && nameAuthor == null && namePublisher == null)
                {
                
                    return;
                }



                string sqlExpression = "UPDATE BibliographicMaterial SET ";


                if (nameBibliographicmaterial != null)
                    sqlExpression += $"`Name` = '{nameBibliographicmaterial}', ";

                if (date != null)
                    sqlExpression += $"`Date` = '{date}', ";

                if (nameAuthor != null)
                    sqlExpression += $"`AuthorId` = '{int.Parse(nameAuthor)}', ";

                if (namePublisher != null)
                    sqlExpression += $"`PublisherId` = '{int.Parse(namePublisher)}', ";

                sqlExpression = sqlExpression.TrimEnd(',', ' ');
                sqlExpression += $" WHERE Id = '{idBibliographicmaterial}'";

                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        public void UpdateAuthor(int idAuthor, string nameAuthor = null, string contactsAuthor = null, string informationAuthor = null)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;




                string sqlExpression = "UPDATE Author SET ";


                if (nameAuthor != null)
                    sqlExpression += $"`FullName` = '{nameAuthor}', ";

                if (contactsAuthor != null)
                    sqlExpression += $"`Contacts` = '{contactsAuthor}', ";

                if (informationAuthor != null)
                    sqlExpression += $"`Information` = '{informationAuthor}', ";

                sqlExpression = sqlExpression.TrimEnd(',', ' ');
                sqlExpression += $" WHERE Id = '{idAuthor}'";

                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        public void UpdatePublisher(int idPublisher, string namePublisher = null, string contactsPublisher = null, string addressPublisher = null)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;




                string sqlExpression = "UPDATE Publisher SET ";


                if (namePublisher != null)
                    sqlExpression += $"`Name` = '{namePublisher}', ";

                if (contactsPublisher != null)
                    sqlExpression += $"`Contacts` = '{contactsPublisher}', ";

                if (addressPublisher != null)
                    sqlExpression += $"`Address` = '{addressPublisher}', ";

                sqlExpression = sqlExpression.TrimEnd(',', ' ');
                sqlExpression += $" WHERE Id = '{idPublisher}'";

                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        public void DeleteBibliographicmaterial(int idBibliographicmaterial)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                string sqlExpression = $"DELETE FROM BibliographicMaterial WHERE Id = {idBibliographicmaterial}";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        public void DeleteAuthor(int idAuthor)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Проверяем, используется ли идентификатор автора в таблице BibliographicMaterial
                string checkUsageQuery = $"SELECT COUNT(*) FROM BibliographicMaterial WHERE AuthorId = {idAuthor}";
                using (var checkUsageCommand = new SqliteCommand(checkUsageQuery, connection))
                {
                    int usageCount = Convert.ToInt32(checkUsageCommand.ExecuteScalar());

                    // Если идентификатор автора используется в таблице BibliographicMaterial, прерываем удаление
                    if (usageCount > 0)
                    {

                        return;
                    }

                }
                // Если идентификатор автора не используется, выполняем удаление
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                string sqlExpression = $"DELETE FROM Author WHERE Id = {idAuthor}";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();

            }
        }
        public void DeletePublisher(int idPublisher)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Проверяем, используется ли идентификатор автора в таблице BibliographicMaterial
                string checkUsageQuery = $"SELECT COUNT(*) FROM BibliographicMaterial WHERE PublisherId = {idPublisher}";
                using (var checkUsageCommand = new SqliteCommand(checkUsageQuery, connection))
                {
                    int usageCount = Convert.ToInt32(checkUsageCommand.ExecuteScalar());

                    // Если идентификатор издательства используется в таблице BibliographicMaterial, прерываем удаление
                    if (usageCount > 0)
                    {

                        return;
                    }

                }
                // Если идентификатор издательства не используется, выполняем удаление
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                string sqlExpression = $"DELETE FROM Publisher WHERE Id = {idPublisher}";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();

            }
        }
        
        public void SortBibliographicmaterial(string nameBibliographicmaterial)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                string sqlExpression  = $"SELECT * FROM BibliographicMaterial ORDER BY {nameBibliographicmaterial} ASC";
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
    }

}