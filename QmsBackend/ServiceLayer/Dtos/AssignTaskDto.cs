using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DomainLayer.Models.AssignTasks;

namespace ServiceLayer.Dtos
{
   public class AssignTaskDto
    {
        public int Id { get; set; }
        public int registerId { get; set; }

        public int tasksId { get; set; }


    }
}
