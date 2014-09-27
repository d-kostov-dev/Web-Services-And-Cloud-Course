namespace Application.WebApi.Models
{
    using System;
    using System.Linq.Expressions;

    using Application.Models;
    
    public class ScoreModel
    {
        public static Expression<Func<User, ScoreModel>> ViewModel
        {
            get
            {
                return x => new ScoreModel()
                {
                    UserName = x.UserName,
                    Rank = x.Rank
                };
            }
        }

        public string UserName { get; set; }

        public int Rank { get; set; }
    }
}