using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Validations;

namespace TaskManagement.Models.Tasks
{
    public class FeedBack : Task, IFeedback
    {
        private int rating;
        private Status status;

        public FeedBack(string title, string description, int id, int rating)
            : base(title, description, id)
        {
            this.Rating = rating;
            this.Status = Status.New;
        }

        #region Properties

        public int Rating
        {
            get
            {
                return this.rating;
            }
            private set
            {
                Validator.ValidateIntRange(value, Constants.RATING_MIN_VALUE, Constants.RATING_MAX_VALUE, Constants.RATING_ERROR_MSG);
                this.rating = value;
            }
        }

        public Status Status
        {
            get { return this.status; }
            private set
            {
                this.status = value;
            }
        }

        #endregion Properties

        #region Methods

        public void ChangeStatus(Status status)
        {
            if (this.status == status)
            {
                AddEventLog($"Status of feedback with ID {this.Id} {this.Title} is already at {this.status}.");
            }
            else
            {
                Status prevStatus = this.status;
                this.status = status;
                AddEventLog($"Status of feedback with ID {this.Id} {this.Title} was changed from {prevStatus} to {this.status}.");
            }
        }

        public void ChangeRating(int number)
        {
            if (this.rating == number)
            {
                AddEventLog($"Rating of feedback with ID {this.Id} {this.Title} is already at {this.rating}.");
            }
            else
            {
                int prevRating = this.Rating;
                this.Rating = number;
                AddEventLog($"Rating of feedback with ID {this.Id} {this.Title} was changed from {prevRating} to {this.Rating}.");
            }
        }

        public override string AdditionalInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Constants.FEEDBACK_HEADER);
            sb.AppendLine($"Status: {this.Status}");
            sb.AppendLine($"Rating: {this.Rating}");

            return sb.ToString();
        }

        #endregion Methods
    }
}