﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Entity.DTOs
{
    public class SliderReturnDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
