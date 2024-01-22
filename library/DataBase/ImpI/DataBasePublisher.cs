using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;

namespace library.DataBase.ImpI
{
    public class DataBasePublisher : DatabaseHelper, IDataBaseHelperModels<Publisher>
    {
        public DataBasePublisher(string dbConnectionString) : base(dbConnectionString) { }
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


        public void Insert(Publisher model)
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

        public IEnumerable<Publisher> Select(Publisher model)
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

        public void Update(Publisher model)
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
}
