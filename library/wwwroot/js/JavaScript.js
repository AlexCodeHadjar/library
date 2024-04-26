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
var allAuthors = AllLibraryInfo.AllAuthors;
var allPublisher = AllLibraryInfo.AllPublishers;
/*  model.Name == null && model.Date == null && model.PublisherId == null && model.AuthorId == null && model.Img == null*/
let nameUse = BooksInfo.Name;
let dateUse = BooksInfo.Date;
let publisherIdUse = BooksInfo.PublisherId;
let authorIdUse = BooksInfo.AuthorId;
let imgUse = BooksInfo.Img;
let idUse = BooksInfo.Id;
let author = AuthorInfo.FullName;


let publisher = PublisherInfo.Name;

console.log("Издательство id",publisherIdUse);
console.log("Автор id", authorIdUse);
console.log("Картинка", imgUse);
console.log("id Книжки", idUse);
console.log("Название",nameUse);
console.log("Дата",dateUse);
//var select1 = document.createElement("select");
//select1.style.position = 'absolute';
//select1.style.top = '30%';
//select1.style.left = '88%';
//var select2 = document.createElement("select");
//// Проходимся по каждому автору и добавляем его имя в select в качестве опции
//allAuthors.forEach((author) => {
//    var option = document.createElement("option");
//    option.text = author.FullName;
//    option.value = author.Id;
//    select1.appendChild(option);
//});
//allPublisher.forEach((publisher) => {
//    var option = document.createElement("option");
//    option.text = publisher.Name;
//    option.value = publisher.Id;
//    select2.appendChild(option);
//});
//document.body.appendChild(select1);
//document.body.appendChild(select2);

