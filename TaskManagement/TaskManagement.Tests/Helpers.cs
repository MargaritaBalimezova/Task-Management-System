using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Tests
{
    public class Helpers
    {
        public static List<string> GetDummyList(int size)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < size; i++)
            {
                result.Add("str");
            }
            return result;
        }
    }
}