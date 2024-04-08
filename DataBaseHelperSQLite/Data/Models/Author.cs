﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable 
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DataBaseHelperSQLite.Data.Models
{

    /// <summary>
    /// Модель автора Author
    /// </summary>
    public partial class Author
    {
        /// <summary>
        /// ID 
        /// </summary>

        public int? Id { get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>

        public string FullName { get; set; }
        /// <summary>
        /// Контакты
        /// </summary>

        public string Contacts { get; set; }
        /// <summary>
        /// Информация
        /// </summary>

        public string Information { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<BibliographicMaterial> BibliographicMaterials { get; set; } = new List<BibliographicMaterial>();
    }
}