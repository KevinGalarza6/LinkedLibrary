using System;

namespace Library
{
    class Program
    {
        static void Main()
        {
            bool showMenu = true;

            Node[] libraryBooks = new Node[] { new Node() };

            libraryBooks = Add(libraryBooks, new Book() { Title = "The Handmaid’s Tale", Author = "Margaret Atwood", PageCount = 368 });
            libraryBooks = Add(libraryBooks, new Book() { Title = "Me Before You", Author = "Jojo Moyes", PageCount = 320 });
            libraryBooks = Add(libraryBooks, new Book() { Title = "Eleanor & Park", Author = "Rainbow Rowell", PageCount = 328 });
            libraryBooks = Add(libraryBooks, new Book() { Title = "Essa Gente", Author = "Chico Buarque", PageCount = 200 });
            libraryBooks = Add(libraryBooks, new Book() { Title = "O Fim em Doses Homeopáticas", Author = "Igor Pires", PageCount = 304 });
            libraryBooks = LinkedList(libraryBooks);

            while (showMenu)
            {
                libraryBooks = Menu(libraryBooks);
            }
        }

        public static Node[] Menu(Node[] libraryBooks)
        {
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    Select option:\n");
            Console.WriteLine($"    1 - Add book");
            Console.WriteLine($"    2 - Remove book");
            Console.WriteLine($"    3 - List books");
            Console.WriteLine($"    4 - Exit");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            var answer = Console.ReadLine();
            Console.Clear();

            if (answer == "1")
                libraryBooks = Add(libraryBooks, MenuAddBook());
            else if (answer == "2")
                libraryBooks = Remove(libraryBooks, MenuRemoveBook(libraryBooks));
            else if (answer == "3")
                MenuBookList(libraryBooks);
            else if (answer == "4")
                Environment.Exit(0);

            return libraryBooks;
        }

        public static Book MenuAddBook()
        {
            Book newBook = new Book();

            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    Title?");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            newBook.Title = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    Author?");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            newBook.Author = Console.ReadLine();
            Console.Clear();

            while (newBook.PageCount < 1)
            {
                Console.WriteLine("\n__________________________________________________________________________________________\n");
                Console.WriteLine($"    Page Count?");
                Console.WriteLine("\n__________________________________________________________________________________________\n");
                if (long.TryParse(Console.ReadLine(), out long answer))
                    newBook.PageCount = answer;

                if (newBook.PageCount < 1)
                {
                    Console.Clear();
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.WriteLine($"    Number of pages is not valid, press enter to try again!");
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.ReadLine();
                }

                Console.Clear();
            }

            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    Book add sucess! press enter for back to menu!");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.ReadLine();
            Console.Clear();

            return newBook;
        }

        public static long MenuRemoveBook(Node[] libraryBooks)
        {
            libraryBooks = Sort(libraryBooks, "ascending");
            long answer = -1;

            while (answer < 0)
            {
                Console.WriteLine("\n__________________________________________________________________________________________\n");
                Console.WriteLine($"    List of Books:\n");

                foreach (Node node in libraryBooks)
                    Console.WriteLine($"    {node.Book.Id} - Title: {node.Book.Title} - Author: {node.Book.Author} - Page Count: {node.Book.PageCount}");

                Console.WriteLine($"\n    What is the \"Id\" of book to remove?");
                Console.WriteLine("\n__________________________________________________________________________________________\n");

                if (long.TryParse(Console.ReadLine(), out long parseAnswer))
                    answer = parseAnswer;

                if (answer == -1)
                {
                    Console.Clear();
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.WriteLine($"    This \"Id\" is not valid, press enter to try again!");
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    Book removed sucess! press enter for back to menu!");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.ReadLine();
            Console.Clear();

            return answer;
        }

        public static void MenuBookList(Node[] libraryBooks)
        {
            long orderBy = -1;

            while (orderBy <= 0)
            {
                Console.WriteLine("\n__________________________________________________________________________________________\n");
                Console.WriteLine($"    Order by:\n");
                Console.WriteLine($"    1 - Ascending");
                Console.WriteLine($"    2 - Descending");
                Console.WriteLine("\n__________________________________________________________________________________________\n");

                if (long.TryParse(Console.ReadLine(), out long order))
                    orderBy = order;

                Console.Clear();

                if (orderBy == -1)
                {
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.WriteLine($"    This order dont exist`s, press enter to try again!");
                    Console.WriteLine("\n__________________________________________________________________________________________\n");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            if (orderBy == 1)
                libraryBooks = Sort(libraryBooks, "ascending");
            else if (orderBy == 2)
                libraryBooks = Sort(libraryBooks, "descending");

            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.WriteLine($"    List of Books:\n");

            foreach (Node node in libraryBooks)
                Console.WriteLine($"    {node.Book.Id} - Title: {node.Book.Title} - Author: {node.Book.Author} - Page Count: {node.Book.PageCount}");

            Console.WriteLine($"\n    Press enter for back to menu!");
            Console.WriteLine("\n__________________________________________________________________________________________\n");
            Console.ReadLine();
            Console.Clear();
        }

        public static Node[] Remove(Node[] library, long bookId)
        {
            Node[] temp = library;
            library = new Node[temp.Length - 1];

            for (var i = 0; i < temp.Length; i++)
            {
                if (temp[i].Book.Id == bookId)
                {
                    for (var j = 0; j < temp.Length; j++)
                    {
                        if (j == i)
                            j++;

                        if (j > i)
                            library[j - 1] = new Node() { Book = temp[j].Book };
                        else
                        {
                            library[j] = new Node() { Book = temp[j].Book };
                        }
                    }
                }
            }

            return LinkedList(library);
        }

        public static Node[] Add(Node[] library, Book book)
        {
            if (library[0].Book == null)
            {
                library[0].Book = book;

                return library;
            }

            Node[] temp = library;
            library = new Node[temp.Length + 1];

            Array.Copy(temp, 0, library, 0, temp.Length);
            library[^1] = new Node() { Book = book, Next = null };

            return library;
        }

        public static Node[] LinkedList(Node[] library)
        {
            library = SetIdOfBooks(library);
            Node[] temp = library;
            library = new Node[temp.Length];

            if (temp.Length > 1)
            {
                for (var i = 0; i < temp.Length; i++)
                {
                    library[i] = temp[i];

                    if (i < temp.Length - 1)
                    {
                        library[i].Next = temp[i + 1];
                    }
                }
            }
            else
            {
                library[0] = temp[0];
            }

            return library;
        }

        public static Node[] Sort(Node[] libraryBooks, string order)
        {
            Book temp;

            for (int i = 0; i < libraryBooks.Length; i++)
            {
                for (int j = i + 1; j < libraryBooks.Length; j++)
                {
                    if (order == "ascending")
                    {
                        if (libraryBooks[i].Book.PageCount > libraryBooks[j].Book.PageCount)
                        {
                            temp = libraryBooks[i].Book;
                            libraryBooks[i].Book = libraryBooks[j].Book;
                            libraryBooks[j].Book = temp;
                        }
                    }
                    else if (order == "descending")
                    {
                        if (libraryBooks[i].Book.PageCount < libraryBooks[j].Book.PageCount)
                        {
                            temp = libraryBooks[i].Book;
                            libraryBooks[i].Book = libraryBooks[j].Book;
                            libraryBooks[j].Book = temp;
                        }
                    }
                }
            }

            return LinkedList(libraryBooks);
        }

        public static Node[] SetIdOfBooks(Node[] arrayOfBooks)
        {
            for (int i = 0; i < arrayOfBooks.Length; i++)
            {
                arrayOfBooks[i].Book.Id = i;
            }

            return arrayOfBooks;
        }

        public class Book
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public long PageCount { get; set; }
        }

        public class Node
        {
            public Book Book { get; set; }
            public Node Next { get; set; }

        }
    }
}
