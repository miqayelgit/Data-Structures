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
    public class UserMangement
    {
        public static void PerformActions()
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


        public static void ActionsQueue()
        {
            Dictionary<User, string> userActions = [];

            List<User> users = GenerateUsers();

            bool isOver = false;

            while (!isOver)
            {
                PromptUser();

                string answer = Console.ReadLine()!;

                try
                {
                    Console.WriteLine();
                    if (answer.Equals("Execute"))
                    {
                        Execute(users, userActions, userDelegate);
                        ShowUsers(users);

                        isOver = true;
                    }

                    else if (int.Parse(answer) == 1)
                    {
                        PerformAction(users, userActions, "Soft Delete");
                    }
                    else if (int.Parse(answer) == 2)
                    {
                        PerformAction(users, userActions, "Hard Delete");
                    }
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message); 
                    Console.WriteLine(); 
                }
            }
        }

        private static void PerformAction(List<User> users, Dictionary<User, string> userActions, string actionName)
        {
            bool isActionPerformed = false;

            while (!isActionPerformed)
            {
                try
                {
                    Console.Write("Id: ");
                    int id = int.Parse(Console.ReadLine()!);

                    if (TryGetUserWithId(users, id, out var user))
                    {
                        if (userActions.TryGetValue(user, out var action))
                        {
                            Console.WriteLine($"Action already performed: {action}\n");
                            isActionPerformed = true;
                            break;
                        }
                        userActions.Add(user, actionName);
                        isActionPerformed = true;
                        break;

                    }

                    if (!isActionPerformed)
                    {
                        Console.WriteLine($"User with given ID {id} not found");
                        break;
                    }

                }

                catch (Exception)
                {
                    Console.WriteLine("Enter correct data!");
                }
            }
        }

        private static bool TryGetUserWithId(List<User> users, int id, [MaybeNullWhen(false)] out User outUser)
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    outUser = user;
                    return true;
                }
            }
            outUser = null;
            return false;
        }
        private static void Execute(List<User> users, Dictionary<User, string> userActions, UserDelegate udelegate)
        {
            foreach (var item in userActions)
            {
                if (udelegate(item, "Hard Delete"))
                {
                    users.Remove(item.Key);
                }

                else if (udelegate(item, "Soft Delete"))
                {
                    item.Key.IsDeleted = true;
                }
            }
        }

        public static void PromptUser()
        {
            Dictionary<int, string> actions = new Dictionary<int, string>();
            actions.Add(1, "Soft Delete");
            actions.Add(2, "Hard Delete");

            foreach (var action in actions)
            {
                Console.WriteLine(action);
            }

            Console.Write("\nChoose Action: ");
        }

        private static void ShowUsers(List<User> users)
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
        }
        public static List<User> GenerateUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User
            {
                Id = 1,
                Name = "Mikayel",
                LastName = "Mkrtchyan",
                Age = 18,
                Email = "mikayel@gmail.com",
                IsMarried = true

            });
            users.Add(new User
            {
                Id = 2,
                Name = "Armen",
                LastName = "Hovhannisyan",
                Age = 19,
                Email = "armen@gmail.com",
                IsMarried = true

            });

            return users;
        }

        public static bool IsEqual(KeyValuePair<User, string> item, string action)
        {
            return item.Value.Equals(action);
        }

        delegate bool UserDelegate(KeyValuePair<User, string> val, string action);

        private static readonly UserDelegate userDelegate = IsEqual;

    }

}