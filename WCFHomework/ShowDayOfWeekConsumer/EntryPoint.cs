namespace ShowDayOfWeekConsumer
{
    using System;

    using ShowDayOfWeekConsumer.ShowDayOfWeekServiceReference;

    public class EntryPoint
    {
        public static void Main()
        {
            ShowDayOfWeekServiceClient client = new ShowDayOfWeekServiceClient();

            Console.Write("Enter Date (dd.mm.yyy): ");
            var date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(client.ShowDayOfWeek(date));
        }
    }
}
