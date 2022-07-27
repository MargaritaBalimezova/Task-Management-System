using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    internal interface IFeedback
    {
        int Rating { get; }
        FeedbackStatus Status { get; }

        void ChangeRating(string number);
    }
}