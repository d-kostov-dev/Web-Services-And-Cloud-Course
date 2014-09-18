namespace MusicCatalog.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Connecting...");
            Console.WriteLine();
            var client = new WebClient();

            var albums = client.DownloadString("http://localhost:25099/api/albums/all");
            Console.WriteLine("Albums:");
            Console.WriteLine(albums);

            Console.WriteLine();
            Console.WriteLine("Adding albums...");
            Console.WriteLine();

            var albumPost = "{'Title': 'Nice Name', 'Year': '2014', 'Producer': 'Pesho'}";
            albumPost += "{'Title': 'Other Name', 'Year': '2013', 'Producer': 'Pesho'}";
            albumPost += "{'Title': 'Gosho ot po4ivka', 'Year': '2008', 'Producer': 'Pesho ot Pernik'}";
            albumPost += "{'Title': 'Zadushaam sa', 'Year': '2014', 'Producer': 'Bat Georgi'}";

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString("http://localhost:25099/api/albums/create", albumPost);

            Console.WriteLine("Albums:");
            albums = client.DownloadString("http://localhost:25099/api/albums/all");
            Console.WriteLine(albums);

        }
    }
}
