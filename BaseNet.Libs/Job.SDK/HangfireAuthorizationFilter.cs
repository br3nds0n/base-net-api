using Hangfire.Dashboard;

namespace BaseNet.Libs.Job.SDK
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}