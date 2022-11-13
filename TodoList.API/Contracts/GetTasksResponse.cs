using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Classes;

namespace TodoList.API.Contracts
{
    public class GetTasksResponse
    {
        public GetTaskResponse[] Tasks {get;set;}
    }
}