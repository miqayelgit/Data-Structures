
using System.Diagnostics.CodeAnalysis;
using Data_Structures.Models;

namespace Data_Structures.Exercise.User_Management
{
    public class ActionsQueue
    {
        public static void ActionsQueueImpl()
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
                        Execute(users, userActions, IsEqual);
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
                            break;
                        }
                        userActions.Add(user, actionName);
                        break;

                    }

                    isActionPerformed = true;

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
        private static void Execute(List<User> users, Dictionary<User, string> userActions, UserDelegate actionEquals)
        {
            foreach (var item in userActions)
            {
                if (actionEquals(item, "Hard Delete"))
                {
                    users.Remove(item.Key);
                }

                else if (actionEquals(item, "Soft Delete"))
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

        delegate bool UserDelegate(KeyValuePair<User, string> item, string action);
    }
}
