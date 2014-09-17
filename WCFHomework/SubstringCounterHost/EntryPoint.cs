namespace SubstringCounterHost
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using SubstringOccurrence;

    /// <summary>
    /// You have to start the host as administrator from the bin/debug folder.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Did you start me as administrator?");
            Console.WriteLine("Press [Enter] to continue.");
            Console.ReadLine();

            Uri serviceAddress = new Uri("http://localhost:6969/stubstringCounter");
            ServiceHost selfHost = new ServiceHost(typeof(SubstringOccurrenceCount), serviceAddress);
            ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();

            serviceBehavior.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(serviceBehavior);

            using (selfHost)
            {
                selfHost.Open();
                Console.WriteLine("The service is started at endpoint " + serviceAddress);

                Console.WriteLine("Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
