﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShowDayOfWeekConsumer.ShowDayOfWeekServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ShowDayOfWeekServiceReference.IShowDayOfWeekService")]
    public interface IShowDayOfWeekService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IShowDayOfWeekService/ShowDayOfWeek", ReplyAction="http://tempuri.org/IShowDayOfWeekService/ShowDayOfWeekResponse")]
        string ShowDayOfWeek(System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IShowDayOfWeekService/ShowDayOfWeek", ReplyAction="http://tempuri.org/IShowDayOfWeekService/ShowDayOfWeekResponse")]
        System.Threading.Tasks.Task<string> ShowDayOfWeekAsync(System.DateTime date);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IShowDayOfWeekServiceChannel : ShowDayOfWeekConsumer.ShowDayOfWeekServiceReference.IShowDayOfWeekService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ShowDayOfWeekServiceClient : System.ServiceModel.ClientBase<ShowDayOfWeekConsumer.ShowDayOfWeekServiceReference.IShowDayOfWeekService>, ShowDayOfWeekConsumer.ShowDayOfWeekServiceReference.IShowDayOfWeekService {
        
        public ShowDayOfWeekServiceClient() {
        }
        
        public ShowDayOfWeekServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ShowDayOfWeekServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShowDayOfWeekServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShowDayOfWeekServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ShowDayOfWeek(System.DateTime date) {
            return base.Channel.ShowDayOfWeek(date);
        }
        
        public System.Threading.Tasks.Task<string> ShowDayOfWeekAsync(System.DateTime date) {
            return base.Channel.ShowDayOfWeekAsync(date);
        }
    }
}
