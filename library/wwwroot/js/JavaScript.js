var first = booksData.AllBibliographicmaterial;
console.log(first);
var Second = booksData.AllAuthors;
console.log(Second);
var Third = booksData.AllPublishers;
console.log(Third);
var AllLibraryInfo = booksData;
var BooksInfo = AllLibraryInfo.AllBibliographicmaterial[0];
var AuthorInfo = AllLibraryInfo.AllAuthors[0];
var PublisherInfo = AllLibraryInfo.AllPublishers[0];

/*  model.Name == null && model.Date == null && model.PublisherId == null && model.AuthorId == null && model.Img == null*/
let nameUse = BooksInfo.Name;
let dateUse = BooksInfo.Date;
let publisherIdUse = BooksInfo.PublisherId;
let authorIdUse = BooksInfo.AuthorId;
let imgUse = BooksInfo.Img;
let idUse = BooksInfo.Id;
let author = AuthorInfo.FullName;


let publisher = PublisherInfo.Name;
console.log(nameUse);
console.log(dateUse);
console.log(publisherIdUse);
console.log(authorIdUse);
console.log(imgUse);
console.log(idUse);
let select = document.createElement("select");
["Опция 1", "Опция 2", "Опция 3"].forEach((text, index) => {
    let option = new Option(text, "value" + index);
    select.add(option);
});
document.body.appendChild(select);

