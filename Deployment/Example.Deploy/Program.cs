using System;

namespace Example.Deploy
{
    class Program
    {

        static Program()
        {
            ConfigureExecutionContext();
        }

        static void Main(string[] args)
        {
            var workflow = new DeploymentWorkflow();
            workflow.Execute();
        }

        private static void ConfigureExecutionContext()
        {
            try
            {
                var value = Environment.GetEnvironmentVariable("TEAMCITY_DATA_PATH",
                                                                  EnvironmentVariableTarget.Machine);
                if (string.IsNullOrWhiteSpace(value))
                {
                    DeploymentWorkflow.OnDeploymentStarting = delegate
                                                              {
                                                                  Console.WriteLine("deployment in progress...");
                                                              };
                    DeploymentWorkflow.OnDeploymentFinished = delegate
                                                              {
                                                                  Console.WriteLine("deployment finished successfully");
                                                                  Console.Read();
                                                              };
                }
                else
                {
                    DeploymentWorkflow.OnDeploymentStarting = delegate
                    {
                        Console.WriteLine("##teamcity[progressStart 'deployment in progress...']");
                    };
                    DeploymentWorkflow.OnDeploymentFinished = delegate
                    {
                        Console.WriteLine("##teamcity[progressFinish 'deployment in progress...']");
                    };
                }
            }
            catch { }
        }

    }

}
