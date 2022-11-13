using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.API.Contracts
{
    public class GetTaskResponse
    {
        public Guid Id {get;set;}
        public string Note {get;set;}
        public DateTimeOffset CreationDate{get;set;}
        public bool IsOpen{get;set;}
    }
}