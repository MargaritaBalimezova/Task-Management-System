using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Tasks
{
    internal class FeedBack : Task, IFeedback
    {
        private const int RATING_MIN_VALUE = 1;
        private const int RATING_MAX_VALUE = 100;
        private static string RATING_ERROR_MSG = $"Rating must be between {RATING_MIN_VALUE} and {RATING_MAX_VALUE} characters long!";
        private const string FEEDBACK_HEADER = "--FEEDBACKS--";

        private int rating;
        private FeedbackStatus status;

        public FeedBack(string title, string description, int rating)
            : base(title, description)
        {
            this.Rating = rating;
            this.Status = FeedbackStatus.New;
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
                Validator.ValidateIntRange(value, RATING_MIN_VALUE, RATING_MAX_VALUE, RATING_ERROR_MSG);
                this.rating = value;
            }
        }

        public FeedbackStatus Status
        {
            get { return this.status; }
            private set
            {
                this.status = value;
            }
        }

        #endregion Properties

        #region Methods

        public void ChangeRating(string number)
        {
            int prevRating = this.Rating;
            this.Rating = int.Parse(number);
            AddEventLog($"Rating with ID {this.Id} {this.Title} was changed from {prevRating} to {this.Rating}");
        }

        public override string AdditionalInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine(FEEDBACK_HEADER);
            sb.AppendLine($"Status: {this.Status}");
            sb.AppendLine($"Rating: {this.Rating}");

            return sb.ToString();
        }

        #endregion Methods
    }
}