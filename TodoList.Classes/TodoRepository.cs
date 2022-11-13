using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoList.Classes
{
    public static class TodoRepository
    {
        private static string BasePathDirectory = $"./taskUser/";
        private static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private static string FullDirectory { get; set; }

        public static string UserName { get; set; }


        public static bool Enter(string userName)
        {

            UserName = userName;
            FullDirectory = Path.Combine(BaseDirectory, BasePathDirectory, UserName);

            if (!Directory.Exists(FullDirectory))
            {
                Directory.CreateDirectory(FullDirectory);
            }

            return true;
        }

        public static bool Add(UserTask task)
        {
            var json = JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented = true });
            var filename = $"{task.Id}.json";
            var fullpath = Path.Combine(FullDirectory, filename);
            File.WriteAllText(fullpath, json);
            return true;
        }

        public static UserTask[] Get()
        {
            var files = Directory.GetFiles(FullDirectory);
            var tasks = new List<UserTask>();
            foreach (var item in files)
            {
                var json = File.ReadAllText(item);
                var task = JsonSerializer.Deserialize<UserTask>(json);
                if (task != null)
                {
                    tasks.Add(task);
                }
                else
                {
                    throw new Exception("Task cannot deserialized");
                }
            }
            return tasks.ToArray();
        }

        public static bool Edit(Guid id, string note)
        {
            var fullpath = $"{FullDirectory}/{id}.json";

            if (!File.Exists(fullpath))
            {
                return false;
            }

            var item = File.ReadAllText(fullpath);
            var jsonObj = JsonSerializer.Deserialize<UserTask>(item);
            if (note != null)
            {
                jsonObj.Note = note;
            }

            var output = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullpath, output);
            return true;
        }

        public static bool Edit(Guid id, bool close)
        {
            var fullpath = $"{FullDirectory}/{id}.json";

            if (!File.Exists(fullpath))
            {
                return false;
            }

            var item = File.ReadAllText(fullpath);
            var jsonObj = JsonSerializer.Deserialize<UserTask>(item);

            jsonObj.IsClose = close;


            var output = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullpath, output);
            return true;
        }



        public static bool Delete(Guid id)
        {
            var fullpath = $"{FullDirectory}/{id}.json";

            if (!File.Exists(fullpath))
            {
                return false;
            }

            File.Delete(fullpath);
            return true;

        }
    }
}