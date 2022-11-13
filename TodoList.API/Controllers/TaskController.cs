using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TodoList.API.Contracts;
using TodoList.Classes;

namespace TodoList.API.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;

    public TaskController(ILogger<TaskController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetTasksResponse),(int)HttpStatusCode.OK)]
    public async Task<IActionResult>  Get(string nickname)
    {
         var userTask= TodoApiRepository.Get(nickname);
         var response = new GetTasksResponse
        {
            Tasks = userTask.Select(x => new GetTaskResponse
            {
                Id = x.Id,
                Note = x.Note,
                CreationDate = x.CreationDate,
                IsOpen = x.IsClose
            }).ToArray()
        };
        return Ok(response);
    }



    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var todoItem = new UserTask(request.Note){Id = Guid.NewGuid()};
        var todoItemId = TodoApiRepository.Add(todoItem,request.Username);
        return Ok(todoItemId);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateTaskRequest request)
    {
        var status = TodoApiRepository.Edit(request.Id, request.Username,request.Text);
        return Ok(status);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Close([FromBody] CloseTaskRequest request)
    {
        var status = TodoApiRepository.Edit(request.Id,request.Username,true);
        return Ok(status);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromBody] DeleteTaskRequest request)
    {
        var status = TodoApiRepository.Delete(request.Id,request.Nickname);
        return Ok(status);
    }

}
