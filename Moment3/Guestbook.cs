using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Moment3
{
    public class Guestbook
    {
        private string usersFile;
        private string postsFile;
        public Guestbook()
        {
            usersFile = "Users.json";
            postsFile = "Posts.json";
        }

        public void AddPost(Post post)
        {
            var posts = GetPosts();
            posts.Add(post);
            try
            {
                var json = JsonSerializer.Serialize(posts);
                using (StreamWriter sw = File.CreateText(postsFile))
                {
                    sw.WriteLine(json);
                }
            }
            catch
            {
                Console.WriteLine("Filen kunde inte skrivas");
            }

        }

        public void DeletePost(int index)
        {
            var posts = GetPosts();
            posts.RemoveAt(index);
            try
            {
                var json = JsonSerializer.Serialize(posts);
                using (StreamWriter sw = File.CreateText(postsFile))
                {
                    sw.WriteLine(json);
                }
            }
            catch
            {
                Console.WriteLine("Filen kunde inte skrivas");
            }

        }

        public void AddPerson(Person person)
        {
            var persons = GetPersons();
            persons.Add(person);
            try
            {

                var json = JsonSerializer.Serialize(persons);
                using (StreamWriter sw = File.CreateText(usersFile)) 
                {
                    sw.WriteLine(json);
                }
            }
            catch
            {
                Console.WriteLine("Filen kunde inte skrivas");
            }
        }

        public List<Person> GetPersons()
        {
            try
            {
                if (File.Exists(usersFile))
                {
                    using (var sr = new StreamReader(usersFile))
                    {
                        var json = sr.ReadToEnd();
                        var persons = JsonSerializer.Deserialize<List<Person>>(json);

                        return persons;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Filen kunde inte läsas");
            }

            return new List<Person>();
        }

        public List<Post> GetPosts()
        {
            try
            {
                if (File.Exists(postsFile))
                {
                    using (var sr = new StreamReader(postsFile))
                    {
                        var json = sr.ReadToEnd();
                        var posts = JsonSerializer.Deserialize<List<Post>>(json);

                        return posts;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Filen kunde inte läsas");
            }

            return new List<Post>();
        }


    }
}
