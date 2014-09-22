using Application.Data;
using Application.Data.Interfaces;
using Application.Wcf.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Application.Wcf
{
    public class TestEntitiesService : ITestEntitiesService
    {
        private IDataProvider data;

        public TestEntitiesService()
        {
            this.data = new DataProvider(new ApplicationDbContext());
        }

        public IEnumerable<TestEntityModel> ShowTestEntities()
        {
            var testEntities = this.data.TestEntities.All().Select(TestEntityModel.ViewModel).ToList();

            return testEntities;
        }
    }
}
