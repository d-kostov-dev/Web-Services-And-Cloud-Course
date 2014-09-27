namespace Application.Wcf.Services
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    using Application.Models;
    using Application.Wcf.DataModels;
    
    [ServiceContract]
    public interface IUsers
    {
        [WebGet(UriTemplate = "")]
        [OperationContract]
        IEnumerable<UserSimpleModel> ShowUsers();

        [WebGet(UriTemplate = "?page={page}")]
        [OperationContract]
        IEnumerable<UserSimpleModel> ShowUsersPerPage(string page);

        [WebGet(UriTemplate = "/{id}")]
        [OperationContract]
        UserDetailedModel ShowUserDetails(string id);
    }
}
