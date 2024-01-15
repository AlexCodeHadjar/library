using library.Data.Models;
using library.ViewModels;
using Microsoft.Data.Sqlite;
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
        private static string _connectionString;
      



        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
         
        }
        
        public class DataBaseAuthor : IDataBaseHelperAuthor
        {
            ///<summary>
            ///перебор всех обьектов Author из базы данных Catalogsdata
            /// </summary>
            public IEnumerable<Author> SelectAuthor(string? nameAuthor = null)
            {
                List<Author> authorList = new List<Author>();
                IEnumerable<Author> allAuthor;
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
                        allAuthor = authorList;
                        return allAuthor;
                    }
                }
            }
            ///<summary>
            ///Вставка новой записи в таблицу "AddAuthor"
            /// </summary>//  

            public void AddAuthor(Author? author)
            {
                if (author.FullName == null || author.Contacts == null || author.Information == null )
                {
                    return;
                    //throw new ArgumentException("FullName and Contacts and Information must be set for the Author.");
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
            public void DeleteAuthor(int? idAuthor)
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

            public void UpdateAuthor(int? idAuthor, string? nameAuthor = null, string? contactsAuthor = null, string? informationAuthor = null)
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
        }
        public class DataBasePublisher : IDataBaseHelperPublisher
        {
            public IEnumerable<Publisher> SelectPublisher(string? namePublisher = null)
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

            ///<summary>
            ///Вставка новой записи в таблицу "Publisher"
            /// </summary>// 

            public void AddPublisher(Publisher? publisher)
            {
                if (publisher.Name == null || publisher.Contacts == null || publisher.Address == null)
                {
                    return ;
                    //throw new ArgumentException("Name and Contacts and Address must be set for the publisher.");
                }
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

            public void UpdatePublisher(int? idPublisher, string? namePublisher = null, string? contactsPublisher = null, string? addressPublisher = null)
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

            public void DeletePublisher(int? idPublisher=null)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

               
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
                
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;

                    string sqlExpression = $"DELETE FROM Publisher WHERE Id = {idPublisher}";
                    command.CommandText = sqlExpression;
                    command.ExecuteNonQuery();

                }
            }
        }

        public class DataBaseBibliographicmaterial : IDataBaseHelperBibliographicmaterial
        {
            ///<summary>
            ///перебор всех обьектов Bibliographicmaterial из базы данных Catalogsdata
            /// </summary>
            public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial()
            {
                List<BibliographicMaterial> bibliographicmaterialsDatebase = new List<BibliographicMaterial>();
                string sqlExpression;


                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    sqlExpression = "SELECT * FROM Bibliographicmaterial";



                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    command.Connection = connection;
                    DataBaseAuthor allPuthor = new DataBaseAuthor();
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
                                var author = allPuthor.SelectAuthor().FirstOrDefault(a => a.Id == Authorid);
                                var publisher = allPublisher.SelectPublisher().FirstOrDefault(p => p.Id == Publisherid);
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

            public IEnumerable<BibliographicMaterial> SelectBibliographicmaterial(string? nameBibliographicmaterial = null, string? date = null, string? nameAuthor = null, string? namePublisher = null)
            {
                DataBaseAuthor author = new();
                DataBasePublisher publisher = new();
                DataBaseBibliographicmaterial bibliographicmaterial = new();
                IEnumerable<BibliographicMaterial> filteredBibliographicmaterial = bibliographicmaterial.SelectBibliographicmaterial();

                if (!string.IsNullOrEmpty(nameBibliographicmaterial))
                {
                    filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => a.Name == nameBibliographicmaterial);
                }

                if (!string.IsNullOrEmpty(date))
                {
                    filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => int.Parse(a.Date) >= int.Parse(date));
                }

                if (!string.IsNullOrEmpty(nameAuthor))
                {
                    var authors = author.SelectAuthor(nameAuthor).Select(a => a.Id);
                    filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => authors.Contains(a.Author.Id));
                }

                if (!string.IsNullOrEmpty(namePublisher))
                {
                    var publishers = publisher.SelectPublisher(namePublisher).Select(a => a.Id);
                    filteredBibliographicmaterial = filteredBibliographicmaterial.Where(a => publishers.Contains(a.Publisher.Id));
                }

                return filteredBibliographicmaterial;
            }

            ///<summary>
            ///Вставка новой записи в таблицу "BibliographicMaterial"
            /// </summary>// 
            
            public void AddBibliographicMaterial(BibliographicMaterial? material=null)
            {
                if (material.Author.Id == null || material.Publisher.Id == null || material.Date == null || material.Name == null || material.Img == null)
                {
                    return;
            
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

            public void DeleteBibliographicmaterial(int? idBibliographicmaterial=null)
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

            public void UpdateBibliographicmaterial(int? idBibliographicmaterial=null, string? nameBibliographicmaterial = null,
                        string? nameAuthor = null, string? namePublisher = null, string? date = null)
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
            public void AddUser(User? material = null)
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
                    // login = _serviceProvider.GetRequiredService<IDataBaseHelperUser>().SelectUser().FirstOrDefault(p => p.Login == user.Login && p.Password == user.Password);


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
                   
                        

                        // добавление пользователя(user)
                        
                        return true;
                    }
                }


                return false;
            }
        }

        public class DataBaseLibraryCatolog : IDataBaseHelperLibraryCatolog
        {

            private readonly IServiceProvider _serviceProvider;

            public DataBaseLibraryCatolog(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }
            public AllLibraryModels SortLibraryModels(string? nameBibliographicmaterial = null, string? nameAuthor = null, string? namePublisher = null, string? date = null, SortBy? sortBy = null)
            {
                AllLibraryModels libraryobj = new AllLibraryModels();
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher);
                if (sortBy.SortNameAuthor)
                {

                    libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Author.FullName);
                }


                if (sortBy.SortNamePublisher)
                {
                    libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Publisher.Name);
                }

                if (sortBy.SortNameBibliographicmaterial)
                {
                    libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderBy(a => a.Name);
                }

                if (sortBy.SortDate)
                {
                    libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Date);
                }
                //libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial(nameBibliographicmaterial, date, nameAuthor, namePublisher).OrderByDescending(a => a.Date);

                libraryobj.AllPublishers = string.IsNullOrEmpty(namePublisher) ? _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().SelectPublisher().OrderBy(a => a.Name) : _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().SelectPublisher(namePublisher).OrderBy(a => a.Name);
                libraryobj.AllAuthors = string.IsNullOrEmpty(nameAuthor) ? _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().SelectAuthor().OrderBy(a => a.FullName) : _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().SelectAuthor(nameAuthor).OrderBy(a => a.FullName);

                return libraryobj;
            }

            public AllLibraryModels StartLibraryModels()
            {
                AllLibraryModels libraryobj = new AllLibraryModels();
                libraryobj.AllAuthors = _serviceProvider.GetRequiredService<IDataBaseHelperAuthor>().SelectAuthor().OrderBy(a => a.FullName);
                libraryobj.AllPublishers = _serviceProvider.GetRequiredService<IDataBaseHelperPublisher>().SelectPublisher().OrderBy(a => a.Name);
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial().OrderBy(a => a.Name);
                return libraryobj;
            }
            public AllLibraryModels PageBibliographicmaterial(int materialId)
            {
                AllLibraryModels libraryobj = new AllLibraryModels();

                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial().Where(a => a.Id == materialId);
                return libraryobj;
            }
            public AllLibraryModels PageBibliographicmaterialAdmin(int materialId)
            {
                AllLibraryModels libraryobj = new AllLibraryModels();
                libraryobj = _serviceProvider.GetRequiredService<IDataBaseHelperLibraryCatolog>().StartLibraryModels();
                libraryobj.AllBibliographicmaterial = _serviceProvider.GetRequiredService<IDataBaseHelperBibliographicmaterial>().SelectBibliographicmaterial().Where(a => a.Id == materialId);
                return libraryobj;
            }
        }

       
    
    }

}