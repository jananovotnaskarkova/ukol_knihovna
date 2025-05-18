namespace UkolKnihovna;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Add a new book 'ADD;title;author;publication_date;pages' " +
                              "or type: 'LIST', 'STATS', 'FIND;[keyword]' or 'END'");
            bool isInputValid = false;

            while (!isInputValid)
            {
                string input = Console.ReadLine();

                if (input.Contains("ADD;"))
                {
                    (bool isBookValid, string title, string author, DateTime date, int pages) = Book.ParseBook(input);

                    if (isBookValid)
                    {
                        Book newBook = new Book(title, author, date, pages);
                        Book.AddBook(newBook);
                        isInputValid = true;
                    }
                    else
                    {
                        Console.WriteLine("The book has a invalid format");
                    }
                }
                else if (input == "LIST")
                {
                    Book.PrintList();
                    isInputValid = true;
                }
                else if (input == "STATS")
                {
                    Book.PrintStats();
                    isInputValid = true;
                }
                else if (input.Contains("FIND;"))
                {
                    string keyword = input.Split(";")[1];
                    if (keyword.Length == 0)
                    {
                        Console.WriteLine("Invalid keyword");
                    }
                    else
                    {
                        Book.FindBook(keyword);
                        isInputValid = true;
                    }
                }
                else if (input == "END")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
