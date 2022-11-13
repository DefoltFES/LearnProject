using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoList.Classes
{
    public static class TodoApiRepository
    {
        private static string BasePathDirectory = $"./taskUser/";
        private static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;


        public static bool Add(UserTask task, string nickname)
        {
            var json = JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented = true });
            var filename = $"{task.Id}.json";
            var userPathDirectory = Path.Combine(BasePathDirectory, BaseDirectory, nickname);
            var fullpath = Path.Combine(userPathDirectory, filename);
            if (!Directory.Exists(userPathDirectory))
            {
                Directory.CreateDirectory(userPathDirectory);
            }
            File.WriteAllText(fullpath, json);
            return true;
        }

        public static UserTask[] Get(string nickname)
        {
            var userPathDirectory = Path.Combine(BasePathDirectory, BaseDirectory, nickname);

            if (!Directory.Exists(userPathDirectory))
            {
                Directory.CreateDirectory(userPathDirectory);
            }
            var files = Directory.GetFiles(userPathDirectory);
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

        public static bool Edit(Guid id, string nickname, string note = null)
        {
            var userPathDirectory = Path.Combine(BasePathDirectory, BaseDirectory, nickname);
            var fullpath = $"{userPathDirectory}/{id}.json";

            if (!File.Exists(fullpath))
            {
                return false;
            }

            var item = File.ReadAllText(fullpath);
            var jsonObj = JsonSerializer.Deserialize<UserTask>(item);                    
            if(note!=null){
            jsonObj.Note = note;
            }

            var output = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fullpath, output);
            return true;
        }

   public static bool Edit(Guid id, string nickname, bool close)
        {
            var userPathDirectory = Path.Combine(BasePathDirectory, BaseDirectory, nickname);
            var fullpath = $"{userPathDirectory}/{id}.json";

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



        public static bool Delete(Guid id, string nickname)
        {
            var userPathDirectory = Path.Combine(BasePathDirectory, BaseDirectory, nickname);
            var fullpath = $"{userPathDirectory}/{id}.json";

            if (!File.Exists(fullpath))
            {
                return false;
            }
            File.Delete(fullpath);
            return true;

        }
    }
}