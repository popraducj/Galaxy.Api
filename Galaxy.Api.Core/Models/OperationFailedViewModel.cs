﻿using System.Collections.Generic;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Models
{
    public class OperationFailedViewModel
    {
        public List<ActionError> Errors { get; set; }
    }
}