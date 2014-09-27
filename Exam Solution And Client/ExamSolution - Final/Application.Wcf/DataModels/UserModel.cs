namespace Application.Wcf.DataModels
{
    using Application.Models;
    using System;
    using System.Linq.Expressions;

    public class UserSimpleModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public static Expression<Func<User, UserSimpleModel>> ViewModel
        {
            get
            {
                return x => new UserSimpleModel()
                {
                    Id = x.Id.ToString(),
                    UserName = x.UserName
                };
            }
        }
    }

    public class UserDetailedModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}