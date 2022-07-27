using System;
using System.Collections.Generic;
using System.Linq;

namespace testing
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ITask bug1 = new Bug
            {
                Name = "Bug1",
                Status = Status.Bug.Status.Active,
                Priority = Priority.Important
            };

            ITask bug2 = new Bug
            {
                Name = "Bug2",
                Status = Status.Bug.Status.Fixed,
                Priority = Priority.Important
            };

            ITask story1 = new Story
            {
                Name = "Story1",
                Status = Status.Story.Status.Done,
                Size = Size.Small
            };

            ITask story2 = new Story
            {
                Name = "Story2",
                Status = Status.Story.Status.NotDone,
                Size = Size.Big
            };

            List<ITask> task = new List<ITask>();
            task.Add(bug1);
            task.Add(bug2);
            task.Add(story1);
            task.Add(story2);

            //var filteredList = task.Select(x => x as Bug).Where(x => x.Status == Status.Bug.Status.Fixed);
            var filteredList = task.OfType<Bug>().Where(x => x.Status == Status.Bug.Status.Fixed);

            Console.WriteLine("Bug - status fixed");
            foreach (var item in filteredList)
            {
                Console.WriteLine(item.ToString());
            }

            var filteredList2 = task.OfType<Story>().Where(x => x.Size == Size.Big);

            Console.WriteLine("Story - size big");
            foreach (var item in filteredList2)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    public interface ITask
    {
        public string Name { get; set; }
    }

    public abstract class Task : ITask
    {
        public string Name { get; set; }
    }

    public class Bug : Task
    {
        public Status.Bug.Status Status { get; set; }

        public Priority Priority { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name} Status: {this.Status} Priority: {this.Priority}";
        }
    }

    public class Story : Task
    {
        public Status.Story.Status Status { get; set; }

        public Size Size { get; set; }

        public override string ToString()
        {
            return $"name: {this.Name} status: {this.Status} size: {this.Size}";
        }
    }

    public enum Priority
    {
        Important,
        NotImportant
    }

    public enum Size
    {
        Big,
        Small
    }
}

namespace Status.Bug
{
    public enum Status
    {
        Active,
        Fixed
    }
}

namespace Status.Story
{
    public enum Status
    {
        Done,
        NotDone
    }
}