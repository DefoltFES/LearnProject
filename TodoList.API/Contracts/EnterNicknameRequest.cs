using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.API.Contracts
{
    public class EnterNicknameRequest
    {
        [Required]
        public string UserName{get;set;}
    }
}