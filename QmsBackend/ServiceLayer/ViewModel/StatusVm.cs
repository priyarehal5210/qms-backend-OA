using DomainLayer.Models;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModel
{
    public class StatusVm
    {
        public Register register { get; set; }
        public Tasks tasks { get; set; }
    }
}
