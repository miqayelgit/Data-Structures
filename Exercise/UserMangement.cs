using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    else if (Int32.Parse(answer) == 1)
                    {
                        bool isDataCorrect = false;

                        while (!isDataCorrect)
                        {
                            try
                            {
                                Console.Write("Id: ");
                                int id = Int32.Parse(Console.ReadLine()!);

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
                                int age = Int32.Parse(Console.ReadLine()!);

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
                    else if (Int32.Parse(answer) == 2)
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

                    else if (Int32.Parse(answer) == 3)
                    {
                        Console.Write("Id: ");
                        int id = Int32.Parse(Console.ReadLine()!);

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
            List<User> users = new List<User>();
            Dictionary<User, string> userActions = new Dictionary<User, string>();
            Dictionary<int, string> actions = new Dictionary<int, string>();

            actions.Add(1, "Soft Delete");
            actions.Add(2, "Hard Delete");

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
                    if (answer.Equals("Do"))
                    {

                        DoIt(users, userActions);
                        ShowUsers(users);

                        isOver = true;
                    }

                    else if (Int32.Parse(answer) == 1)
                    {
                        bool isActionPerformed = false;

                        while (!isActionPerformed)
                        {
                            try
                            {
                                Console.Write("Id: ");
                                int id = Int32.Parse(Console.ReadLine()!);

                                foreach (var user in users)
                                {
                                    if (user.Id == id)
                                    {
                                        if(userActions.TryGetValue(user, out var action))
                                        {
                                            Console.WriteLine($"Action already performed: {action}\n");
                                            isActionPerformed = true;
                                            break;
                                        }
                                        userActions.Add(user, "Soft Delete");
                                        isActionPerformed = true;
                                        break;
                                    }
                                }
                            }

                            catch (Exception ex) { Console.WriteLine("Enter correct data!"); }
                        }
                        
                    }
                    else if (Int32.Parse(answer) == 2)
                    {
                        Console.Write("Id: ");
                        int id = Int32.Parse(Console.ReadLine()!);

                        foreach (var user in users)
                        {
                            if(user.Id == id)
                            {
                                if (userActions.TryGetValue(user, out var action))
                                    {
                                        Console.WriteLine($"Action already performed: {action}\n");
                                        break;
                                    }
                                    
                                userActions.Add(user, "Hard Delete");
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); Console.WriteLine(); }
            }
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

        private static void DoIt(List<User> users, Dictionary<User, string> userActions)
        {
            foreach(var item in userActions)
            {
               if(item.Value.Equals("Hard Delete"))
                {
                    users.Remove(item.Key);
                }

               else if(item.Value.Equals("Soft Delete"))
                {
                    item.Key.IsDeleted = true;
                }
            }
        }
    }


}
