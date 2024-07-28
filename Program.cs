
namespace TG_Bot_Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel(k => k.ListenAnyIP(80))
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
