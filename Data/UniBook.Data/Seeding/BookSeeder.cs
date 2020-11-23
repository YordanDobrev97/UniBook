namespace UniBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using UniBook.Data.Models;

    public class BookSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var folderPath = Assembly.GetExecutingAssembly().Location;
            folderPath = folderPath.Replace("UniBook.Data.dll", "Books");

            var contentBooks = new List<string>();

            foreach (var path in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                var body = File.ReadAllText(path);
                contentBooks.Add(body);
            }

            contentBooks = contentBooks.Take(40).ToList();

            await this.Seed(dbContext, contentBooks);

            Console.WriteLine("Ready! All books are inserted");
        }

        public async Task Seed(ApplicationDbContext db, List<string> books)
        {
            List<string> bookNames = new List<string>()
            {
                "Гори, вещице, гори", "Седем стъпки до сатаната",
                "Аелита", "Хиперболоид на инженер Гарин",
                "Златното ключе или приключенията на буратино",
                "Алената буква", "Войната се завръща",
                "Аз преди теб", "Дракула", "Човек на име уве",
                "Дебнещият страх", "Алената чума", "Бялото мълчание",
                "Малката стопанка на голямата къща",
                "Морският вълк", "Децата от гарата", "Без дом",
                "Изпитания на малката периета", "Малкият моряк",
                "Престъпление и наказание", "Хайди",
                "Хари потър и философският камък",
                "Планините на безумието", "Нора", "Под игото",
                "Кери", "Българи от старо време",
                "Маминото детенце", "Алиса в страната на чудесата",
                "Моят път към себе си", "Мрак под слънцето",
                "Перфектна химия", "Петдесет нюанса по-тъмно",
                "Афродита - антични нрави", "Доктор джекил и мистър хайд",
                "Черната стрела", "Островът на съкровищата",
                "Шантарам", "Клетниците", "Тетрадката"
            };

            List<string> authorBook = new List<string>()
            {
                "Абрахам Мерит", "Абрахам Мерит",
                "Алексей Толстой", "Алексей Толстой", "Алексей Толстой",
                "Натаниъл Хоторн", "Анри Пози", "Джоджо Мойс",
                "Брам Стокър", "ФредрикБакман", "Хауърд Лъвкрафт",
                "Джек Лондон", "Джек Лондон", "Джек Лондон", "Джек Лондон",
                "Едит Несбит", "Ектор Мало", "Ектор Мало", "Ектор Мало",
                "Фьодор Достоевски", "Йохана Спири", "Джоан Роулинг",
                "Хауърд Лъвкрафт", "Иван Вазов", "Иван Вазов",
                "Стивън Кинг", "Любен Каравелов", "Любен Каравелов",
                "Луис Карол", "Ървин Ялом", "Дийн Кунц", "Симон Елкелес",
                "Е. Л. Джеймс", "Пиер Луис", "Робърт Луис Стивънсън",
                "Робърт Луис Стивънсън", "Робърт Луис Стивънсън",
                "Грегъри Дейвид Робъртс", "Виктор Юго", "Никълъс Спаркс"
            };

            List<string> links = new List<string>()
            {
                "https://assets.chitanka.info/thumb/?book-cover/19/6522.250.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/04/1180.max.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/12/4835.250.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/10/4147.250.jpg",
                "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1404660308l/22670117.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/22/8708.250.jpg",
                "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/v/o/cd233f3ad74a88ac1095cc9f17e7722e/voynata-se-zavrashta-31.jpg",
                "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/a/z/6d3e59789317f6a5d3c5e6dffb38e9db/az-predi-teb-30.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/0c/3183.200.jpg",
                "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/c/h/caff1f5f5bb00badea10dd446f66be7e/chovek-na-ime-uve-meki-koritsi-31.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/01/458.max.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/00/193.200.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/01/330.250.jpg",
                "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1381845495l/18680276.jpg",
                "https://i5.helikon.bg/products/8485/21/218485/218485_b.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/21/8687.250.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/00/251.200.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/f/i/file_267_71.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/13/4869.250.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/8/9/89311.jpeg",
                "https://www.book.store.bg/d-prdimages/lrg/51214.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/f/i/file_255_12.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/p/l/planinite-na-bezumieto.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/f/i/file_269_39.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/p/o/pod-igoto_ivan-vazov-damyan-yakov.jpg",
                "https://www.pleiadbooks.com/korici/Keri.jpg",
                "https://www.book.store.bg/d-prdimages/lrg/24707.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/6/9/69528.jpeg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/a/l/alisa-v-stranata-na-chudesata-miranda_1.jpg",
                "https://i1.helikon.bg/products/4989/21/214989/214989_b.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/27/10009.250.jpg",
                "https://i3.helikon.bg/products/7996/19/197996/197996_b.jpg",
                "https://i.ytimg.com/vi/wrZFTAbWuaw/maxresdefault.jpg",
                "https://knizhen-pazar.net/books/055/5574/557436.jpg",
                "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/s/t/5aed37860582e11ba1b688df7a68382d/stranniyat-sluchay-s-doktor-dzhekil-i-mistar-hayd-31.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/0c/3094.200.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/o/s/ostrovat-na-sakrovishtata-labirint.jpg",
                "https://assets.chitanka.info/thumb/?book-cover/18/6368.max.jpg",
                "https://www.book.store.bg/d-prdimages/lrg/24068.jpg",
                "https://ciela.com/media/catalog/product/cache/3/image/204x304/9df78eab33525d08d6e5fb8d27136e95/1/0/103998.jpeg"
            };

            for (int i = 0; i < books.Count; i++)
            {
                var body = books[i];
                var bookName = bookNames[i];
                var authorName = authorBook[i];
                var image = links[i];

                var author = db.Authors.FirstOrDefault(e => e.Name == authorName);

                if (author == null)
                {
                    author = new Author
                    {
                        Name = authorName,
                    };
                }

                var book = new Book
                {
                    Name = bookName,
                    CreatedOn = DateTime.UtcNow,
                    ImageUrl = image,
                    Body = body,
                    Author = author,
                    Votes = 0,
                    IsDeleted = false,
                };

                await db.Books.AddAsync(book);
                await db.SaveChangesAsync();
            }
        }
    }
}
