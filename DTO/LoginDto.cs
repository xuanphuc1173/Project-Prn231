﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginDTO
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public int Type {  get; set; }
    }
}
