namespace BenchmarkFilter.ServiceTools
{
    public static class StaticServiceProvider
    {
        private static IServiceScopeFactory ServiceScopeFactory { get; set; }
        public static void CreateInstance(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

        public static TService GetService<TService>()
        {
            var serviceScope = ServiceScopeFactory.CreateScope();

            return serviceScope.ServiceProvider.GetService<TService>();
        }
    }
}
