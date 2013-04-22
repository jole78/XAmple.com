using System;
using System.Threading;

namespace Example.Deploy
{
    public class DeploymentWorkflow
    {
        public static Action OnDeploymentStarting = delegate { };
        public static Action OnDeploymentFinished = delegate { };

        public void Execute()
        {
            OnDeploymentStarting();

            // simulating deployment by pausing for 10 seconds...
            Thread.Sleep(TimeSpan.FromSeconds(10));

            OnDeploymentFinished();
        }
    }
}