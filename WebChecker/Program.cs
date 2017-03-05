using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace WebChecker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                var content = GetContent(new Uri(arguments[0]));
                Console.WriteLine(content);
                if (arguments.HasKey("r"))
                {
                    Console.WriteLine(SearchByRegex(content, arguments["r"]));
                }
                else if (arguments.HasKey("s"))
                {
                    Console.WriteLine(Search(content, arguments["s"]));
                }
                else
                {
                    throw new Exception("Invalid arguments.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}{Environment.NewLine}Usage: WebChecker url [-s=search|-r=regex]");
            }
        }

        private static bool Search(string content, string search) =>
            content.ToUpper().Contains(search.ToUpper());

        private static bool SearchByRegex(string content, string search) =>
            new Regex(search, RegexOptions.IgnoreCase).IsMatch(content);

        private static string GetContent(Uri url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            using (var responce = request.GetResponse())
            {
                using (var stream = responce.GetResponseStream())
                {
                    if (stream == null) return string.Empty;
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

    }
}