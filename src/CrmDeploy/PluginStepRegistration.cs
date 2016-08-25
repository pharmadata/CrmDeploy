using CrmDeploy.Entities;
using CrmDeploy.Enums;
using Microsoft.Xrm.Sdk;

namespace CrmDeploy
{
    public class PluginStepRegistration
    {

        public PluginStepRegistration(PluginTypeRegistration pluginTypeRegistration, string sdkMessageName, string primaryEntityName, string secondaryEntityName = "", string config = "")
        {
            PluginTypeRegistration = pluginTypeRegistration;
            SdkMessageProcessingStep = new SdkMessageProcessingStep();
            SdkMessageName = sdkMessageName;
            PluginTypeRegistration.PluginType.PropertyChanged += PluginType_PropertyChanged;
            SdkMessageProcessingStep.plugintype_sdkmessageprocessingstep = pluginTypeRegistration.PluginType;
            SdkMessageProcessingStep.plugintypeid_sdkmessageprocessingstep = pluginTypeRegistration.PluginType;
            SdkMessageProcessingStep.Name = primaryEntityName == "none"
                ? $"{sdkMessageName}"
                : $"{sdkMessageName} on {primaryEntityName}";
            PrimaryEntityName = primaryEntityName;
            SecondaryEntityName = secondaryEntityName;
            Config = config;
        }

        public PluginStepRegistration(PluginTypeRegistration pluginTypeRegistration, SdkMessageNames sdkMessageName, string primaryEntityName, string secondaryEntityName = "", string config = "")
            : this(pluginTypeRegistration, sdkMessageName.ToString(), primaryEntityName, secondaryEntityName, config)
        {
        }

        void PluginType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var pluginType = PluginTypeRegistration.PluginType;
            // PluginTypeRegistration.PluginType.PluginTypeId
            if (e.PropertyName == "PluginTypeId")
            {
                if (pluginType.PluginTypeId == null)
                {
                    SdkMessageProcessingStep.EventHandler = null;
                }
                else
                {
                    SdkMessageProcessingStep.EventHandler = new EntityReference(pluginType.LogicalName, pluginType.PluginTypeId.Value);
                }
            }
            //throw new System.NotImplementedException();
        }

        public PluginTypeRegistration PluginTypeRegistration { get; set; }

        public SdkMessageProcessingStep SdkMessageProcessingStep { get; set; }

        public string SdkMessageName { get; set; }

        public string PrimaryEntityName { get; set; }

        public string SecondaryEntityName { get; set; }
        public string Config { get; set; }
    }
}