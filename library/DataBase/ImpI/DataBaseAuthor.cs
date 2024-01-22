using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;

namespace library.DataBase.ImpI
{
    public class DataBaseAuthor : DatabaseHelper, IDataBaseHelperModels<Author>
    {
        public DataBaseAuthor(string dbConnectionString) : base(dbConnectionString) { }
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


        public IEnumerable<Author> Select(Author model)
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
}
