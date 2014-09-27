namespace Application.Wcf.DataModels
{
    using System;
    using System.Linq.Expressions;

    using Application.Models;
   
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
                    Name = x.Name
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}