using System;

namespace Moment3
{
    public class Program
    {
        static void Main(string[] args)
        {

            var running = true;
            while (running)
            {
                Console.Clear();  // Rensa konsolen
                Console.WriteLine("1. Skriv i gästboken");
                Console.WriteLine("2. Ta bort inlägg");
                Console.WriteLine("3. Visa inlägg");
                Console.WriteLine("4. Lägg till användare");
                Console.WriteLine("5. Visa användare");
                Console.WriteLine("0. Avsluta");
                Console.Write("Ange ditt val: ");

                var input = Console.ReadLine();
                Console.Clear();  // Rensa konsolen

                switch (input)
                {
                    case "1":
                        AddPost();
                        break;
                    case "2":
                        DeletePost();
                        break;
                    case "3":
                        ListPosts();
                        break;
                    case "4":
                        AddPerson();
                        break;
                    case "5":
                        ListPersons();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ListPersons()
        {
            Console.WriteLine("Visa användare");
            var guestbook = new Guestbook();
            var persons = guestbook.GetPersons();
            foreach (var person in persons)
            {
                Console.WriteLine(person.Name);
            }
            Pause();
        }

        public static void AddPerson()
        {
            Console.WriteLine("Ange användarens namn: ");
            var name = Console.ReadLine();
            if (name == "")
            {
                PrintMessage("Du måste ange ett namn");
            }
            else
            {
                var person = new Person
                {
                    Name = name
                };
                var guestbook = new Guestbook();
                guestbook.AddPerson(person);
            }
        }

        public static Person SelectPerson()
        {
            Console.WriteLine("Vem är du?");
            var guestbook = new Guestbook();
            var persons = guestbook.GetPersons();
            int i = 1;
            foreach (var person in persons)
            {
                Console.WriteLine(i++ + ": " + person.Name);
            }
            var input = Console.ReadLine();
            int selectedUser;
            if(!int.TryParse(input,out selectedUser))
            {
                PrintMessage("Du har inte angivigt en siffra");
            }
            else
            {
                try
                {
                    var person = persons[selectedUser - 1];
                    Console.Clear(); // Rensa konsolen
                    return person;
                }
                catch
                {
                    PrintMessage("Användaren finns inte");
                }
            }
            return null;
        }

        public static void AddPost()
        {
            var person = SelectPerson();
            if (person == null)
            {
                return;
            }
            Console.WriteLine("Skriv ett inlägg");
            var text = Console.ReadLine();
            if (text == "")
            {
                PrintMessage("Du måste skriva ett inlägg");
            }
            else
            {
                var post = new Post
                {
                    Text = text,
                    Owner= person
                };
                Guestbook guestbook = new Guestbook();
                guestbook.AddPost(post);
            }
        }

        public static void ListPosts()
        {
            Console.WriteLine("Gästboksinlägg");
            var guestbook = new Guestbook();
            var posts = guestbook.GetPosts();
            foreach (var post in posts)
            {
                Console.WriteLine($"{post.Owner.Name}: {post.Text}");
            }

            Pause();
        }

        public static void DeletePost()
        {
            Console.WriteLine("Vilket inlägg vill du ta bort?");
            var guestbook = new Guestbook();
            var posts = guestbook.GetPosts();
            int i = 1;
            foreach (var post in posts)
            {
                Console.WriteLine($"{i++}: {post.Owner.Name}, {post.Text}");
            }
            var input = Console.ReadLine();
            int selectedPost;
            if (!int.TryParse(input, out selectedPost))
            {
                PrintMessage("Du har inte angivigt en siffra");
            }
            else
            {
                try
                {
                    var post = posts[selectedPost - 1];
                    guestbook.DeletePost(selectedPost - 1);
                }
                catch
                {
                    PrintMessage("Inlägget finns inte");
                }
            }
        }

        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.Write("Tryck på en valfri tangent för att fortsätta");
            Console.ReadKey();
        }

        public static void Pause()
        {
            Console.Write("Tryck på en valfri tangent för att fortsätta");
            Console.ReadKey();
        }
    }
}




    