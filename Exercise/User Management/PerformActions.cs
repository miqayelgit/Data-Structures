using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Data_Structures.Models;

namespace Data_Structures.Exercise
{
    public class PerformActions
    {
        public static void PerformActionImpl()
        {
            Dictionary<int, string> actions = new Dictionary<int, string>();
            List<User> users = new List<User>();
            HashSet<int> userIds = new HashSet<int>();

            actions.Add(1, "User Registration");
            actions.Add(2, "Show Users List");
            actions.Add(3, "Delete Any User By Id");

            bool isOver = false;

            while (!isOver)
            {
                foreach (var action in actions)
                {
                    Console.WriteLine(action);
                }

                Console.Write("\nChoose Action: ");

                string answer = Console.ReadLine()!;

                try
                {
                    Console.WriteLine();
                    if (answer.Equals("exit"))
                    {
                        isOver = true;
                    }

                    else if (int.Parse(answer) == 1)
                    {
                        bool isDataCorrect = false;

                        while (!isDataCorrect)
                        {
                            try
                            {
                                Console.Write("Id: ");
                                int id = int.Parse(Console.ReadLine()!);

                                if (userIds.Contains(id))
                                {
                                    Console.WriteLine($"Id {id} is already taken. Use another one!\n");
                                    continue;
                                }

                                Console.Write("Name: ");
                                string name = Console.ReadLine()!;

                                Console.Write("Last Name: ");
                                string lastName = Console.ReadLine()!;

                                Console.Write("Age: ");
                                int age = int.Parse(Console.ReadLine()!);

                                Console.Write("Email: ");
                                string email = Console.ReadLine()!;

                                Console.Write("IsMarried: ");
                                bool isMarried = bool.Parse(Console.ReadLine()!);

                                User user = new User()
                                {
                                    Id = id,
                                    Name = name,
                                    LastName = lastName,
                                    Age = age,
                                    Email = email,
                                    IsMarried = isMarried
                                };

                                userIds.Add(id);
                                users.Add(user);
                                Console.WriteLine("\nUser Registered Successfully.\n");
                                isDataCorrect = true;

                            }
                            catch (Exception ex) { Console.WriteLine("Enter correct data!"); }
                        }
                    }
                    else if (int.Parse(answer) == 2)
                    {
                        if (users.Count == 0)
                        {
                            Console.WriteLine("Users List Is Empty");
                        }

                        else
                        {
                            Console.WriteLine("Users List");
                            foreach (var user in users)
                            {
                                Console.WriteLine(user);
                            }
                            Console.WriteLine();
                        }

                        Console.ReadLine();
                    }

                    else if (int.Parse(answer) == 3)
                    {
                        Console.Write("Id: ");
                        int id = int.Parse(Console.ReadLine()!);

                        foreach (var user in users)
                        {
                            if (user.Id == id)
                            {
                                users.Remove(user);
                                userIds.Remove(id);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); Console.WriteLine(); }
            }
        }
    }

}