

using Data_Structures.List;

CustomList<int> list = new CustomList<int>(6);
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);
list.Add(5);
list.Add(6);
list.Insert(1, 7);

foreach (var num in list)
{
    Console.WriteLine(num);
}
