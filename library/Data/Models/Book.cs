namespace library.Data.Models
{
    public class Bibliographicmaterial
    {
        public int id { get; set; }
        public string name { get; set; }
        public Author author { get; set; }
        public Publisher publisher { get; set; }

    }
}
