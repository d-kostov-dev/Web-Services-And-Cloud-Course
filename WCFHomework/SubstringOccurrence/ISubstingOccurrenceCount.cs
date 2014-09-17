﻿namespace SubstringOccurrence
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface ISubstingOccurrenceCount
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
        int CountSubstringOccurrence(string mainString, string searchString);
    }
}