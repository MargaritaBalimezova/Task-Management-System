using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Validations
{
    public class Constants
    {
        //COMMONS
        public static string CREATED_MSG = "Successfuly created {0} with name {1}!";
        public static string CREATE_TASK_MSG = "Successfuly created {0} with ID: {1}!";
        public static string ARGUMENTS_ERROR_MSG = "Invalid number of arguments. Expected: {0}, Received: {1}";
        public static string STRING_LEN_ERROR_MSG = "{0} must be between {1} and {2} characters!";
        public static string NULL_ERROR_MSG = "{0} cannot be null value!";
        public static string SPACES2 = new string(' ', 2);
        public static string SPACES4 = new string(' ', 4);

        public static string INVALID_ID_MSG = "Invalid first parameter, id of task is expected, received: {0}";
        public static string MEMBER_NOT_FOUND_ERR_MSG = "Member with name {0} was not found in the member list of team {1}";
        public static string PARAMETER_DOESNOT_EXIST_ERR_MSG = "Parameter with name {0} does not exist!";
        //TEAM
        public const string TEAM_HEADER = "--TEAM--";

        public const int TEAM_NAME_MIN_LEN = 5;
        public const int TEAM_NAME_MAX_LEN = 15;

        //BOARD
        public const int BOARD_NAME_MIN_LEN = 5;
        public const int BOARD_NAME_MAX_LEN = 10;

        public const string TASK_ADDED_MSG = "Successfuly added {0} with ID: {1} to board {2}!";
        public const string TASK_REMOVE_MSG = "Successfuly removed {0} with ID: {1} to board{2}!";
        public const string NO_TASKS_TO_SHOW_HEADER = "--NO TASKS ON THIS BOARD--";
        public const string BOARD_HEADER = "--BOARD--";

        //MEMBER
        public const int MEMBER_NAME_MIN_LENGTH = 5;

        public const int MEMBER_NAME_MAX_LENGTH = 15;

        public const string NO_TASK_HEADER = "--NO TASKS--";
        public const string TASK_HEADER = "--TASKS--";
        public const string MEMBER_HEADER = "--MEMBER--";

        //TASK
        public const int TITLE_MIN_LEN = 10;

        public const int TITLE_MAX_LEN = 50;
        public const int DESCRIPTION_MIN_LEN = 10;
        public const int DESCRIPTION_MAX_LEN = 500;

        public const string NO_COMMENT_HEADER = "--NO COMMENTS--";
        public const string COMMENT_HEADER = "--COMMENTS--";

        //FEEDBACK
        public const int RATING_MIN_VALUE = 1;

        public const int RATING_MAX_VALUE = 100;
        public static string RATING_ERROR_MSG = $"Rating must be between {RATING_MIN_VALUE} and {RATING_MAX_VALUE} points!";
        public const string FEEDBACK_HEADER = "--FEEDBACKS--";

        //BUG 
        public const string BUGS_HEADER = "--BUGS--";       
    }
}