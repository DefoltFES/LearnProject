using System;
using System.Text.Json;
namespace TodoList.Classes{
   public class TodoRepository
   {

    private const string BasePathDirectory=$"./taskUser/";
    
    private string UserPathDirectory{get;set;}

    public string nameUser {get;set;}
    
    public TodoRepository(string nickname)
    {
        this.nameUser=nickname;
        this.UserPathDirectory=Path.Combine(BasePathDirectory,$"{nickname}/");
    }
        public bool Add(UserTask task){
            var id = Guid.NewGuid();
            task.Id=id;
            var json = JsonSerializer.Serialize(task,new JsonSerializerOptions{WriteIndented=true});
            var filename=$"{id}.json";
            var fullpath= Path.Combine(UserPathDirectory,filename);
            if(!Directory.Exists(UserPathDirectory))
            {
                Directory.CreateDirectory(UserPathDirectory);
            }
            File.WriteAllText(fullpath,json);
            return true;
        }

        public UserTask[] Get()
        {
            if(!Directory.Exists(UserPathDirectory))
            {
                Directory.CreateDirectory(UserPathDirectory);
            }
            var files=Directory.GetFiles(UserPathDirectory);
            var tasks = new List<UserTask>();
            foreach (var item in files)
            {
                var json = File.ReadAllText(item);
                var task = JsonSerializer.Deserialize<UserTask>(json);
                if(task!=null)
                {
                    tasks.Add(task);
                }else
                {
                    throw new Exception("Task cannot deserialized");
                }
            }
            return tasks.ToArray();
        }

        public void Edit(UserTask task){
           var fullpath = $"{UserPathDirectory}/{task.Id}.json";
           var item = File.ReadAllText(fullpath);
           var jsonObj = JsonSerializer.Deserialize<UserTask>(item);
           jsonObj.IsOpen=task.IsOpen;
           jsonObj.Note=task.Note;
           var output = JsonSerializer.Serialize(jsonObj,new JsonSerializerOptions{WriteIndented=true});
           File.WriteAllText(fullpath,output);
        }

        public void Delete(UserTask task){
            var fullpath = $"{UserPathDirectory}/{task.Id}.json";
            File.Delete(fullpath);

        }

   }
}