namespace TodoList.Classes;
public class UserTask
{
    public Guid Id {get;set;}
    public string Note {get;set;}
    public DateTimeOffset CreationDate{get;set;}
    public bool IsClose{get;set;}

    public UserTask(string Note){
        this.Note=Note;
        this.CreationDate=DateTimeOffset.Now;
        this.IsClose=false;
    }

}
