using System.Globalization;

namespace UkolKnihovna
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public DateTime PublishedDate { get; }
        public int Pages { get; }
        private static List<Book> BookList = [];

        public Book(string title, string author, DateTime date, int pages)
        {
            Title = title;
            Author = author;
            PublishedDate = date;
            Pages = pages;
        }

        public static (bool IsValid, string Title, string Author, DateTime Date, int Pages) ParseBook(string input)
        {
            string[] inputSplitted = input.Split(";");
            DateTime date = DateTime.Now;
            int pages = 0;

            if (inputSplitted.Length != 5)
            {
                return (false, "", "", date, pages);
            }

            bool result = (
                (inputSplitted.Length == 5) &&
                (inputSplitted[0] == "ADD") &&
                DateTime.TryParseExact(
                    inputSplitted[3],
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out date
                ) &&
                int.TryParse(inputSplitted[4], out pages) &&
                (pages > 0)
            );
            return (result, inputSplitted[1], inputSplitted[2], date, pages);
        }

        public static void AddBook(Book newBook)
        {
            BookList.Add(newBook);
        }

        public void PrintBook()
        {
            Console.WriteLine($"Book: {Title}, author: {Author}, published {PublishedDate:dd.MM.yyyy}, number of pages: {Pages}");
        }

        public static void PrintList()
        {
            if (BookList.Count == 0)
            {
                Console.WriteLine("Book list is empty");
                return;
            }

            var listOrderedByDate = BookList.OrderBy(b => b.PublishedDate);
            foreach (Book Book in listOrderedByDate)
            {
                Book.PrintBook();
            }
        }

        public static void PrintStats()
        {
            if (BookList.Count == 0)
            {
                Console.WriteLine("Book list is empty");
                return;
            }

            Console.WriteLine("Statistics:");
            Console.WriteLine($"Average number of pages: {Math.Round(BookList.Average(b => b.Pages))}");

            Console.WriteLine("Number of books by author:");
            var booksGroupedByAuthor = BookList.GroupBy(b => b.Author);
            foreach (var bookGroup in booksGroupedByAuthor)
            {
                Console.WriteLine($" - {bookGroup.Key}: {bookGroup.Count()}");
            }

            Console.WriteLine($"Number of unique words in book titles: {BookList.SelectMany(b => b.Title.Split(" ")).Distinct().Count()}");
        }

        public static void FindBook(string word)
        {
            if (BookList.Count == 0)
            {
                Console.WriteLine("Book list is empty");
                return;
            }

            var result = BookList.Where(b => b.Title.Contains(word, StringComparison.InvariantCultureIgnoreCase));
            if (!result.Any())
            {
                Console.WriteLine("Keyword was not found");
                return;
            }

            Console.WriteLine($"Search results for '{word}':");
            foreach (var book in result)
            {
                Console.WriteLine($" - {book.Title}");
            }
        }
    }
}