using System;
using System.Threading;

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
                Console.WriteLine("##teamcity[progressStart 'deployment in progress...']");
            }
            else
            {
                Console.WriteLine("deployment in progress...");
            }
            
            // START deployment
            // simulated by pausing for 10 seconds...
            Thread.Sleep(TimeSpan.FromSeconds(10));

            if (m_ExecutionContext == ExecutionContext.TeamCity)
            {
                Console.WriteLine("##teamcity[progressFinish 'deployment completed successfully']");
            }
            else
            {
                Console.WriteLine("deployment completed successfully");
                Console.Read();
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
