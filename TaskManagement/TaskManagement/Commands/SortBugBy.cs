﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Commands
{
    public class SortBugBy : BaseCommand
    {
        public SortBugBy(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 1)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            IEnumerable<IBug> bugs = new List<IBug>();
            switch (CommandParameters[0].ToLower())
            {
                case "title":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Title);
                    break;
                case "priority":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Priority);
                    break;
                case "severity":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Severity);
                    break;                
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
