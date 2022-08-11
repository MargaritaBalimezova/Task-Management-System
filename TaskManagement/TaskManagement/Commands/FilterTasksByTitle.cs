using System;
using System.Collections.Generic;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class FilterTasksByTitle : BaseCommand
    {
        private const int ExpectedParamsCount = 1; 

        public FilterTasksByTitle(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            var filter = this.CommandParameters[0];

            return this.Repository.FilterTaskBy(filter);
        }
    }
}
