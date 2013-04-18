using System;

namespace Example.Deploy
{
    class Program
    {
        static ExecutionContext m_ExecutionContext = ExecutionContext.Unknown;

        static Program()
        {
            DetermineExecutionContext();
        }

        private static void DetermineExecutionContext()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_DATA_PATH", EnvironmentVariableTarget.Machine)))
                {
                    m_ExecutionContext = ExecutionContext.Windows;
                }
                else
                {
                    m_ExecutionContext = ExecutionContext.TeamCity;
                }
            }
            catch
            {
                m_ExecutionContext = ExecutionContext.Windows;
            }


        }

        static void Main(string[] args)
        {
            if (m_ExecutionContext == ExecutionContext.TeamCity)
            {
                Console.WriteLine("Executing in TeamCity");
            }
            else
            {
                Console.WriteLine("Executing in a Windows Console");
            }
        }

    }

    internal enum ExecutionContext
    {
        Unknown = 0,
        Windows = 1,
        TeamCity = 2
    }
}
