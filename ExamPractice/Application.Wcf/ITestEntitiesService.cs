namespace Application.Wcf
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    using Application.Wcf.DataModels;

    [ServiceContract]
    public interface ITestEntitiesService
    {
        [WebGet(UriTemplate = "api/testEntities")]
        [OperationContract]
        IEnumerable<TestEntityModel> ShowTestEntities();
    }
}
