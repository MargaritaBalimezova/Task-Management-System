using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.Commands.Tests.Common
{
    public static class Constants
    {
        public const string MemberName = "DummyMember";
        public const string MemberName2 = "DummyMember2";
        public const string TeamName = "DummyTeamName";
        public const string BoardName = "DummyBoard";
        public const string Comment = "this is dummy comment";
        public const string Title = "TitleDummy";
        public const string Description = "DescriptionDummy";
        public const PriorityType priority = PriorityType.High;
        public const SizeType size = SizeType.Medium;

        public const PriorityType priorityHigh = PriorityType.High;
        public const PriorityType priorityMedium = PriorityType.Medium;
        public const PriorityType priorityLow = PriorityType.Low;

        public const SizeType sizeLarge = SizeType.Large;
        public const SizeType sizeMedium = SizeType.Medium;
        public const SizeType sizeSmall = SizeType.Small;

        public const string Title2 = "TitleDummy2";
    }
}
