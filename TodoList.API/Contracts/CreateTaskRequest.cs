using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TodoList.API.Contracts
{
    public class CreateTaskRequest
    {  
        [Required]
        public string Username {get;set;}
        [Required]
        public string Note {get;set;}
    }
}