using library.Data.Models;

namespace library.DataBase
{
    public interface IDataBaseHelperBibliographicmaterial
    {
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial();
        public IEnumerable<Bibliographicmaterial> SelectBibliographicmaterial(string? nameBibliographicmaterial = null, string? date = null, string? nameAuthor = null, string? namePublisher = null);
        public void AddBibliographicMaterial(Bibliographicmaterial? material=null);
        public void DeleteBibliographicmaterial(int? idBibliographicmaterial=null);
        public void UpdateBibliographicmaterial(int? idBibliographicmaterial=null, string? nameBibliographicmaterial = null,
     string? nameAuthor = null, string? namePublisher = null, string? date = null);

    }
}
