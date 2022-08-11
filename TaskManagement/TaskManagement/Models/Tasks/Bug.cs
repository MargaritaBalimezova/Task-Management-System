using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Validations;


namespace TaskManagement.Models.Tasks
{
    public class Bug : Task, IBug
    {
        private Severity severity;
        private PriorityType priority;
        private Status status= Status.Active;
        private IMember assignee = null;
        private IList<string> stepsToReproduce;

        public Bug(string title, string description, int id, PriorityType priority, Severity severity, IList<string> steps)
             : base(title, description, id)
        {
            this.stepsToReproduce = new List<string>(steps);

            this.Priority = priority;
            this.Severity = severity;
        }

        #region Properties

        public IMember Assignee
        {
            get
            {
                return this.assignee;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Assignee");
                this.assignee = value;
            }
        }

        public PriorityType Priority
        {
            get
            {
                return this.priority;
            }
            private set
            {
                this.priority = value;
            }
        }

        public Severity Severity
        {
            get
            {
                return this.severity;
            }
            private set
            {
                this.severity = value;
            }
        }

        public IList<string> StepsToReproduce
        {
            get
            {
                var copy = new List<string>(this.stepsToReproduce);
                return copy;
            }
        }

        public Status Status
        {
            get
            {
                return this.status;
            }
            private set
            {
                this.status = value;
            }
        }

        #endregion Properties

        #region Methods

        public void ChangeStatus(Status newStatus)
        {
            if (newStatus == this.status)
            {
                throw new InvalidOperationException
                     ($"Status of bug with ID {this.Id} {this.Title} is already at {this.status}.");
            }
            else
            {
                Status lastStatus = this.Status;
                this.status = newStatus;
                AddEventLog($"Status of bug with ID {this.Id} {this.Title} was changed from {lastStatus} to {this.status}.");
            }
        }

        public void ChangeSeverity(Severity newSeverity)
        {
            if (newSeverity == this.severity)
            {
                throw new InvalidOperationException
                     ($"Severity of bug with ID {this.Id} {this.Title} is already at {this.severity}!");
            }
            else
            {
                Severity lastSeverity = this.Severity;
                this.severity = newSeverity;
                AddEventLog($"Severity of bug with ID {this.Id} {this.Title} was changed from {lastSeverity} to {this.severity}!");
            }
        }

        public void ChangePriority(PriorityType newPriority)
        {
            if (newPriority == this.priority)
            {
                throw new InvalidOperationException
                     ($"Priority of bug with ID {this.Id} {this.Title} is already at {this.priority}!");
            }
            else
            {
                PriorityType lastPriority = this.priority;
                this.priority = newPriority;
                AddEventLog($"Priority of bug with ID {this.Id} {this.Title} was changed from {lastPriority} to {this.priority}!");
            }
        }

        public void AddAssignee(IMember assignee)
        {
            this.Assignee = assignee;
        }

        public void RemoveAssignee()
        {
            this.assignee = null;
        }

        public override string AdditionalInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (assignee==null)
            {
                sb.AppendLine($"{Constants.SPACES4}Assignee: No assignee");
            }
            else
            {
                sb.AppendLine($"{Constants.SPACES4}Assignee: {this.Assignee.Name}");
            }
            sb.AppendLine($"{Constants.SPACES4}Priority: {this.Priority}");
            sb.AppendLine($"{Constants.SPACES4}Severity: {this.Severity}");           
            sb.AppendLine($"{Constants.SPACES4}Status: {this.Status}");
            sb.AppendLine($"{Constants.SPACES4}Steps to reproduce:");
            sb.AppendLine(PrintSteps());

            return sb.ToString();
        }

        public string PrintSteps()
        {
            int counter = 1;
            StringBuilder sb = new StringBuilder();

            foreach (string item in stepsToReproduce)
            {
                sb.AppendLine($"{Constants.SPACES4} {counter}.{item}");
                counter++;
            }

            return sb.ToString();
        }

        #endregion Methods
    }
}