using library.Data.Models;
using library.ViewModels;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
//using System.Security.Policy;

//using System.Security.Policy;


//using System.Security.Policy;

//using System.Security.Policy;
using static library.Controllers.HomeController;
using static library.DataBase.DatabaseHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace library.DataBase
{
  
    ///<summary>
    ///класс для работы с базой данных CatalogsData
    /// </summary>// 
    public class DatabaseHelper
    {
     

        public class SortBy
        {
            public bool SortNameBibliographicmaterial { get; set; }
            public bool SortNameAuthor { get; set; }
            public bool SortNamePublisher { get; set; }
            public bool SortDate { get; set; }
        }
        //получение строки подключения
        public static string _connectionString;
      



        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
         
        }
        public class DataBaseAuthor : IDataBaseHelperModels<Author>
        {
            public void Delete(int idAuthor)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                 
                    string checkUsageQuery = $"SELECT COUNT(*) FROM BibliographicMaterial WHERE AuthorId = {idAuthor}";
                    using (var checkUsageCommand = new SqliteCommand(checkUsageQuery, connection))
                    {
                        int usageCount = Convert.ToInt32(checkUsageCommand.ExecuteScalar());

                  
                        if (usageCount > 0)
                        {

                            return;
                        }

                    }
                  
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;

                    string sqlExpression = $"DELETE FROM Author WHERE Id = {idAuthor}";
                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();

                }
            }


            public void Insert(Author author)
                {
                    if (author.FullName == null || author.Contacts == null || author.Information == null)
                    {
                        return;
                       
                    }
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
            

            public IEnumerable<Author> Select(Author model = null)
            {
                List<Author> authorList = new List<Author>();
                IEnumerable<Author> allAuthor;
                string sqlExpression;
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    if (model == null)
                    {
                        sqlExpression = "SELECT * FROM Author ";
                    }
                    else
                    {
                        sqlExpression = $"SELECT * FROM Author WHERE fullname = '{model.FullName}'";
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
                        allAuthor = authorList;
                        return allAuthor;
                    }
                }
            }

            public void Update(Author author)
            {
                if (author.FullName == null && author.Contacts == null && author.Information == null)
                {
                    return;

                }
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;




                    string sqlExpression = "UPDATE Author SET ";


                    if (author.FullName != null)
                        sqlExpression += $"`FullName` = '{author.FullName}', ";

                    if (author.Contacts != null)
                        sqlExpression += $"`Contacts` = '{author.Contacts}', ";

                    if (author.Information != null)
                        sqlExpression += $"`Information` = '{author.Information}', ";

                    sqlExpression = sqlExpression.TrimEnd(',', ' ');
                    sqlExpression += $" WHERE Id = '{author.Id}'";

                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();
                }
            }
        }
        public class DataBasePublisher : IDataBaseHelperModels<Publisher>
        {
            public void Delete(int id)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();


                    string checkUsageQuery = $"SELECT COUNT(*) FROM BibliographicMaterial WHERE PublisherId = {id}";
                    using (var checkUsageCommand = new SqliteCommand(checkUsageQuery, connection))
                    {
                        int usageCount = Convert.ToInt32(checkUsageCommand.ExecuteScalar());

                     
                        if (usageCount > 0)
                        {

                            return;
                        }

                    }

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;

                    string sqlExpression = $"DELETE FROM Publisher WHERE Id = {id}";
                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();

                }
            }
        

            public void Insert(Publisher model = null)
            {
                 if (model.Name == null || model.Contacts == null || model.Address == null)
                {
                    return;
              
                }
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO Publisher (Name, Contacts, Address) VALUES (@Name, @Contacts, @Address)";
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Contacts", model.Contacts);
                    command.Parameters.AddWithValue("@Address", model.Address);

                    command.ExecuteNonQuery();
                }
            }

            public IEnumerable<Publisher> Select(Publisher model = null)
            {
                List<Publisher> publisherList = new List<Publisher>();
                string sqlExpression;

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    if (model == null)
                    {
                        sqlExpression = "SELECT * FROM Publisher";
                    }
                    else
                    {
                        sqlExpression = $"SELECT * FROM Publisher WHERE Name = '{model.Name}'";
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

            public void Update(Publisher model = null)
            {
                 if (model.Name == null && model.Contacts == null && model.Address == null)
                {
                    return;
                   
                }
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;




                    string sqlExpression = "UPDATE Publisher SET ";


                    if (model.Name != null)
                        sqlExpression += $"`Name` = '{model.Name}', ";

                    if (model.Contacts != null)
                        sqlExpression += $"`Contacts` = '{model.Contacts}', ";

                    if (model.Address != null)
                        sqlExpression += $"`Address` = '{model.Address}', ";

                    sqlExpression = sqlExpression.TrimEnd(',', ' ');
                    sqlExpression += $" WHERE Id = '{model.Id}'";

                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();
                }
            }
        }
        public class DataBaseBibliographicmaterial : IDataBaseHelperModels<BibliographicMaterial>
        {
            public void Delete(int id)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;

                    string sqlExpression = $"DELETE FROM BibliographicMaterial WHERE Id = {id}";
                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();
                }


            }

            public void Insert(BibliographicMaterial model = null)
            {
                if (model == null)
                {
                    return;
                }
                model.Img = "pict1";
                if (model.PublisherId == null || model.AuthorId == null || model.Date == null || model.Name == null || model.Img == null)
                {
                    return;

                }

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;


                    command.CommandText = "INSERT INTO BibliographicMaterial (Name, Date, Img, AuthorId, PublisherId) VALUES (@Name, @Date, @Img, @AuthorId, @PublisherId)";
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Date", model.Date);
                    command.Parameters.AddWithValue("@Img", model.Img);
                    command.Parameters.AddWithValue("@AuthorId", model.AuthorId);
                    command.Parameters.AddWithValue("@PublisherId", model.PublisherId);

                    command.ExecuteNonQuery();


                }
            }


            public IEnumerable<BibliographicMaterial> Select(BibliographicMaterial model = null)
            {

                List<BibliographicMaterial> bibliographicmaterialsDatebase = new List<BibliographicMaterial>();
                string sqlExpression;


                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    sqlExpression = "SELECT * FROM Bibliographicmaterial";



                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Connection = connection;
                    DataBaseAuthor allAuthor = new DataBaseAuthor();
                    DataBasePublisher allPublisher = new DataBasePublisher();
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
                                var author = allAuthor.Select().FirstOrDefault(a => a.Id == Authorid);
                                var publisher = allPublisher.Select().FirstOrDefault(p => p.Id == Publisherid);
                                bibliographicmaterialsDatebase.Add(new BibliographicMaterial()
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


            public void Update(BibliographicMaterial model = null)
            {

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    if (model.Name == null && model.Date == null && model.AuthorId == null && model.PublisherId == null)
                    {

                        return;
                    }



                    string sqlExpression = "UPDATE BibliographicMaterial SET ";


                    if (model.Name != null)
                        sqlExpression += $"`Name` = '{model.Name}', ";

                    if (model.Date != null)
                        sqlExpression += $"`Date` = '{model.Date}', ";

                    if (model.AuthorId != null)
                        sqlExpression += $"`AuthorId` = '{model.AuthorId}', ";

                    if (model.PublisherId != null)
                        sqlExpression += $"`PublisherId` = '{model.PublisherId}', ";

                    sqlExpression = sqlExpression.TrimEnd(',', ' ');
                    sqlExpression += $" WHERE Id = '{model.Id}'";

                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();
                }
            }



        }
        public class DataBaseUser : IDataBaseHelperUser
        {
            private readonly IServiceProvider _serviceProvider;
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
                                var id = reader.GetInt32(0);
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
            ///Вставка новой записи в таблицу "AddUser"
            /// </summary>//
            public void AddUser(User  material)
            {
                if (material.Login== null || material.Password == null || material.Admin == null )
                {
                    return;
            
                }
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

            public string Regist(User user, User login)
            {
               
               if (user != null && !string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                {


                    if (login != null)
                    {
                        if (login.Admin == "false")
                        {
                            return "false";
                        }
                        else
                        {
                            return "true";
                        }
                            


                    }


             
                }
                return "error";
               
            }

            public bool Authorization(User user, bool userExists)
            {
                if (user != null && !string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                {
                    

                    if (!userExists)
                    {
                   
                        

                       
                        
                        return true;
                    }
                }


                return false;
            }
        }
       
    }
    }
    

