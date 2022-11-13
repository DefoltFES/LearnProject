namespace TodoList;
using System;
using System.Text;
using TodoList.Classes;
class Program
{
    public static List<UserTask> logs = new List<UserTask>();

    public static DateTimeOffset now = DateTimeOffset.Now;

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.Unicode;
        TodoRepository.Enter(AuthUser());
        int? inputUser = null;
        do
        {
            inputUser = Menu();
            if (inputUser == 1)
            {
                AddTask();
            }
            if (inputUser == 2)
            {
                ShowTask();
            }
            if (inputUser == 3)
            {
                EditTask();
            }
            if (inputUser == 4)
            {
                DeleteTask();
            }
        } while (inputUser != 5);

    }

    public static string AuthUser()
    {
        Console.WriteLine("Введите имя пользователя");
        var nickname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nickname))
        {
            AuthUser();
        }
        return nickname;
    }


    public static int Menu()
    {
        Console.WriteLine("Что хотите выбрать? \n1.Написать новую задачу\n2.Посмотреть задачи\n3.Редактировать задачу \n4.Удалить задачу\n5.Выйти");
        var input = Console.ReadLine();
        int choice = 0;
        if (!string.IsNullOrWhiteSpace(input))
        {
            choice = Convert.ToInt32(input);
        }
        return choice;
    }

    public static void AddTask()
    {
        Console.WriteLine("Введите текст задачи:");
        var taskText = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(taskText))
        {
            TodoRepository.Add(new UserTask(taskText));
        }
        else
        {
            Console.WriteLine("Вы ввели пустую строку!");
        }
        ShowTask();
    }

    public static void EditTask()
    {
        ShowTask();
        Console.WriteLine("Выберите задачу которую хотите отредактировать?");
        var userInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(userInput) && Int32.TryParse(userInput, out int choice))
        {
            Console.WriteLine("Что хотите отредактировать?  \n1.Редактировать запись\n2.Помнять готовность учебы");
            if (Int32.TryParse(Console.ReadLine(), out int choiceEdit))
            {
                var task = logs[choice - 1];
                if (choiceEdit == 1)
                {
                    Console.WriteLine($"Исходный текст задачи {task.Note}\nВведите текст изменненый");
                    var text = Console.ReadLine();
                    
                    TodoRepository.Edit(task.Id,text);
                }
                if (choiceEdit == 2)
                {
                    Console.WriteLine("Изменить статус задачи:\nНе выполнено - 0\nВыполнено - 1");
                    if (Int32.TryParse(Console.ReadLine(), out int status))
                    {
                        if (status == 0 || status == 1)
                        {
                            TodoRepository.Edit(task.Id,Convert.ToBoolean(status));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не правильный статус!");
                    }
                }

            }
        }
        else
        {
            Console.WriteLine("Вы ввели пустую строку!");
        }
        ShowTask();
    }

    public static void DeleteTask()
    {
        ShowTask();
        Console.WriteLine("Выберите задачу которую хотите удалить?");
        var userInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(userInput) && Int32.TryParse(userInput, out int choice))
        {
            TodoRepository.Delete(logs[choice - 1].Id);
        }
        ShowTask();
    }


    public static void ShowTask()
    {
        logs = new List<UserTask>(TodoRepository.Get());
        if (logs.Count == 0)
        {
            Console.WriteLine("Нет задач");
            return;
        }
        int i = 1;
        foreach (var item in logs)
        {
            var status = item.IsClose == true ? "Выполнена" : "Не выполнена";
            Console.WriteLine($"Номер:{i} Текст:{item.Note} Статус:{status} Дата создания:{item.CreationDate:dd.MM.yyyy}");
            i++;
        }
    }



}
