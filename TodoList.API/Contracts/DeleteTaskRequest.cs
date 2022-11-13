using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.API.Contracts
{
    public class DeleteTaskRequest
    {
        [Required]
        public string Nickname {get;set;}

        [Required]
        public Guid Id { get; set; }
    }
}