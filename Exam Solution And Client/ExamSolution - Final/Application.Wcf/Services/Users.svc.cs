namespace Application.Wcf.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Application.Data;
    using Application.Data.Interfaces;
    using Application.Models;
    using Application.Wcf.DataModels;

    public class Users : IUsers
    {
        private const int ItemsPerPage = 10;

        private IDataProvider data;

        public Users()
        {
            this.data = new DataProvider(new ApplicationDbContext());
        }

        public IEnumerable<UserSimpleModel> ShowUsers()
        {
            return this.ShowUsersPerPage("0");
        }


        public IEnumerable<UserSimpleModel> ShowUsersPerPage(string pageNum)
        {
            var page = int.Parse(pageNum);

            var users = this.data.Users.All()
                        .OrderBy(x=>x.UserName)
                        .Skip(ItemsPerPage * page)
                        .Take(ItemsPerPage)
                        .Select(UserSimpleModel.ViewModel);

            return users;
        }

        public UserDetailedModel ShowUserDetails(string id)
        {
            //var id = int.Parse(idNum);

            var user = this.data.Users.Find(id);

            var userToReturn = new UserDetailedModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return userToReturn;
        }
    }
}
