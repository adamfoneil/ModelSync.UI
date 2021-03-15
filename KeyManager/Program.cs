using System;
using System.Net.Http;

namespace KeyManager
{
    class Program
    {
        private static HttpClient _client = new HttpClient();

        static void Main(string[] args)
        {
            // create: create a new key for an email address (after someone purchases) and send to that person

            // query: find a key belonging to an email address (help someone recover)

            // verify: test validation of a key as a sanity check
        }
    }
}
