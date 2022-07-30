﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.FeedbackStatus;

namespace TaskManagement.Models.Contracts
{
    internal interface IFeedback
    {
        int Rating { get; }
        Status Status { get; }

        void ChangeRating(int number);
    }
}