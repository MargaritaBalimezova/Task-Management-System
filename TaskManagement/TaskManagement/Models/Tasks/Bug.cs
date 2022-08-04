using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;

namespace TaskManagement.Models.Tasks
{
    public class Bug : Task, IBug
    {
        private Severity severity;
        private PriorityType priority;
        private Status status;
        private IMember assignee = null;
        private IList<string> stepsToReproduce;               

        public Bug(string title, string description, int id, PriorityType priority, Severity severity, IList<string> steps)
             : base(title, description, id)
        {
            this.stepsToReproduce = new List<string>(steps);

            this.Priority = priority;
            this.Severity = severity;
            this.Status = Status.Active;
        
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

        #endregion

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


        public override string AdditionalInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Priority: {this.Priority}");
            sb.AppendLine($"Severity: {this.Severity}");
            sb.AppendLine($"Assignee: {this.Assignee}");
            sb.AppendLine("Steps to reproduce:");
            sb.AppendLine(PrintSteps());

            return sb.ToString(); 
        }

        public string PrintSteps()
        {
            int counter = 1;
            StringBuilder sb = new StringBuilder();

            foreach (string item in stepsToReproduce)
            {
                sb.AppendLine($"{counter}.{item}") ;
                counter++;
            }

            return sb.ToString();
        }

       
        #endregion
    }
}
