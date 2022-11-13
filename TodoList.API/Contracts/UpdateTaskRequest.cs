using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.API.Contracts
{
    public class UpdateTaskRequest
    {
        
        [Required]
        public string Username {get;set;}
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}