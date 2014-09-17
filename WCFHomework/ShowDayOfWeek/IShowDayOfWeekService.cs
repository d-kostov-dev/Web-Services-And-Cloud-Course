namespace ShowDayOfWeek
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IShowDayOfWeekService
    {
        [OperationContract]
        string ShowDayOfWeek(DateTime date);
    }
}
