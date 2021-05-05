using Microsoft.Extensions.Configuration;
using System.Linq;


namespace CSharp_ExtensionMethod.Stand
{
    public static class ConfigurationExtensions
    {
        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }

        public static IConfigurationBuilder AddStandardProviders(this IConfigurationBuilder configBuilder)
        {
            return configBuilder.AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .AddJsonFile("config/config.json", optional: true)
                                .AddJsonFile("secrets/secrets.json", optional: true);
        }
    }

    public class Target
    {
        private string _id;

        protected string Id
        {
            get { return _id; }
            set { _id = value.Trim(); }
        }

        public string GetId()
        {
            return _id;
        }

        public Target(string id)
        {
            _id = id;
        }

        //public string GetStandardizedId()
        //{
        //    return _id.ToLower();
        //}
    }

    internal class InternalTarget
    {
        protected class ProtectedSubclass
        { }
    }

    public static class TargetExtensions
    {
        
        internal static void Extendinternal(this InternalTarget target)
        { 
            //To do
        }
        /*
        //Protected class cannot access. Only child class can be access. Therefore, this one won't work.
        internal static void ExternalProtected(this InternalTarget.ProtectedSubclass target)
        {
            //To do
        }
        */

        public static string GetStandardizedId(this Target target)
        {
            return target.GetId().ToUpper();
        }
    }
}
