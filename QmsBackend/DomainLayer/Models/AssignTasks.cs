using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
   public class AssignTasks:BasicEntity
    {
        public int registerId { get; set; }
        public Register register { get; set; }
        public int tasksId { get; set; }
        public Tasks tasks { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public status Status { get; set; }

        public bool isChecked { get; set; }
        public enum status
        {
          
            notstarted,
            started,
            completed
        }
    }
}
