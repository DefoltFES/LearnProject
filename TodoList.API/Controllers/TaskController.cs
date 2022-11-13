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

    [HttpGet("EnterNickname")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> EnterNickname([FromBody] EnterNicknameRequest request)
    {
        var response = TodoRepository.Enter(request.UserName);
        return Ok(response);
    }

    [HttpGet("GetTasks")]
    [ProducesResponseType(typeof(GetTasksResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        if (TodoRepository.UserName == null)
        {
            return NotFound("Username is empty");
        }

        var userTask = TodoRepository.Get();
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



    [HttpPost("CreateTask")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        if (TodoRepository.UserName == null)
        {
            return NotFound("Username is empty");
        }

        var todoItem = new UserTask(request.Note) { Id = Guid.NewGuid() };
        var todoItemId = TodoRepository.Add(todoItem);
        return Ok(todoItemId);
    }

    [HttpPut("ChangeTextTask")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateTaskRequest request)
    {
        if (TodoRepository.UserName == null)
        {
            return NotFound("Username is empty");
        }

        var status = TodoRepository.Edit(request.Id, request.Text);
        return Ok(status);
    }

    [HttpPut("CloseTask")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Close([FromBody] CloseTaskRequest request)
    {
        if (TodoRepository.UserName == null)
        {
            return NotFound("Username is empty");
        }

        var status = TodoRepository.Edit(request.Id, true);
        return Ok(status);
    }

    [HttpDelete("DeleteTask")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromBody] DeleteTaskRequest request)
    {
        if (TodoRepository.UserName == null)
        {
            return NotFound("Username is empty");
        }

        var status = TodoRepository.Delete(request.Id);
        return Ok(status);
    }

}
