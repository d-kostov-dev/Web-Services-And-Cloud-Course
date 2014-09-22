namespace Application.WebApi.Models
{
    using Application.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    /// <summary>
    /// This data model is created for testing purposes only. Ignore it!
    /// </summary>
    public class TestEntityModel
    {
        public static Expression<Func<TestEntity, TestEntityModel>> ViewModel
        {
            get
            {
                return x => new TestEntityModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserName = x.User.UserName != null ? x.User.UserName : null,
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string UserName { get; set; }

    }
}