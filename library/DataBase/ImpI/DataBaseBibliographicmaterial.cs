using library.Data.Models;
using library.DataBase.Contract;
using Microsoft.Data.Sqlite;

namespace library.DataBase.ImpI
{
    public class DataBaseBibliographicmaterial : DatabaseHelper, IDataBaseHelperModels<BibliographicMaterial>
    {
        public DataBaseBibliographicmaterial(string dbConnectionString) : base(dbConnectionString) { }
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

        public void Insert(BibliographicMaterial model)
        {
            if (model == null)
            {
                return;
            }
            //model.Img = "pict1";
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


        public IEnumerable<BibliographicMaterial> Select(BibliographicMaterial model)
        {
            //string dbConnectionString = null;
            DatabaseHelper databaseHelper = new(null);
            List<BibliographicMaterial> bibliographicmaterialsDatebase = new List<BibliographicMaterial>();
            string sqlExpression;


            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                sqlExpression = "SELECT * FROM Bibliographicmaterial";



                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.Connection = connection;
                DataBaseAuthor allAuthor = new DataBaseAuthor(databaseHelper._connectionString);
                DataBasePublisher allPublisher = new DataBasePublisher(databaseHelper._connectionString);
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
                            var author = allAuthor.Select(null).FirstOrDefault(a => a.Id == Authorid);
                            var publisher = allPublisher.Select(null).FirstOrDefault(p => p.Id == Publisherid);
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


        public void Update(BibliographicMaterial model)
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                if (model.Name == null && model.Date == null && model.AuthorId == null && model.PublisherId == null && model.Img == null)
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
                if (model.Img != null)
                    sqlExpression += $"`Img` = '{model.Img}', ";

                sqlExpression = sqlExpression.TrimEnd(',', ' ');
                sqlExpression += $" WHERE Id = '{model.Id}'";

                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }



    }
}
