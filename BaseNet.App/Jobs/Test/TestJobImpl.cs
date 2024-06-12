using Hangfire;
using Hangfire.JobsLogger;
using Hangfire.RecurringJobAdmin;
using Hangfire.Server;

namespace BaseNet.App.Jobs.Test
{
    public class TestJobImpl : TestJob
    {
        public void Init()
        {
            var cron = "5 * * * *";
            var tag = "test";
            RecurringJob.AddOrUpdate(tag, () => Test(null, tag), cron);
        }

        [DisableConcurrentlyJobExecution("test")]
        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60, Order = 10)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void Test(PerformContext? context, string tag)
        {
            try
            {
                Console.WriteLine("TestJobImpl.Test()");
            }
            catch (Exception ex)
            {
                context!.LogInformation($"Erro: {ex.Message}");
            }
        }
    }
}