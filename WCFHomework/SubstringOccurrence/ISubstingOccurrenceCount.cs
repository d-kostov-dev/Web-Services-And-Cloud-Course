namespace SubstringOccurrence
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface ISubstingOccurrenceCount
    {
        [OperationContract]
        [WebInvoke(Method = "GET",             UriTemplate = "substringCounter?mainString={mainString}&searchString={searchString}")]
        int CountSubstringOccurrence(string mainString, string searchString);
    }
}
