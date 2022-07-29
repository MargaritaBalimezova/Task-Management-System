using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Tasks
{
    public class Bug : Task, IBug
    {        
        private string assignee;

        private IList<string> stepsToReproduce;

        //TODO
        //Assignee
        //Status

        public Bug(string title, string description, int id, PriorityType priority, Severity severity, string assignee, IList<string> steps)
             : base(title, description, id)
        {
            this.stepsToReproduce = new List<string>();

            this.Priority = priority;
            this.Severity = severity;
            //this.Assignee = assignee;
        
        }

        #region Properties

        public string Assignee
        {
            get
            {
                return this.assignee;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Assignee");
                //TODO
                //Check if assigne exists in team members list and add it or throw an exception.

                this.assignee = value;
            }
        }

        public PriorityType Priority { get; }

        public Severity Severity { get; }

        public IList<string> StepsToReproduce 
        {
            get
            {
                var copy = new List<string>(this.stepsToReproduce);
                return copy;
            }           
        }

        #endregion

        #region Methods

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
