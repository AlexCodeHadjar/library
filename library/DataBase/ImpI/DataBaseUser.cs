using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;

namespace library.DataBase.ImpI
{
    public class DataBaseUser : DatabaseHelper, IDataBaseHelperModels<User>
    {
        public DataBaseUser(string _connectionString) : base(_connectionString) { }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(User model = null)
        {
            if (model.Login == null || model.Password == null || model.Admin == null)
            {
                return;

            }
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;


                command.CommandText = "INSERT INTO User (Login, Password,Admin) VALUES (@login, @password,@Admin)";
                command.Parameters.AddWithValue("@login", model.Login);
                command.Parameters.AddWithValue("@password", model.Password);
                command.Parameters.AddWithValue("@Admin", model.Admin);


                command.ExecuteNonQuery();


            }
        }

        public IEnumerable<User> Select(User model)
        {
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
        }

        public void Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}

