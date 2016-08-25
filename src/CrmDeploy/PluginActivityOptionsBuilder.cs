using System.Activities;
using Microsoft.Xrm.Sdk;

namespace CrmDeploy
{
    public class PluginActivityOptionsBuilder
    {
        public PluginActivityOptionsBuilder(PluginAssemblyOptionsBuilder pluginAssemblyOptionsBuilder,
            PluginTypeRegistration pluginTypeRegistration)
        {
            PluginAssemblyOptions = pluginAssemblyOptionsBuilder;
            PluginTypeRegistration = pluginTypeRegistration;
        }

        protected PluginTypeRegistration PluginTypeRegistration { get; set; }

        public PluginAssemblyOptionsBuilder PluginAssemblyOptions { get; set; }

        public IRegistrationDeployer DeployTo(string orgConnectionString)
        {
            return PluginAssemblyOptions.RegistrationOptions.DeployTo(orgConnectionString);
        }

        public PluginTypeOptionsBuilder AndHasPlugin<T>() where T : IPlugin
        {
            return PluginAssemblyOptions.HasPlugin<T>();
        }

        public PluginActivityOptionsBuilder AndHasActivity<T>(string name, string group) where T : CodeActivity
        {
            return PluginAssemblyOptions.HasActivity<T>(name, group);
        }
    }
}